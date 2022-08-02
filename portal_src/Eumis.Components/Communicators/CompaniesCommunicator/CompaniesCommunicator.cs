using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class CompaniesCommunicator : ICompaniesCommunicator
    {
        public ContractCompany GetCompany(string uin, string uinType)
        {
            return CompaniesApi.GetCompany(uin, uinType).ToObject<ContractCompany>();
        }

        public ContractEumisCompany GetEumisCompany(string uin, string uinType)
        {
            return CompaniesApi.GetEumisCompany(uin, uinType).ToObject<ContractEumisCompany>();
        }
    }
}
