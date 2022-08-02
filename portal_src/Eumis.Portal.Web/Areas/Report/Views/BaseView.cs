using Eumis.Components.Communicators;
using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Report.Views
{
    public abstract class ReportBaseViewPage<TModel> : WebViewPage<TModel>
    {
        private EumisUser _currentUser;
        protected EumisUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    ClaimsIdentity ci = this.User.Identity as ClaimsIdentity;
                    _currentUser = EumisUserManager.LoadUser(ci);
                }
                return _currentUser;
            }
        }

        protected Eumis.Documents.Contracts.ContractBFPContractMetadata ContractMetadata
        {
            get
            {
                return DependencyResolver.Current.GetService<IBFPContractCommunicator>()
                            .GetContractMetadata(CurrentUser.AccessToken,
                                new Guid(this.ViewContext.RouteData.Values["cgid"].ToString()));
            }
        }
    }
}