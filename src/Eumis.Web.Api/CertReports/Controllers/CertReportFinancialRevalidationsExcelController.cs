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
    public class CertReportFinancialRevalidationsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public CertReportFinancialRevalidationsExcelController(
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

        [Route("api/certReports/{certReportId:int}/financialRevalidations/excelExport")]
        public HttpResponseMessage GetCertReportFinancialRevalidations(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportFinancialRevalidations = this.certReportsRepository.GetCertReportFinancialRevalidations(certReportId);

            var workbook = this.GetWorkbook(certReportFinancialRevalidations);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<CertReportFinancialRevalidationVO> certReportFinancialRevalidations)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Преп. на ниво РОД към ДС");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Договор";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Номер на пакет";
            ws.Cell("E1").Value = "Верифицирана сума-БФП";
            ws.Cell("F1").Value = "Верифицирана сума-СФ";
            ws.Cell("G1").Value = "Сертифицирана сума-БФП";
            ws.Cell("H1").Value = "Сертифицирана сума-СФ";
            ws.Cell("I1").Value = "Статус";
            ws.Cell("J1").Value = "Пореден номер";
            ws.Cell("K1").Value = "Дата на създаване";
            ws.Cell("L1").Value = "Бележки";

            var rngHeaders = ws.Range(1, 1, 1, 12);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 12).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReportFinancialRevalidation in certReportFinancialRevalidations)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReportFinancialRevalidation.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certReportFinancialRevalidation.ContractName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReportFinancialRevalidation.ProcedureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReportFinancialRevalidation.ReportOrderNum;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReportFinancialRevalidation.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReportFinancialRevalidation.ApprovedSelfAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReportFinancialRevalidation.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReportFinancialRevalidation.CertifiedSelfAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReportFinancialRevalidation.Status.GetEnumDescription();

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReportFinancialRevalidation.OrderNum;

                ws.Cell(rowIndex, "K").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certReportFinancialRevalidation.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = certReportFinancialRevalidation.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Columns(2, 3).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).Width = 40;
            ws.Columns(4, 12).AdjustToContents();

            return workbook;
        }
    }
}
