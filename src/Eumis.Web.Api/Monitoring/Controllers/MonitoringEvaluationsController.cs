using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/evaluations")]
    public class MonitoringEvaluationsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringEvaluationsController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IMonitoringReportsRepository monitoringReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.monitoringReportsRepository = monitoringReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<EvaluationsReportItem> GetEvaluationsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetEvaluationsReport(
                programmeId,
                programmePriorityId,
                procedureId);
        }

        [Route("export")]
        public HttpResponseMessage GetEvaluationsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetEvaluationsReport(
                programmeId,
                programmePriorityId,
                procedureId);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Оценки");

            // Headers
            ws.Cell("A1").Value = "Номер от ИСУН на ПП";
            ws.Cell("B1").Value = "Кандидат";
            ws.Cell("C1").Value = "ЕИК на кандидат";
            ws.Cell("D1").Value = "Стойност на подаденото ПП Общо";
            ws.Cell("E1").Value = "Стойност на подаденото ПП БФП";
            ws.Cell("F1").Value = "Стойност на подаденото ПП СФ";
            ws.Cell("G1").Value = "Стойност на одобреното ПП Общо";
            ws.Cell("H1").Value = "Стойност на одобреното ПП БФП";
            ws.Cell("I1").Value = "Стойност на одобреното ПП СФ";
            ws.Cell("J1").Value = "Дата на сформиране на комисия";
            ws.Cell("K1").Value = "Дата на приключване на комисия";
            ws.Cell("L1").Value = "Продължителност на комуникация с кандидата (дни)";
            ws.Cell("M1").Value = "Брой комуникации с кандидата";

            var rngHeaders = ws.Range("A1", "M1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.ProjectRegNum;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.Company;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.CompanyUin;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = data.InitialProjectTotalAmount;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = data.InitialProjectBfpAmount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.InitialProjectSelfAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.ActualProjectTotalAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.ActualProjectBfpAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.ActualProjectSelfAmount;

                ws.Cell(row, 10).Style.NumberFormat.Format = "@";
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.CommitteeStartDate.HasValue ? data.CommitteeStartDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 11).Style.NumberFormat.Format = "@";
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.CommitteeEndDate.HasValue ? data.CommitteeEndDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = data.CommunicationsDuration;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.CommunicationsCount;

                row++;
            }

            ws.Column(1).Width = 25;
            ws.Column(2).Width = 50;
            ws.Columns(3, 13).Width = 25;
            ws.Columns(1, 13).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "evaluations");
        }
    }
}
