using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractCommunicationInfos
    {
        public List<ContractCommunicationInfo> results { get; set; }
        public int count { get; set; }
    }

    public class ContractCommunicationInfo
    {
        public Guid xmlGid { get; set; }

        public ContractEnumNomenclature status { get; set; }

        public string statusNote { get; set; }

        public ContractEnumNomenclature source { get; set; }

        public string regNumber { get; set; }

        public string subject { get; set; }

        public DateTime? readDate { get; set; }

        public int orderNum { get; set; }

        public DateTime? sendDate { get; set; }

        public DateTime? modifyDate { get; set; }

        public bool isDraft
        {
            get
            {
                return this.status.value.Equals("draft");
            }
        }

        public bool isBeneficiary
        {
            get
            {
                return this.source.value.Equals("beneficiary");
            }
        }

        public bool isRead
        {
            get
            {
                return this.readDate.HasValue;
            }
        }
    }
}