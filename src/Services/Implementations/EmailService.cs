using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using sportdesk_backend.Auth;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class EmailService(IOptions<EmailSettings> emailOptions) : IEmailService
{
    private readonly EmailSettings _settings = emailOptions.Value;

    public async Task SendInvitationAsync(string toEmail, string registrationLink)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.From));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "You're invited to SportDesk";
        var builder = new BodyBuilder
        {
            HtmlBody = $"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                  <meta charset="UTF-8" />
                  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                </head>
                <body style="margin:0;padding:0;background-color:#f4f4f7;font-family:Arial,sans-serif;">
                  <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#f4f4f7;padding:40px 0;">
                    <tr>
                      <td align="center">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);">
                          <!-- Header -->
                          <tr>
                            <td style="background-color:#1a1a2e;padding:32px 40px;text-align:center;">
                              <h1 style="margin:0;color:#ffffff;font-size:24px;letter-spacing:1px;">SportDesk</h1>
                            </td>
                          </tr>
                          <!-- Body -->
                          <tr>
                            <td style="padding:40px 40px 24px;">
                              <h2 style="margin:0 0 16px;color:#1a1a2e;font-size:20px;">You're invited!</h2>
                              <p style="margin:0 0 16px;color:#555555;font-size:15px;line-height:1.6;">
                                You've been invited to create your <strong>SportDesk</strong> account and manage your sports club in one place.
                              </p>
                              <p style="margin:0 0 32px;color:#555555;font-size:15px;line-height:1.6;">
                                Click the button below to complete your registration. This link is valid for <strong>24 hours</strong>.
                              </p>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td align="center">
                                    <a href="{registrationLink}"
                                       style="display:inline-block;background-color:#4f46e5;color:#ffffff;text-decoration:none;font-size:15px;font-weight:bold;padding:14px 36px;border-radius:6px;letter-spacing:0.5px;">
                                      Complete Registration
                                    </a>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <!-- Divider -->
                          <tr>
                            <td style="padding:0 40px;">
                              <hr style="border:none;border-top:1px solid #eeeeee;margin:0;" />
                            </td>
                          </tr>
                          <!-- Footer -->
                          <tr>
                            <td style="padding:24px 40px 32px;">
                              <p style="margin:0 0 8px;color:#999999;font-size:13px;line-height:1.5;">
                                If the button doesn't work, copy and paste this link into your browser:
                              </p>
                              <p style="margin:0 0 16px;font-size:12px;">
                                <a href="{registrationLink}" style="color:#4f46e5;word-break:break-all;">{registrationLink}</a>
                              </p>
                              <p style="margin:0;color:#bbbbbb;font-size:12px;">
                                If you didn't request this, you can safely ignore this email.
                              </p>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </body>
                </html>
                """,
            TextBody = $"You've been invited to create your SportDesk account.\n\n" +
                       $"Complete your registration (valid for 24 hours):\n{registrationLink}\n\n" +
                       $"If you didn't request this, you can safely ignore this email."
        };
        message.Body = builder.ToMessageBody();

        await SendAsync(message);
    }

    public async Task SendPasswordResetAsync(string toEmail, string resetLink)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.From));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "Reset your SportDesk password";

        var builder = new BodyBuilder
        {
            HtmlBody = $"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                  <meta charset="UTF-8" />
                  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                </head>
                <body style="margin:0;padding:0;background-color:#f4f4f7;font-family:Arial,sans-serif;">
                  <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#f4f4f7;padding:40px 0;">
                    <tr>
                      <td align="center">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);">
                          <!-- Header -->
                          <tr>
                            <td style="background-color:#1a1a2e;padding:32px 40px;text-align:center;">
                              <h1 style="margin:0;color:#ffffff;font-size:24px;letter-spacing:1px;">SportDesk</h1>
                            </td>
                          </tr>
                          <!-- Body -->
                          <tr>
                            <td style="padding:40px 40px 24px;">
                              <h2 style="margin:0 0 16px;color:#1a1a2e;font-size:20px;">Reset your password</h2>
                              <p style="margin:0 0 16px;color:#555555;font-size:15px;line-height:1.6;">
                                We received a request to reset your <strong>SportDesk</strong> password.
                              </p>
                              <p style="margin:0 0 32px;color:#555555;font-size:15px;line-height:1.6;">
                                Click the button below to choose a new password. This link is valid for <strong>1 hour</strong>.
                              </p>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td align="center">
                                    <a href="{resetLink}"
                                       style="display:inline-block;background-color:#4f46e5;color:#ffffff;text-decoration:none;font-size:15px;font-weight:bold;padding:14px 36px;border-radius:6px;letter-spacing:0.5px;">
                                      Reset Password
                                    </a>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <!-- Divider -->
                          <tr>
                            <td style="padding:0 40px;">
                              <hr style="border:none;border-top:1px solid #eeeeee;margin:0;" />
                            </td>
                          </tr>
                          <!-- Footer -->
                          <tr>
                            <td style="padding:24px 40px 32px;">
                              <p style="margin:0 0 8px;color:#999999;font-size:13px;line-height:1.5;">
                                If the button doesn't work, copy and paste this link into your browser:
                              </p>
                              <p style="margin:0 0 16px;font-size:12px;">
                                <a href="{resetLink}" style="color:#4f46e5;word-break:break-all;">{resetLink}</a>
                              </p>
                              <p style="margin:0;color:#bbbbbb;font-size:12px;">
                                If you didn't request a password reset, you can safely ignore this email.
                              </p>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </body>
                </html>
                """,
            TextBody = $"We received a request to reset your SportDesk password.\n\n" +
                       $"Reset your password (valid for 1 hour):\n{resetLink}\n\n" +
                       $"If you didn't request this, you can safely ignore this email."
        };
        message.Body = builder.ToMessageBody();

        await SendAsync(message);
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
