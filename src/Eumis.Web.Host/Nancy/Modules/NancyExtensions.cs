using Autofac;
using Autofac.Integration.Owin;
using Eumis.Authentication.AccessContexts;
using Eumis.Common.Auth;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Extensions;
using Nancy.Owin;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using System;
using System.Threading.Tasks;

namespace Eumis.Web.Host.Nancy.Modules
{
    public static class NancyExtensions
    {
        public static void RequiresOwinAuthentication(this INancyModule module, string authenticationType, string redirect)
        {
            module.AddBeforeHookOrExecute(
                async ctx =>
                {
                    bool authenticated = false;

                    IAuthenticationManager auth = ctx.GetAuthenticationManager();
                    if (auth != null)
                    {
                        AuthenticateResult authRes = await auth.AuthenticateAsync(authenticationType);
                        authenticated = authRes != null && authRes.Identity != null && authRes.Identity.IsAuthenticated;
                    }

                    return authenticated
                        ? (Response)null
                        : new RedirectResponse(redirect, RedirectResponse.RedirectType.SeeOther);
                },
                "Requires Owin " + authenticationType + " authentication");
        }

        public static IAccessContext GetAccessContext(this INancyModule module, string authenticationType)
        {
            var owinContext = new OwinContext(module.Context.GetOwinEnvironment());
            return owinContext.GetAccessContext(authenticationType);
        }

        public static async void AddBeforeHookOrExecute(this INancyModule module, Func<NancyContext, Task<Response>> beforeDelegate, string earlyExitReason = null)
        {
            if (module.RouteExecuting())
            {
                var result = await beforeDelegate.Invoke(module.Context);

                if (result != null)
                {
                    throw new RouteExecutionEarlyExitException(result, earlyExitReason);
                }
            }
            else
            {
                module.Before.AddItemToEndOfPipeline((ctx, ct) => beforeDelegate(ctx));
            }
        }

        public static TService Resolve<TService>(this NancyContext ctx)
        {
            var owinContext = new OwinContext(ctx.GetOwinEnvironment());
            var lifetimeScope = owinContext.GetAutofacLifetimeScope();
            return lifetimeScope.Resolve<TService>();
        }

        public static void WithNoCacheHeaders(this NegotiationContext negotiationContext)
        {
            negotiationContext.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            negotiationContext.Headers.Add("Pragma", "no-cache");
            negotiationContext.Headers.Add("Expires", DateTime.MinValue.ToString("R"));
        }
    }
}