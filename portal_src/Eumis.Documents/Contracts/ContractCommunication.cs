using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractCommunication : ContractDocumentXml
    {
        public ContractEnumNomenclature status { get; set; }

        public string statusNote { get; set; }

        public ContractEnumNomenclature source { get; set; }

        public string regNumber { get; set; }

        public DateTime? readDate { get; set; }

        public int orderNum { get; set; }

        public DateTime? sendDate { get; set; }

        public DateTime createDate { get; set; }
    }
}