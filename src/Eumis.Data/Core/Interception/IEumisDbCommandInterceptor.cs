using System.Data.Entity.Infrastructure.Interception;

namespace Eumis.Data.Core.Interception
{
    internal interface IEumisDbCommandInterceptor : IDbCommandInterceptor
    {
        /// <summary>
        /// A number indicating the position in the interceptor execution order.
        /// Lower order executes first.
        /// </summary>
        int Order { get; }
    }
}
