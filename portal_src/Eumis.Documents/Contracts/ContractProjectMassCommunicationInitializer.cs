using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProjectMassCommunicationInitializer
    {
        public ContractNomenclature subject { get; set; }

        public string message { get; set; }

        public List<ContractAttachedFilePVO> files { get; set; }
    }
}
