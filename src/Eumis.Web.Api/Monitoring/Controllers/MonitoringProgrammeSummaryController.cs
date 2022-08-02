using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/programmeSummary")]
    public class MonitoringProgrammeSummaryController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringProgrammeSummaryController(
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
        public IList<ProgrammeSummaryReportItem> GetProgrammeSummaryReport(
            GroupingLevel groupingLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetProgrammeSummaryReport(
                groupingLevel,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
        }

        [Route("export")]
        public HttpResponseMessage GetProgrammeSummaryExcelReport(
            GroupingLevel groupingLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetProgrammeSummaryReport(
                groupingLevel,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("ОП - обобщени");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Cell("B1").Value = "Приоритетна ос";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Договор";
            ws.Cell("E1").Value = "Фонд";
            ws.Cell("F1").Value = "Бюджет по програма – Общо (БФП)";
            ws.Cell("G1").Value = "Бюджет по програма – ЕС";
            ws.Cell("H1").Value = "Бюджет по програма – НФ";
            ws.Cell("I1").Value = "Договорени средства – Общо";
            ws.Cell("J1").Value = "Договорени средства – ЕС";
            ws.Cell("K1").Value = "Договорени средства – НФ";
            ws.Cell("L1").Value = "Договорени средства – Собствено финансиране";
            ws.Cell("M1").Value = "Отчетени средства – Общо";
            ws.Cell("N1").Value = "Отчетени средства – ЕС";
            ws.Cell("O1").Value = "Отчетени средства – НФ";
            ws.Cell("P1").Value = "Отчетени средства – Собствено финансиране";
            ws.Cell("Q1").Value = "Реално изплатени суми– Общо";
            ws.Cell("R1").Value = "Реално изплатени суми – ЕС";
            ws.Cell("S1").Value = "Реално изплатени суми – НФ";
            ws.Cell("T1").Value = "Реално изплатени суми – Собствено финансиране";
            ws.Cell("U1").Value = "Верифицирани разходи– Общо";
            ws.Cell("V1").Value = "Верифицирани разходи – ЕС";
            ws.Cell("W1").Value = "Верифицирани разходи – НФ";
            ws.Cell("X1").Value = "Верифицирани разходи – Собствено финансиране";
            ws.Cell("Y1").Value = "Сертифицирани разходи – Общо";
            ws.Cell("Z1").Value = "Сертифицирани разходи – ЕС";
            ws.Cell("AA1").Value = "Сертифицирани разходи – НФ";
            ws.Cell("AB1").Value = "Сертифицирани разходи – Собствено финансиране";

            var rngHeaders = ws.Range("A1", "AB1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.ProgrammeName;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.ProgrammePriorityName;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.ProcedureName;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.ContractRegNum;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.ContractRegNum;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.ProgrammeBudgetBfpTotalAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.ProgrammeBudgetEuAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.ProgrammeBudgetBgAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.ContractedTotalAmount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = data.ContractedEuAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = data.ContractedBgAmount;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = data.ContractedSelfAmount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.ReportedTotalAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.ReportedEuAmount;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = data.ReportedBgAmount;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.ReportedSelfAmount;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.ActuallyPaidTotalAmount;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.ActuallyPaidEuAmount;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = data.ActuallyPaidBgAmount;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.ActuallyPaidSelfAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.ApprovedTotalAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.ApprovedEuAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.ApprovedBgAmount;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = data.ApprovedSelfAmount;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = data.CertifiedTotalAmount;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 26).DataType = XLCellValues.Number;
                ws.Cell(row, 26).Value = data.CertifiedEuAmount;

                ws.Cell(row, 27).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 27).DataType = XLCellValues.Number;
                ws.Cell(row, 27).Value = data.CertifiedBgAmount;

                ws.Cell(row, 28).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 28).DataType = XLCellValues.Number;
                ws.Cell(row, 28).Value = data.CertifiedSelfAmount;

                row++;
            }

            ws.Columns(1, 5).Width = 40;
            ws.Columns(6, 28).Width = 25;
            ws.Columns(1, 28).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "programmeSummary");
        }
    }
}
