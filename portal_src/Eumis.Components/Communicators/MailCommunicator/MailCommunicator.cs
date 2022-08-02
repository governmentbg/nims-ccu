using Eumis.Common.Config;
using Eumis.Documents.Contracts;
using System.Configuration;
namespace Eumis.Components.Communicators
{
    public class MailCommunicator : IMailCommunicator
    {
        public void Send(string recipient, string subject, string name, string email, string messageText)
        {
            var mailTemplateName = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:MailTemplateName");

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    recipient = recipient,
                    mailTemplateName = mailTemplateName,
                    context = new 
                    {
                        subject = subject,
                        name = name,
                        email = email,
                        messageText = messageText
                    }
                });

            MailApi.Send(body);
        }
    }
}