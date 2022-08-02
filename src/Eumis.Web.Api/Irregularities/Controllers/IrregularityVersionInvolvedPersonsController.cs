using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularityVersions/{versionId:int}/involvedPersons")]
    public class IrregularityVersionInvolvedPersonsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularityVersionsRepository irregularityVersionsRepository;
        private IIrregularityVersionService irregularityVersionService;

        public IrregularityVersionInvolvedPersonsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularityVersionsRepository irregularityVersionsRepository,
            IIrregularityVersionService irregularityVersionService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularityVersionsRepository = irregularityVersionsRepository;
            this.irregularityVersionService = irregularityVersionService;
        }

        [Route("")]
        public IList<IrregularityInvolvedPersonVO> GetVersionInvolvedPersons(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.View, versionId);

            return this.irregularityVersionsRepository.GetInvolvedPersons(versionId);
        }

        [Route("{personId:int}")]
        public IrregularityVersionInvolvedPersionDO GetVersionInvolvedPerson(int versionId, int personId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.View, versionId);

            var version = this.irregularityVersionsRepository.Find(versionId);

            var person = version.GetInvolvedPerson(personId);

            return new IrregularityVersionInvolvedPersionDO(person, version.Status, version.Version);
        }

        [HttpGet]
        [Route("new")]
        public IrregularityVersionInvolvedPersionDO NewVersionInvolvedPerson(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            var version = this.irregularityVersionsRepository.Find(versionId);

            return new IrregularityVersionInvolvedPersionDO(versionId, version.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.InvolvedPersons.Create), IdParam = "versionId")]
        public object AddVersionInvolvedPerson(int versionId, IrregularityVersionInvolvedPersionDO person)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot add new involved person.");
            }

            var version = this.irregularityVersionsRepository.FindForUpdate(versionId, person.Version);
            IrregularityVersionInvolvedPerson newPerson = null;

            switch (person.LegalType.Value)
            {
                case InvolvedPersonLegalType.Person:
                    newPerson = version.AddInvolvedPerson(
                        person.Uin,
                        person.UinType.Value,
                        person.UndisclosureMotives,
                        person.FirstName,
                        person.MiddleName,
                        person.LastName,
                        person.CountryId,
                        person.SettlementId,
                        person.PostCode,
                        person.Street,
                        person.Address);
                    break;
                case InvolvedPersonLegalType.LegalPerson:
                    newPerson = version.AddInvolvedLegalPerson(
                        person.Uin,
                        person.UinType.Value,
                        person.UndisclosureMotives,
                        person.CompanyName,
                        person.TradeName,
                        person.HoldingName,
                        person.CountryId,
                        person.SettlementId,
                        person.PostCode,
                        person.Street,
                        person.Address);
                    break;
            }

            this.unitOfWork.Save();

            return new { VersionId = newPerson.IrregularityVersionId, PersonId = newPerson.IrregularityVersionInvolvedPersonId };
        }

        [HttpPut]
        [Route("{personId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.InvolvedPersons.Edit), IdParam = "versionId", ChildIdParam = "personId")]
        public void UpdateVersionInvolvedPerson(int versionId, int personId, IrregularityVersionInvolvedPersionDO person)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot edit involved person.");
            }

            var version = this.irregularityVersionsRepository.FindForUpdate(versionId, person.Version);

            switch (person.LegalType.Value)
            {
                case InvolvedPersonLegalType.Person:
                    version.UpdateInvolvedPerson(
                        personId,
                        person.Uin,
                        person.UinType.Value,
                        person.UndisclosureMotives,
                        person.FirstName,
                        person.MiddleName,
                        person.LastName,
                        person.CountryId,
                        person.SettlementId,
                        person.PostCode,
                        person.Street,
                        person.Address);
                    break;
                case InvolvedPersonLegalType.LegalPerson:
                    version.UpdateInvolvedLegalPerson(
                        personId,
                        person.Uin,
                        person.UinType.Value,
                        person.UndisclosureMotives,
                        person.CompanyName,
                        person.TradeName,
                        person.HoldingName,
                        person.CountryId,
                        person.SettlementId,
                        person.PostCode,
                        person.Street,
                        person.Address);
                    break;
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{personId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.InvolvedPersons.Delete), IdParam = "versionId", ChildIdParam = "personId")]
        public void DeleteVersionInvolvedPerson(int versionId, int personId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot delete involved person.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, vers);

            irrVersion.RemoveInvolvedPerson(personId);

            this.unitOfWork.Save();
        }
    }
}
