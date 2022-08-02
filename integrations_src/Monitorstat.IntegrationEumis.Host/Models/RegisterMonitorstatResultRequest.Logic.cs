using Monitorstat.IntegrationEumis.Host.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Models
{
    public partial class RegisterMonitorstatResultRequest
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void Validate()
        {
            Action<string> log = (s) =>
            {
                var exception = new NullReferenceException(s + ": cannot be null");
                Logger.Error(exception);
                throw exception;
            };

            if (string.IsNullOrEmpty(this.SubjectIdentifier))
            {
                log(nameof(this.SubjectIdentifier));
            }

            if (this.File == null)
            {
                log(nameof(this.File));
            }

            if (string.IsNullOrEmpty(this.ProcedureIdentifier))
            {
                log(nameof(this.ProcedureIdentifier));
            }

            if (this.File.Content.Length == 0)
            {
                log(nameof(this.File));
            }

            if (string.IsNullOrEmpty(this.File.Name))
            {
                this.File.Name = Configuration.DefaultFilename;
            }
        }
    }
}
