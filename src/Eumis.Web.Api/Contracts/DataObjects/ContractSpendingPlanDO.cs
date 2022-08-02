using System;
using Eumis.Domain.Contracts;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractSpendingPlanDO
    {
        public ContractSpendingPlanDO()
        {
        }

        public ContractSpendingPlanDO(string createdByUser)
        {
            this.Source = Domain.Contracts.Source.AdministrativeAuthority;
            this.Status = ContractSpendingPlanStatus.Draft;
            this.CreatedByUser = createdByUser;
            this.CreateDate = DateTime.Now;
        }

        public ContractSpendingPlanDO(ContractSpendingPlanXml spendingPlan, string username)
        {
            this.ContractSpendingPlanId = spendingPlan.ContractSpendingPlanXmlId;
            this.Gid = spendingPlan.Gid;
            this.ContractId = spendingPlan.ContractId;
            this.Source = spendingPlan.Source;
            this.Status = spendingPlan.Status;
            this.CreateDate = spendingPlan.CreateDate;
            this.CreatedByUser = username;
            this.CreateNote = spendingPlan.CreateNote;
            this.Version = spendingPlan.Version;
        }

        public int ContractSpendingPlanId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public Source Source { get; set; }

        public ContractSpendingPlanStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedByUser { get; set; }

        public string CreateNote { get; set; }

        public byte[] Version { get; set; }
    }
}
