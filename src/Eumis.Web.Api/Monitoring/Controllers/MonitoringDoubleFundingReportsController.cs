using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/doubleFunding")]
    public class MonitoringDoubleFundingReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringDoubleFundingReportsController(
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
        public IList<DoubleFundingReportItem> GetDoubleFundingReport(string uin)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetDoubleFundingReport(uin);
        }

        [Route("export")]
        public HttpResponseMessage GetDoubleFundingExcelReport(string uin)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetDoubleFundingReport(uin);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Двойно финансиране");

            // Headers
            ws.Cell("A1").Value = "Бенефициент";
            ws.Range("A1", "B1").Merge();
            ws.Cell("A2").Value = "Име";
            ws.Cell("B2").Value = "Булстат";
            ws.Cell("C1").Value = "Партньор";
            ws.Range("C1", "D1").Merge();
            ws.Cell("C2").Value = "Име";
            ws.Cell("D2").Value = "Булстат";
            ws.Cell("E1").Value = "Номер на ДБФП";
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = "Обща стойност на ДБФП";
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = "БФП на ДБФП";
            ws.Range("G1", "G2").Merge();

            var rngHeaders = ws.Range("A1", "G2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "G2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;

            var groupedReport = report.GroupBy(i => i.ContractId);
            foreach (var data in groupedReport)
            {
                var firstItem = data.First();

                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = firstItem.BeneficiaryName;

                ws.Cell(row, 2).Style.NumberFormat.Format = "@";
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = firstItem.BeneficiaryUin;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = firstItem.ContractRegNum;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = firstItem.ContractTotalAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = firstItem.ContractBfpAmount;

                foreach (var item in data)
                {
                    ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(row, 3).DataType = XLCellValues.Text;
                    ws.Cell(row, 3).Value = item.ContractPartnerName;

                    ws.Cell(row, 4).Style.NumberFormat.Format = "@";
                    ws.Cell(row, 4).DataType = XLCellValues.Text;
                    ws.Cell(row, 4).Value = item.ContractPartnerUin;

                    row++;
                }
            }

            ws.Column(1).Width = 40;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Column(2).AdjustToContents();
            ws.Column(3).Width = 40;
            ws.Column(3).Style.Alignment.SetWrapText();
            ws.Columns(4, 5).AdjustToContents();
            ws.Columns(6, 7).Width = 20;
            ws.Columns(6, 7).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_double_funding", uin));
        }
    }
}
