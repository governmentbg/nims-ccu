using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialVO
    {
        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialStatus Status { get; set; }

        public ContractReportFinancialStatus StatusName { get; set; }

        public string StatusNote { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string ContractRegistrationEmail { get; set; }
    }
}
