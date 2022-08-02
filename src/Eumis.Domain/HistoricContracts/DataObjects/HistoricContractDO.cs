using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractDO
    {
        public HistoricContractDO()
        {
            this.Activities = new List<HistoricContractActivityDO>();
            this.Locations = new List<HistoricContractLocationDO>();
            this.Partners = new List<HistoricContractPartnerDO>();
            this.ProcurementPlans = new List<HistoricContractProcurementPlanDO>();
            this.ContractedAmounts = new List<HistoricContractContractedAmountDO>();
            this.ActuallyPaidAmounts = new List<HistoricContractActuallyPaidAmountDO>();
            this.ReimbursedAmounts = new List<HistoricContractReimbursedAmountDO>();
        }

        public int HistoricContractId { get; set; }

        public string ProcedureCode { get; set; }

        public int ProcedureId { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameEn { get; set; }

        public string CompanyUin { get; set; }

        public UinType? CompanyUinType { get; set; }

        public string CompanyTypeCode { get; set; }

        public int CompanyTypeId { get; set; }

        public int CompanyLegalTypeId { get; set; }

        public string SeatCountryCode { get; set; }

        public string SeatSettlementCode { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public HistoricContractExecutionStatus? ExecutionStatus { get; set; }

        public IList<HistoricContractActivityDO> Activities { get; set; }

        public NutsLevel? NutsLevel { get; set; }

        public IList<HistoricContractLocationDO> Locations { get; set; }

        public IList<HistoricContractPartnerDO> Partners { get; set; }

        public IList<HistoricContractProcurementPlanDO> ProcurementPlans { get; set; }

        public IList<HistoricContractContractedAmountDO> ContractedAmounts { get; set; }

        public IList<HistoricContractActuallyPaidAmountDO> ActuallyPaidAmounts { get; set; }

        public IList<HistoricContractReimbursedAmountDO> ReimbursedAmounts { get; set; }
    }
}
