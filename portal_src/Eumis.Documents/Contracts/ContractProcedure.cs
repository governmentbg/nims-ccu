using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Eumis.Documents.Contracts
{
    public class ContractProcedure
    {
        public string procedureName { get; set; }
        public string procedureCode { get; set; }
        public string procedureNameAlt { get; set; }
        public string displayName { get { return SystemLocalization.GetDisplayName(procedureName, procedureNameAlt); } }
        public string procedureDescription { get; set; }
        public DateTime? endingDate { get; set; }

        public ContractEnumNomenclature applicationFormType { get; set; }

        public ContractEnumNomenclature procedureKind { get; set; }

        public ContractEnumNomenclature allowedRegistrationType { get; set; }

        public ContractEnumNomenclature nutsLevel { get; set; }
        public int? year { get; set; }
        public int? projectDuration { get; set; }
        public bool isActive { get; set; }

        public List<ContractProgramme> programmes { get; set; }

        public List<ContractSpecField> specFields { get; set; }

        public List<ContractApplicationDoc> applicationDocs { get; set; }

        public List<ContractLocation> locations { get; set; }

        public List<ContractProcedureApplicationSection> applicationSections { get; set; }

        public List<ContractDirectionPair> directions { get; set; }

        public List<ContractProcedureDeclaration> declarations { get; set; }
    }
}
