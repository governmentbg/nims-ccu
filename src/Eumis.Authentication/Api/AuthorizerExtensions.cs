using System;
using System.Net;
using System.Web.Http;
using Eumis.Authentication.Authorization;

namespace Eumis.Authentication.Api
{
    public static class AuthorizerExtensions
    {
        public static void AssertCanDo(this IAuthorizer authorizer, Enum action)
        {
            if (!authorizer.CanDo(action))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        public static void AssertCanDo(this IAuthorizer authorizer, Enum action, int id)
        {
            if (!authorizer.CanDo(action, id))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        public static void AssertCanDoAny(this IAuthorizer authorizer, params Tuple<Enum, int?>[] authorizations)
        {
            var authorized = false;

            foreach (var authorization in authorizations)
            {
                authorized = authorization.Item2.HasValue ?
                    authorizer.CanDo(authorization.Item1, authorization.Item2.Value) :
                    authorizer.CanDo(authorization.Item1);

                if (authorized)
                {
                    break;
                }
            }

            if (!authorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}
