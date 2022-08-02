using Eumis.ApplicationServices.Services.Company;
using Eumis.ApplicationServices.Services.ProjectRegistration;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Eumis.Domain.Services;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Print;
using Eumis.Rio;
using Eumis.Web.Api.Companies.DataObjects;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Projects.DataObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        // repositories
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private IProjectTypeNomsRepository projectTypeNomsRepository;
        private ICompaniesRepository companiesRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private INomenclatureDomainService nomenclatureDomainService;
        private IPrintManager printManager;
        private IProjectRegistrationService projectRegistrationService;
        private ICompanyCreationService companyCreationService;
        private IEvalSessionsRepository evalSessionsRepository;

        public ProjectsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            //// repositories
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IRegProjectXmlsRepository regProjectXmlsRepository,
            IProjectTypeNomsRepository projectTypeNomsRepository,
            ICompaniesRepository companiesRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            //// services
            INomenclatureDomainService nomenclatureDomainService,
            IPrintManager printManager,
            IProjectRegistrationService projectRegistrationService,
            ICompanyCreationService companyCreationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;

            // repositories
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.regProjectXmlsRepository = regProjectXmlsRepository;
            this.projectTypeNomsRepository = projectTypeNomsRepository;
            this.companiesRepository = companiesRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.nomenclatureDomainService = nomenclatureDomainService;
            this.printManager = printManager;
            this.projectRegistrationService = projectRegistrationService;
            this.companyCreationService = companyCreationService;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        [Route("")]
        public IList<ProjectRegistrationsVO> GetProjectRegistrations(
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string projectNumber = null)
        {
            this.authorizer.AssertCanDo(ProjectListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectPermissions.CanRead);

            return this.projectsRepository.GetProjectRegistrations(programmeIds, programmePriorityId, procedureId, fromDate, toDate, projectNumber);
        }

        [Route("~/api/contractProjects")]
        public IList<ProjectRegistrationsVO> GetProjectRegistrationsForContract(
            int? procedureId = null,
            string projectNumber = null)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            return this.projectsRepository.GetProjectRegistrations(programmeIds, null, procedureId, null, null, projectNumber);
        }

        [Route("~/api/projectDossierProjects")]
        public IList<ProjectRegistrationsVO> GetProjectRegistrationsForProjectDossier(
            int? procedureId = null,
            string projectNumber = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectDossierPermissions.CanRead);

            return this.projectsRepository.GetProjectRegistrationsForProjectDossier(programmeIds, procedureId, projectNumber);
        }

        [Route("~/api/irregularitySignalProjects")]
        public IList<ProjectRegistrationsVO> GetIrregularitySignalProjects(int? programmeId = null, int? procedureId = null, string projectNumber = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Create);

            var programmeIds = System.Array.Empty<int>();

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.projectsRepository.GetProjectRegistrations(programmeIds, null, procedureId, null, null, projectNumber);
        }

        [Route("{projectId:int}")]
        public ProjectRegistrationDO GetProjectRegistration(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);

            var project = this.projectsRepository.Find(projectId);
            var hasVersions = this.projectVersionXmlsRepository.GetProjectVersions(projectId).Any();
            var hasRegistration = this.projectsRepository.HasAssociatedRegistration(projectId);

            return new ProjectRegistrationDO(project, hasVersions, hasRegistration);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/project")]
        public ProjectRegistrationDO GetProjectRegistrationForSheet(int sheetId)
        {
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.View, sheetData.ProjectId);

            var project = this.projectsRepository.Find(sheetData.ProjectId);
            var hasVersions = this.projectVersionXmlsRepository.GetProjectVersions(sheetData.ProjectId).Any();
            var hasRegistration = this.projectsRepository.HasAssociatedRegistration(sheetData.ProjectId);

            return new ProjectRegistrationDO(project, hasVersions, hasRegistration);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/project")]
        public ProjectRegistrationDO GetProjectRegistrationForStandpoint(int standpointId)
        {
            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.View, standpointData.ProjectId);

            var project = this.projectsRepository.Find(standpointData.ProjectId);
            var hasVersions = this.projectVersionXmlsRepository.GetProjectVersions(standpointData.ProjectId).Any();
            var hasRegistration = this.projectsRepository.HasAssociatedRegistration(standpointData.ProjectId);

            return new ProjectRegistrationDO(project, hasVersions, hasRegistration);
        }

        [HttpGet]
        [Route("~/api/projectDossier/{projectId:int}/project")]
        public ProjectRegistrationDO GetProjectForProjectDossier(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            var project = this.projectsRepository.Find(projectId);
            var hasVersions = this.projectVersionXmlsRepository.GetProjectVersions(projectId).Any();
            var hasRegistration = this.projectsRepository.HasAssociatedRegistration(projectId);

            return new ProjectRegistrationDO(project, hasVersions, hasRegistration);
        }

        [HttpGet]
        [Route("new")]
        public ProjectRegistrationDO NewProject(int procedureId, int companyId, int? regProjectXmlId = null)
        {
            this.authorizer.AssertCanDo(ProcedureActions.CreateProject, procedureId);

            var currentDate = DateTime.Now;
            string name = null;
            string nameAlt = null;
            if (regProjectXmlId != null)
            {
                var regProject = this.regProjectXmlsRepository.Find(regProjectXmlId.Value);

                if (regProject.Status != RegProjectXmlStatus.Submitted)
                {
                    throw new Exception("Cannot register RegProjectXmls that are not submitted");
                }

                name = regProject.ProjectName;
                nameAlt = regProject.ProjectNameAlt;
            }

            var company = this.companiesRepository.Find(companyId);

            return new ProjectRegistrationDO()
            {
                RegProjectXmlId = regProjectXmlId,
                RegistrationStatus = ProjectRegistrationStatus.Registered,
                ProjectTypeId = this.projectTypeNomsRepository.GetNomByAlias("projectProposal").NomValueId,
                ProcedureId = procedureId,
                CompanyId = company.CompanyId,
                CompanyName = company.Name,
                CompanyUin = company.Uin,
                CompanyUinType = company.UinType,
                Name = name,
                NameAlt = nameAlt,
                RegDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day),
                RegTime = ((currentDate.Hour * 60) + currentDate.Minute) * 60000,
                RecieveDate = currentDate,
                SubmitDate = currentDate,
            };
        }

        [HttpGet]
        [Route("companyByUin")]
        public ProjectCompanyDO GetProjectCompanyByUin(int procedureId, UinType uinType, string uin)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProcedureActions.CreateProject, procedureId),
                Tuple.Create<Enum, int?>(ContractListActions.Create, null));

            var company = this.companiesRepository.FindByUinOrDefault(uin, uinType);

            CandidateDO companyDO;
            if (company == null)
            {
                var defaultCountryId = this.countryNomsRepository.GetNomIdByCode("BG");

                companyDO = new CandidateDO
                {
                    Uin = uin,
                    UinType = uinType,
                    SeatCountryId = defaultCountryId,
                    CorrCountryId = defaultCountryId,
                    IsCreated = false,
                };
            }
            else
            {
                companyDO = new CandidateDO(company, true);
            }

            return new ProjectCompanyDO()
            {
                ProcedureId = procedureId,
                Company = companyDO,
            };
        }

        [HttpGet]
        [Route("companyByCode")]
        public ProjectCompanyDO GetProjectCompanyByCode(string code)
        {
            var regProject = this.regProjectXmlsRepository.Find(code);

            if (regProject.Status != RegProjectXmlStatus.Submitted)
            {
                throw new Exception("Cannot register RegProjectXmls that are not submitted");
            }

            var regProjectDoc = regProject.GetDocument();

            string uin = regProjectDoc.Get(d => d.Candidate.Uin);
            UinType? uinType = regProjectDoc.GetEnum<Rio.Project, UinType>(d => d.Candidate.UinType.Id);
            int procedureId = regProject.ProcedureId;

            // TODO validate instead of this check
            if (uinType == null ||
                string.IsNullOrEmpty(uin))
            {
                // TODO should we continue?
                throw new Exception("Missing required project attributes!");
            }

            this.authorizer.AssertCanDo(ProcedureActions.CreateProject, procedureId);

            var company = this.companiesRepository.FindByUinOrDefault(uin, uinType.Value);
            bool companyExists = true;

            if (company == null)
            {
                // do not save the company here
                // the user can abort the creation
                company = this.companyCreationService.CreateFromRioCompany(regProjectDoc.Candidate);
                companyExists = false;
            }

            return new ProjectCompanyDO()
            {
                RegProjectXmlId = regProject.RegProjectXmlId,
                ProcedureId = procedureId,
                Company = new CandidateDO(company, companyExists),
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Register))]
        public ProjectRegistrationResultDO RegisterProject(ProjectRegistrationDO projectRegistration)
        {
            this.authorizer.AssertCanDo(ProcedureActions.CreateProject, projectRegistration.ProcedureId);

            int projectId;
            DateTime regDate = projectRegistration.RegDate.Value.AddMilliseconds(projectRegistration.RegTime.Value);

            // the registration was created for a RegProjectXml
            if (projectRegistration.RegProjectXmlId != null)
            {
                projectId = this.projectRegistrationService.RegisterSubmitted(
                    projectRegistration.RegProjectXmlId.Value,
                    projectRegistration.CompanyId,
                    projectRegistration.ProjectTypeId.Value,
                    projectRegistration.RegistrationStatus.Value,
                    regDate,
                    projectRegistration.RecieveType.Value,
                    projectRegistration.RecieveDate.Value,
                    projectRegistration.SubmitDate.Value,
                    projectRegistration.StoragePlace,
                    projectRegistration.Originals,
                    projectRegistration.Copies,
                    projectRegistration.Notes);
            }
            else
            {
                projectId = this.projectRegistrationService.RegisterEmpty(
                    projectRegistration.ProcedureId,
                    projectRegistration.Name,
                    projectRegistration.NameAlt,
                    projectRegistration.CompanyId,
                    projectRegistration.ProjectTypeId.Value,
                    projectRegistration.RegistrationStatus.Value,
                    regDate,
                    projectRegistration.RecieveType.Value,
                    projectRegistration.RecieveDate.Value,
                    projectRegistration.SubmitDate.Value,
                    projectRegistration.StoragePlace,
                    projectRegistration.Originals,
                    projectRegistration.Copies,
                    projectRegistration.Notes);
            }

            return new ProjectRegistrationResultDO
            {
                ProjectId = projectId,
            };
        }

        [HttpPut]
        [Route("{projectId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit), IdParam = "projectId")]
        public void UpdateProject(int projectId, ProjectRegistrationDO projectRegistration)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            if (this.projectVersionXmlsRepository.GetProjectVersions(projectId).Any())
            {
                throw new InvalidOperationException("Cannot edit project registration data.");
            }

            var oldProject = this.projectsRepository.FindForUpdate(projectId, projectRegistration.Version);

            oldProject.UpdateAttributes(
                projectRegistration.Name,
                projectRegistration.RegistrationStatus.Value,
                projectRegistration.RecieveType.Value,
                projectRegistration.RecieveDate.Value,
                projectRegistration.SubmitDate.Value,
                projectRegistration.StoragePlace,
                projectRegistration.Originals,
                projectRegistration.Copies,
                projectRegistration.Notes);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isCodeExisting")]
        public bool IsCodeExisting(string code)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectPermissions.CanRegister);

            return this.regProjectXmlsRepository.SubmittedHashesStartingWith(code, programmeIds).Length == 1;
        }

        [HttpGet]
        [Route("{projectId:int}/print")]
        public HttpResponseMessage Print(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);

            var regData = this.projectsRepository.GetProjectRegistrationData(projectId);
            JObject context = JObject.FromObject(regData);

            var pdfBytes = this.printManager.Print(TemplateType.ProjectRegistration, PrintType.PDF, context);

            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(pdfBytes);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            responseMessage.Content.Headers.ContentDisposition.FileName = regData.RegNumber + ".pdf";

            return responseMessage;
        }

        [HttpPost]
        [Route("{projectId:int}/canWithdraw")]
        public ErrorsDO CanWithdrawProject(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.Withdraw, projectId);

            var errorList = this.projectsRepository.CanWithdrawProject(projectId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{projectId:int}/withdraw")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Withdraw), IdParam = "projectId")]
        public void WithdrawProject(int projectId, string version)
        {
            this.authorizer.AssertCanDo(ProjectActions.Withdraw, projectId);

            byte[] vers = System.Convert.FromBase64String(version);
            var project = this.projectsRepository.FindForUpdate(projectId, vers);

            var evalSessions = this.evalSessionsRepository.GetNonCanceledEvalSessionsByProcedure(project.ProcedureId);

            project.Withdraw(evalSessions);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isRegNumExisting")]
        public bool IsProjectNumExisting(string projectNum, int? procedureId = null)
        {
            return this.projectsRepository.IsProjectNumExisting(projectNum, procedureId);
        }

        [HttpGet]
        [Route("contractProjectByNumber")]
        public ProjectRegistrationDO GetContractProjectByNumber(string projectNum)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var project = this.projectsRepository.FindByRegNumber(projectNum);

            return new ProjectRegistrationDO(project, false, false);
        }

        [HttpGet]
        [Route("projectDossierProjectByNumber")]
        public ProjectRegistrationDO GetProjectDossierProjectByNumber(string projectNum)
        {
            var project = this.projectsRepository.FindByRegNumber(projectNum);

            if (this.projectsRepository.IsProjectValidForProjectDossier(project.ProjectId).Any())
            {
                throw new Exception("Project is not valid for project dossier");
            }

            this.authorizer.AssertCanDo(ProjectDossierActions.View, project.ProjectId);

            return new ProjectRegistrationDO(project, false, false);
        }

        [HttpPost]
        [Route("isProjectValidForProjectDossier")]
        public ErrorsDO IsProjectValidForProjectDossier(string projectNum)
        {
            var project = this.projectsRepository.FindByRegNumber(projectNum);

            var errorList = this.projectsRepository.IsProjectValidForProjectDossier(project.ProjectId);

            return new ErrorsDO(errorList);
        }

        [HttpGet]
        [Route("~/api/projectDossier/{projectId:int}/documents")]
        public IList<ProjectDossierDocumentVO> GetProjectDossierDocuments(
            int projectId,
            int? contractId = null,
            string objDescription = null,
            string fileDescription = null,
            [FromUri] ProjectDossierDocumentType[] docTypes = null)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            return this.projectsRepository.GetProjectDossierDocuments(projectId, contractId, docTypes, objDescription, fileDescription);
        }
    }
}
