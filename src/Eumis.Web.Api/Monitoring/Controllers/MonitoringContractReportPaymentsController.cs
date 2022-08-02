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
    [RoutePrefix("api/monitoringReports/contractReportPayments")]
    public class MonitoringContractReportPaymentsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringContractReportPaymentsController(
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
        public IList<ContractReportPaymentsReportItem> GetContractReportPaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetContractReportPaymentsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency);
        }

        [Route("export")]
        public HttpResponseMessage GetContractReportPaymentsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetContractReportPaymentsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("ИП");

            // Headers
            ws.Cell("A1").Value = "Номер на договор от ИСУН";
            ws.Cell("B1").Value = "Пореден номер на ПОД";
            ws.Cell("C1").Value = "Пореден номер на ИП";
            ws.Cell("D1").Value = "Булстат";
            ws.Cell("E1").Value = "Име на Бенефициент";
            ws.Cell("F1").Value = "Дата на регистрация";
            ws.Cell("G1").Value = "Тип на ИП";
            ws.Cell("H1").Value = "Статус";
            ws.Cell("I1").Value = "Обща стойност на ИП (БФП)";
            ws.Cell("J1").Value = "Верифицирана от ООф сума (БФП)";
            ws.Cell("K1").Value = "Сума за плащане";
            ws.Cell("L1").Value = "Дата на крайна проверка";
            ws.Cell("M1").Value = "Сертифицирана сума";
            ws.Cell("N1").Value = "Изплатена сума";
            ws.Cell("O1").Value = "Дата на плащане";
            ws.Cell("P1").Value = "Възстановени суми";
            ws.Cell("Q1").Value = "Дата на възстановяване";

            var rngHeaders = ws.Range("A1", "Q1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.ContractRegNum;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.ReportNum;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.PaymentNum;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.CompanyUin;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.CompanyName;

                ws.Cell(row, 6).Style.NumberFormat.Format = "@";
                ws.Cell(row, 6).DataType = XLCellValues.Text;
                ws.Cell(row, 6).Value = data.RegDate.HasValue ? data.RegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 7).DataType = XLCellValues.Text;
                ws.Cell(row, 7).Value = data.PaymentType.HasValue ? data.PaymentType.Value.GetEnumDescription() : null;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.PaymentStatus.HasValue ? data.PaymentStatus.Value.GetEnumDescription() : null;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.PaymentTotalAmount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = data.PaymentApprovedAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = data.PaymentPaidAmount;

                ws.Cell(row, 12).Style.NumberFormat.Format = "@";
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.PaymentCheckDate.HasValue ? data.PaymentCheckDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.PaymentCertifiedAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.PaymentActuallyPaidAmount;

                ws.Cell(row, 15).Style.NumberFormat.Format = "@";
                ws.Cell(row, 15).DataType = XLCellValues.Text;
                ws.Cell(row, 15).Value = data.PaymentActuallyPaidDate.HasValue ? data.PaymentActuallyPaidDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.PaymentReimbursedAmount;

                ws.Cell(row, 17).Style.NumberFormat.Format = "@";
                ws.Cell(row, 17).DataType = XLCellValues.Text;
                ws.Cell(row, 17).Value = data.PaymentReimbursementDate.HasValue ? data.PaymentReimbursementDate.Value.ToString("dd.MM.yyyy") : null;

                row++;
            }

            ws.Columns(1, 4).Width = 25;
            ws.Column(5).Width = 50;
            ws.Columns(6, 17).Width = 25;
            ws.Columns(1, 17).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "reportPayments");
        }
    }
}
