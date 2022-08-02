using Eumis.ApplicationServices.Services.ProjectRegistration;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.NonAggregates.Repositories.Repos;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Registrations;
using Eumis.Domain.Services;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    [RoutePrefix("api/registration")]
    public class RegProjectXmlsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProcedureDomainService procedureDomainService;
        private IProjectRegistrationService projectRegistrationService;
        private IBlobsRepository blobsRepository;

        public RegProjectXmlsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IRegProjectXmlsRepository regProjectXmlsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProcedureDomainService procedureDomainService,
            IProjectRegistrationService projectRegistrationService,
            IBlobsRepository blobsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.regProjectXmlsRepository = regProjectXmlsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.procedureDomainService = procedureDomainService;
            this.projectRegistrationService = projectRegistrationService;
            this.blobsRepository = blobsRepository;
        }

        [Route("projects")]
        public PagePVO<RegProjectXmlPVO> GetAll(RegProjectXmlStatus type, int offset = 0, int? limit = null)
        {
            return this.regProjectXmlsRepository.GetAllForRegistration(this.accessContext.RegistrationId, type, offset, limit);
        }

        [Route("projectManagingAuthorityCommunications")]
        public PagePVO<RegProjectXmlPVO> GetProjectManagingAuthorityCommunications(int offset = 0, int? limit = null)
        {
            return this.regProjectXmlsRepository.GetAllProjectCommunicationsForRegistration(this.accessContext.RegistrationId, offset, limit);
        }

        [Route("projects/{gid:guid}")]
        public XmlDO Get(Guid gid)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(this.accessContext.RegistrationId, gid);

            return new XmlDO
            {
                Xml = regProjectXml.Xml,
                Version = regProjectXml.Version,
            };
        }

        [Route("projectVersions/{gid:guid}")]
        public XmlDO GetProjectVersion(Guid gid)
        {
            var projectVersionXml = this.projectVersionXmlsRepository.Find(gid);

            return new XmlDO
            {
                Xml = projectVersionXml.Xml,
                Version = projectVersionXml.Version,
            };
        }

        [HttpPost]
        [Route("projects")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.Create), DisablePostData = true)]
        public XmlDO Create(XmlDO regProjectXmlsDO)
        {
            var regProjectXml = new RegProjectXml(this.accessContext.RegistrationId, regProjectXmlsDO.Xml, this.procedureDomainService);

            this.regProjectXmlsRepository.Add(regProjectXml);
            this.blobsRepository.ResurrectBlobs(regProjectXml.XmlFiles.Select(f => f.BlobKey));

            this.unitOfWork.Save();

            return new XmlDO
            {
                Gid = regProjectXml.Gid,
                ModifyDate = regProjectXml.ModifyDate,
                Version = regProjectXml.Version,
            };
        }

        [HttpPut]
        [Route("projects/{gid:guid}")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.Update), DisablePostData = true)]
        public XmlDO Update(Guid gid, XmlDO regProjectXmlsDO)
        {
            var regProjectXml = this.regProjectXmlsRepository.FindForUpdate(this.accessContext.RegistrationId, gid, regProjectXmlsDO.Version);

            if (regProjectXml.Status != RegProjectXmlStatus.Draft)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { error = "Cannot update a project xml of type other than draft." }));
            }

            regProjectXml.SetXml(regProjectXmlsDO.Xml, this.procedureDomainService);

            this.unitOfWork.Save();

            return new XmlDO
            {
                ModifyDate = regProjectXml.ModifyDate,
                Version = regProjectXml.Version,
            };
        }

        [HttpDelete]
        [Route("projects/{gid:guid}")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.Delete))]
        public void Delete(Guid gid)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(this.accessContext.RegistrationId, gid);

            if (regProjectXml.Status != RegProjectXmlStatus.Draft &&
                regProjectXml.Status != RegProjectXmlStatus.Finalized)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { error = "Cannot delete a project xml of type other than draft or finalized." }));
            }

            this.regProjectXmlsRepository.Remove(regProjectXml);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("projects/{gid:guid}/finalize")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.Finalize))]
        public void MakeFinal(Guid gid, XmlDO regProjectXmlsDO)
        {
            var regProjectXml = this.regProjectXmlsRepository.FindForUpdate(this.accessContext.RegistrationId, gid, regProjectXmlsDO.Version);

            if (regProjectXml.Status != RegProjectXmlStatus.Draft)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { error = "Cannot finalize a project xml of type other than draft." }));
            }

            regProjectXml.MakeFinal();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("projects/{gid:guid}/definalize")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.MakeDraft))]
        public void MakeDraft(Guid gid)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(this.accessContext.RegistrationId, gid);

            if (regProjectXml.Status != RegProjectXmlStatus.Finalized)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { error = "Cannot definalize a project xml of type other than finalized." }));
            }

            regProjectXml.MakeDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("projects/{gid:guid}/submit")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegProjectXmls.Submit))]
        public object Submit(Guid gid)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(this.accessContext.RegistrationId, gid);

            if (regProjectXml.Status != RegProjectXmlStatus.Finalized)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { error = "Cannot submit a project xml of type other than finalized." }));
            }

            regProjectXml.MakeSubmitted();

            this.unitOfWork.Save();

            return new
            {
                hash = regProjectXml.Hash,
            };
        }

        [HttpPost]
        [Route("projects/register")]
        [Transaction]
        public object Register(SubmitDO submitDO)
        {
            if (!submitDO.IsValid())
            {
                throw new Exception("Invalid isun file signatures.");
            }

            var xml = submitDO.UnzipData();

            try
            {
                var regNumber = this.projectRegistrationService.RegisterRegistrationXml(this.accessContext.RegistrationId, xml, submitDO.Isun, submitDO.Signatures);

                this.actionLogger.LogAction(
                    typeof(ActionLogPortalGroups.RegProjectXmls.Register),
                    null,
                    null,
                    null,
                    null);

                return new
                {
                    regNumber = regNumber,
                };
            }
            catch (Exception exception)
            {
                if (exception.Message == "Procedure time limit expired")
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.ProcedureInactive }));
                }

                throw;
            }
        }
    }
}
