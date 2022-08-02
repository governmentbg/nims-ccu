using Eumis.Domain.Core;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckDocumentVO
    {
        public int CertAuthorityCheckDocumentId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
