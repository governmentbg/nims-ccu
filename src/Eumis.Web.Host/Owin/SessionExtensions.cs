using System.Net.Http;
using Owin;

namespace Eumis.Web.Host.Owin
{
    public static class SessionExtensions
    {
        public static IAppBuilder UseSession(this IAppBuilder app)
        {
            return app.Use(typeof(SessionMiddleware));
        }

        public static string GetSessionKey(this HttpRequestMessage request)
        {
            return request.GetOwinEnvironment()[SessionMiddleware.SessionIdOwinEnvKey] as string;
        }
    }
}
