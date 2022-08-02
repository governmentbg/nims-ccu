using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Core
{
    public class ErrorsDO
    {
        public ErrorsDO()
        {
            this.Errors = new List<string>();
        }

        public ErrorsDO(IList<string> errors)
        {
            this.Errors = errors;
        }

        public IList<string> Errors { get; set; }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
