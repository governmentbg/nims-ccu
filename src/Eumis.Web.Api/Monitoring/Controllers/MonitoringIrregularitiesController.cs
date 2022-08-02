using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/irregularities")]
    public class MonitoringIrregularitiesController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringIrregularitiesController(
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
        public IList<IrregularitiesReportItem> GetIrregularitiesReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetIrregularitiesReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate);
        }

        [Route("export")]
        public HttpResponseMessage GetIrregularitiesExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetIrregularitiesReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Нередности");

            // Headers
            ws.Cell("A1").Value = "Бенефициент";
            ws.Cell("B1").Value = "ЕИК";
            ws.Cell("C1").Value = "Тип на бенефициента";
            ws.Cell("D1").Value = "Вид на бенефициента";
            ws.Cell("E1").Value = "Адрес на бенефициента";
            ws.Cell("F1").Value = "Адрес за кореспонденция";
            ws.Cell("G1").Value = "Номер на договор за БФП ИСУН";
            ws.Cell("H1").Value = "Наименование на проекта";
            ws.Cell("I1").Value = "Сигнал за нередност";
            ws.Cell("J1").Value = "Дата на регистриране на сигнала";
            ws.Cell("K1").Value = "Статус";
            ws.Cell("L1").Value = "Номер на Нередност";
            ws.Cell("M1").Value = "Дата на нередността";
            ws.Cell("N1").Value = "Стойност на нередността";
            ws.Cell("O1").Value = "Финансови корекции";

            var rngHeaders = ws.Range("A1", "O1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.BeneficiaryName;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.BeneficiaryUin;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.BeneficiaryType;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.BeneficiaryLegalType;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.BeneficiarySeatAddress;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.BeneficiaryCorrespondenceAddress;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.ContractRegNum;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.Project;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.IrregularitySignal;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.IrregularitySignalRegDate.HasValue ? data.IrregularitySignalRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.Status.HasValue ? data.Status.Value.GetEnumDescription() : null;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.IrregularityRegNum;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 13).DataType = XLCellValues.Text;
                ws.Cell(row, 13).Value = data.IrregularityRegDate.HasValue ? data.IrregularityRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.IrregularityValue;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 15).DataType = XLCellValues.Text;
                ws.Cell(row, 15).Value = data.FinancialCorrections;

                row++;
            }

            ws.Columns(1, 8).Width = 50;
            ws.Columns(9, 15).Width = 25;
            ws.Columns(1, 15).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "irregularities");
        }
    }
}
