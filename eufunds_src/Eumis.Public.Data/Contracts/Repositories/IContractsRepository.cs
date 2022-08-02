using Eumis.Public.Data.Contracts.ViewObjects;

namespace Eumis.Public.Data.Contracts.Repositories
{
    public interface IContractsRepository
    {
        ContractBasicDataVO GetContractBasicData(int contractId, bool isHistoric);

        ContractActivitiesVO GetContractActivities(int contractId, bool isHistoric);

        ContractProcurementsVO GetContractProcurements(int contractId, bool isHistoric);

        ContractParticipantsVO GetContractParticipants(int contractId, bool isHistoric);

        ContractFinancialInformationVO GetContractFinancialInformation(int contractId, bool isHistoric);

        ContractIndicatorsVO GetContractIndicators(int contractId, bool isHistoric);
    }
}
