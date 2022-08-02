using System;

namespace Eumis.Documents.Contracts
{
    public class ContractAttachedFilePVO
    {
        public Guid fileKey { get; set; }

        public string fileName { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }
}
