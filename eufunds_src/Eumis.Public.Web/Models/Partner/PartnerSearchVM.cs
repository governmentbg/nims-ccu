using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Partner
{
    public class PartnerSearchVM
    {
        public string Partner { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public string CompanyUin { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractPartnerVO> SearchResults { get; set; }

        public static void EncryptProperties(PartnerSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Partner = ConfigurationBasedStringEncrypter.Encrypt(vm.Partner);
            vm.CompanyType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyType);
            vm.CompanyLegalType = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyLegalType);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
        }
    }
}