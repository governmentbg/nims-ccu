using Eumis.Common.Log;
using Microsoft.Owin;

namespace Eumis.Log
{
    public interface ILoggerFactory
    {
        ILogger Create();

        ILogger Create(IOwinContext owinContext);
    }
}
