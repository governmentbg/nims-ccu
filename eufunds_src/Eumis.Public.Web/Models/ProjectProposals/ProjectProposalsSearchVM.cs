using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Indicators
{
    public class ProjectProposalsSearchVM
    {
        public string ProgrammeId { get; set; }

        public string PriorityAxisId { get; set; }

        public string ProcedureId { get; set; }

        public bool ShowRes { get; set; }

        public ProjectProposalVO SearchResultsTotals { get; set; }

        public IPagedList<ProjectProposalVO> SearchResults { get; set; }

        public static void EncryptProperties(ProjectProposalsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.PriorityAxisId = ConfigurationBasedStringEncrypter.Encrypt(vm.PriorityAxisId);
            vm.ProcedureId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProcedureId);
        }
    }
}