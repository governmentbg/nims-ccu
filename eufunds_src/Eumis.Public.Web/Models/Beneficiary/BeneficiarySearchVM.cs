using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Beneficiary
{
    public class BeneficiarySearchVM
    {
        public string Beneficiary { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractBeneficiaryVO> SearchResults { get; set; }

        public static void EncryptProperties(BeneficiarySearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Beneficiary = ConfigurationBasedStringEncrypter.Encrypt(vm.Beneficiary);
            vm.CompanyType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyType);
            vm.CompanyLegalType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyLegalType);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
        }
    }
}