using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Contractor
{
    public class ContractorSearchVM
    {
        public string Contractor { get; set; }

        public string CompanyUin { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractContractorVO> SearchResults { get; set; }

        public static void EncryptProperties(ContractorSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Contractor = ConfigurationBasedStringEncrypter.Encrypt(vm.Contractor);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
        }
    }
}