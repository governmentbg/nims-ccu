using Eumis.Common.Json;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureMonitorstatDocumentVO
    {
        public int? ProcedureMonitorstatDocumentId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMonitorstatDocumentStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public MonitorstatYear Year { get; set; }

        public string SurveyName { get; set; }

        public string SurveyCode { get; set; }

        public string ReportName { get; set; }

        public string ReportCode { get; set; }

        public int ReportId { get; set; }
    }
}
