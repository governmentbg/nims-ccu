using System;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractProcurementPVO
    {
        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public EnumPVO<Source> Source { get; set; }

        public EnumPVO<ContractProcurementStatus> Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
