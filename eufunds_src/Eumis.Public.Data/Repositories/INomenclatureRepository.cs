using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Collections.Generic;

namespace Eumis.Public.Model.Repositories
{
    public interface INomenclatureRepository
    {
        IEnumerable<CompanyType> GetCompanyTypes();

        IEnumerable<CompanyLegalType> GetCompanyLegalTypes();

        Select2VO GetProgramme(int id);

        IEnumerable<Select2VO> GetProgrammes(string term);

        Select2VO GetPriorityLine(int id);

        IEnumerable<Select2VO> GetPriorityLines(string term, int parentId);

        Select2VO GetProcedure(int id);

        IEnumerable<Select2VO> GetProcedures(string term, int parentId);

        IEnumerable<Select2VO> GetProceduresWithProjectEvaluation(string term, int parentId);

        IEnumerable<Select2VO> GetProceduresByProgramme(string term, int parentId);

        IEnumerable<Select2VO> GetProcedureResultTypes(string term, int parentId);

        Select2VO GetSettlement(int id);

        IEnumerable<Select2VO> GetSettlements(string term);

        Select2VO GetCompany(int id);

        IEnumerable<Select2VO> GetCompanies(string term);

        Select2VO GetErrandLegalAct(int id);

        IEnumerable<Select2VO> GetErrandLegalActs(string term);
    }
}
