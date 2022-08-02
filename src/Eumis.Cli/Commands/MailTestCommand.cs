using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace Eumis.Cli
{
    public class MailTestCommand : ICommand
    {
        private const string TestMailSender = "noreply.umis2020@egov.bg";
        private const string TestMailDestination = "eumis-dev@projects.david.bg";

        public string Name { get; } = "mailtest";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var mailServerArg = app.Argument("mailserver", "Mail server address.");

            app.OnExecute(() =>
            {
                string mailServer = mailServerArg.Value;

                if (string.IsNullOrEmpty(mailServer))
                {
                    Console.WriteLine("Missing argument: mailserver!");
                    app.ShowHelp();

                    return 1;
                }

                this.TestMailServer(mailServer, string.Empty, string.Empty, string.Empty);

                Console.WriteLine("Test OK!");

                return 0;
            });
        }

        private void TestMailServer(string server, string username, string password, string domain)
        {
            using (SmtpClient smtpClient = new SmtpClient(server))
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var credentials = new NetworkCredential(username, password);

                    if (!string.IsNullOrEmpty(domain))
                    {
                        credentials.Domain = domain;
                    }

                    smtpClient.Credentials = credentials;
                }

                MailAddress from = new MailAddress(TestMailSender);
                MailAddress to = new MailAddress(TestMailDestination);

                MailMessage mailMessage = new MailMessage(from, to)
                {
                    Subject = "Test mail",
                    Body = "Test mail <b>body</b>",
                    SubjectEncoding = System.Text.Encoding.GetEncoding(1251),
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.None,
                };

                smtpClient.Send(mailMessage);
            }
        }
    }
}
