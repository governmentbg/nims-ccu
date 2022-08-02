using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace Eumis.Cli
{
    public interface ICommand
    {
        string Name { get; }

        void Configure(CommandLineApplication app, CancellationToken stopped);
    }
}
