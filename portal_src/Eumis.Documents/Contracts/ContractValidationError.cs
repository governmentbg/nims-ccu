using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractValidationError
    {
        public string error { get; set; }

        public string errorAlt { get; set; }

        public string displayError
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.error, this.errorAlt);
            }
        }

        public bool isRequired { get; set; }
    }
}
