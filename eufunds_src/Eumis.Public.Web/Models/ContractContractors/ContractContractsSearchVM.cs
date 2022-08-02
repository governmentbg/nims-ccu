using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.ContractContracts.ViewObjects;
using PagedList;
using System;

namespace Eumis.Public.Web.Models.ContractContractors
{
    public class ContractContractsSearchVM
    {
        public string ProgrammeId { get; set; }

        public string Beneficiary { get; set; }

        public string CompanyUin { get; set; }

        public string ErrandLegalActId { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractContractVO> SearchResults { get; set; }

        public static void EncryptProperties(ContractContractsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Beneficiary = ConfigurationBasedStringEncrypter.Encrypt(vm.Beneficiary);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.ErrandLegalActId = ConfigurationBasedStringEncrypter.Encrypt(vm.ErrandLegalActId);
        }
    }
}
