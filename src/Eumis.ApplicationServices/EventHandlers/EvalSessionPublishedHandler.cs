using Eumis.Common.Config;
using Eumis.Common.Crypto;
using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class EvalSessionPublishedHandler : EventHandler<EvalSessionPublishedEvent>
    {
        private IProceduresRepository proceduresRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEmailsRepository emailsRepository;

        public EvalSessionPublishedHandler(
            IEvalSessionsRepository evalSessionsRepository,
            IProceduresRepository proceduresRepository,
            IEmailsRepository emailsRepository)
        {
            this.proceduresRepository = proceduresRepository;
            this.emailsRepository = emailsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public override void Handle(EvalSessionPublishedEvent e)
        {
            var procedureData = this.proceduresRepository.GetProcedureBasicData(e.ProcedureId);

            var encryptedEvalSessionResultId = ConfigurationBasedStringEncrypter.Encrypt(e.EvalSessionResultId.ToString());
            var encryptedEvalSessionResultType = ConfigurationBasedStringEncrypter.Encrypt(((int)e.EvalSessionResultType).ToString());
            var encodedEvalSessionResultId = WebUtility.UrlEncode(encryptedEvalSessionResultId);
            var encodedEvalSessionResultType = WebUtility.UrlEncode(encryptedEvalSessionResultType);

            var publicUrl = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PublicUrl");

            string reportUrl = $"{publicUrl}bg/0/0/EvalSessionResult/Show?resultId={encodedEvalSessionResultId}&resultType={encodedEvalSessionResultType}";

            var emails = this.evalSessionsRepository.GetPublishedProjectEmails(e.EvalSessionId);

            string resultType = string.Empty;
            switch (e.EvalSessionResultType)
            {
                case Domain.EvalSessions.EvalSessionResultType.Preliminary:
                    resultType = ApplicationServicesTexts.EvalSessionPublishedHandler_ResultType_Preliminary;
                    break;
                case Domain.EvalSessions.EvalSessionResultType.AdminAdmiss:
                    resultType = ApplicationServicesTexts.EvalSessionPublishedHandler_ResultType_ASD;
                    break;
                case Domain.EvalSessions.EvalSessionResultType.Standing:
                    resultType = ApplicationServicesTexts.EvalSessionPublishedHandler_ResultType_Standing;
                    break;
                default:
                    break;
            }

            foreach (var emailAddress in emails)
            {
                Email email = new Email(
                emailAddress,
                EmailTemplate.EvalSessionPublishedMessage,
                JObject.FromObject(
                    new
                    {
                        procCode = procedureData.Code,
                        procName = procedureData.Name,
                        publicUrl = reportUrl,
                        resultType,
                    }));

                // Mark email as sent, to prevent actual sending to candidates, when eval session publish event is risen
                email.Status = Domain.NonAggregates.EmailStatus.Sent;

                this.emailsRepository.Add(email);
            }
        }
    }
}
