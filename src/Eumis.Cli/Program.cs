using System;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace Eumis.Cli
{
    public class Program
    {
        public static int Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            var app = new CommandLineApplication(throwOnUnexpectedArg: false);

            Action<ICommand> addCommand = (cmd) => app.Command(cmd.Name, (subApp) => cmd.Configure(subApp, cts.Token));

            addCommand(new SignCommand());
            addCommand(new VerifyCommand());
            addCommand(new MigrateCommand());
            addCommand(new MailTestCommand());
            addCommand(new ChangeLogCommand());
            addCommand(new ReportCommand());

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });

            return app.Execute(args);
        }
    }
}
