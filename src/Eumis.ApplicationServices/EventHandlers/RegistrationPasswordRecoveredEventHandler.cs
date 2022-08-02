using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class RegistrationPasswordRecoveredEventHandler : EventHandler<RegistrationPasswordRecoveredEvent>
    {
        private IRegistrationsRepository registrationsRepository;
        private IEmailsRepository emailsRepository;

        public RegistrationPasswordRecoveredEventHandler(IRegistrationsRepository registrationsRepository, IEmailsRepository emailsRepository)
        {
            this.registrationsRepository = registrationsRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(RegistrationPasswordRecoveredEvent e)
        {
            var reg = this.registrationsRepository.Find(e.RegistrationId);

            Email email = new Email(
                reg.Email,
                EmailTemplate.RecoverPasswordMessage,
                JObject.FromObject(
                    new
                    {
                        code = reg.PasswordRecoveryCode,
                        registrationEmail = reg.Email,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
