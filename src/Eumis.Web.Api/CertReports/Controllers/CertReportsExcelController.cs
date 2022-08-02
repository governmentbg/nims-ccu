using ClosedXML.Excel;
using Eumis.ApplicationServices.Services.CertReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    public class CertReportsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportsRepository certReportsRepository;

        public CertReportsExcelController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportsRepository certReportsRepository)
        {
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
        }

        [HttpGet]
        [Route("api/certReports/excelExport")]
        public HttpResponseMessage GetCertReportExcelExports()
        {
            this.authorizer.AssertCanDo(CertReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            var certReports = this.certReportsRepository.GetCertReports(programmeIds);

            var workbook = this.GetWorkbook(certReports, "Доклади по сертификация");

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        [Route("api/certReportChecks/excelExport")]
        public HttpResponseMessage GetCertReportChecksExcelExport()
        {
            this.authorizer.AssertCanDo(CertReportCheckListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            var certReports = this.certReportsRepository.GetCertReportChecksCertReports(programmeIds);

            var workbook = this.GetWorkbook(certReports, "Проверки на ДС");

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<CertReportVO> certReports, string worksheetTitle)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add(worksheetTitle);

            // Headers
            ws.Cell("A1").Value = "Пореден номер";
            ws.Cell("B1").Value = "Версия";
            ws.Cell("C1").Value = "Номер на Доклад по сертификация";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Оперативна програма";
            ws.Cell("F1").Value = "Дата на регистрация";
            ws.Cell("G1").Value = "Верифицирана сума-БФП";
            ws.Cell("H1").Value = "Верифицирана сума-СФ";
            ws.Cell("I1").Value = "Сертифицирана сума-БФП";
            ws.Cell("J1").Value = "Сертифицирана сума-СФ";
            ws.Cell("K1").Value = "Дата на одобрение";
            ws.Cell("L1").Value = "Период от";
            ws.Cell("M1").Value = "Период до";
            ws.Cell("N1").Value = "Тип";

            var rngHeaders = ws.Range(1, 1, 1, 14);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 14).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReport in certReports)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReport.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "B").Value = certReport.OrderVersionNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReport.CertReportNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReport.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReport.ProgrammeName;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReport.RegDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReport.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReport.ApprovedSelfAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReport.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReport.CertifiedSelfAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certReport.ApprovalDate?.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "L").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = certReport.DateFrom.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "M").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = certReport.DateTo.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = certReport.Type.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 2).Width = 15;
            ws.Columns(3, 4).Width = 20;
            ws.Column(5).Width = 40;
            ws.Columns(6, 14).Width = 20;

            return workbook;
        }
    }
}
