using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    public class AnnualAccountReportCertifiedRevalidationCorrectionsExcelController : ApiController
    {
        private IAnnualAccountReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public AnnualAccountReportCertifiedRevalidationCorrectionsExcelController(
            IAnnualAccountReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/annualAccountReports/{annualAccountReportId:int}/certifiedRevalidationCorrections/excelExport")]
        public HttpResponseMessage GetAnnualAccountReportCertifiedRevalidationCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var certReportCorrections = this.certReportsRepository.GetAnnualAccountReportCertRevalidationCorrections(annualAccountReportId);

            var workbook = this.GetWorkbook(certReportCorrections);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<ContractReportRevalidationCertAuthorityCorrectionVO> certifiedCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(СО) на преп. суми ГСО");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "Статус";
            ws.Cell("C1").Value = "Номер";
            ws.Cell("D1").Value = "Тип";
            ws.Cell("E1").Value = "Дата";

            var rngHeaders = ws.Range(1, 1, 1, 5);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certAuthorityCorrection in certifiedCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certAuthorityCorrection.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certAuthorityCorrection.StatusDescr.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certAuthorityCorrection.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certAuthorityCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certAuthorityCorrection.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 5).AdjustToContents();

            return workbook;
        }
    }
}
