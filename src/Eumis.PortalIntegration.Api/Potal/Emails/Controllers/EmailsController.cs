using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Emails.Repositories;
using Eumis.Domain.Emails;
using Eumis.PortalIntegration.Api.Portal.Emails.DataObjects;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Emails.Controllers
{
    [RoutePrefix("api/emails")]
    public class EmailsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEmailsRepository emailsRepository;

        public EmailsController(
            IUnitOfWork unitOfWork,
            IEmailsRepository emailsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.emailsRepository = emailsRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Transaction]
        [Route("send")]
        public void SendEmail(EmailDO emailDO)
        {
            Email email = new Email(
                emailDO.Recipient,
                emailDO.MailTemplateName,
                emailDO.Context);

            this.emailsRepository.Add(email);

            this.unitOfWork.Save();
        }
    }
}
