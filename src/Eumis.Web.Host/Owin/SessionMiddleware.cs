using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Web.Host.Owin
{
    public class SessionMiddleware
    {
        public const string SessionIdOwinEnvKey = "eumis.SessionId";
        public const string CookieName = "sessionCookie";

        private readonly AppFunc next;

        public SessionMiddleware(AppFunc next)
        {
            this.next = next;
        }

        private IEnumerable<Tuple<string, string>> ParseCookie(string cookie)
        {
            var split = cookie.Split(';');
            foreach (var item in split)
            {
                var split2 = item.Split('=');
                if (split2.Length != 2)
                {
                    continue;
                }

                yield return new Tuple<string, string>(split2[0].Trim(), split2[1].Trim());
            }
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var request = new OwinRequest(env);
            var response = new OwinResponse(env);
            string sessionKey = null;

            if (request.Headers.TryGetValue("Cookie", out string[] cookies))
            {
                var cookieValue = cookies
                    .SelectMany(x => this.ParseCookie(x))
                    .FirstOrDefault(x => x.Item1 == CookieName);
                if (cookieValue != null)
                {
                    sessionKey = cookieValue.Item2;
                }
            }

            if (string.IsNullOrEmpty(sessionKey))
            {
                sessionKey = Guid.NewGuid().ToString();

                string sessionCookieValue = string.Format(
                    "{0}={1}",
                    Uri.EscapeDataString(CookieName),
                    Uri.EscapeDataString(sessionKey));

                if (!response.Headers.TryGetValue("Set-Cookie", out string[] setCookieContainer))
                {
                    setCookieContainer = Array.Empty<string>();
                }

                var dest = new string[setCookieContainer.Length + 1];
                Array.Copy(setCookieContainer, dest, setCookieContainer.Length);
                dest[dest.Length - 1] = sessionCookieValue;

                response.Headers.SetValues("Set-Cookie", dest);
            }

            env[SessionIdOwinEnvKey] = sessionKey;

            await this.next(env);
        }
    }
}
