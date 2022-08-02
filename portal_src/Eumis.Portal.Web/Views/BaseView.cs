using Eumis.Common.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Views
{
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
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
    }
}