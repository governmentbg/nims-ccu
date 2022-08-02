using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportTechnicalMember
{
    public class ContractReportTechnicalMemberService : IContractReportTechnicalMemberService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;

        public ContractReportTechnicalMemberService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
        }

        public void CreateContractReportTechnicalMembers(ContractReportTechnical technical)
        {
            IList<Eumis.Domain.Contracts.ContractReportTechnicalMember> newContractReportTechnicalMembers = new List<Eumis.Domain.Contracts.ContractReportTechnicalMember>();

            var technicalDoc = technical.GetDocument();
            if (technicalDoc.Team != null && technicalDoc.Team.TeamMemberCollection != null)
            {
                foreach (var member in technicalDoc.Team.TeamMemberCollection)
                {
                    var uinType = member.GetEnum<Rio.TechnicalReportTeamMember, PortalPersonalUinType>(c => c.UinType.Id);
                    var newContractReportTechnicalMember = new Eumis.Domain.Contracts.ContractReportTechnicalMember(
                        technical.ContractId,
                        technical.ContractReportId,
                        technical.ContractReportTechnicalId,
                        member.Name,
                        member.Position,
                        member.Uin,
                        uinType.HasValue ? (uinType.Value == PortalPersonalUinType.EGN ? PersonalUinType.PersonalBulstat : PersonalUinType.ForeignNumber) : (PersonalUinType?)null,
                        member.GetEnum<Rio.TechnicalReportTeamMember, CommitmentType>(c => c.CommitmentType.Value),
                        member.Date,
                        member.Hours,
                        member.Activity,
                        member.Result);

                    newContractReportTechnicalMembers.Add(newContractReportTechnicalMember);
                }
            }

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportTechnicalMember>(newContractReportTechnicalMembers);
        }

        public void DeleteContractReportTechnicalMembers(ContractReportTechnical technical)
        {
            this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportTechnicalMember>(p => p.ContractReportTechnicalId == technical.ContractReportTechnicalId);
        }
    }
}
