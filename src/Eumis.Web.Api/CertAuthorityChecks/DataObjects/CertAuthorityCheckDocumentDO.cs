using Eumis.Domain.CertAuthorityChecks;
using Eumis.Domain.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.DataObjects
{
    public class CertAuthorityCheckDocumentDO
    {
        public CertAuthorityCheckDocumentDO()
        {
        }

        public CertAuthorityCheckDocumentDO(int certAuthorityCheckId, byte[] version)
        {
            this.CertAuthorityCheckId = certAuthorityCheckId;
            this.Version = version;
        }

        public CertAuthorityCheckDocumentDO(CertAuthorityCheckDocument certAuthorityCheckDocument, byte[] version)
        {
            this.CertAuthorityCheckDocumentId = certAuthorityCheckDocument.CertAuthorityCheckDocumentId;
            this.CertAuthorityCheckId = certAuthorityCheckDocument.CertAuthorityCheckId;
            this.Name = certAuthorityCheckDocument.Name;
            this.Description = certAuthorityCheckDocument.Description;

            if (certAuthorityCheckDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = certAuthorityCheckDocument.File.Key,
                    Name = certAuthorityCheckDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int CertAuthorityCheckDocumentId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
