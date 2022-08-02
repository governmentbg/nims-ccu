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
    [RoutePrefix("api/monitoringReports/contractReports")]
    public class MonitoringContractReportsReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringContractReportsReportsController(
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
        public IList<ContractsReportReportItem> GetContractReportsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            int? contractId = null,
            DateTime? toDate = null,
            ContractReportType? reportType = null,
            ContractReportStatus? reportStatus = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetContractReportsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                contractId,
                toDate,
                reportType,
                reportStatus);
        }

        [Route("export")]
        public HttpResponseMessage GetContractReportsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            int? contractId = null,
            DateTime? toDate = null,
            ContractReportType? reportType = null,
            ContractReportStatus? reportStatus = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetContractReportsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                contractId,
                toDate,
                reportType,
                reportStatus);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Отчети");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Range("A1", "A2").Merge();

            ws.Cell("B1").Value = "Приоритетна ос";
            ws.Range("B1", "B2").Merge();

            ws.Cell("C1").Value = "Процедура";
            ws.Range("C1", "C2").Merge();

            ws.Cell("D1").Value = "Рег. номер на договор";
            ws.Range("D1", "D2").Merge();

            ws.Cell("E1").Value = "Статус на договор";
            ws.Range("E1", "E2").Merge();

            ws.Cell("F1").Value = "Име на договор";
            ws.Range("F1", "F2").Merge();

            ws.Cell("G1").Value = "ЕИК";
            ws.Range("G1", "G2").Merge();

            ws.Cell("H1").Value = "Име на организацията";
            ws.Range("H1", "H2").Merge();

            ws.Cell("I1").Value = "Рег. номер на отчет";
            ws.Range("I1", "I2").Merge();

            ws.Cell("J1").Value = "Тип";
            ws.Range("J1", "J2").Merge();

            ws.Cell("K1").Value = "Начална дата";
            ws.Range("K1", "K2").Merge();

            ws.Cell("L1").Value = "Крайна дата";
            ws.Range("L1", "L2").Merge();

            ws.Cell("M1").Value = "Статус на отчет";
            ws.Range("M1", "M2").Merge();

            ws.Cell("N1").Value = "Дата на представяне";
            ws.Range("N1", "N2").Merge();

            ws.Cell("O1").Value = "Стойност на исканите средства";
            ws.Range("O1", "O2").Merge();

            ws.Cell("P1").Value = "Отчетени средства";
            ws.Range("P1", "T1").Merge();
            ws.Cell("P2").Value = "Общо";
            ws.Cell("Q2").Value = "БФП";
            ws.Cell("R2").Value = "Финансиране от ЕС";
            ws.Cell("S2").Value = "Финансиране от НФ";
            ws.Cell("T2").Value = "Собствено финансиране";

            ws.Cell("U1").Value = "Верифицирани средства";
            ws.Range("U1", "Y1").Merge();
            ws.Cell("U2").Value = "Общо";
            ws.Cell("V2").Value = "БФП";
            ws.Cell("W2").Value = "Финансиране от ЕС";
            ws.Cell("X2").Value = "Финансиране от НФ";
            ws.Cell("Y2").Value = "Собствено финансиране";

            ws.Cell("Z1").Value = "Верифицирани средства, покриващи авансово плащане по чл. 131 от Регл. EC 1303/2013";
            ws.Range("Z1", "AD1").Merge();
            ws.Cell("Z2").Value = "Общо";
            ws.Cell("AA2").Value = "БФП";
            ws.Cell("AB2").Value = "Финансиране от ЕС";
            ws.Cell("AC2").Value = "Финансиране от НФ";
            ws.Cell("AD2").Value = "Собствено финансиране";

            ws.Cell("AE1").Value = "Сертифицирани средства";
            ws.Range("AE1", "AI1").Merge();
            ws.Cell("AE2").Value = "Общо";
            ws.Cell("AF2").Value = "БФП";
            ws.Cell("AG2").Value = "Финансиране от ЕС";
            ws.Cell("AH2").Value = "Финансиране от НФ";
            ws.Cell("AI2").Value = "Собствено финансиране";

            ws.Cell("AJ1").Value = "Сертифицирани средства, покриващи авансово плащане по чл. 131 от Регл. EC 1303/2013";
            ws.Range("AJ1", "AN1").Merge();
            ws.Cell("AJ2").Value = "Общо";
            ws.Cell("AK2").Value = "БФП";
            ws.Cell("AL2").Value = "Финансиране от ЕС";
            ws.Cell("AM2").Value = "Финансиране от НФ";
            ws.Cell("AN2").Value = "Собствено финансиране";

            ws.Cell("AO1").Value = "Реално изплатени суми (без възстановени суми)";
            ws.Range("AO1", "AR1").Merge();
            ws.Cell("AO2").Value = "Общо";
            ws.Cell("AP2").Value = "БФП";
            ws.Cell("AQ2").Value = "Финансиране от ЕС";
            ws.Cell("AR2").Value = "Финансиране от НФ";

            ws.Cell("AS1").Value = "Номер на доклад по сертификация";
            ws.Range("AS1", "AS2").Merge();

            var rngHeaders = ws.Range("A1", "AS2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "AS2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.ProgrammePriority;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.Procedure;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.ContractRegNumber;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.ContractExecutionStatus?.GetEnumDescription();

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.ContractName;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.CompanyUin;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.CompanyName;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.RegNumber;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.ReportType?.GetEnumDescription();

                ws.Cell(row, 11).Style.NumberFormat.Format = "@";
                ws.Cell(row, 11).DataType = XLCellValues.DateTime;
                ws.Cell(row, 11).Value = data.DateFrom?.ToString("dd.MM.yyyy");

                ws.Cell(row, 12).Style.NumberFormat.Format = "@";
                ws.Cell(row, 12).DataType = XLCellValues.DateTime;
                ws.Cell(row, 12).Value = data.DateTo?.ToString("dd.MM.yyyy");

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 13).DataType = XLCellValues.Text;
                ws.Cell(row, 13).Value = data.Status?.GetEnumDescription();

                ws.Cell(row, 14).Style.NumberFormat.Format = "@";
                ws.Cell(row, 14).DataType = XLCellValues.DateTime;
                ws.Cell(row, 14).Value = data.SubmitDate?.ToString("dd.MM.yyyy");

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = data.RequestedAmount;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.ReportedTotalAmount;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.ReportedBfpAmount;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.ReportedEuAmount;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = data.ReportedBgAmount;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.ReportedSelfAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.VerifiedTotalAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.VerifiedBfpAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.VerifiedEuAmount;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = data.VerifiedBgAmount;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = data.VerifiedSelfAmount;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 26).DataType = XLCellValues.Number;
                ws.Cell(row, 26).Value = data.VerifiedTotalAdvancePaymentAmount;

                ws.Cell(row, 27).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 27).DataType = XLCellValues.Number;
                ws.Cell(row, 27).Value = data.VerifiedBfpAdvancePaymentAmount;

                ws.Cell(row, 28).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 28).DataType = XLCellValues.Number;
                ws.Cell(row, 28).Value = data.VerifiedEuAdvancePaymentAmount;

                ws.Cell(row, 29).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 29).DataType = XLCellValues.Number;
                ws.Cell(row, 29).Value = data.VerifiedBgAdvancePaymentAmount;

                ws.Cell(row, 30).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 30).DataType = XLCellValues.Number;
                ws.Cell(row, 30).Value = data.VerifiedSelfAdvancePaymentAmount;

                ws.Cell(row, 31).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 31).DataType = XLCellValues.Number;
                ws.Cell(row, 31).Value = data.CertTotalAmount;

                ws.Cell(row, 32).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 32).DataType = XLCellValues.Number;
                ws.Cell(row, 32).Value = data.CertBfpAmount;

                ws.Cell(row, 33).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 33).DataType = XLCellValues.Number;
                ws.Cell(row, 33).Value = data.CertEuAmount;

                ws.Cell(row, 34).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 34).DataType = XLCellValues.Number;
                ws.Cell(row, 34).Value = data.CertBgAmount;

                ws.Cell(row, 35).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 35).DataType = XLCellValues.Number;
                ws.Cell(row, 35).Value = data.CertSelfAmount;

                ws.Cell(row, 36).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 36).DataType = XLCellValues.Number;
                ws.Cell(row, 36).Value = data.CertTotalAdvancePaymentAmount;

                ws.Cell(row, 37).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 37).DataType = XLCellValues.Number;
                ws.Cell(row, 37).Value = data.CertBfpAdvancePaymentAmount;

                ws.Cell(row, 38).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 38).DataType = XLCellValues.Number;
                ws.Cell(row, 38).Value = data.CertEuAdvancePaymentAmount;

                ws.Cell(row, 39).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 39).DataType = XLCellValues.Number;
                ws.Cell(row, 39).Value = data.CertBgAdvancePaymentAmount;

                ws.Cell(row, 40).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 40).DataType = XLCellValues.Number;
                ws.Cell(row, 40).Value = data.CertSelfAdvancePaymentAmount;

                ws.Cell(row, 41).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 41).DataType = XLCellValues.Number;
                ws.Cell(row, 41).Value = data.PaidTotalAmount;

                ws.Cell(row, 42).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 42).DataType = XLCellValues.Number;
                ws.Cell(row, 42).Value = data.PaidBfpAmount;

                ws.Cell(row, 43).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 43).DataType = XLCellValues.Number;
                ws.Cell(row, 43).Value = data.PaidEuAmount;

                ws.Cell(row, 44).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 44).DataType = XLCellValues.Number;
                ws.Cell(row, 44).Value = data.PaidBgAmount;

                ws.Cell(row, 45).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 45).DataType = XLCellValues.Text;
                ws.Cell(row, 45).Value = data.CertReportNumber;

                row++;
            }

            ws.Columns(1, 3).Width = 40;
            ws.Columns(1, 3).Style.Alignment.SetWrapText();

            ws.Columns(4, 5).AdjustToContents();

            ws.Column(6).Width = 40;
            ws.Column(6).Style.Alignment.SetWrapText();

            ws.Column(7).AdjustToContents();

            ws.Column(8).Width = 40;
            ws.Column(8).Style.Alignment.SetWrapText();

            ws.Columns(9, 14).AdjustToContents();

            ws.Column(15).Width = 16;
            ws.Column(15).Style.Alignment.SetWrapText();

            ws.Columns(16, 44).Width = 16;
            ws.Columns(16, 44).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "contract_reports");
        }
    }
}
