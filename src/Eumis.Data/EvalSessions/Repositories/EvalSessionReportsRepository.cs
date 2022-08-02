using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionReportsRepository : AggregateRepository<EvalSessionReport>, IEvalSessionReportsRepository
    {
        public EvalSessionReportsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<EvalSessionReport, object>>[] Includes
        {
            get
            {
                return new Expression<Func<EvalSessionReport, object>>[]
                {
                    p => p.Projects.Select(t => t.ProjectKidCode),
                    p => p.Projects.Select(t => t.CompanyKidCode),
                    p => p.Projects.Select(t => t.Partners.Select(tp => tp.PartnerLegalType)),
                };
            }
        }

        public IList<EvalSessionReportVO> GetEvalSessionReports(int evalSessionId)
        {
            return (from esr in this.unitOfWork.DbContext.Set<EvalSessionReport>()
                    where esr.EvalSessionId == evalSessionId
                    orderby esr.CreateDate descending
                    select new EvalSessionReportVO
                    {
                        EvalSessionId = esr.EvalSessionId,
                        EvalSessionReportId = esr.EvalSessionReportId,
                        RegNumber = esr.RegNumber,
                        Type = esr.Type,
                        Description = esr.Description,
                        IsDeleted = esr.IsDeleted,
                        IsDeletedNote = esr.IsDeletedNote,
                        CreateDate = esr.CreateDate,
                    }).ToList();
        }
    }
}
