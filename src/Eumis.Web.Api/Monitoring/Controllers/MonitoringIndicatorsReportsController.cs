using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Contracts;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/indicators")]
    public class MonitoringIndicatorsReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringIndicatorsReportsController(
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
        public IList<IndicatorReportItemVO> GetIndicatorsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null,
            ContractExecutionStatus? contractExecutionStatus = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetIndicatorsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                toDate,
                contractExecutionStatus,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
        }

        [Route("export")]
        public HttpResponseMessage GetIndicatorsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null,
            ContractExecutionStatus? contractExecutionStatus = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetIndicatorsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                toDate,
                contractExecutionStatus,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Индикатори");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Cell("B1").Value = "Процедура";
            ws.Cell("C1").Value = "Номер на договор от ИСУН";
            ws.Cell("D1").Value = "Наименование на проекта";
            ws.Cell("E1").Value = "Място/места на изпълнение";
            ws.Cell("F1").Value = "Статус на договора";
            ws.Cell("G1").Value = "Дата на приключване/прекратяване";
            ws.Cell("H1").Value = "Бенефициент";
            ws.Cell("I1").Value = "ЕИК";
            ws.Cell("J1").Value = "Тип на организацията";
            ws.Cell("K1").Value = "Вид на организацията";
            ws.Cell("L1").Value = "Категория на предприятие";
            ws.Cell("M1").Value = "Име на индикатора";
            ws.Cell("N1").Value = "Тип на индикатора";
            ws.Cell("O1").Value = "Вид на индикатора";
            ws.Cell("P1").Value = "Мерна единица";
            ws.Cell("Q1").Value = "Базова стойност";
            ws.Cell("R1").Value = "Целева стойност";
            ws.Cell("S1").Value = "Отчетена стойност";
            ws.Cell("T1").Value = "Верифицирана стойност";

            var rngHeaders = ws.Range("A1", "T1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.Procedure;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.ContractRegNum;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.ContractName;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.NutsFullPathName;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.ContractExecutionStatus.HasValue ? data.ContractExecutionStatus.Value.GetEnumDescription() : null;

                ws.Cell(row, 7).Style.NumberFormat.Format = "@";
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.ContractEndTerminationDate.HasValue ? data.ContractEndTerminationDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.CompanyName;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.CompanyUin;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.CompanyType;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.CompanyLegalType;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.CompanySizeType;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 13).DataType = XLCellValues.Text;
                ws.Cell(row, 13).Value = data.Name;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 14).DataType = XLCellValues.Text;
                ws.Cell(row, 14).Value = data.Name;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 15).DataType = XLCellValues.Text;
                ws.Cell(row, 15).Value = data.Name;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 16).DataType = XLCellValues.Text;
                ws.Cell(row, 16).Value = data.Measure;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.BaseTotalValue;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.TargetTotalValue;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = data.ReportedTotalValue;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.ApprovedTotalValue;

                row++;
            }

            ws.Columns(1, 2).Width = 50;
            ws.Column(3).Width = 30;
            ws.Columns(4, 5).Width = 50;
            ws.Columns(1, 5).Style.Alignment.SetWrapText();
            ws.Columns(6, 7).AdjustToContents();
            ws.Column(8).Width = 50;
            ws.Column(8).Style.Alignment.SetWrapText();
            ws.Columns(9, 12).AdjustToContents();
            ws.Column(13).Width = 50;
            ws.Column(13).Style.Alignment.SetWrapText();
            ws.Columns(14, 20).AdjustToContents();

            return this.Request.CreateXmlResponse(workbook, "indicators");
        }
    }
}
