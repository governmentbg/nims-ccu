using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class RegixCommunicator : IRegixCommunicator
    {
        public ContractCompany GetCompany(string accessToken, string procedureCode, string uin, string uinType)
        {
            return RegixApi.GetCompany(accessToken, procedureCode, uin, uinType).ToObject<ContractCompany>();
        }
    }
}
