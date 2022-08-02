using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Data.Users.Repositories;
using Eumis.Web.Host.Nancy.Models;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Eumis.Web.Host.Nancy.Modules
{
    public class RecoverPasswordModule : NancyModule
    {
        public RecoverPasswordModule()
            : base("/recoverPassword")
        {
            this.Get(string.Empty, this.RecoverPassword);

            this.Post(string.Empty, this.ChangePassword, (ctx) => !string.IsNullOrWhiteSpace(ctx.Request.Query.User) && !string.IsNullOrWhiteSpace(ctx.Request.Query.Code));
            this.Post(string.Empty, this.StartPasswordRecovery, (ctx) => string.IsNullOrWhiteSpace(ctx.Request.Query.User) || string.IsNullOrWhiteSpace(ctx.Request.Query.Code));
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic RecoverPassword(dynamic parameters)
        {
            this.ViewBag.Sent = this.Request.Query.Sent.HasValue ?
                this.Request.Query.Sent.Value :
                false;
            this.ViewBag.PasswordChanged = this.Request.Query.PasswordChanged.HasValue ?
                this.Request.Query.PasswordChanged.Value :
                false;

            var model = new RecoverPasswordModel()
            {
                Username = this.Request.Query.User,
                Code = this.Request.Query.Code,
            };

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["recoverPassword", model];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic StartPasswordRecovery(dynamic parameters)
        {
            var data = this.Bind<RecoverPasswordModel>();

            if (string.IsNullOrEmpty(data.Username))
            {
                return this.RedirectToError(data, RecoverPasswordError.UsernameRequired);
            }

            using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository>>>())
            {
                IUnitOfWork unitOfWork = dependencies.Value.Item1;
                IUsersRepository usersRepository = dependencies.Value.Item2;

                using (var transaction = unitOfWork.BeginTransaction())
                {
                    var user = usersRepository.Find(data.Username);

                    if (user != null)
                    {
                        user.SetPasswordRecoveryCode();

                        unitOfWork.Save();

                        transaction.Commit();
                    }
                }
            }

            // even if the user does not exist proceed
            // we do not want to give an attacker the posibility to guess usernames
            return this.Context.GetRedirect(string.Format("~/recoverPassword?user={0}&sent=true", data.Username));
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic ChangePassword(dynamic parameters)
        {
            var data = this.Bind<RecoverPasswordModel>();

            if (string.IsNullOrEmpty(data.Password1) ||
                string.IsNullOrEmpty(data.Password2))
            {
                return this.RedirectToError(data, RecoverPasswordError.MissingRequired);
            }

            if (data.Password1 != data.Password2)
            {
                return this.RedirectToError(data, RecoverPasswordError.PasswordMismatch);
            }

            if (!Regex.IsMatch(data.Password1, ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordRegex")))
            {
                return this.RedirectToError(data, RecoverPasswordError.PasswordInvalidFormat);
            }

            using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository>>>())
            {
                IUnitOfWork unitOfWork = dependencies.Value.Item1;
                IUsersRepository usersRepository = dependencies.Value.Item2;

                using (var transaction = unitOfWork.BeginTransaction())
                {
                    var user = usersRepository.FindByPasswordRecoveryCode(data.Code);

                    if (user != null)
                    {
                        user.RecoverPassword(data.Password1);

                        unitOfWork.Save();

                        transaction.Commit();
                    }
                }
            }

            return this.Context.GetRedirect(string.Format("recoverPassword?user={0}&passwordChanged=true", data.Username));
        }

        private dynamic RedirectToError(RecoverPasswordModel model, RecoverPasswordError error)
        {
            this.ViewBag.Error = error;
            this.ViewBag.PasswordInvalidFormatMessage = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordInvalidFormatMessage");
            this.ViewBag.PasswordInvalidFormatMessageEn = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordInvalidFormatMessageEn");

            return this.View["recoverPassword", model];
        }
    }
}