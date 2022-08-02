using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Eumis.ApplicationServices.Services.Company;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Counters;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.Repositories.Repos;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Eumis.Domain.RioExtensions;
using Eumis.Domain.Services;
using Eumis.Rio;

namespace Eumis.ApplicationServices.Services.ProjectRegistration
{
    internal class ProjectRegistrationService : IProjectRegistrationService
    {
        private IUnitOfWork unitOfWork;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectXmlsRepository;
        private IProjectFilesRepository projectFilesRepository;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private ICountersRepository countersRepository;
        private IProcedureDomainService procedureDomainService;
        private INomenclatureDomainService nomenclatureDomainService;
        private ICompaniesRepository companiesRepository;
        private IProjectTypeNomsRepository projectTypeNomsRepository;
        private ICompanyCreationService companyCreationService;
        private IProceduresRepository proceduresRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IBlobsRepository blobsRepository;

        public ProjectRegistrationService(
            IUnitOfWork unitOfWork,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectXmlsRepository,
            IProjectFilesRepository projectFilesRepository,
            IRegProjectXmlsRepository regProjectXmlsRepository,
            ICountersRepository countersRepository,
            IProcedureDomainService procedureDomainService,
            INomenclatureDomainService nomenclatureDomainService,
            ICompaniesRepository companiesRepository,
            IProjectTypeNomsRepository projectTypeNomsRepository,
            ICompanyCreationService companyCreationService,
            IProceduresRepository proceduresRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            IBlobsRepository blobsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.projectsRepository = projectsRepository;
            this.projectXmlsRepository = projectXmlsRepository;
            this.projectFilesRepository = projectFilesRepository;
            this.regProjectXmlsRepository = regProjectXmlsRepository;
            this.countersRepository = countersRepository;
            this.procedureDomainService = procedureDomainService;
            this.nomenclatureDomainService = nomenclatureDomainService;
            this.companiesRepository = companiesRepository;
            this.projectTypeNomsRepository = projectTypeNomsRepository;
            this.companyCreationService = companyCreationService;
            this.proceduresRepository = proceduresRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.blobsRepository = blobsRepository;
        }

        public int RegisterEmpty(
            int procedureId,
            string projectName,
            string projectNameAlt,
            int companyId,
            int projectTypeId,
            ProjectRegistrationStatus regStatus,
            DateTime regDate,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes)
        {
            this.countersRepository.CreateProjectCounter(procedureId);

            var company = this.companiesRepository.Find(companyId);

            var projectId = this.Register(
                procedureId,
                projectName,
                projectNameAlt,
                companyId,
                company.Name,
                company.NameAlt,
                company.Uin,
                company.UinType,
                company.CompanyTypeId,
                company.CompanyLegalTypeId,
                company.ContactEmail,
                company.SeatCountryId,
                company.SeatSettlementId,
                company.SeatPostCode,
                company.SeatStreet,
                company.SeatAddress,
                company.CorrCountryId,
                company.CorrSettlementId,
                company.CorrPostCode,
                company.CorrStreet,
                company.CorrAddress,
                projectTypeId,
                regStatus,
                regDate,
                recieveType,
                recieveDate,
                submitDate,
                storagePlace,
                originals,
                copies,
                notes,
                null,
                null,
                null,
                null,
                null).Item1;

            return projectId;
        }

        public int RegisterSubmitted(
            int regProjectXmlId,
            int companyId,
            int projectTypeId,
            ProjectRegistrationStatus regStatus,
            DateTime regDate,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(regProjectXmlId);

            if (regProjectXml.Status != RegProjectXmlStatus.Submitted)
            {
                throw new Exception("Cannot register RegProjectXmls that are not submitted");
            }

            var regProjectDoc = regProjectXml.GetDocument();

            this.countersRepository.CreateProjectCounter(regProjectXml.ProcedureId);

            var budget = regProjectDoc.GetBudget();
            var projectPlace = regProjectDoc.GetProjectPlace();
            var projectId = this.Register(
                regProjectXml.ProcedureId,
                regProjectXml.ProjectName,
                regProjectXml.ProjectNameAlt,
                companyId,
                regProjectDoc.Candidate.Name,
                regProjectDoc.Candidate.NameEN,
                regProjectDoc.Candidate.Uin,
                regProjectDoc.Candidate.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value,
                regProjectDoc.Candidate.GetPrivateNomId(d => d.CompanyType, this.nomenclatureDomainService.GetCompanyTypeNomIdByGid).Value,
                regProjectDoc.Candidate.GetPrivateNomId(d => d.CompanyLegalType, this.nomenclatureDomainService.GetCompanyLegalTypeNomIdByGid).Value,
                regProjectDoc.Candidate.CompanyContactPersonEmail,
                regProjectDoc.Candidate.GetPublicNomId(d => d.Seat.Country, this.countryNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Seat.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.Get(d => d.Seat.PostCode),
                regProjectDoc.Candidate.Get(d => d.Seat.Street),
                regProjectDoc.Candidate.Get(d => d.Seat.FullAddress),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Correspondence.Country, this.countryNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Correspondence.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.Get(d => d.Correspondence.PostCode),
                regProjectDoc.Candidate.Get(d => d.Correspondence.Street),
                regProjectDoc.Candidate.Get(d => d.Correspondence.FullAddress),
                projectTypeId,
                regStatus,
                regDate,
                recieveType,
                recieveDate,
                submitDate,
                storagePlace,
                originals,
                copies,
                notes,
                int.Parse(regProjectDoc.ProjectBasicData.Duration),
                projectPlace.Item1,
                projectPlace.Item2,
                budget.Select(b => b.GrandAmount).Aggregate(0M, (a, b) => a + b),
                budget.Select(b => b.SelfAmount).Aggregate(0M, (a, b) => a + b)).Item1;

            regProjectXml.MarkPaperRegistered(projectId);

            // creating first project version
            this.projectXmlsRepository.Add(new Eumis.Domain.Projects.ProjectVersionXml(
                projectId,
                regProjectXml.Xml,
                Eumis.Domain.Users.User.SystemUserId,
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectRegistrationService_CreatedByRegistration), new CultureInfo(SystemLocalization.Bg_BG)),
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectRegistrationService_CreatedByRegistration), new CultureInfo(SystemLocalization.En_GB)),
                regDate));

            this.unitOfWork.Save();

            return projectId;
        }

        public string RegisterRegistrationXml(int registrationId, string xml, byte[] isunFile, IList<byte[]> signatures)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("xml cannot be null or empty");
            }

            if (isunFile == null)
            {
                throw new ArgumentException("isunFile cannot be null");
            }

            if (signatures == null || signatures.Count == 0)
            {
                throw new ArgumentException("signatures cannot be null or empty");
            }

            var regDate = DateTime.Now;
            var hash = RegProjectXml.GetHash(xml);

            RegProjectXml regProjectXml = this.regProjectXmlsRepository.FindOrDefault(registrationId, hash);

            if (regProjectXml == null)
            {
                regProjectXml = new RegProjectXml(registrationId, xml, this.procedureDomainService);

                this.regProjectXmlsRepository.Add(regProjectXml);
                this.blobsRepository.ResurrectBlobs(regProjectXml.XmlFiles.Select(f => f.BlobKey));

                regProjectXml.MakeFinal();
            }
            else
            {
                if (regProjectXml.Status != RegProjectXmlStatus.Finalized)
                {
                    throw new Exception("Cannot submit a project xml of type other than finalized.");
                }
            }

            var regProjectDoc = regProjectXml.GetDocument();

            string uin = regProjectDoc.Get(d => d.Candidate.Uin);
            UinType? uinType = regProjectDoc.GetEnum<Rio.Project, UinType>(d => d.Candidate.UinType.Id);
            int procedureId = regProjectXml.ProcedureId;

            // TODO validate instead of this check
            if (uinType == null ||
                string.IsNullOrEmpty(uin))
            {
                // TODO should we continue?
                throw new Exception("Missing required project attributes!");
            }

            var procedureInTime = this.proceduresRepository.IsProcedureInTimeLimit(procedureId);
            if (!procedureInTime)
            {
                throw new Exception("Procedure time limit expired");
            }

            this.countersRepository.CreateProjectCounter(procedureId);

            Eumis.Domain.Companies.Company company = this.companiesRepository.FindByUinOrDefault(uin, uinType.Value);

            // create the company if it does not exist
            if (company == null)
            {
                company = this.companyCreationService.CreateFromRioCompany(regProjectDoc.Candidate);
                this.companiesRepository.Add(company);
                this.unitOfWork.Save();
            }

            var budget = regProjectDoc.GetBudget();
            var projectPlace = regProjectDoc.GetProjectPlace();
            var reg = this.Register(
                procedureId,
                regProjectXml.ProjectName,
                regProjectXml.ProjectNameAlt,
                company.CompanyId,
                regProjectDoc.Candidate.Name,
                regProjectDoc.Candidate.NameEN,
                regProjectDoc.Candidate.Uin,
                regProjectDoc.Candidate.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value,
                regProjectDoc.Candidate.GetPrivateNomId(d => d.CompanyType, this.nomenclatureDomainService.GetCompanyTypeNomIdByGid).Value,
                regProjectDoc.Candidate.GetPrivateNomId(d => d.CompanyLegalType, this.nomenclatureDomainService.GetCompanyLegalTypeNomIdByGid).Value,
                regProjectDoc.Candidate.CompanyContactPersonEmail,
                regProjectDoc.Candidate.GetPublicNomId(d => d.Seat.Country, this.countryNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Seat.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.Get(d => d.Seat.PostCode),
                regProjectDoc.Candidate.Get(d => d.Seat.Street),
                regProjectDoc.Candidate.Get(d => d.Seat.FullAddress),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Correspondence.Country, this.countryNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.GetPublicNomId(d => d.Correspondence.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                regProjectDoc.Candidate.Get(d => d.Correspondence.PostCode),
                regProjectDoc.Candidate.Get(d => d.Correspondence.Street),
                regProjectDoc.Candidate.Get(d => d.Correspondence.FullAddress),
                this.projectTypeNomsRepository.GetNomByAlias("projectProposal").NomValueId,
                ProjectRegistrationStatus.Registered, // TODO should we check the time?
                regDate,
                ProjectRecieveType.Electronic,
                regDate,
                regDate,
                null,
                null,
                null,
                null,
                int.Parse(regProjectDoc.ProjectBasicData.Duration),
                projectPlace.Item1,
                projectPlace.Item2,
                budget.Select(b => b.GrandAmount).Aggregate(0M, (a, b) => a + b),
                budget.Select(b => b.SelfAmount).Aggregate(0M, (a, b) => a + b));

            int projectId = reg.Item1;
            string regNumber = reg.Item2;

            regProjectXml.MakeRegistered(projectId);

            // creating first project version
            var projectVersionXml = new Eumis.Domain.Projects.ProjectVersionXml(
                projectId,
                xml,
                Eumis.Domain.Users.User.SystemUserId,
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectRegistrationService_CreatedByRegistration), new CultureInfo(SystemLocalization.Bg_BG)),
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectRegistrationService_CreatedByRegistration), new CultureInfo(SystemLocalization.En_GB)),
                regDate);

            this.projectXmlsRepository.Add(projectVersionXml);

            this.unitOfWork.Save();

            var procedureCode = this.proceduresRepository.GetProcedureBasicData(procedureId).Code;

            var isunFileTuple = new Tuple<byte[], string>(isunFile, procedureCode + "-" + hash + ".isun");

            var signaturesTuples = new List<Tuple<byte[], string>>();

            int counter = 1;
            foreach (var signature in signatures)
            {
                signaturesTuples.Add(new Tuple<byte[], string>(signature, procedureCode + "-" + hash + "-sig" + counter + ".p7s"));
                counter++;
            }

            this.projectFilesRepository.Add(new ProjectFile(projectVersionXml.ProjectVersionXmlId, isunFileTuple, signaturesTuples));

            this.unitOfWork.Save();

            return regNumber;
        }

        private Tuple<int, string> Register(
            int procedureId,
            string projectName,
            string projectNameAlt,
            int companyId,
            string companyName,
            string companyNameAlt,
            string companyUin,
            UinType companyUinType,
            int companyTypeId,
            int companyLegalTypeId,
            string companyEmail,
            int? companySeatCountryId,
            int? companySeatSettlementId,
            string companySeatPostCode,
            string companySeatStreet,
            string companySeatAddress,
            int? companyCorrespondenceCountryId,
            int? companyCorrespondenceSettlementId,
            string companyCorrespondencePostCode,
            string companyCorrespondenceStreet,
            string companyCorrespondenceAddress,
            int projectTypeId,
            ProjectRegistrationStatus regStatus,
            DateTime regDate,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes,
            int? duration,
            string nutsAddressFullPath,
            string nutsAddressFullPathName,
            decimal? totalBfpAmount,
            decimal? coFinancingAmount)
        {
            string regNumber = this.countersRepository.GetNextProjectNumber(procedureId);

            var project = new Eumis.Domain.Projects.Project(
                procedureId,
                projectTypeId,
                companyId,
                companyName,
                companyNameAlt,
                companyUin,
                companyUinType,
                companyTypeId,
                companyLegalTypeId,
                companyEmail,
                companySeatCountryId,
                companySeatSettlementId,
                companySeatPostCode,
                companySeatStreet,
                companySeatAddress,
                companyCorrespondenceCountryId,
                companyCorrespondenceSettlementId,
                companyCorrespondencePostCode,
                companyCorrespondenceStreet,
                companyCorrespondenceAddress,
                projectName,
                projectNameAlt,
                regStatus,
                regNumber,
                regDate,
                recieveType,
                recieveDate,
                submitDate,
                storagePlace,
                originals,
                copies,
                notes,
                duration,
                nutsAddressFullPath,
                nutsAddressFullPathName,
                totalBfpAmount,
                coFinancingAmount);

            this.projectsRepository.Add(project);

            this.unitOfWork.Save();

            return Tuple.Create(project.ProjectId, regNumber);
        }
    }
}
