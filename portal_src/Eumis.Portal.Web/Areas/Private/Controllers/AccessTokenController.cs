using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    //[Route("/api/private/")]
    public class AccessTokenController : ApiController
    {
        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        [HttpPost]
        public void Update(ContractActivation activation)
        {
            if (AppContext.Current != null && AppContext.Current.WorkingDocument != null)
            {
                AppContext.Current.WorkingDocument.token = activation.accessToken;
                SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(activation.accessToken), false, false);
            }
            else
            {
                throw new System.Web.HttpException(403, "Forbidden, there is no active session");
            }
        }
    }
}
