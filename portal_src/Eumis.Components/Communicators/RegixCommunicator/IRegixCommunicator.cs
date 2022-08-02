using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public interface IRegixCommunicator
    {
        ContractCompany GetCompany(string accessToken, string procedureCode, string uin, string uinType);
    }
}
