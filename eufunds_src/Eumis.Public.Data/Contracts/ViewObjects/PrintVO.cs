namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class PrintVO
    {
        public ContractBasicDataVO ContractBasicData { get; set; }

        public ContractActivitiesVO ContractActivities { get; set; }

        public ContractProcurementsVO ContractProcurements { get; set; }

        public ContractParticipantsVO ContractParticipants { get; set; }

        public ContractFinancialInformationVO ContractFinancialInformation { get; set; }

        public ContractIndicatorsVO ContractIndicators { get; set; }
    }
}
