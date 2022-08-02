using System;

namespace Eumis.PortalIntegration.Api.Core
{
    public class XmlDO
    {
        public Guid? Gid { get; set; }

        public string Xml { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
