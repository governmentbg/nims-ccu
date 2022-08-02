using Eumis.Public.Data.Procedures.ViewObjects;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Data.Procedures.Repositories
{
    public interface IProceduresRepository
    {
        PageVO<ProcedureVO> GetProcedures(
            int? settlementId = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            int offset = 0,
            int? limit = null);
    }
}
