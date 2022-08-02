using System.Collections.Generic;

namespace Eumis.PortalIntegration.Api.Core
{
    public class ErrorsDO
    {
        public ErrorsDO()
        {
            this.Errors = new List<string>();
            this.Warnings = new List<string>();
        }

        public ErrorsDO(IList<string> errors)
        {
            this.Errors = errors;
        }

        public ErrorsDO(IList<string> errors, IList<string> warnings)
        {
            this.Errors = errors;
            this.Warnings = warnings;
        }

        public IList<string> Errors { get; set; }

        public IList<string> Warnings { get; set; }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
