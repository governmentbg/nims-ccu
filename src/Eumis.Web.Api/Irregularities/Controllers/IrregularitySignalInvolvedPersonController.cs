using System.Collections.Generic;
using System.Web.Http;
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
    [RoutePrefix("api/irregularitySignals/{signalId:int}/involvedPersons")]
    public class IrregularitySignalInvolvedPersonController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularitySignalsRepository irregularitySignalsRepository;

        public IrregularitySignalInvolvedPersonController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularitySignalsRepository irregularitySignalsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
        }

        [Route("")]
        public IList<IrregularityInvolvedPersonVO> GetSignalInvolvedPersons(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            return this.irregularitySignalsRepository.GetInvolvedPersons(signalId);
        }

        [Route("{personId:int}")]
        public IrregularitySignalInvolvedPersionDO GetSignalInvolvedPerson(int signalId, int personId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            var signal = this.irregularitySignalsRepository.Find(signalId);

            var person = signal.GetInvolvedPerson(personId);

            return new IrregularitySignalInvolvedPersionDO(person, signal.Version);
        }

        [HttpGet]
        [Route("new")]
        public IrregularitySignalInvolvedPersionDO NewSignalInvolvedPerson(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.Find(signalId);

            return new IrregularitySignalInvolvedPersionDO(signalId, signal.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.InvolvedPersons.Create), IdParam = "signalId")]
        public object AddSignalInvolvedPerson(int signalId, IrregularitySignalInvolvedPersionDO person)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, person.Version);
            IrregularitySignalInvolvedPerson newPerson = null;

            switch (person.LegalType.Value)
            {
                case InvolvedPersonLegalType.Person:
                    newPerson = signal.AddInvolvedPerson(
                        person.Uin,
                        person.UinType.Value,
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
                    newPerson = signal.AddInvolvedLegalPerson(
                        person.Uin,
                        person.UinType.Value,
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

            return new { SignalId = newPerson.IrregularitySignalId, PersonId = newPerson.IrregularitySignalInvolvedPersonId };
        }

        [HttpPut]
        [Route("{personId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.InvolvedPersons.Edit), IdParam = "signalId", ChildIdParam = "personId")]
        public void UpdateSignalInvolvedPerson(int signalId, int personId, IrregularitySignalInvolvedPersionDO person)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, person.Version);

            switch (person.LegalType.Value)
            {
                case InvolvedPersonLegalType.Person:
                    signal.UpdateInvolvedPerson(
                        personId,
                        person.Uin,
                        person.UinType.Value,
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
                    signal.UpdateInvolvedLegalPerson(
                        personId,
                        person.Uin,
                        person.UinType.Value,
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
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.InvolvedPersons.Delete), IdParam = "signalId", ChildIdParam = "personId")]
        public void DeleteSignalInvolvedPerson(int signalId, int personId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            signal.RemoveInvolvedPerson(personId);

            this.unitOfWork.Save();
        }
    }
}
