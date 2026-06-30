using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Meguri.Services;

public class EmailSender : IEmailSender<IdentityUser> {
    private readonly ILogger _logger;

    public EmailSender(
        IOptions<SMTPServerConf> optionsAccessor, ILogger<EmailSender> logger
    ) {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public SMTPServerConf Options { get; }

    // .NET 8 新インターフェースの実装
    public async Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink) {
        await Execute("アカウントの確認", $"以下のリンクをクリックしてアカウントを確定してください。<br><a href='{confirmationLink}'>ココをクリック</a>", email);
    }

    public async Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink) {
        await Execute("パスワードのリセット", $"以下のリンクをクリックしてパスワードをリセットしてください。<br><a href='{resetLink}'>ココをクリック</a>", email);
    }

    public async Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode) {
        await Execute("パスワードリセットコード", $"リセットコードは次の通りです: {resetCode}", email);
    }

    public async Task Execute(string subject, string message, string toEmail) {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(
            Options.Account, Options.Account)
        );
        mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart("html") {
            Text = message
        };
        try {
            using (var client = new SmtpClient()) {
                client.LocalDomain = Options.LocalDomain;
                await client.ConnectAsync(
                    Options.HostName, Options.Port, SecureSocketOptions.Auto
                );
                await client.AuthenticateAsync(
                    Options.Account, Options.Password
                );
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        } catch (Exception ex) {
            _logger.LogInformation(
                "Failure Email to {ToEmail}. {ExMessage}", toEmail, ex.Message
            );
            return;
        }
        _logger.LogInformation(
            "Email to {ToEmail} queued successfully!", toEmail
        );
    }
}