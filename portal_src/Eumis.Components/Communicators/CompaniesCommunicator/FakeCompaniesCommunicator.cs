using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class FakeCompaniesCommunicator : ICompaniesCommunicator
    {
        public ContractCompany GetCompany(string uin, string uinType)
        {
            return new ContractCompany()
            {
                
            };
        }

        public ContractEumisCompany GetEumisCompany(string uin, string uinType)
        {
            return new ContractEumisCompany()
            {

            };
        }
    }
}