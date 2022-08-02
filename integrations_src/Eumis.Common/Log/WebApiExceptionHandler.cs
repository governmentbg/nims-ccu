using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Eumis.Common.Log
{
    public class WebApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var info = ExceptionDispatchInfo.Capture(context.Exception);
            info.Throw();
        }
    }
}
