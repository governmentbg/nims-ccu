using Eumis.Common.Json;
using Eumis.Domain.Procurements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procurements.DataOjects
{
    public class ProcurementDO
    {
        public ProcurementDO()
        {
            this.Status = ProcurementStatus.Draft;
        }

        public ProcurementDO(Procurement procurement)
        {
            this.ProcurementId = procurement.ProcurementId;
            this.Name = procurement.Name;
            this.ShortName = procurement.ShortName;
            this.Status = procurement.Status;

            this.ErrandAreaId = procurement.ErrandAreaId;
            this.ErrandLegalActId = procurement.ErrandLegalActId;
            this.ErrandTypeId = procurement.ErrandTypeId;

            this.PrognosysAmount = procurement.PrognosysAmount;
            this.Description = procurement.Description;
            this.InternetAddress = procurement.InternetAddress;
            this.ExpectedAmount = procurement.ExpectedAmount;
            this.PPANumber = procurement.PPANumber;
            this.PlanDate = procurement.PlanDate;
            this.OffersDeadlineDate = procurement.OffersDeadlineDate;
            this.AnnouncedDate = procurement.AnnouncedDate;

            this.Version = procurement.Version;
        }

        public int? ProcurementId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public ProcurementStatus Status { get; set; }

        public int? ErrandAreaId { get; set; }

        public int? ErrandLegalActId { get; set; }

        public int? ErrandTypeId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PrognosysAmount { get; set; }

        public string Description { get; set; }

        public string InternetAddress { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpectedAmount { get; set; }

        public string PPANumber { get; set; }

        public DateTime? PlanDate { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public byte[] Version { get; set; }
    }
}
