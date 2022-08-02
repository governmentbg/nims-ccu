using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractCommunicationVO
    {
        public int ContractCommunicationXmlId { get; set; }

        public Guid XmlGid { get; set; }

        public int ContractId { get; set; }

        public string Subject { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractCommunicationStatus Status { get; set; }

        public string StatusNote { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractCommunicationSource Source { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
