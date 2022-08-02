using System.Collections.Generic;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Companies.Repositories
{
    public interface ICompaniesRepository : IAggregateRepository<Company>
    {
        Company FindByUinOrDefault(string uin, UinType uinType);

        IList<CompaniesVO> GetCompanies(
            string name = null,
            UinType? uinType = null,
            string uin = null);

        CompanyPVO GetPortalCompany(string uin, UinType uinType);

        IList<string> CanDeleteCompany(int companyId);

        IList<string> CanCreateCompany(UinType uinType, string uin);

        bool IsUniqueUin(string uin, UinType uinType, int? companyId = null);

        CompanyLegalType GetLegalTypeByCommercialRegisterCode(string code);

        CompanyLegalType GetLegalTypeByBulstatRegisterCode(string code);

        IList<LocalActionGroupMunicipalitiesVO> GetLocalActionGroupMunicipalities(int companyId);

        CompanyPVO GetPortalCompany(Company company);
    }
}
