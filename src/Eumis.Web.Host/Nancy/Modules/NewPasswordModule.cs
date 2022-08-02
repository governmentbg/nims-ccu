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
    public class NewPasswordModule : NancyModule
    {
        public NewPasswordModule()
            : base("/newPassword")
        {
            this.Get(string.Empty, this.NewPassword);

            this.Post(string.Empty, this.SetPassword, (ctx) => !string.IsNullOrWhiteSpace(ctx.Request.Query.User) && !string.IsNullOrWhiteSpace(ctx.Request.Query.Code));
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic NewPassword(dynamic parameters)
        {
            this.ViewBag.PasswordSet = this.Request.Query.PasswordSet.HasValue ?
                this.Request.Query.PasswordSet.Value :
                false;

            var model = new NewPasswordModel()
            {
                Username = this.Request.Query.User,
                Code = this.Request.Query.Code,
            };

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["newPassword", model];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic SetPassword(dynamic parameters)
        {
            var data = this.Bind<NewPasswordModel>();

            if (string.IsNullOrEmpty(data.Password1) ||
                string.IsNullOrEmpty(data.Password2))
            {
                return this.RedirectToError(data, NewPasswordError.MissingRequired);
            }

            if (data.Password1 != data.Password2)
            {
                return this.RedirectToError(data, NewPasswordError.PasswordMismatch);
            }

            if (!Regex.IsMatch(data.Password1, ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordRegex")))
            {
                return this.RedirectToError(data, NewPasswordError.PasswordInvalidFormat);
            }

            using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository>>>())
            {
                IUnitOfWork unitOfWork = dependencies.Value.Item1;
                IUsersRepository usersRepository = dependencies.Value.Item2;

                using (var transaction = unitOfWork.BeginTransaction())
                {
                    var user = usersRepository.FindByNewPasswordCode(data.Code);

                    if (user != null)
                    {
                        user.SetNewPassword(data.Password1);

                        unitOfWork.Save();

                        transaction.Commit();
                    }
                }
            }

            return this.Context.GetRedirect(string.Format("newPassword?user={0}&passwordSet=true", data.Username));
        }

        private dynamic RedirectToError(NewPasswordModel model, NewPasswordError error)
        {
            this.ViewBag.Error = error;
            this.ViewBag.PasswordInvalidFormatMessage = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordInvalidFormatMessage");
            this.ViewBag.PasswordInvalidFormatMessageEn = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordInvalidFormatMessageEn");

            return this.View["newPassword", model];
        }
    }
}