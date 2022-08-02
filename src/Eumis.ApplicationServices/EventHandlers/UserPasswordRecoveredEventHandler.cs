using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class UserPasswordRecoveredEventHandler : EventHandler<UserPasswordRecoveredEvent>
    {
        private IUsersRepository usersRepository;
        private IEmailsRepository emailsRepository;

        public UserPasswordRecoveredEventHandler(IUsersRepository usersRepository, IEmailsRepository emailsRepository)
        {
            this.usersRepository = usersRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(UserPasswordRecoveredEvent e)
        {
            var user = this.usersRepository.Find(e.UserId);

            Email email = new Email(
            user.Email,
            EmailTemplate.SystemRecoverPasswordMessage,
            JObject.FromObject(
                new
                {
                    user = user.Username,
                    code = user.PasswordRecoveryCode,
                    email = user.Email,
                }));

            this.emailsRepository.Add(email);
        }
    }
}
