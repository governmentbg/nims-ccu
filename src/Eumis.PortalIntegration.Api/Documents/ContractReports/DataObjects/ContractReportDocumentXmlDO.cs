using System;
using System.Collections.Generic;

namespace Eumis.PortalIntegration.Api.Core
{
    public abstract class ContractReportDocumentXmlDO
    {
        public ContractReportDocumentXmlDO(string xml, byte[] version, IList<string> canEnterErrors)
        {
            this.Xml = xml;
            this.Version = version;
            this.CanEnterErrors = canEnterErrors;
        }

        public Guid? Gid { get; set; }

        public string Xml { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public IList<string> CanEnterErrors { get; set; }
    }
}
