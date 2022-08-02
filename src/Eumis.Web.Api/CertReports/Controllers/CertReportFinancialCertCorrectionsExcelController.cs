using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.CertReports.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    public class CertReportFinancialCertCorrectionsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public CertReportFinancialCertCorrectionsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/certReports/{certReportId:int}/financialCertCorrections/excelExport")]
        public HttpResponseMessage GetCertReportFinancialCertCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportFinancialCertCorrections = this.certReportsRepository.GetCertReportFinancialCertCorrections(certReportId);

            var workbook = this.GetWorkbook(certReportFinancialCertCorrections);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<CertReportFinancialCertCorrectionVO> certReportFinancialCertCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Изравн.(СС) ниво РОД към ДС");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Договор";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Номер на пакет";
            ws.Cell("E1").Value = "Сертифицирана сума-БФП";
            ws.Cell("F1").Value = "Сертифицирана сума-СФ";
            ws.Cell("G1").Value = "Статус";
            ws.Cell("H1").Value = "Пореден номер";
            ws.Cell("I1").Value = "Дата на създаване";
            ws.Cell("J1").Value = "Бележки";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReportFinancialCertCorrection in certReportFinancialCertCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReportFinancialCertCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certReportFinancialCertCorrection.ContractName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReportFinancialCertCorrection.ProcedureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReportFinancialCertCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReportFinancialCertCorrection.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReportFinancialCertCorrection.CertifiedSelfAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReportFinancialCertCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReportFinancialCertCorrection.OrderNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReportFinancialCertCorrection.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReportFinancialCertCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Columns(2, 3).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).Width = 40;
            ws.Columns(4, 10).AdjustToContents();

            return workbook;
        }
    }
}
