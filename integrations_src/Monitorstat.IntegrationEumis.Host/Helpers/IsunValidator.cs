using Monitorstat.IntegrationEumis.Host.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host
{
    public class IsunValidator : UserNamePasswordValidator
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(Configuration.IsunUser) || string.IsNullOrEmpty(Configuration.IsunPassword))
            {
                Logger.Error("Missing configuration: Username and/or password are not set");
                throw new NullReferenceException();
            }

            if (userName == Configuration.IsunUser && password == Configuration.IsunPassword)
            {
                Logger.Info("User logged in");
                return;
            }

            Logger.Debug("Unknown Username or Password");
            throw new System.IdentityModel.Tokens.SecurityTokenException("Unknown Username or Password");
        }
    }
}
