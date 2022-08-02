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
    [RoutePrefix("api/monitoringReports/advancePayments")]
    public class MonitoringAdvancePaymentsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringAdvancePaymentsController(
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
        public IList<AdvancePaymentsReportItem> GetAdvancePaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetAdvancePaymentsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate);
        }

        [Route("export")]
        public HttpResponseMessage GetAdvancePaymentsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetAdvancePaymentsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Аванси (чл.131)");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Бенефициент";
            ws.Cell("C1").Value = "Стойност на верифицирани авансови плащания";
            ws.Cell("D1").Value = "Стойност на верифицирани разходи, покриващи авансовите плащания";
            ws.Cell("E1").Value = "Стойност на непокритите верифицирани авансови плащания";
            ws.Cell("F1").Value = "Стойност на сертифицираните авансови плащания";
            ws.Cell("G1").Value = "Стойност на сертифицираните разходи, покриващи авансовите плащания";
            ws.Cell("H1").Value = "Стойност на непокритите сертифицирани авансови плащания";
            ws.Cell("I1").Value = "ДС, в които са включени верифицираните авансови плащания";
            ws.Cell("J1").Value = "ДС, в които са включени разходите, покриващи верифицираните авансови плащания";

            var rngHeaders = ws.Range("A1", "J1");
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
                ws.Cell(row, 2).Value = data.BeneficiaryName;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 3).DataType = XLCellValues.Number;
                ws.Cell(row, 3).Value = data.VerifiedValue;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = data.VerifiedCosts;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = data.VerifiedNonCoveredValue;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.CertifiedPayment;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.CertAdvancePaymentExpenses;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.CertifiedNonCoveredValue;

                ws.Cell(row, 9).Style.NumberFormat.Format = "@";
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.CertReportsWithAdvencePayments;

                ws.Cell(row, 10).Style.NumberFormat.Format = "@";
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.CertReportsWithAdvancePaymentExpenses;

                row++;
            }

            ws.Columns(1, 3).Width = 25;
            ws.Columns(4, 10).Width = 30;
            ws.Columns(1, 10).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "advancePayments");
        }
    }
}
