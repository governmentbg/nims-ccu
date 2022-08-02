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
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/financialExecution")]
    public class MonitoringFinancialExecutionReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringFinancialExecutionReportsController(
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

        [Route("table1")]
        public IList<FinancialExecutionTable1ReportItem> GetTable1Report(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetFinancialExecutionTable1Report(programmeId, date, programmePriorityId);
        }

        [Route("exportTable1")]
        public HttpResponseMessage GetTable1ExcelReport(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetFinancialExecutionTable1Report(programmeId, date, programmePriorityId);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Таблица 1");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Приоритетна ос";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Категория на региона";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Финансови средства, отпуснати по приоритетната ос въз основа на оперативната програма";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Кумулативни данни за финансовия напредък по оперативната програма";
            ws.Range("E1", "J1").Merge();
            ws.Cell("E2").Value = "Общ размер на допустимите разходи за операциите, избрани за подкрепа (в евро)";
            ws.Cell("F2").Value = "Дял от общия размер на отпуснатите средства, покрит с избраните операции (в процентно изражение)";
            ws.Cell("G2").Value = "Допустими публични разходи за операциите, избрани за подкрепа (в евро)";
            ws.Cell("H2").Value = "Общ размер на допустимите разходи, декларирани от бенефициентите пред управляващия орган";
            ws.Cell("I2").Value = "Дял от общия размер на отпуснатите средства, покрит с допустимите разходи, декларирани от бенефициентите (в процентно изражение)";
            ws.Cell("J2").Value = "Брой на избраните операции";

            var rngHeaders = ws.Range("A1", "J2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "J2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = item.ProgrammePriority;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.RegionCategory.GetEnumDescription();

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = item.BudgetBfpAmount;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = item.ContractedTotalAmount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = item.ContractedPercent / 100;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = item.ContractedBfpAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = item.ReportedTotalAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = item.ReportedPercent / 100;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = item.ContractsCount;

                row++;
            }

            ws.Columns(1, 4).Width = 30;
            ws.Column(5).Width = 18;
            ws.Column(6).Width = 50;
            ws.Columns(7, 8).Width = 20;
            ws.Column(9).Width = 16;
            ws.Column(10).Width = 12;
            ws.Columns(1, 10).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "financial_execution_table1");
        }

        [Route("table2")]
        public IList<FinancialExecutionTable2ReportItem> GetTable2Report(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetFinancialExecutionTable2Report(programmeId, date, programmePriorityId);
        }

        [Route("exportTable2")]
        public HttpResponseMessage GetTable2ExcelReport(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetFinancialExecutionTable2Report(programmeId, date, programmePriorityId);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Таблица 2");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Приоритетна ос";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Фонд";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Категория на региона";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Категоризация съобразно измеренията";
            ws.Range("E1", "L1").Merge();
            ws.Cell("E2").Value = "Област на интервенция";
            ws.Cell("F2").Value = "Форма на финансиране";
            ws.Cell("G2").Value = "Териториално измерение";
            ws.Cell("H2").Value = "Териториален механизъм за изпълнение";
            ws.Cell("I2").Value = "Измерение, свързано с тематичната цел";
            ws.Cell("J2").Value = "Вторична тема ЕСФ";
            ws.Cell("K2").Value = "Икономическо измерение";
            ws.Cell("L2").Value = "Измерение, свързано с местоположението";
            ws.Cell("M1").Value = "Финансови данни";
            ws.Range("M1", "P1").Merge();
            ws.Cell("M2").Value = "Общ размер на допустимите разходи за операциите, избрани за подкрепа (в евро)";
            ws.Cell("N2").Value = "Допустими публични разходи за операциите, избрани за подкрепа (в евро)";
            ws.Cell("O2").Value = "Общ размер на допустимите разходи, декларирани от бенефициентите пред управляващия орган";
            ws.Cell("P2").Value = "Брой на избраните операции";

            var rngHeaders = ws.Range("A1", "P2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "P2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = item.ProgrammePriority;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.RegionCategory.GetEnumDescription();

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = item.RegionCategory.GetEnumDescription();

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = item.InterventionField;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = item.FormOfFinance;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = item.TerritorialDimension;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = item.TerritorialDeliveryMechanism;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = item.ThematicObjective;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = item.ESFSecondaryTheme;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = item.EconomicDimension;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = item.NutsName;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = item.ContractTotalAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = item.ContractBfpAmount;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = item.ReportedTotalAmount;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = item.ContractsCount;
                row++;
            }

            ws.Columns(1, 2).Width = 30;
            ws.Column(3).Width = 12;
            ws.Column(4).Width = 26;
            ws.Columns(5, 12).Width = 50;
            ws.Columns(13, 15).Width = 20;
            ws.Column(16).Width = 12;
            ws.Columns(1, 16).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "financial_execution_table2");
        }

        [Route("table3")]
        public IList<FinancialExecutionTable3ReportItem> GetTable3Report(int programmeId, Year year)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetFinancialExecutionTable3Report(programmeId, year);
        }

        [Route("exportTable3")]
        public HttpResponseMessage GetTable3ExcelReport(int programmeId, Year year)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetFinancialExecutionTable3Report(programmeId, year);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Таблица 3");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Фонд";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Категория на региона";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Принос на Съюза - текуща финансова година";
            ws.Range("D1", "E1").Merge();
            ws.Cell("D2").Value = "януари — октомври";
            ws.Cell("E2").Value = "ноември — декември";
            ws.Cell("F1").Value = "Принос на Съюза - следваща финансова година";
            ws.Cell("F2").Value = "януари — декември";

            var rngHeaders = ws.Range("A1", "F2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "F2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = string.Empty;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.RegionCategory.GetEnumDescription();

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = item.CurrYearGroup1PaymentsEuAmount;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = item.CurrYearGroup2PaymentsEuAmount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = item.NextYearPaymentsEuAmount;

                row++;
            }

            ws.Column(1).Width = 30;
            ws.Column(2).Width = 12;
            ws.Column(3).Width = 26;
            ws.Columns(4, 6).Width = 22;
            ws.Columns(1, 6).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "financial_execution_table3");
        }
    }
}
