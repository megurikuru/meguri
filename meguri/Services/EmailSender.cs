using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Meguri.Services;

public class EmailSender : IEmailSender {
    private readonly ILogger _logger;

    public EmailSender(
        IOptions<SMTPServerConf> optionsAccessor, ILogger<EmailSender> logger
    ) {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public SMTPServerConf Options { get; }

    public async Task SendEmailAsync(
        string toEmail, string subject, string message
    ) {
        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message, string toEmail) {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(
            Options.Accout, Options.Accout)
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
                    Options.Accout, Options.Password
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
