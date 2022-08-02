using System;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckProgrammeItemVO
    {
        public int? CertAuthorityCheckItemId { get; set; }

        public int ItemId { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string RegulationNumber { get; set; }

        public DateTime? RegulationDate { get; set; }

        public string Company { get; set; }
    }
}
