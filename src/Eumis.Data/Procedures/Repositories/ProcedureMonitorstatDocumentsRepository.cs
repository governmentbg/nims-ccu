using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureMonitorstatDocumentsRepository : AggregateRepository<ProcedureMonitorstatDocument>, IProcedureMonitorstatDocumentsRepository
    {
        public ProcedureMonitorstatDocumentsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void CreateProcedureDcuments(int procedureId, int[] reportIds)
        {
            var reports = (from mr in this.unitOfWork.DbContext.Set<MonitorstatReport>().Where(x => reportIds.Contains(x.MonitorstatReportId))
                           join ms in this.unitOfWork.DbContext.Set<MonitorstatSurvey>() on mr.MonitorstatSurveyId equals ms.MonitorstatSurveyId

                           select new
                           {
                               mr.MonitorstatReportId,
                               mr.MonitorstatSurveyId,
                               ms.Year,
                           }).ToList();

            var currentDateTime = DateTime.Now;

            reports.ForEach(x => this.Add(new ProcedureMonitorstatDocument()
            {
                CreateDate = currentDateTime,
                ModifyDate = currentDateTime,
                ProcedureId = procedureId,
                MonitorstatReportId = x.MonitorstatReportId,
                MonitorstatSurveyId = x.MonitorstatSurveyId,
                Status = ProcedureMonitorstatDocumentStatus.Draft,
                Year = x.Year,
            }));
        }

        public IList<ProcedureMonitorstatDocumentVO> GetProcedureDocuments(int procedureId)
        {
            return (from pmd in this.unitOfWork.DbContext.Set<ProcedureMonitorstatDocument>().Where(x => x.ProcedureId == procedureId)
                    join mr in this.unitOfWork.DbContext.Set<MonitorstatReport>() on pmd.MonitorstatReportId equals mr.MonitorstatReportId
                    join ms in this.unitOfWork.DbContext.Set<MonitorstatSurvey>() on pmd.MonitorstatSurveyId equals ms.MonitorstatSurveyId
                    select new ProcedureMonitorstatDocumentVO
                    {
                        ProcedureMonitorstatDocumentId = pmd.ProcedureMonitorstatDocumentId,
                        Status = pmd.Status,
                        ReportId = mr.MonitorstatReportId,
                        ReportName = mr.Name,
                        ReportCode = mr.Code,
                        SurveyCode = ms.Code,
                        SurveyName = ms.Name,
                        Year = ms.Year,
                    })
                .OrderBy(x => x.ReportName)
                .ToList();
        }

        public IList<ReportDO> GetProcedureInquiryReports(int procedureId)
        {
            return (from md in this.Set().Where(x => x.ProcedureId == procedureId)
                    join sr in this.unitOfWork.DbContext.Set<MonitorstatReport>() on md.MonitorstatReportId equals sr.MonitorstatReportId
                    join sv in this.unitOfWork.DbContext.Set<MonitorstatSurvey>() on md.MonitorstatSurveyId equals sv.MonitorstatSurveyId
                    select new ReportDO
                    {
                        ReportCode = sr.Code,
                        SurveyCode = sv.Code,
                        Year = (int)sv.Year,
                    })
                    .ToList();
        }

        public IList<ProcedureMonitorstatDocumentVO> GetUnattachedReports(int procedureId, MonitorstatYear? year, int? surveyId)
        {
            var attachedReports = this.Set().Where(t => t.ProcedureId == procedureId).Select(x => x.MonitorstatReportId);
            var predicate = PredicateBuilder.True<MonitorstatSurvey>();
            if (year.HasValue)
            {
                predicate = predicate.And(x => x.Year == year.Value);
            }

            if (surveyId.HasValue)
            {
                predicate = predicate.And(x => x.MonitorstatSurveyId == surveyId.Value);
            }

            return (from ms in this.unitOfWork.DbContext.Set<MonitorstatSurvey>().Where(predicate)
                    join mr in this.unitOfWork.DbContext.Set<MonitorstatReport>().Where(t => !attachedReports.Contains(t.MonitorstatReportId)) on ms.MonitorstatSurveyId equals mr.MonitorstatSurveyId
                    select new ProcedureMonitorstatDocumentVO
                    {
                        SurveyCode = ms.Code,
                        SurveyName = ms.Name,
                        Year = ms.Year,
                        ReportCode = mr.Code,
                        ReportName = mr.Name,
                        ReportId = mr.MonitorstatReportId,
                    }).ToList();
        }
    }
}
