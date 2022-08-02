using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractUserDO
    {
        public ContractUserDO()
        {
        }

        public ContractUserDO(int contractId, byte[] version)
        {
            this.ContractId = contractId;
            this.Version = version;
        }

        public ContractUserDO(ContractUser contractUser, byte[] version)
        {
            this.ContractUserId = contractUser.ContractUserId;
            this.ContractId = contractUser.ContractId;
            this.UserId = contractUser.UserId;

            this.Version = version;
        }

        public int? ContractUserId { get; set; }

        public int? ContractId { get; set; }

        public int? UserId { get; set; }

        public byte[] Version { get; set; }
    }
}
