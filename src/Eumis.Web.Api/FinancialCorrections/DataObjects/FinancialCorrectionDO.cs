using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using System;

namespace Eumis.Web.Api.FinancialCorrections.DataObjects
{
    public class FinancialCorrectionDO
    {
        public FinancialCorrectionDO()
        {
        }

        public FinancialCorrectionDO(FinancialCorrection financialCorrection, Contract contract)
        {
            this.FinancialCorrectionId = financialCorrection.FinancialCorrectionId;
            this.OrderNum = financialCorrection.OrderNum;
            this.Status = financialCorrection.Status;
            this.ImpositionDate = financialCorrection.ImpositionDate;
            this.ContractId = financialCorrection.ContractId;
            this.ContractContractId = financialCorrection.ContractContractId;
            this.ContractBudgetLevel3AmountId = financialCorrection.ContractBudgetLevel3AmountId;
            this.IsDeleted = financialCorrection.Status == FinancialCorrectionStatus.Removed;
            this.DeleteNote = financialCorrection.DeleteNote;

            this.CreateDate = financialCorrection.CreateDate;
            this.ModifyDate = financialCorrection.ModifyDate;
            this.Version = financialCorrection.Version;

            this.Contract = new ContractDataDO(contract);
        }

        public int? FinancialCorrectionId { get; set; }

        public int? OrderNum { get; set; }

        public FinancialCorrectionStatus? Status { get; set; }

        public DateTime? ImpositionDate { get; set; }

        public int? ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public int? ContractBudgetLevel3AmountId { get; set; }

        public bool IsDeleted { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractDataDO Contract { get; set; }
    }
}
