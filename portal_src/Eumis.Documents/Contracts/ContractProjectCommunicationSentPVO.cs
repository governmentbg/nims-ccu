using Eumis.Common.Localization;
using System;

namespace Eumis.Documents.Contracts
{
    public class ContractProjectCommunicationSentPVO
    {
        public Guid registeredGid { get; set; }

        public string communicationRegNumber { get; set; }

        public string projectRegNumber { get; set; }

        public string procedureCode { get; set; }

        public string projectName { get; set; }

        public string projectNameAlt { get; set; }

        public string displayProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(projectName, projectNameAlt);
            }
        }

        public string procedureName { get; set; }

        public string procedureNameAlt { get; set; }

        public string displayProcedureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(procedureName, procedureNameAlt);
            }
        }

        public ContractEnumNomenclature subject { get; set; }
    }
}
