using System;
using System.Web.Http.Filters;
using Eumis.Domain.ActionLogs;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Log.ActionLogger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionLogAttribute : AbstractActionLogAttribute
    {
        public Type Action { get; set; }

        protected override ActionLogGroupDescriptor GetActionLogGroupDescriptor(HttpActionExecutedContext actionExecutedContext)
        {
            return ActionLogGroupUtils.GetClassDescriptor(this.Action);
        }
    }
}
