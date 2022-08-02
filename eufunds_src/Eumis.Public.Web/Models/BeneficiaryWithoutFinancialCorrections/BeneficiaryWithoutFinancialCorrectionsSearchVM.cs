using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.BeneficiaryWithoutFinancialCorrections
{
    public class BeneficiaryWithoutFinancialCorrectionsSearchVM
    {
        public string Beneficiary { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public string Seat { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractBeneficiaryWithoutFinancialCorrectionsVO> SearchResults { get; set; }

        public static void EncryptProperties(BeneficiaryWithoutFinancialCorrectionsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Beneficiary = ConfigurationBasedStringEncrypter.Encrypt(vm.Beneficiary);
            vm.CompanyType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyType);
            vm.CompanyLegalType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyLegalType);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
            vm.Seat = ConfigurationBasedStringEncrypter.Encrypt(vm.Seat);
        }
    }
}
