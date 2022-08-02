using System;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractVersionPVO
    {
        public Guid Gid { get; set; }

        public EnumPVO<ContractVersionType> VersionType { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public DateTime? ContractDate { get; set; }

        public string RegNumber { get; set; }

        public EnumPVO<ContractVersionStatus> Status { get; set; }
    }
}
