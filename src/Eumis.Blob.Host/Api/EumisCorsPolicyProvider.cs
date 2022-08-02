using Eumis.Common.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Eumis.Blob.Host.Api
{
    public class EumisCorsPolicyProvider : ICorsPolicyProvider
    {
        private static IList<string> corsAllowedOrigins =
            ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:ClientDomains")
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

        public EumisCorsPolicyProvider()
        {
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
        {
            var policy = new CorsPolicy();
            policy.AllowAnyHeader = true;
            policy.AllowAnyMethod = true;

            if (corsAllowedOrigins.Contains("*"))
            {
                policy.AllowAnyOrigin = true;
            }
            else
            {
                policy.AllowAnyOrigin = false;
                foreach (var key in corsAllowedOrigins)
                {
                    policy.Origins.Add(key);
                }
            }

            return Task.FromResult(policy);
        }
    }
}