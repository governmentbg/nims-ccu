using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Member
{
    public class MemberSearchVM
    {
        public string Member { get; set; }

        public string CompanyUin { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ContractSubcontractorVO> SearchResults { get; set; }

        public static void EncryptProperties(MemberSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.Member = ConfigurationBasedStringEncrypter.Encrypt(vm.Member);
            vm.CompanyUin = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyUin);
        }
    }
}