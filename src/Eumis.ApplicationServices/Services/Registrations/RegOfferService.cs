using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Contracts;
using System;

namespace Eumis.ApplicationServices.Services.Registrations
{
    internal class RegOfferService : IRegOfferService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IContractProcurementsRepository contractProcurementsRepository;
        private readonly IRegOfferXmlsRepository regOfferXmlsRepository;
        private readonly IAccessContext accessContext;
        private readonly IContractsRepository contractsRepository;

        public RegOfferService(
            IUnitOfWork unitOfWork,
            IContractProcurementsRepository contractProcurementsRepository,
            IRegOfferXmlsRepository regOfferXmlsRepository,
            IAccessContext accessContext,
            IContractsRepository contractsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
        }

        public void SubmitRegistrationOffer(Guid offerGid, byte[] version)
        {
            var regOfferXml = this.regOfferXmlsRepository.FindForUpdate(this.accessContext.RegistrationId, offerGid, version);

            var contractDifferentiatedPosition = this.contractsRepository.GetContractDifferentiatedPosition(regOfferXml.ContractDifferentiatedPositionId);

            var procurement = this.contractProcurementsRepository.GetContractProcurement(contractDifferentiatedPosition.ContractProcurementPlanId);
            procurement.AssertNotExpired();

            regOfferXml.ChangeStatusToSubmitted();

            var count = this.regOfferXmlsRepository.GetSubmittedCount(contractDifferentiatedPosition.ContractDifferentiatedPositionId);
            contractDifferentiatedPosition.SubmittedOffersCount = count + 1;

            this.unitOfWork.Save();
        }

        public void WithdrawRegistrationOffer(Guid offerGid, byte[] version)
        {
            var regOfferXml = this.regOfferXmlsRepository.FindForUpdate(this.accessContext.RegistrationId, offerGid, version);

            var contractDifferentiatedPosition = this.contractsRepository.GetContractDifferentiatedPosition(regOfferXml.ContractDifferentiatedPositionId);

            var procurement = this.contractProcurementsRepository.GetContractProcurement(contractDifferentiatedPosition.ContractProcurementPlanId);
            procurement.AssertNotExpired();

            regOfferXml.ChangeStatusToWithdrawn();

            var count = this.regOfferXmlsRepository.GetSubmittedCount(contractDifferentiatedPosition.ContractDifferentiatedPositionId);
            contractDifferentiatedPosition.SubmittedOffersCount = count - 1;

            this.unitOfWork.Save();
        }
    }
}
