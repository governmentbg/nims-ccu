using Newtonsoft.Json.Linq;

namespace Eumis.PortalIntegration.Api.Portal.Emails.DataObjects
{
    public class EmailDO
    {
        public string Recipient { get; set; }

        public string MailTemplateName { get; set; }

        public JObject Context { get; set; }
    }
}
