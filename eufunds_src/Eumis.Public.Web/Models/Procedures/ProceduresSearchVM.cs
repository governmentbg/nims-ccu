using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.Procedures.ViewObjects;
using PagedList;
using System;

namespace Eumis.Public.Web.Models.Procedures
{
    public class ProceduresSearchVM
    {
        public string SettlementId { get; set; }

        public string CompanyTypeId { get; set; }

        public string CompanyLegalTypeId { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ProcedureVO> SearchResults { get; set; }

        public static void EncryptProperties(ProceduresSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.CompanyTypeId = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyTypeId);
            vm.CompanyLegalTypeId = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyLegalTypeId);
            vm.SettlementId = ConfigurationBasedStringEncrypter.Encrypt(vm.SettlementId);
        }
    }
}
