using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Owin;
using Owin;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Common.Owin
{
    public static class AppBuilderExtensions
    {
        public static void DisposeContainerOnShutdown(this IAppBuilder app, IContainer container)
        {
            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");

            if (token != CancellationToken.None)
            {
                token.Register(container.Dispose);
            }
        }

        /// <summary>
        /// Conditionally creates a branch in the request pipeline that is rejoined to the main pipeline.
        /// </summary>
        /// <param name="app">App builder.</param>
        /// <param name="predicate">Invoked with the request environment to determine if the branch should be taken.</param>
        /// <param name="configuration">Configures a branch to take.</param>
        /// <returns>The configured app builder.</returns>
        public static IAppBuilder UseWhen(this IAppBuilder app, Func<IOwinContext, bool> predicate, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // Create and configure the branch builder right away; otherwise,
            // we would end up running our branch after all the components
            // that were subsequently added to the main builder.
            var branchBuilder = app.New();
            configuration(branchBuilder);

            return app.Use(new Func<AppFunc, AppFunc>(main =>
            {
                // This is called only when the main application builder
                // is built, not per request.
                branchBuilder.Run(context => main(context.Environment));
                var branch = (AppFunc)branchBuilder.Build(typeof(AppFunc));

                return context =>
                {
                    if (predicate(new OwinContext(context)))
                    {
                        return branch(context);
                    }
                    else
                    {
                        return main(context);
                    }
                };
            }));
        }
    }
}
