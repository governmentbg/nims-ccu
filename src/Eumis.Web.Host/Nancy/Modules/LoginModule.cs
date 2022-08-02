using Autofac.Features.OwnedInstances;
using Eumis.Authentication.AccessContexts;
using Eumis.Common;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Users;
using Eumis.Log.ActionLogger;
using Eumis.Web.Host.Nancy.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Eumis.Web.Host.Nancy.Modules
{
    public class LoginModule : NancyModule
    {
        private static readonly int MaxLoginAttempts = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:MaxLoginAttempts"));

        public LoginModule()
        {
            this.Get("/login", this.GetLogin);
            this.Post("/login", this.PostLoginData);
            this.Get("/logout", this.Logout);
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic GetLogin(dynamic parameters)
        {
            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["login", new LoginModel()];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic PostLoginData(dynamic parameters)
        {
            using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository, IActionLogger>>>())
            {
                IUnitOfWork unitOfWork = dependencies.Value.Item1;
                IUsersRepository usersRepository = dependencies.Value.Item2;
                IActionLogger actionLogger = dependencies.Value.Item3;

                var userData = this.Bind<LoginModel>();

                User user = usersRepository.Find(userData.Username);
                if (user != null && user.IsActive)
                {
                    if (user.VerifyPassword(userData.Password))
                    {
                        if (user.IsDeleted)
                        {
                            this.ViewBag.Error = LoginError.Deleted;
                            return this.View["login", new LoginModel()];
                        }

                        if (user.IsLocked)
                        {
                            this.ViewBag.Error = LoginError.Locked;
                            return this.View["login", new LoginModel()];
                        }

                        if (user.FailedAttempts > 0)
                        {
                            // reset the failed attemps counter on successful login
                            using (var transaction = unitOfWork.BeginTransaction())
                            {
                                user.ZeroFailedAttempts();
                                unitOfWork.Save();
                                transaction.Commit();
                            }
                        }

                        AuthenticationProperties properties = AuthExtensions.CreateUserAuthenticationProperties(user.UserId, user.Username);

                        ClaimsIdentity cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);

                        var authenticationManager = this.Context.GetAuthenticationManager();
                        authenticationManager.SignIn(properties, cookiesIdentity);

                        actionLogger.LogSuccessfulLoginAction(user.Username);

                        return this.Context.GetRedirect("~/");
                    }

                    // increment failed attempts
                    using (var transaction = unitOfWork.BeginTransaction())
                    {
                        user.IncrementFailedAttempts();
                        if (user.FailedAttempts >= MaxLoginAttempts && !user.IsLocked)
                        {
                            user.SetIsLocked(true);
                        }

                        unitOfWork.Save();
                        transaction.Commit();
                    }
                }

                actionLogger.LogUnsuccessfulLoginAction(userData.Username);

                this.ViewBag.Error = LoginError.Unauthorized;
                return this.View["login", new LoginModel()];
            }
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic Logout(dynamic parameters)
        {
            var authenticationManager = this.Context.GetAuthenticationManager();

            authenticationManager.SignOut(AuthenticationTypes.Cookie, AuthenticationTypes.Bearer);

            return this.Context.GetRedirect("~/");
        }
    }
}