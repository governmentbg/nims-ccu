using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/pin")]
    public class MonitoringPinController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringPinController(
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
        public IList<PinReportItem> GetPinReport(
            int? programmeId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetPinReport(
                programmeId,
                fromDate,
                toDate,
                uin);
        }

        [Route("export")]
        public HttpResponseMessage GetPinExcelReport(
            int? programmeId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetPinReport(
                programmeId,
                fromDate,
                toDate,
                uin);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("ЕГН");

            // Headers
            ws.Cell("A1").Value = "ЕГН/ЛНЧ";
            ws.Cell("B1").Value = "Име";
            ws.Cell("C1").Value = "Дата";
            ws.Cell("D1").Value = "Отработени часове";
            ws.Cell("E1").Value = "Номер на договор от ИСУН";

            var rngHeaders = ws.Range("A1", "E1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.Uin;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.Name;

                ws.Cell(row, 3).Style.NumberFormat.Format = "@";
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.Date.HasValue ? data.Date.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = data.Hours;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.ContractRegNum;

                row++;
            }

            ws.Column(1).Width = 30;
            ws.Column(2).Width = 50;
            ws.Columns(3, 4).Width = 20;
            ws.Column(5).Width = 50;
            ws.Columns(1, 5).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "pin");
        }
    }
}
