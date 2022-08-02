using System;
using Eumis.Domain.Contracts;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractVersionDO
    {
        public ContractVersionDO()
        {
        }

        public ContractVersionDO(ContractVersionType type, string createdByUser)
        {
            this.VersionType = type;
            this.Status = ContractVersionStatus.Draft;
            this.CreatedByUser = createdByUser;
            this.CreateDate = DateTime.Now;
        }

        public ContractVersionDO(ContractVersionXml version, string username)
        {
            this.ContractVersionId = version.ContractVersionXmlId;
            this.Gid = version.Gid;
            this.ContractId = version.ContractId;
            this.VersionType = version.VersionType;
            this.VersionNumber = string.Format("{0}.{1}", version.VersionNum, version.VersionSubNum);
            this.RegNumber = version.RegNumber;
            this.ContractDate = version.ContractDate;
            this.Status = version.Status;
            this.CreatedByUser = username;
            this.CreateDate = version.CreateDate;
            this.CreateNote = version.CreateNote;
            this.Version = version.Version;
        }

        public int ContractVersionId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public ContractVersionType VersionType { get; set; }

        public string VersionNumber { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public ContractVersionStatus Status { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateNote { get; set; }

        public byte[] Version { get; set; }
    }
}
