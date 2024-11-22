using KT.Application.DTOs;
using MimeKit;
using MailKit.Net.Smtp;

namespace KT.Infrastructure.Data
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        } // todo : option pattern

        public async Task SendOTPAsync(SendMessageDTO sendMessageDTO)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration["SmtpSettings:SenderName"], _configuration["SmtpSettings:SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress("", sendMessageDTO.ToEmail));
            emailMessage.Subject = sendMessageDTO.Subject;
            emailMessage.Body = new TextPart("plain") { Text = sendMessageDTO.Message };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                //await client.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                //await client.SendAsync(emailMessage);
                //await client.DisconnectAsync(true);
            }
        }
    }
}
