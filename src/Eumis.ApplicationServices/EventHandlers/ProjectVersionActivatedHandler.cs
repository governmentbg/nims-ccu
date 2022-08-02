using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.RioExtensions;
using Eumis.Domain.Services;
using Eumis.Rio;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProjectVersionActivatedHandler : EventHandler<ProjectVersionActivatedEvent>
    {
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private INomenclatureDomainService nomenclaturesDomainService;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;

        public ProjectVersionActivatedHandler(
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            INomenclatureDomainService nomenclaturesDomainService,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository)
        {
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.nomenclaturesDomainService = nomenclaturesDomainService;
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
        }

        public override void Handle(ProjectVersionActivatedEvent e)
        {
            var projectVersion = this.projectVersionXmlsRepository.Find(e.ProjectVersionId);
            var versionDoc = projectVersion.GetDocument();

            var project = this.projectsRepository.Find(projectVersion.ProjectId);

            var candidate = versionDoc.Candidate;
            var budget = versionDoc.GetBudget();
            var projectPlace = versionDoc.GetProjectPlace();
            project.UpdateProjectData(
                versionDoc.ProjectBasicData.Name,
                candidate.Name,
                candidate.GetPrivateNomId(d => d.CompanyType, this.nomenclaturesDomainService.GetCompanyTypeNomIdByGid).Value,
                candidate.GetPrivateNomId(d => d.CompanyLegalType, this.nomenclaturesDomainService.GetCompanyLegalTypeNomIdByGid).Value,
                candidate.CompanyContactPersonEmail,
                candidate.GetPublicNomId(d => d.Seat.Country, this.countryNomsRepository.GetNomIdByCode),
                candidate.GetPublicNomId(d => d.Seat.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                candidate.Get(d => d.Seat.PostCode),
                candidate.Get(d => d.Seat.Street),
                candidate.Get(d => d.Seat.FullAddress),
                candidate.GetPublicNomId(d => d.Correspondence.Country, this.countryNomsRepository.GetNomIdByCode),
                candidate.GetPublicNomId(d => d.Correspondence.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                candidate.Get(d => d.Correspondence.PostCode),
                candidate.Get(d => d.Correspondence.Street),
                candidate.Get(d => d.Correspondence.FullAddress),
                candidate.GetPublicNomId(d => d.KidCodeProject, this.nomenclaturesDomainService.GetKidCodeNomIdByCode),
                int.Parse(versionDoc.ProjectBasicData.Duration),
                projectPlace.Item1,
                projectPlace.Item2,
                budget.Select(b => b.GrandAmount).Aggregate(0m, (a, b) => a + b),
                budget.Select(b => b.SelfAmount).Aggregate(0m, (a, b) => a + b));
        }
    }
}
