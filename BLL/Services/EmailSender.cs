using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace AngularApp1.Server.Services
{
    public class EmailSender : IEmailSender<User>
    {
        private readonly SmtpClient _smtpClient;
        // Rabbitmq or kafka connection?
        public EmailSender(SmtpClient smtpClient)
        {
            this._smtpClient = smtpClient;
        }

        public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            string subject = "Confirm your email";
            string body = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";

            return SendEmailAsync(email, subject, body);
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mail = new MailMessage("PoliceProject@gmail.com", email)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = htmlMessage
                };
                _smtpClient.Send(mail);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            string subject = "Reset your password";
            string body = $"Your password reset code is: {resetCode}";

            return SendEmailAsync(email, subject, body);
        }

        public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            string subject = "Reset your password";
            string body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>.";

            return SendEmailAsync(email, subject, body);
        }
    }
}