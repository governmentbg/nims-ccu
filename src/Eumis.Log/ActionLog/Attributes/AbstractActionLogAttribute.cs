using Autofac;
using Autofac.Integration.WebApi;
using Eumis.Common.Log;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.Log.ActionLogger.Attributes
{
    public abstract class AbstractActionLogAttribute : ActionFilterAttribute
    {
        public string IdParam { get; set; }

        public string ChildIdParam { get; set; }

        public bool DisablePostData { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var lifetimeScope = dependencyScope.GetRequestLifetimeScope();
            var logger = lifetimeScope.Resolve<ILogger>();
            var actionLogger = lifetimeScope.Resolve<IActionLogger>();

            try
            {
                object postData = !this.DisablePostData ? actionExecutedContext.ActionContext.ActionArguments : null;

                int? aggregateRootId = this.GetParamIdValue(actionExecutedContext.ActionContext.ActionArguments, this.IdParam);
                int? childRootId = this.GetParamIdValue(actionExecutedContext.ActionContext.ActionArguments, this.ChildIdParam);

                var actionLogGroupDescriptor = this.GetActionLogGroupDescriptor(actionExecutedContext);

                actionLogger.LogAction(
                    actionLogGroupDescriptor.ActionLogType,
                    actionLogGroupDescriptor.Key,
                    aggregateRootId,
                    childRootId,
                    postData,
                    null);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message, ex);
            }
            finally
            {
                base.OnActionExecuted(actionExecutedContext);
            }
        }

        protected abstract ActionLogGroupDescriptor GetActionLogGroupDescriptor(HttpActionExecutedContext actionExecutedContext);

        private int? GetParamIdValue(Dictionary<string, object> argumentsDictionary, string idParam)
        {
            int? value = null;

            if (!string.IsNullOrWhiteSpace(idParam))
            {
                string[] keys = idParam.Split(new char[] { '.' });

                object propertyObj = argumentsDictionary[keys[0]];

                if (keys.Length > 1)
                {
                    for (int i = 1; i < keys.Length; i++)
                    {
                        propertyObj = propertyObj.GetType().GetProperty(keys[i]).GetValue(propertyObj);
                    }
                }

                value = (int?)propertyObj;
            }

            return value;
        }
    }
}
