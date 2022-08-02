using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ContractsReportItem
    {
        public string Programme { get; set; }

        public string Procedure { get; set; }

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public string CompanyKidCode { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyCorrespondenceAddress { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanySizeType { get; set; }

        public int? ProjectDuration { get; set; }

        public string ProjectKidCode { get; set; }

        public DateTime? InitialContractDate { get; set; }

        public DateTime? ActualContractDate { get; set; }

        public DateTime? InitialStartDate { get; set; }

        public DateTime? InitialCompletionDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }

        public DateTime? ContractTerminationDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractExecutionStatus? ContractExecutionStatus { get; set; }

        public ICollection<ContractsReportContractAmountsItem> ContractAmounts { get; set; }

        public decimal? PaidAdvanceEuAmount { get; set; }

        public decimal? PaidAdvanceBgAmount { get; set; }

        public decimal? PaidAdvanceTotalAmount { get; set; }

        public decimal? PaidIntermediateEuAmount { get; set; }

        public decimal? PaidIntermediateBgAmount { get; set; }

        public decimal? PaidIntermediateTotalAmount { get; set; }

        public decimal? PaidFinalEuAmount { get; set; }

        public decimal? PaidFinalBgAmount { get; set; }

        public decimal? PaidFinalTotalAmount { get; set; }

        public decimal? ReimbursedPrincipalEuAmount { get; set; }

        public decimal? ReimbursedPrincipalBgAmount { get; set; }

        public decimal? ReimbursedPrincipalTotalAmount { get; set; }

        public decimal? ReimbursedInterestEuAmount { get; set; }

        public decimal? ReimbursedInterestBgAmount { get; set; }

        public decimal? ReimbursedInterestTotalAmount { get; set; }
    }
}
