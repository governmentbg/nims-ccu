using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractVersionSAPDataVO
    {
        public ContractVersionSAPDataVO()
        {
            this.EuAmounts = new List<FinanceSourceAmountsVO>();
            this.ProgrammePriorityBudgets = new List<ProgrammePriorityAmountsVO>();
        }

        public int ContractVersionId { get; set; }

        public int ContractId { get; set; }

        public string ProgrammeCode { get; set; }

        public IEnumerable<string> ProgrammePriorityCodes { get; set; }

        public string ProcedureCode { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string CompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CompanyNameBg, this.CompanyNameEn);
            }
        }

        [JsonIgnore]
        public string CompanyNameBg { get; set; }

        [JsonIgnore]
        public string CompanyNameEn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public decimal? TotalAmountBGN { get; set; }

        public decimal? TotalAmountEUR { get; set; }

        public decimal? BfpTotalAmountBGN { get; set; }

        public decimal? BfpTotalAmountEUR { get; set; }

        public decimal? TotalSelfAmountBGN { get; set; }

        public decimal? TotalSelfAmountEUR { get; set; }

        public decimal? BgAmountBGN { get; set; }

        public decimal? BgAmountEUR { get; set; }

        public IEnumerable<FinanceSourceAmountsVO> EuAmounts { get; set; }

        public IEnumerable<ProgrammePriorityAmountsVO> ProgrammePriorityBudgets { get; set; }
    }
}
