using MimeKit;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace MailSender
{
    class Program
    {
        private static SmtpSettings _smtpSetting;

        static async Task Main(string[] args)
        {
            _smtpSetting = GetMyClient();

            await SendEmailAsync();
        }

        public static async Task SendEmailAsync()
        {
            Console.WriteLine("Dati adresa destinatarului");
            string email = Console.ReadLine();

            Console.WriteLine("Dati subiect");
            string subject = Console.ReadLine();

            Console.WriteLine("Dati continutul mesajului");
            string body = Console.ReadLine();

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSetting.SenderName, _smtpSetting.SenderEmail));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_smtpSetting.Server, _smtpSetting.Port, true);
                    await client.AuthenticateAsync(_smtpSetting.UserName, _smtpSetting.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    Console.WriteLine($"A fost trimis mesaj de la [{_smtpSetting.SenderEmail}] catre [{email}]");
                }
            }

            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        private static SmtpSettings GetMyClient()
        {
            Console.WriteLine("Configurare SMTP Client");
            var smtpSettings = new SmtpSettings();

            Console.WriteLine("Server:");
            smtpSettings.Server = Console.ReadLine();

            Console.WriteLine("Port:");
            smtpSettings.Port = int.Parse(Console.ReadLine());

            Console.WriteLine("Sender name:");
            smtpSettings.SenderName = Console.ReadLine();

            Console.WriteLine("Sender email:");
            smtpSettings.SenderEmail = Console.ReadLine();

            Console.WriteLine("User name:");
            smtpSettings.UserName = Console.ReadLine();

            Console.WriteLine("Password:");
            smtpSettings.Password = Console.ReadLine();

            return smtpSettings;
        }
    }
}
