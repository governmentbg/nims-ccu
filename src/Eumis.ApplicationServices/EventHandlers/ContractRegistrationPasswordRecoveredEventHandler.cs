using Eumis.Common.Email;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractRegistrationPasswordRecoveredEventHandler : EventHandler<ContractRegistrationPasswordRecoveredEvent>
    {
        private IContractRegistrationsRepository contractRegistrationsRepository;
        private IEmailsRepository emailsRepository;

        public ContractRegistrationPasswordRecoveredEventHandler(IContractRegistrationsRepository contractRegistrationsRepository, IEmailsRepository emailsRepository)
        {
            this.contractRegistrationsRepository = contractRegistrationsRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(ContractRegistrationPasswordRecoveredEvent e)
        {
            var contractReg = this.contractRegistrationsRepository.Find(e.ContractRegistrationId);

            Email email = new Email(
                contractReg.Email,
                EmailTemplate.ContractRegistrationRecoverPasswordMessage,
                JObject.FromObject(
                    new
                    {
                        code = contractReg.PasswordRecoveryCode,
                        registrationEmail = contractReg.Email,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
