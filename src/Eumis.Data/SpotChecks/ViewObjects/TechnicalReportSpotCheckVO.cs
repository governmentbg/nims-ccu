using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class TechnicalReportSpotCheckVO
    {
        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public string Number { get; set; }

        public string CheckSubject { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? XmlGid { get; set; }
    }
}
