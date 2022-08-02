using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Eumis.IntegrationRegiX.Host.Auth
{
    public static class RegixAutExtensions
    {
        internal const string RegixEmailPropertyAuthPropertiesKey = "regixUserEmail";
        internal const string RegixNamePropertyAuthPropertiesKey = "regixUserName";
        internal const string RegixIdPropertyAuthPropertiesKey = "regixUserId";
        internal const string RegixPositionPropertyAuthPropertiesKey = "regixUserPosition";

        public static IRegixCallContext GetRegixCallContext(this HttpRequestMessage request)
        {
            var authProps = request.GetOwinContext().GetAuthenticationProperties();

            if (authProps != null &&
                authProps.ContainsKey(RegixAutExtensions.RegixEmailPropertyAuthPropertiesKey) &&
                authProps.ContainsKey(RegixAutExtensions.RegixNamePropertyAuthPropertiesKey) &&
                authProps.ContainsKey(RegixAutExtensions.RegixIdPropertyAuthPropertiesKey) &&
                authProps.ContainsKey(RegixAutExtensions.RegixPositionPropertyAuthPropertiesKey))
            {
                return new RegixCallContext(
                    authProps[RegixAutExtensions.RegixEmailPropertyAuthPropertiesKey],
                    authProps[RegixAutExtensions.RegixNamePropertyAuthPropertiesKey],
                    authProps[RegixAutExtensions.RegixIdPropertyAuthPropertiesKey],
                    authProps[RegixAutExtensions.RegixPositionPropertyAuthPropertiesKey]);
            }
            else
            {
                return new RegixCallContext();
            }
        }
    }
}
