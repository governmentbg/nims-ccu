namespace Eumis.Documents.Contracts
{
    public class ContractRegistrationInfo
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public byte[] version { get; set; }
    }
}