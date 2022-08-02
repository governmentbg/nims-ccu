using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using System;

namespace Eumis.Public.Data.Companies.ViewObjects
{
    public class PartnerProjectsVO : ProjectDetailsVO
    {
        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public string StatusDescription
        {
            get
            {
                return this.ExecutionStatus.HasValue ? this.ExecutionStatus.GetEnumDescription() : string.Empty;
            }
        }

        public int? MonthsDuration
        {
            get
            {
                if (this.StartDate.HasValue && this.CompletionDate.HasValue)
                {
                    return DataUtils.GetMonthsDuration(this.StartDate.Value, this.CompletionDate.Value);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
