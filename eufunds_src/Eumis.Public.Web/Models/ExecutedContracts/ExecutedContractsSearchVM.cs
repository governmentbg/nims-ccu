using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.ExecutedContracts.ViewObjects;
using PagedList;
using System;

namespace Eumis.Public.Web.Models.ExecutedContracts
{
    public class ExecutedContractsSearchVM
    {
        public string ProgrammeId { get; set; }

        public string ProcedureId { get; set; }

        public bool ShowRes { get; set; }

        public string CompanyId { get; set; }

        public IPagedList<ExecutedContractVO> SearchResults { get; set; }

        public static void EncryptProperties(ExecutedContractsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.CompanyId = ConfigurationBasedStringEncrypter.Encrypt(vm.CompanyId);
            vm.ProcedureId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProcedureId);
        }
    }
}
