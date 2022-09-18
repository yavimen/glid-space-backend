using MailKit.Net.Smtp;
using MimeKit;

namespace Main.API.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        public async Task SendEmailWithHtmlContentAsync(Message message)
        {
            var emailMessage = CreateEmailMessageWithHtmlContent(message);

            await SendAsync(emailMessage);
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong with smpt server connection in " +
                        "EmailServicr\\EmailServer\\Send(): " + ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private void Send(MimeMessage emailMessage)
        {
            using(var client = new SmtpClient()) 
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong with smpt server connection in " +
                        "EmailServicr\\EmailServer\\Send(): " + ex.Message);
                }
                finally 
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.Date = DateTime.Now;
            mimeMessage.From.Add(new MailboxAddress("Glid", _emailConfiguration.From));
            mimeMessage.To.AddRange(message.To);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return mimeMessage;
        }

        private MimeMessage CreateEmailMessageWithHtmlContent(Message message)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.Date = DateTime.Now;
            mimeMessage.From.Add(new MailboxAddress("Glid", _emailConfiguration.From));
            mimeMessage.To.AddRange(message.To);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return mimeMessage;
        }
    }
}
