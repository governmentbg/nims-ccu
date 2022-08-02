using System.Linq;
using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Eumis.Domain.Users.PermissionTables;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class UserActivatedHandler : EventHandler<UserActivatedEvent>
    {
        private IUsersRepository usersRepository;
        private IProgrammesRepository programmesRepository;
        private IEmailsRepository emailsRepository;

        public UserActivatedHandler(IUsersRepository usersRepository, IProgrammesRepository programmesRepository, IEmailsRepository emailsRepository)
        {
            this.usersRepository = usersRepository;
            this.programmesRepository = programmesRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(UserActivatedEvent e)
        {
            var user = this.usersRepository.Find(e.UserId);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();
            var programmeIds = programmes.Keys.ToArray();

            var permissions = new PermissionTable(programmes, user.GetUserPermissions(programmeIds), true, null);

            Email email = new Email(
                user.Email,
                EmailTemplate.UserActivatedMessage,
                JObject.FromObject(
                    new
                    {
                        code = user.NewPasswordCode,
                        email = user.Email,
                        username = user.Username,
                        fullname = user.Fullname,
                        phone = user.Phone,
                        address = user.Address,
                        position = user.Position,
                        permissions = permissions,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
