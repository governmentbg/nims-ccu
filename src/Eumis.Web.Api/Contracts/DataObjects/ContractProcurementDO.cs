using System;
using Eumis.Domain.Contracts;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractProcurementDO
    {
        public ContractProcurementDO()
        {
        }

        public ContractProcurementDO(string createdByUser)
        {
            this.Source = Domain.Contracts.Source.AdministrativeAuthority;
            this.Status = ContractProcurementStatus.Draft;
            this.CreatedByUser = createdByUser;
            this.CreateDate = DateTime.Now;
        }

        public ContractProcurementDO(ContractProcurementXml procurement, string username)
        {
            this.ContractProcurementId = procurement.ContractProcurementXmlId;
            this.Gid = procurement.Gid;
            this.ContractId = procurement.ContractId;
            this.Source = procurement.Source;
            this.Status = procurement.Status;
            this.CreateDate = procurement.CreateDate;
            this.CreatedByUser = username;
            this.CreateNote = procurement.CreateNote;
            this.Version = procurement.Version;
        }

        public int ContractProcurementId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public Source Source { get; set; }

        public ContractProcurementStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedByUser { get; set; }

        public string CreateNote { get; set; }

        public byte[] Version { get; set; }
    }
}
