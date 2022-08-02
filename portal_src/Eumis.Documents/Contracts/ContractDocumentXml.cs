using System;
using System.Collections.Generic;
namespace Eumis.Documents.Contracts
{
    public class ContractDocumentXml
    {
        public Guid? gid { get; set; }
        public DateTime? modifyDate { get; set; }
        public string xml { get; set; }
        public byte[] version { get; set; }

        public IList<string> CanEnterErrors { get; set; }
    }
}