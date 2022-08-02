using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Subcontractor
{
    public class SubcontractorSearchVM
    {
        public string Subcontractor { get; set; }

        public string CompanyUin { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractSubcontractorVO> SearchResults { get; set; }

        public static void EncryptProperties(SubcontractorSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Subcontractor = ConfigurationBasedStringEncrypter.Encrypt(vm.Subcontractor);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
        }
    }
}