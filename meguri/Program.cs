using Meguri.Areas.Identity;
using Meguri.Authorization;
using Meguri.Data;
using Meguri.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using IdConst = Meguri.Constants.IdConst;

namespace Meguri;

public class Program {
    public static void Main(string[] args) {

        // Apache 搭載の Linux で ASP.NET Core をホストする 
        // https://learn.microsoft.com/ja-jp/aspnet/core/host-and-deploy/linux-apache?view=aspnetcore-8.0

        var builder = WebApplication.CreateBuilder(args);

        // データベース
        var connectionString = builder.Configuration.GetConnectionString(
            "DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."
        );
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString)
        );
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // ID管理
        // ロールサービスを追加する。
        builder.Services.AddDefaultIdentity<IdentityUser>(
            options => options.SignIn.RequireConfirmedAccount = true
        ).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddErrorDescriber<LocalizedIdentityErrorDescriber>(); ;

        builder.Services.Configure<IdentityOptions>(options => {
            // パスワード強度
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = IdConst.MinLength;
            options.Password.RequiredUniqueChars = IdConst.RequiredUniqueChars;
            // ロックアウト
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(IdConst.LockoutMinutes);
            options.Lockout.MaxFailedAccessAttempts = IdConst.MaxFailedAccessAttempts;
            options.Lockout.AllowedForNewUsers = true;
        });

        // メール
        var smtpServerConf = builder.Configuration.GetSection("SMTPServerConf");
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<SMTPServerConf>(smtpServerConf);

        // クッキー
        builder.Services.ConfigureApplicationCookie(o => {
            o.ExpireTimeSpan = TimeSpan.FromDays(3);
            o.SlidingExpiration = true;
        });

        // ローカライズサービスを登録する（リソースファイルの格納ディレクトリを指定）。
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        // ビューの多言語対応、データアノテーション（検証エラー等）での共通リソースの使用を設定する。
        builder.Services.AddControllersWithViews()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
            });

        builder.Services.AddRazorPages();

        // 認証されたユーザーを要求する。
        builder.Services.AddAuthorizationBuilder()
        .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

        // 認可ハンドラー
        builder.Services.AddScoped<
            IAuthorizationHandler, WorkIsOwnerAuthorizationHandler
        >();

        var app = builder.Build();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        //// ミドルウェアを構成して、X-Forwarded-For および X-Forwarded-Proto ヘッダーを転送する。
        //app.UseForwardedHeaders(new ForwardedHeadersOptions {
        //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        //});

        var forwardedHeadersOptions = new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };

        // 以下の2行を追加して、プロキシの制限をクリアします
        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(forwardedHeadersOptions);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseMigrationsEndPoint();
        } else {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (!app.Environment.IsDevelopment()) {
            app.UseHttpsRedirection();
        }

        // サポートする言語を設定する。
        var supportedCultures = new[] { "ja", "en" };               // 日本語、英語
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("ja")                                // デフォルトは日本語
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        // ローカライゼーションを有効化する。
        app.UseRequestLocalization(localizationOptions);

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
        app.MapRazorPages();

        app.Run();
    }
}
