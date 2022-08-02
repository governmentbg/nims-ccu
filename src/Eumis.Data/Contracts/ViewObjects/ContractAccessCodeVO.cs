using System;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractAccessCodeVO
    {
        public int ContractAccessCodeId { get; set; }

        public int ContractId { get; set; }

        public string ContractRegNumber { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Identifier { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
