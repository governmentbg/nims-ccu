using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public interface ICompaniesCommunicator
    {
        //[RoutePrefix("api/companies")]
        //[AllowAnonymous]
        ContractCompany GetCompany(string uin, string uinType);

        ContractEumisCompany GetEumisCompany(string uin, string uinType);
    }
}