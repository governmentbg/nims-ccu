using Eumis.Common.Localization;
using System;

namespace Eumis.Documents.Contracts
{
    public class ContractEvalDocument : ContractDocumentXml
    {
        public string projectName { get; set; }
        public string projectNameAlt { get; set; }
        public string displayProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.projectName, this.projectNameAlt);
            }
        }
        public string projectRegNumber { get; set; }
        public string assessorName { get; set; }

        public string procedureName { get; set; }
        public string procedureNameAlt { get; set; }
        public string displayProcedureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.procedureName, this.procedureNameAlt);
            }
        }
        public string companyName { get; set; }
        public string companyNameAlt { get; set; }
        public string displayCompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.companyName, this.companyNameAlt);
            }
        }
        public string evalTableTypeText { get; set; }

        public string evalTableTypeTextAlt { get; set; }

        public string displayEvalTableTypeText
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.evalTableTypeText, this.evalTableTypeTextAlt);
            }
        }

    }
}