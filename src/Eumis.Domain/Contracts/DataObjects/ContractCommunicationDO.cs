using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractCommunicationDO
    {
        public ContractCommunicationDO()
        {
        }

        public ContractCommunicationDO(ContractCommunicationXml communication)
        {
            this.ContractCommunicationXmlId = communication.ContractCommunicationXmlId;
            this.XmlGid = communication.Gid;
            this.ContractId = communication.ContractId;
            this.Status = communication.Status;
            this.StatusNote = communication.StatusNote;
            this.Subject = communication.Subject;
            this.Source = communication.DisplaySource;
            this.RegNumber = communication.RegNumber;
            this.ReadDate = communication.ReadDate;
            this.SendDate = communication.SendDate;
            this.Version = communication.Version;
        }

        public int ContractCommunicationXmlId { get; set; }

        public Guid XmlGid { get; set; }

        public int ContractId { get; set; }

        public ContractCommunicationStatus Status { get; set; }

        public string StatusNote { get; set; }

        public string Subject { get; set; }

        public ContractCommunicationSource Source { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? SendDate { get; set; }

        public byte[] Version { get; set; }
    }
}
