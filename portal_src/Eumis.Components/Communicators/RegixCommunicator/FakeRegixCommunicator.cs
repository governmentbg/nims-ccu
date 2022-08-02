using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class FakeRegixCommunicator : IRegixCommunicator
    {
        public ContractCompany GetCompany(string accessToken, string procedureCode, string uin, string uinType)
        {
            return new ContractCompany()
            {
            };
        }
    }
}
