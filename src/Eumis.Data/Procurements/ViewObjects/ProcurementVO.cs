using Eumis.Common.Json;
using Eumis.Domain.Procurements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procurements.ViewObjects
{
    public class ProcurementVO
    {
        public int ProcurementId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcurementStatus Status { get; set; }

        public string Name { get; set; }

        public string ErrandArea { get; set; }

        public string ErrandLegalAct { get; set; }

        public decimal? PrognosysAmount { get; set; }

        public string PPANumber { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? AnnouncedDate { get; set; }
    }
}
