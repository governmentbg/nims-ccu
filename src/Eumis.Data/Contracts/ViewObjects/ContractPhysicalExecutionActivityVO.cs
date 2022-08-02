using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractPhysicalExecutionActivityVO
    {
        public int ContractVersionId { get; set; }

        public int ContractId { get; set; }

        public string ContractRegNum { get; set; }

        public string ActivityName { get; set; }

        public string StatusDesc { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Amount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
