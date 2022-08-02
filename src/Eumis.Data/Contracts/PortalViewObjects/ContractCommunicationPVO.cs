using System;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractCommunicationPVO
    {
        public Guid XmlGid { get; set; }

        public EnumPVO<ContractCommunicationStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public EnumPVO<ContractCommunicationSource> Source { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ReadDate { get; set; }

        public int OrderNum { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string Subject { get; set; }
    }
}
