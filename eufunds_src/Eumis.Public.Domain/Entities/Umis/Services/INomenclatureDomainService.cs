using System;

namespace Eumis.Public.Domain.Entities.Umis.Services
{
    public interface INomenclatureDomainService
    {
        int GetCompanyLegalTypeNomIdByGid(Guid gid);
        int GetCompanySizeTypeNomIdByGid(Guid gid);
        int GetCompanyTypeNomIdByGid(Guid gid);
        int GetKidCodeNomIdByCode(string code);
        int GetCountryNomIdByCode(string code);
        int GetSettlementNomIdByCode(string code);
    }
}
