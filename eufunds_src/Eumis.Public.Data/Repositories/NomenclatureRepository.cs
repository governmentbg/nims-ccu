using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Attributed;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;

namespace Eumis.Public.Model.Repositories
{
    internal class NomenclatureRepository : Repository, INomenclatureRepository
    {
        public NomenclatureRepository([WithKey(DbKey.Umis)]IUnitOfWork uow)
            : base(uow)
        {
        }

        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            return this.unitOfWork.DbContext.Set<CompanyType>();
        }

        public IEnumerable<CompanyLegalType> GetCompanyLegalTypes()
        {
            return this.unitOfWork.DbContext.Set<CompanyLegalType>();
        }

        public Select2VO GetProgramme(int id)
        {
            var mapNode = this.unitOfWork.DbContext.Set<Programme>()
                .SingleOrDefault(e => e.MapNodeId == id);

            return new Select2VO { id = mapNode.MapNodeId.ToString(), text = mapNode.TransName };
        }

        public IEnumerable<Select2VO> GetProgrammes(string term)
        {
            var predicate = PredicateBuilder.True<Programme>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            return this.unitOfWork.DbContext.Set<Programme>()
                .Where(predicate)
                .OrderBy(e => e.PortalOrderNum)
                .ToList()
                .Select(e => new Select2VO { id = e.MapNodeId.ToString(), text = e.TransName });
        }

        public Select2VO GetPriorityLine(int id)
        {
            var mapNode = this.unitOfWork.DbContext.Set<MapNode>().SingleOrDefault(e => e.MapNodeId == id);

            return new Select2VO { id = mapNode.MapNodeId.ToString(), text = mapNode.TransName };
        }

        public IEnumerable<Select2VO> GetPriorityLines(string term, int parentId)
        {
            var predicate = PredicateBuilder.True<MapNode>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            var priorityLines = from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                                join mn in this.unitOfWork.DbContext.Set<MapNode>() on mnr.MapNodeId equals mn.MapNodeId
                                where mnr.ParentMapNodeId == parentId
                                select mn;

            return priorityLines.Where(predicate).OrderBy(e => e.Name)
                .ToList()
                .Select(e => new Select2VO { id = e.MapNodeId.ToString(), text = e.TransName });
        }

        public Select2VO GetProcedure(int id)
        {
            var procedure = this.unitOfWork.DbContext.Set<Procedure>().SingleOrDefault(e => e.ProcedureId == id);

            return new Select2VO { id = procedure.ProcedureId.ToString(), text = procedure.TransName };
        }

        public IEnumerable<Select2VO> GetProcedures(string term, int parentId)
        {
            var predicate = PredicateBuilder.True<Procedure>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            predicate = predicate
                .And(e => e.ProcedureStatus != ProcedureStatus.Canceled)
                .And(е => е.ActivationDate.HasValue);

            var procedures = from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                             join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on mnr.MapNodeId equals ps.ProgrammePriorityId
                             join p in this.unitOfWork.DbContext.Set<Procedure>() on ps.ProcedureId equals p.ProcedureId
                             where mnr.MapNodeId == parentId
                             select p;

            return procedures.Where(predicate).OrderBy(e => e.Name)
                .ToList()
                .Distinct()
                .Select(e => new Select2VO { id = e.ProcedureId.ToString(), text = e.TransName });
        }

        public IEnumerable<Select2VO> GetProceduresWithProjectEvaluation(string term, int parentId)
        {
            var predicate = PredicateBuilder.True<Procedure>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            predicate = predicate.Or(e => e.Code.ToLower().Contains(term.ToLower()));

            var procedures = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(x => x.ProgrammeId == parentId)
                             join p in this.unitOfWork.DbContext.Set<Procedure>() on ps.ProcedureId equals p.ProcedureId
                             join aar in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(x => x.Status == EvalSessionResultStatus.Published) on p.ProcedureId equals aar.ProcedureId
                             select p;

            return procedures.Where(predicate).OrderBy(e => e.Code)
                .ToList()
                .Distinct()
                .Select(e => new Select2VO { id = e.ProcedureId.ToString(), text = $"({e.Code}) {e.TransName}" });
        }

        public IEnumerable<Select2VO> GetProceduresByProgramme(string term, int parentId)
        {
            var predicate = PredicateBuilder.True<Procedure>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            predicate = predicate.And(e => e.ProcedureStatus != ProcedureStatus.Canceled);

            var procedures = from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                             join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on mnr.MapNodeId equals ps.ProgrammeId
                             join p in this.unitOfWork.DbContext.Set<Procedure>() on ps.ProcedureId equals p.ProcedureId
                             where mnr.MapNodeId == parentId
                             select p;

            return procedures
                .Where(predicate)
                .OrderBy(e => e.Name)
                .ToList()
                .Distinct()
                .Select(e => new Select2VO { id = e.ProcedureId.ToString(), text = e.TransName });
        }

        public IEnumerable<Select2VO> GetProcedureResultTypes(string term, int parentId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionResult>()
                   .Where(x => x.ProcedureId == parentId && x.Status == EvalSessionResultStatus.Published)
                   .Select(x => x.Type)
                   .Distinct()
                   .ToList()
                   .Select(e => new Select2VO { id = ((int)e).ToString(), text = e.GetEnumDescription() });
        }

        public Select2VO GetSettlement(int id)
        {
            var settlement = this.unitOfWork.DbContext.Set<Settlement>()
                .SingleOrDefault(e => e.SettlementId == id);

            return new Select2VO { id = settlement.SettlementId.ToString(), text = settlement.TransName };
        }

        public IEnumerable<Select2VO> GetSettlements(string term)
        {
            var predicate = PredicateBuilder.True<Settlement>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            return this.unitOfWork.DbContext.Set<Settlement>()
                .Where(predicate)
                .ToList()
                .Select(e => new Select2VO { id = e.SettlementId.ToString(), text = e.TransName });
        }

        public Select2VO GetCompany(int id)
        {
            var company = this.unitOfWork.DbContext.Set<Company>()
                .SingleOrDefault(e => e.CompanyId == id);

            return new Select2VO { id = company.CompanyId.ToString(), text = company.TransName };
        }

        public IEnumerable<Select2VO> GetCompanies(string term)
        {
            var predicate = PredicateBuilder.True<Company>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    predicate = predicate.And(e => e.NameAlt.ToLower().Contains(term.ToLower()));
                }
                else
                {
                    predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
                }
            }

            return this.unitOfWork.DbContext.Set<Company>()
                .Where(predicate)
                .ToList()
                .Select(e => new Select2VO { id = e.CompanyId.ToString(), text = e.TransName });
        }

        public Select2VO GetErrandLegalAct(int id)
        {
            var errandLegalAct = this.unitOfWork.DbContext.Set<ErrandLegalAct>()
                .SingleOrDefault(e => e.ErrandLegalActId == id);

            return new Select2VO { id = errandLegalAct.ErrandLegalActId.ToString(), text = errandLegalAct.Name };
        }

        public IEnumerable<Select2VO> GetErrandLegalActs(string term)
        {
            var predicate = PredicateBuilder.True<ErrandLegalAct>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                predicate = predicate.And(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            return this.unitOfWork.DbContext.Set<ErrandLegalAct>()
                .Where(predicate)
                .ToList()
                .Select(e => new Select2VO { id = e.ErrandLegalActId.ToString(), text = e.Name });
        }
    }
}
