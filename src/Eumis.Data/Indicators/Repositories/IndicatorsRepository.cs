using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Indicators;
using Eumis.Domain.Measures;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Indicators.Repositories
{
    internal class IndicatorsRepository : AggregateRepository<Indicator>, IIndicatorsRepository
    {
        public IndicatorsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<Indicator> FindAll(int[] ids)
        {
            return this.Set()
                .Where(e => ids.Contains(e.IndicatorId))
                .ToList();
        }

        public IList<IndicatorsVO> GetIndicators(int[] programmeIds)
        {
            return (from i in this.unitOfWork.DbContext.Set<Indicator>()
                    join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on i.ProgrammeId equals p.MapNodeId
                    where programmeIds.Contains(p.MapNodeId)
                    select new IndicatorsVO
                    {
                        IndicatorId = i.IndicatorId,
                        Name = i.Name,
                        HasGenderDivision = i.HasGenderDivision,
                        MeasureName = m.Name,
                        ProgrammeName = p.ShortName,
                    })
                    .ToList();
        }

        public IList<string> CanDeleteIndicator(int indicatorId)
        {
            var errors = new List<string>();

            var linkedProcedures = (from i in this.unitOfWork.DbContext.Set<Indicator>()

                                    join pri in this.unitOfWork.DbContext.Set<ProcedureIndicator>() on i.IndicatorId equals pri.IndicatorId into g1
                                    from pri in g1.DefaultIfEmpty()

                                    join pr in this.unitOfWork.DbContext.Set<Procedure>() on pri.ProcedureId equals pr.ProcedureId into g2
                                    from pr in g2.DefaultIfEmpty()

                                    where i.IndicatorId == indicatorId && pr != null
                                    select pr).ToList();

            Func<string, string> errorBg = s => DomainEnumTexts.ResourceManager.GetString(s, new CultureInfo(SystemLocalization.Bg_BG));

            foreach (var linkedProcedure in linkedProcedures)
            {
                errors.Add($"Процедура с номер {linkedProcedure.Code}");
            }

            var resultErrors = new List<string>();

            if (errors.Any())
            {
                if (errors.Count == 1)
                {
                    resultErrors.Add($"Индикаторът не може да бъде изтрит, защото е свързан с елемент от оперативната карта - {errors.First()}");
                }
                else
                {
                    resultErrors.Add($"Индикаторът не може да бъде изтрит, защото е свързан със следните елементи от оперативната карта:\r\n\t{string.Join(";\r\n\t", errors.ToArray())}");
                }
            }

            return resultErrors;
        }

        public int GetIndicatorIdByGid(Guid gid)
        {
            return this.unitOfWork.DbContext.Set<Indicator>()
                .Where(i => i.Gid == gid)
                .Select(i => i.IndicatorId)
                .Single();
        }
    }
}
