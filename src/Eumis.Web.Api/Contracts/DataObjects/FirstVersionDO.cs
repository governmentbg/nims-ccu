using System;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class FirstVersionDO
    {
        public FirstVersionDO()
        {
        }

        public FirstVersionDO(ContractVersionXml version)
        {
            this.VersionId = version.ContractVersionXmlId;
            this.Gid = version.Gid;
            this.Status = version.Status;
            this.Version = version.Version;
        }

        public int VersionId { get; set; }

        public Guid Gid { get; set; }

        public ContractVersionStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
