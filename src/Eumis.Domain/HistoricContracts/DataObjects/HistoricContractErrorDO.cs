namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractErrorDO
    {
        public HistoricContractErrorDO(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
