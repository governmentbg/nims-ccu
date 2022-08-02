using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/financialCorrections")]
    public class MonitoringFinancialCorrectionsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringFinancialCorrectionsController(
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
        public IList<FinancialCorrectionsReportItem> GetFinancialCorrectionsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetFinancialCorrectionsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency);
        }

        [Route("export")]
        public HttpResponseMessage GetFinancialCorrectionsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetFinancialCorrectionsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Финансови корекции");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Cell("B1").Value = "Процедура";
            ws.Cell("C1").Value = "Рег. номер на договор";
            ws.Cell("D1").Value = "ЕИК на бенефициента";
            ws.Cell("E1").Value = "Име на организацията";
            ws.Cell("F1").Value = "Тип на организацията";
            ws.Cell("G1").Value = "Вид на организацията";
            ws.Cell("H1").Value = "Дата на налагане на ФК";
            ws.Cell("I1").Value = "Пореден номер на ФК";
            ws.Cell("J1").Value = "Договор с изпълнител (ако е приложимо)";
            ws.Cell("K1").Value = "Име на Изпълнител";
            ws.Cell("L1").Value = "ЕИК на Изпълнител";
            ws.Cell("M1").Value = "Процент от Договор с изпълнител-първоначална ФК (ако е приложимо)";
            ws.Cell("N1").Value = "Абсолютна стойност на наложената първоначална финансова корекция - Общо";
            ws.Cell("O1").Value = "Абсолютна стойност на наложената първоначална финансова корекция - БФП";
            ws.Cell("A1").Value = "Абсолютна стойност на наложената първоначална финансова корекция - ЕС";
            ws.Cell("P1").Value = "Абсолютна стойност на наложената първоначална финансова корекция - НФ";
            ws.Cell("Q1").Value = "Абсолютна стойност на наложената първоначална финансова корекция - Собствено финансиране";
            ws.Cell("R1").Value = "Основание за налагане на ФК - първоначална ФК";
            ws.Cell("S1").Value = "Други констатирани нарушения - първоначална ФК";
            ws.Cell("T1").Value = "Орган/ институция установила нарушението за ФК - първоначална ФК";
            ws.Cell("U1").Value = "Следва да се понесе от - първоначална ФК";
            ws.Cell("V1").Value = "Процент от Договор с изпълнител - текуща версия на ФК (ако е приложимо)";
            ws.Cell("W1").Value = "Абсолютна стойност на текуща версия на наложената финансова корекция - Общо";
            ws.Cell("X1").Value = "Абсолютна стойност на текуща версия на наложената финансова корекция - БФП";
            ws.Cell("Y1").Value = "Абсолютна стойност на текуща версия на наложената финансова корекция - ЕС";
            ws.Cell("Z1").Value = "Абсолютна стойност на текуща версия на наложената финансова корекция - НФ";
            ws.Cell("AA1").Value = "Абсолютна стойност на текуща версия на наложената финансова корекция - Собствено финансиране";
            ws.Cell("AB1").Value = "Причина за изменението";
            ws.Cell("AC1").Value = "Основание за налагане на ФК - текуща версия на ФК";
            ws.Cell("AD1").Value = "Други констатирани нарушения - текуща версия на ФК";
            ws.Cell("AE1").Value = "Орган/ институция установила нарушението за ФК - текуща версия на ФК";
            ws.Cell("AF1").Value = "Следва да се понесе от - текуща версия на ФК";
            ws.Cell("AG1").Value = "Нередност (ако е приложимо)";
            ws.Cell("AH1").Value = "Основа за налагане на финансовата корекция";
            ws.Cell("AI1").Value = "Стойност на извършените финансови корекции - Общо";
            ws.Cell("AJ1").Value = "Стойност на извършените финансови корекции - БФП";
            ws.Cell("AK1").Value = "Стойност на извършените финансови корекции - ЕС";
            ws.Cell("AL1").Value = "Стойност на извършените финансови корекции - НФ";
            ws.Cell("AM1").Value = "Стойност на извършените финансови корекции - Собствено финансиране";
            ws.Cell("AN1").Value = "Списък с искания за плащане, в които е включена ФК";
            ws.Cell("AO1").Value = "Списък с доклади по сертификация, в които е включена ФК";

            var rngHeaders = ws.Range("A1", "AO1");
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

                ws.Cell(row, 4).Style.NumberFormat.Format = "@";
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.CompanyUin;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.CompanyName;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.CompanyType;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.CompanyLegalType;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.CorrectionDate.ToString("dd.MM.yyyy");

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.CorrectionNum;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.ContractContractName;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.ContractContractCompanyName;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.ContractContractUin;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.InitialContractContractPercent;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.InitialAmountTotal;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = data.InitialAmountBfp;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.InitialAmountEu;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.InitialAmountBg;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.InitialAmountSelf;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 19).DataType = XLCellValues.Text;
                ws.Cell(row, 19).Value = data.InitialReason;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 20).DataType = XLCellValues.Text;
                ws.Cell(row, 20).Value = data.InitialViolations;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 21).DataType = XLCellValues.Text;
                ws.Cell(row, 21).Value = data.InitialViolationFoundBy.HasValue ? data.InitialViolationFoundBy.Value.GetEnumDescription() : null;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 22).DataType = XLCellValues.Text;
                ws.Cell(row, 22).Value = data.InitialBearer.HasValue ? data.InitialBearer.Value.GetEnumDescription() : null;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.CurrentContractContractPercent;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = data.CurrentAmountTotal;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = data.CurrentAmountBfp;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 26).DataType = XLCellValues.Number;
                ws.Cell(row, 26).Value = data.CurrentAmountEu;

                ws.Cell(row, 27).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 27).DataType = XLCellValues.Number;
                ws.Cell(row, 27).Value = data.CurrentAmountBg;

                ws.Cell(row, 28).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 28).DataType = XLCellValues.Number;
                ws.Cell(row, 28).Value = data.CurrentAmountSelf;

                ws.Cell(row, 29).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 29).DataType = XLCellValues.Text;
                ws.Cell(row, 29).Value = data.AmendmentReason.HasValue ? data.AmendmentReason.Value.GetEnumDescription() : null;

                ws.Cell(row, 30).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 30).DataType = XLCellValues.Text;
                ws.Cell(row, 30).Value = data.CurrentReason;

                ws.Cell(row, 31).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 31).DataType = XLCellValues.Text;
                ws.Cell(row, 31).Value = data.CurrentViolations;

                ws.Cell(row, 32).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 32).DataType = XLCellValues.Text;
                ws.Cell(row, 32).Value = data.CurrentViolationFoundBy.HasValue ? data.CurrentViolationFoundBy.Value.GetEnumDescription() : null;

                ws.Cell(row, 33).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 33).DataType = XLCellValues.Text;
                ws.Cell(row, 33).Value = data.CurrentBearer.HasValue ? data.CurrentBearer.Value.GetEnumDescription() : null;

                ws.Cell(row, 34).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 34).DataType = XLCellValues.Text;
                ws.Cell(row, 34).Value = data.Irregularity;

                ws.Cell(row, 35).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 35).DataType = XLCellValues.Number;
                ws.Cell(row, 35).Value = data.CorretionAmountTotal;

                ws.Cell(row, 36).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 36).DataType = XLCellValues.Number;
                ws.Cell(row, 36).Value = data.CorretionAmountBfp;

                ws.Cell(row, 37).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 37).DataType = XLCellValues.Number;
                ws.Cell(row, 37).Value = data.CorretionAmountEu;

                ws.Cell(row, 38).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 38).DataType = XLCellValues.Number;
                ws.Cell(row, 38).Value = data.CorretionAmountBg;

                ws.Cell(row, 39).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 39).DataType = XLCellValues.Number;
                ws.Cell(row, 39).Value = data.CorretionAmountSelf;

                ws.Cell(row, 40).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 40).DataType = XLCellValues.Text;
                ws.Cell(row, 40).Value = data.ContractReportPayments;

                ws.Cell(row, 41).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 41).DataType = XLCellValues.Text;
                ws.Cell(row, 41).Value = data.CertReports;

                row++;
            }

            ws.Columns(1, 12).Width = 50;
            ws.Columns(13, 41).Width = 25;
            ws.Columns(1, 41).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "financialCorrections");
        }
    }
}
