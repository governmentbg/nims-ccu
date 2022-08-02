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
    [RoutePrefix("api/monitoringReports/budgetLevels")]
    public class MonitoringBudgetLevelsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringBudgetLevelsController(
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
        public IList<BudgetLevelsReportItem> GetBudgetLevelsReport(
            BudgetLevel budgetLevel,
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

            return this.monitoringReportsRepository.GetBudgetLevelsReport(
                budgetLevel,
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
        public HttpResponseMessage GetBudgetLevelsExcelReport(
            BudgetLevel budgetLevel,
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

            var report = this.monitoringReportsRepository.GetBudgetLevelsReport(
                budgetLevel,
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

            var ws = workbook.Worksheets.Add("Бюджетни пера");

            // Headers
            ws.Cell("A1").Value = "Процедура";
            ws.Cell("B1").Value = "Първо ниво от бюджета";
            ws.Cell("C1").Value = "Второ ниво от бюджета";
            ws.Cell("D1").Value = "Договорени средства от текущия договор (Общо)";
            ws.Cell("E1").Value = "Договорени средства от текущия договор (БФП)";
            ws.Cell("F1").Value = "Договорени средства от текущия договор (Финансиране от ЕС)";
            ws.Cell("G1").Value = "Договорени средства от текущия договор (Финансиране от НФ)";
            ws.Cell("H1").Value = "Договорени средства от текущия договор (Собствено финансиране)";
            ws.Cell("I1").Value = "Отчетени от бенефициента средства (Общо)";
            ws.Cell("J1").Value = "Отчетени от бенефициента средства (БФП)";
            ws.Cell("K1").Value = "Отчетени от бенефициента средства (Финансиране от ЕС)";
            ws.Cell("L1").Value = "Отчетени от бенефициента средства (Финансиране от НФ)";
            ws.Cell("M1").Value = "Отчетени от бенефициента средства (Собствено финансиране)";
            ws.Cell("N1").Value = "Верифицирани средства (Общо)";
            ws.Cell("O1").Value = "Верифицирани средства (БФП)";
            ws.Cell("P1").Value = "Верифицирани средства (Финансиране от ЕС)";
            ws.Cell("Q1").Value = "Верифицирани средства (Финансиране от НФ)";
            ws.Cell("R1").Value = "Верифицирани средства (Собствено финансиране)";
            ws.Cell("S1").Value = "Сертифицирани средства (Общо)";
            ws.Cell("T1").Value = "Сертифицирани средства (БФП)";
            ws.Cell("U1").Value = "Сертифицирани средства (Финансиране от ЕС)";
            ws.Cell("V1").Value = "Сертифицирани средства (Финансиране от НФ)";
            ws.Cell("W1").Value = "Сертифицирани средства (Собствено финансиране)";

            var rngHeaders = ws.Range("A1", "W1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.ProcedureName;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.BudgetLevel1Name;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.BudgetLevel2Name;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = data.ContractedTotalAmount;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = data.ContractedBfpTotalAmount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.ContractedEuAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.ContractedBgAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.ContractedSelfAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.ReportedTotalAmount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = data.ReportedBfpTotalAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = data.ReportedEuAmount;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = data.ReportedBgAmount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.ReportedSelfAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.ApprovedTotalAmount;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = data.ApprovedBfpTotalAmount;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.ApprovedEuAmount;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.ApprovedBgAmount;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.ApprovedSelfAmount;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = data.CertifiedTotalAmount;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.CertifiedBfpTotalAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.CertifiedEuAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.CertifiedBgAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.CertifiedSelfAmount;

                row++;
            }

            ws.Column(1).Width = 50;
            ws.Columns(2, 23).Width = 25;
            ws.Columns(1, 23).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "budgetLevels");
        }
    }
}
