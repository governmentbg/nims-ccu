using System;
using Eumis.Domain.Contracts;
using Eumis.PortalIntegration.Api.Core;

namespace Eumis.PortalIntegration.Api.Portal.ContractCommunications.DataObjects
{
    public class ContractCommunicationDO : XmlDO
    {
        public ContractCommunicationDO()
        {
        }

        public ContractCommunicationDO(ContractCommunicationXml communication)
        {
            this.Gid = communication.Gid;
            this.CreateDate = communication.CreateDate;
            this.Status = new EnumDO<ContractCommunicationStatus>()
            {
                Description = communication.Status,
                Value = communication.Status,
            };
            this.StatusNote = communication.StatusNote;

            var source = communication.DisplaySource;
            this.Source = new EnumDO<ContractCommunicationSource>()
            {
                Description = source,
                Value = source,
            };
            this.RegNumber = communication.RegNumber;
            this.ReadDate = communication.ReadDate;
            this.SendDate = communication.SendDate;
            this.Subject = communication.Subject;
            this.Xml = communication.Xml;
            this.ModifyDate = communication.ModifyDate;
            this.Version = communication.Version;
        }

        public EnumDO<ContractCommunicationStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public EnumDO<ContractCommunicationSource> Source { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Subject { get; set; }
    }
}
