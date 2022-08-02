using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using Eumis.ApplicationServices;
using Eumis.Data;
using Eumis.Domain;
using Eumis.Log.NLog;
using Microsoft.Extensions.CommandLineUtils;

namespace Eumis.Cli
{
    public class MigrateCommand : ICommand
    {
        public string Name { get; } = "migrate";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var migrationArg = app.Argument("migration", "The name of the migration to run");

            app.OnExecute(() =>
            {
                using (var container = this.CreateAutofacContainer())
                using (var lifetimeScope = container.BeginLifetimeScope())
                {
                    var migrations = lifetimeScope.Resolve<IEnumerable<IMigration>>();

                    var migration = migrations.Where(m => m.Name == migrationArg.Value).SingleOrDefault();

                    if (migration == null)
                    {
                        Console.WriteLine("Invalid migration!");

                        app.ShowHelp();

                        Console.WriteLine("Valid migrations:");

                        foreach (var m in migrations)
                        {
                            Console.WriteLine($"  {m.Name}");
                        }

                        return 1;
                    }

                    migration.Migrate();

                    Console.WriteLine("Migration finished successfully.");

                    return 0;
                }
            });
        }

        private IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ApplicationServicesModule());
            builder.Register(c => new NLogLoggerFactory("Eumis.Cli").Create());

            // migrations
            builder.RegisterType<ContractReportMicroType2Rebuild>().As<IMigration>();

            return builder.Build();
        }
    }
}
