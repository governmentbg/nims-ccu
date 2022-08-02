using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/expenseTypes")]
    public class MonitoringExpenseTypesController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringExpenseTypesController(
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
        public IList<ExpenseTypesReportItem> GetExpenseTypesReport(
            int? programmeId = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetExpenseTypesReport(programmeId, toDate);
        }

        [Route("export")]
        public HttpResponseMessage GetExpenseTypesExcelReport(int? programmeId = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetExpenseTypesReport(programmeId, toDate);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Типове разходи");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Тип на разхода";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Фонд";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Верифицирани разходи";
            ws.Range("D1", "F1").Merge();
            ws.Cell("D2").Value = "БФП";
            ws.Cell("E2").Value = "Собствен принос";
            ws.Cell("F2").Value = "Общо";
            ws.Cell("G1").Value = "Сертифицирани разходи";
            ws.Range("M1", "O1").Merge();
            ws.Cell("M2").Value = "БФП";
            ws.Cell("N2").Value = "Собствен принос";
            ws.Cell("O2").Value = "Общо";

            var rngHeaders = ws.Range("A1", "O1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.ExpenseType;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.ExpenseType;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = data.ApprovedBfpAmount;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = data.ApprovedSelfAmount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.ApprovedTotalAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.CertifiedBfpAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.CertifiedSelfAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.CertifiedTotalAmount;

                row++;
            }

            ws.Columns(1, 9).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "expenseTypes");
        }
    }
}
