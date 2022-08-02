using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractCommunicationInitializer
    {
        public R_09987.CommunicationTypeNomenclature type { get; set; }

        public string subject { get; set; }

        public string body { get; set; }

        public List<AttachedFilePVO> files { get; set; }
    }

    public class AttachedFilePVO
    {
        public AttachedFilePVO()
        {
        }

        public Guid fileKey { get; set; }

        public string fileName { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }
}
