using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Meguri.Data;
using Meguri.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Meguri.Authorization;

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
        .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.Configure<IdentityOptions>(options => {
            // パスワード強度
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 12;
            options.Password.RequiredUniqueChars = 8;
            // ロックアウト
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 3;
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

        builder.Services.AddControllersWithViews();

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


        // ミドルウェアを構成して、X-Forwarded-For および X-Forwarded-Proto ヘッダーを転送する。
        app.UseForwardedHeaders(new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

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

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
        app.MapRazorPages();

        app.Run();
    }
}
