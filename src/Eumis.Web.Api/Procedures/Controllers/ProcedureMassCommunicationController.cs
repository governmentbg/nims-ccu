using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;

using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedureMassCommunications")]
    public class ProcedureMassCommunicationController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IProcedureMassCommunicationsRepository procedureMassCommunicationsRespository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private ICountersRepository countersRepository;

        public ProcedureMassCommunicationController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IProcedureMassCommunicationsRepository procedureMassCommunicationsRespository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            ICountersRepository countersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.procedureMassCommunicationsRespository = procedureMassCommunicationsRespository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.countersRepository = countersRepository;
        }

        [Route("")]
        public IList<ProcedureMassCommunicationVO> GetProcedureMassCommunications()
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            return this.procedureMassCommunicationsRespository.GetProcedureMassCommunications(programmeIds);
        }

        [Route("{procedureMassCommunicationId:int}")]
        public ProcedureMassCommunicationDO GetProcedureMassCommunication(int procedureMassCommunicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, procedureMassCommunicationId);

            var procedureMassCommunication = this.procedureMassCommunicationsRespository.Find(procedureMassCommunicationId);

            return new ProcedureMassCommunicationDO(procedureMassCommunication);
        }

        [Route("{procedureMassCommunicationId:int}/info")]
        public ProcedureMassCommunicationInfoVO GetProcedureMassCommunicationInfo(int procedureMassCommunicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, procedureMassCommunicationId);

            return this.procedureMassCommunicationsRespository.GetInfo(procedureMassCommunicationId);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureMassCommunicationDO NewProcedureMassCommunication()
        {
            return new ProcedureMassCommunicationDO()
            {
                Status = ProcedureMassCommunicationStatus.Draft,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Create))]
        public object ProcedureMassCommunication(ProcedureMassCommunicationDO communication)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Create, communication.ProcedureId.Value);

            var newCommunication = new Domain.Procedures.ProcedureMassCommunication(
                communication.ProgrammeId.Value,
                communication.ProcedureId.Value,
                communication.Subject,
                communication.Body);

            this.procedureMassCommunicationsRespository.Add(newCommunication);

            this.unitOfWork.Save();

            return new { newCommunication.ProcedureMassCommunicationId };
        }

        [HttpDelete]
        [Route("{procedureMassCommunicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Delete), IdParam = "procedureMassCommunicationId")]
        public void DeleteProcedureMassCommunication(int procedureMassCommunicationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, procedureMassCommunicationId);

            byte[] vers = Convert.FromBase64String(version);

            this.procedureMassCommunicationsRespository.DeleteProcedureMassCommunication(procedureMassCommunicationId, vers);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{procedureMassCommunicationId:int}/canDelete")]
        public ErrorsDO CanDeleteProcedureMassCommunication(int procedureMassCommunicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, procedureMassCommunicationId);

            var communication = this.procedureMassCommunicationsRespository.FindWithoutIncludes(procedureMassCommunicationId);
            var errorList = communication.CanDelete();

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{procedureMassCommunicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Edit), IdParam = "procedureMassCommunicationId")]
        public void UpdateProcedureMassCommunication(int procedureMassCommunicationId, ProcedureMassCommunicationDO communication)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, procedureMassCommunicationId);

            var massCommunication = this.procedureMassCommunicationsRespository.FindForUpdate(procedureMassCommunicationId, communication.Version);
            massCommunication.UpdateAttributes(
                communication.ProgrammeId.Value,
                communication.ProcedureId.Value,
                communication.Subject,
                communication.Body);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{procedureMassCommunicationId:int}/canSend")]
        public ErrorsDO CanSendMassCommunication(int procedureMassCommunicationId)
        {
            var massCommunication = this.procedureMassCommunicationsRespository.Find(procedureMassCommunicationId);

            return new ErrorsDO(massCommunication.CanSend());
        }

        [HttpPost]
        [Route("{procedureMassCommunicationId:int}/send")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Send), IdParam = "procedureMassCommunicationId")]
        public void SendMassCommunication(int procedureMassCommunicationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, procedureMassCommunicationId);

            byte[] vers = System.Convert.FromBase64String(version);

            var massCommunication = this.procedureMassCommunicationsRespository.FindForUpdate(procedureMassCommunicationId, vers);

            var xml = this.documentRestApiCommunicator.CreateContractCommunicationXml(new CommunicationPVO(massCommunication, ContractCommunicationType.Administrative));

            massCommunication.Recipients.ForEach(x =>
            {
                string newXml = xml.Replace(ContractCommunicationXml.CommunicationTemplateXmlKey, Guid.NewGuid().ToString());

                ContractCommunicationXml newCommunication = new ContractCommunicationXml(
                x.ContractId,
                ContractCommunicationType.Administrative,
                Source.AdministrativeAuthority,
                newXml);

                this.contractCommunicationXmlsRepository.Add(newCommunication);
                this.unitOfWork.Save();

                this.countersRepository.CreateContractCommunicationCounter(x.ContractId);

                var regNumber = this.countersRepository.GetNextContractCommunicationNumber(x.ContractId);
                newCommunication.Activate(regNumber);
            });

            massCommunication.Status = ProcedureMassCommunicationStatus.Sent;
            this.unitOfWork.Save();
        }
    }
}
