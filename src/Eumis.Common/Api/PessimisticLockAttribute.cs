using Autofac;
using Autofac.Integration.Owin;
using Eumis.Common.Db;
using Eumis.Common.Log;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eumis.Common.Api
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PessimisticLockAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var owinContext = actionContext.Request.GetOwinContext();

            var lockName = this.GetLockName(actionContext);
            var truncatedLockName = lockName.Truncate(255);

            if (lockName != truncatedLockName)
            {
                ILogger logger = owinContext.GetAutofacLifetimeScope().Resolve<ILogger>();
                logger.Log(LogLevel.Warn, $"Lockname truncated - {lockName}");
            }

            var lockHandle = await owinContext.GetAutofacLifetimeScope().Resolve<IUnitOfWork>().AcquireLockAsync(truncatedLockName);

            owinContext.Set<IDisposable>("eumis.PessimisticLockHandle", lockHandle);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var owinContext = actionExecutedContext.Request.GetOwinContext();
            var lockHandle = owinContext.Get<IDisposable>("eumis.PessimisticLockHandle");

            lockHandle.Dispose();
        }

        private string GetLockName(HttpActionContext context)
        {
            var controller = context.ControllerContext.ControllerDescriptor.ControllerType.Name;
            var action = context.ActionDescriptor.ActionName;
            var actionParameters = context.ActionArguments
                .Where(x => x.Value != null && this.IsSimple(x.Value.GetType()))
                .Select(x => x.Key + "=" + this.GetValue(x.Value));

            return $"{controller}.{action}({string.Join(";", actionParameters)})";
        }

        private bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple
                return this.IsSimple(type.GetGenericArguments()[0]);
            }
            else if (type.IsArray)
            {
                // array, check if the nested type is simple
                return this.IsSimple(type.GetElementType());
            }

            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(Guid));
        }

        private string GetValue(object val)
        {
            if (val is IEnumerable && !(val is string))
            {
                var paramArray = (val as IEnumerable).Cast<object>().Select(o => o.ToString());
                return string.Join("|", paramArray);
            }

            return val.ToString();
        }
    }
}