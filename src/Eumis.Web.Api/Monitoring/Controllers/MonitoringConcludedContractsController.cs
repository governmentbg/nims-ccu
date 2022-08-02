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
    [RoutePrefix("api/monitoringReports/concludedContracts")]
    public class MonitoringConcludedContractsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringConcludedContractsController(
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
        public IList<ConcludedContractsReportItem> GetConcludedContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetConcludedContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                uin);
        }

        [Route("export")]
        public HttpResponseMessage GetConcludedContractsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetConcludedContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                uin);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Сключени договори");

            // Headers
            ws.Cell("A1").Value = "Номер на договор с изпълнител";
            ws.Cell("B1").Value = "Име на изпълнител";
            ws.Cell("C1").Value = "ЕИК на изпълнител";
            ws.Cell("D1").Value = "Дата на сключване на Договора с изпълнителя";
            ws.Cell("E1").Value = "Номер от ИСУН на договор за БФП";
            ws.Cell("F1").Value = "Име на бенефициента";
            ws.Cell("G1").Value = "ЕИК на бенефициента";
            ws.Cell("H1").Value = "Тип на организацията";
            ws.Cell("I1").Value = "Вид на организацията";
            ws.Cell("J1").Value = "Предмет на процедурата за избор на изпълнител";
            ws.Cell("K1").Value = "Обект на процедурата";
            ws.Cell("L1").Value = "Приложим нормативен акт";
            ws.Cell("M1").Value = "Тип на процедурата";
            ws.Cell("N1").Value = "Процедурата е преминала през предварителен контрол от ОО";
            ws.Cell("O1").Value = "Процедурата е преминала през предварителен контрол от АОП";
            ws.Cell("P1").Value = "Обща сума на договора финансирана по проекта";
            ws.Cell("Q1").Value = "Сума на договора, финансирана по проекта без ДДС";
            ws.Cell("R1").Value = "Сума на ДДС, по договора, финансирана по проекта, ако е допустим разход";
            ws.Cell("S1").Value = "Подизпълнители";
            ws.Cell("T1").Value = "Членове на обединение";
            ws.Cell("U1").Value = "Обща сума на отчетените разходи по договора";
            ws.Cell("V1").Value = "БФП на отчетените разходи по договора";
            ws.Cell("W1").Value = "Собствено финансиране на отчетените разходи по договора";
            ws.Cell("X1").Value = "Обща сума на верифицираните разходи по договора БФП на верифицираните разходи по договора";
            ws.Cell("Y1").Value = "БФП на верифицираните разходи по договора";
            ws.Cell("Z1").Value = "Собствено финансиране на верифицираните разходи по договора";
            ws.Cell("AA1").Value = "Обща сума на сертифицираните разходи по договора";
            ws.Cell("AB1").Value = "БФП на сертифицираните разходи по договора";
            ws.Cell("AC1").Value = "Собствено финансиране на сертифицираните разходи по договора";
            ws.Cell("AD1").Value = "Абсолютна стойност на наложената финансова корекция (текуща) - Общо";
            ws.Cell("AE1").Value = "Абсолютна стойност на наложената финансова корекция (текуща) – БФП";
            ws.Cell("AF1").Value = "Абсолютна стойност на наложената финансова корекция (текуща) – ЕС";
            ws.Cell("AG1").Value = "Абсолютна стойност на наложената финансова корекция (текуща) – НФ";
            ws.Cell("AH1").Value = "Абсолютна стойност на наложената финансова корекция (текуща) – Собствено финансиране ";

            var rngHeaders = ws.Range("A1", "AH1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.ContractContractRegNum;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.CompanyName;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.CompanyUin;

                ws.Cell(row, 4).Style.NumberFormat.Format = "@";
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.ContractDate.HasValue ? data.ContractDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.ContractRegNum;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.ContractCompanyName;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.ContractCompanyUin;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.ContractCompanyType;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.ContractCompanyLegalType;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.ContractContractName;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.ErrandArea;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.ErrandLegalAct;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 13).DataType = XLCellValues.Text;
                ws.Cell(row, 13).Value = data.ErrandType;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 14).DataType = XLCellValues.Text;
                ws.Cell(row, 14).Value = null;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 15).DataType = XLCellValues.Text;
                ws.Cell(row, 15).Value = null;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.TotalFundedValue;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.VATAmountIfEligible;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.VATAmountIfEligible;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 19).DataType = XLCellValues.Text;
                ws.Cell(row, 19).Value = data.Subcontractors;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 20).DataType = XLCellValues.Text;
                ws.Cell(row, 20).Value = data.UnionMembers;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.ReportedTotalAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.ReportedBfpAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.ReportedSelfAmount;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = data.ApprovedTotalAmount;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = data.ApprovedBfpAmount;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 26).DataType = XLCellValues.Number;
                ws.Cell(row, 26).Value = data.ApprovedSelfAmount;

                ws.Cell(row, 27).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 27).DataType = XLCellValues.Number;
                ws.Cell(row, 27).Value = data.CertifiedTotalAmount;

                ws.Cell(row, 28).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 28).DataType = XLCellValues.Number;
                ws.Cell(row, 28).Value = data.CertifiedBfpAmount;

                ws.Cell(row, 29).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 29).DataType = XLCellValues.Number;
                ws.Cell(row, 29).Value = data.CertifiedSelfAmount;

                ws.Cell(row, 30).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 30).DataType = XLCellValues.Number;
                ws.Cell(row, 30).Value = data.FinancialCorrectionTotalAmount;

                ws.Cell(row, 31).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 31).DataType = XLCellValues.Number;
                ws.Cell(row, 31).Value = data.FinancialCorrectionBfpAmount;

                ws.Cell(row, 32).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 32).DataType = XLCellValues.Number;
                ws.Cell(row, 32).Value = data.FinancialCorrectionEuAmount;

                ws.Cell(row, 33).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 33).DataType = XLCellValues.Number;
                ws.Cell(row, 33).Value = data.FinancialCorrectionBgAmount;

                ws.Cell(row, 34).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 34).DataType = XLCellValues.Number;
                ws.Cell(row, 34).Value = data.FinancialCorrectionSelfAmount;

                row++;
            }

            ws.Column(1).Width = 40;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Column(2).Width = 50;
            ws.Columns(3, 5).Width = 40;
            ws.Columns(2, 5).Style.Alignment.SetWrapText();
            ws.Column(6).Width = 50;
            ws.Column(6).Style.Alignment.SetWrapText();
            ws.Columns(7, 9).AdjustToContents();
            ws.Column(10).Width = 50;
            ws.Column(10).Style.Alignment.SetWrapText();
            ws.Columns(11, 18).AdjustToContents();
            ws.Columns(19, 20).Width = 50;
            ws.Columns(21, 34).Width = 25;
            ws.Columns(21, 34).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "concludedContracts");
        }
    }
}
