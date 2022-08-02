using Eumis.Common.Json;
using Eumis.Domain.Audits;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Audits.ViewObjects
{
    public class TechnicalReportAuditVO
    {
        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public string InstitutionName { get; set; }

        public string TypeDesc { get; set; }

        public string KindDesc { get; set; }

        public DateTime? FinalReportDate { get; set; }

        public string FinalReportNumber { get; set; }

        public Guid? XmlGid { get; set; }
    }
}
