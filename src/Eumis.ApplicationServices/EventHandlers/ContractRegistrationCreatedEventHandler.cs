using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractRegistrationCreatedEventHandler : EventHandler<ContractRegistrationCreatedEvent>
    {
        private IEmailsRepository emailsRepository;

        public ContractRegistrationCreatedEventHandler(IEmailsRepository emailsRepository)
        {
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(ContractRegistrationCreatedEvent e)
        {
            Email email = new Email(
                e.Email,
                EmailTemplate.ContractRegistrationActivationMessage,
                JObject.FromObject(
                    new
                    {
                        activationCode = e.ActivationCode,
                        registrationEmail = e.Email,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
