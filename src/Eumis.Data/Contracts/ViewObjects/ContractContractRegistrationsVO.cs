using Eumis.Domain.Core;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractContractRegistrationsVO
    {
        public int ContractsContractRegistrationId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public FileVO File { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
