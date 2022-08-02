using System;

namespace Eumis.Data.FlatFinancialCorrections.ViewObjects
{
    public class FlatFinancialCorrectionProgrammeItemVO : FlatFinancialCorrectionItemVO
    {
        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string RegulationNumber { get; set; }

        public DateTime? RegulationDate { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public string Company { get; set; }
    }
}
