using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    public class AnnualAccountReportCorrectionsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IAnnualAccountReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public AnnualAccountReportCorrectionsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IAnnualAccountReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/annualAccountReports/{annualAccountReportId:int}/corrections/excelExport")]
        public HttpResponseMessage GetAnnualAccountReportCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var certReportCorrections = this.certReportsRepository.GetAnnualAccountReportCorrections(annualAccountReportId);

            var workbook = this.GetWorkbook(certReportCorrections);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<AnnualAccountReportCorrectionVO> corrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(ВС) към ДС");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "Статус";
            ws.Cell("C1").Value = "Номер";
            ws.Cell("D1").Value = "Знак";
            ws.Cell("E1").Value = "Верифицирана сума-БФП";
            ws.Cell("F1").Value = "Верифицирана сума-СФ";
            ws.Cell("G1").Value = "Сертифицирана сума-БФП";
            ws.Cell("H1").Value = "Сертифицирана сума-СФ";
            ws.Cell("I1").Value = "Дата на проверка на СО";
            ws.Cell("J1").Value = "Тип";
            ws.Cell("K1").Value = "Дата";

            var rngHeaders = ws.Range(1, 1, 1, 11);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 11).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReportCorrection in corrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReportCorrection.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certReportCorrection.CertStatus.HasValue ?
                    certReportCorrection.CertStatus.Value.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReportCorrection.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReportCorrection.Sign.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReportCorrection.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReportCorrection.ApprovedSelfAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReportCorrection.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReportCorrection.CertifiedSelfAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReportCorrection.CertCheckedDate.HasValue ?
                    certReportCorrection.CertCheckedDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReportCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "K").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certReportCorrection.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 11).AdjustToContents();

            return workbook;
        }
    }
}
