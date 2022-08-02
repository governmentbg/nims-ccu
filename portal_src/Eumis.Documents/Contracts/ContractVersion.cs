using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractVersionPVO
    {
        public Guid gid { get; set; }

        public ContractEnumNomenclature versionType { get; set; }

        public int versionNum { get; set; }

        public int versionSubNum { get; set; }

        public DateTime? contractDate { get; set; }

        public string regNumber { get; set; }

        public ContractEnumNomenclature status { get; set; }
    }

    public enum ContractVersionType
    {
        newContract = 1,
        annex = 2,
        change = 3
    }

    public enum ContractVersionStatus
    {
        draft = 1,
        entered = 2,
        active = 3,
        archived = 4
    }

    public class ActualContractDO
    {
        public string contractVersionXml { get; set; }
        public string contractProcurementXml { get; set; }
    }
}
