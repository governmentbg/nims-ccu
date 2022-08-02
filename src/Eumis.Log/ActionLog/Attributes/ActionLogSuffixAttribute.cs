using System;
using System.Web.Http.Filters;
using Eumis.Domain.ActionLogs;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Log.ActionLogger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionLogSuffixAttribute : AbstractActionLogAttribute
    {
        public string SuffixAction { get; set; }

        protected override ActionLogGroupDescriptor GetActionLogGroupDescriptor(HttpActionExecutedContext actionExecutedContext)
        {
            if (this.SuffixAction == null)
            {
                throw new Exception("Invalid actionLog \"SuffixAction\" parameter.");
            }

            var prefixActionAttributes =
                actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<ActionLogPrefixAttribute>(true);

            if (prefixActionAttributes.Count == 0)
            {
                throw new Exception("Missing \"ActionLogPrefixAttribute\" attribute.");
            }

            return ActionLogGroupUtils.GetClassDescriptor(prefixActionAttributes[0].PrefixAction, this.SuffixAction);
        }
    }
}
