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
    public class CertReportPaymentsExcelController : ApiController
    {
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public CertReportPaymentsExcelController(
            ICertReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/certReports/{certReportId:int}/payments/excelExport")]
        public HttpResponseMessage GetCertReportPayments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportPayments = this.certReportsRepository.GetCertReportPayments(certReportId);

            var workbook = this.GetWorkbook(certReportPayments);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<CertReportPaymentVO> certReportPayments)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ИП към ДС");

            // Headers
            ws.Cell("A1").Value = "Номер на пакет";
            ws.Cell("B1").Value = "Номер на ИП";
            ws.Cell("C1").Value = "Дата на верификация";
            ws.Cell("D1").Value = "Тип на ИП";
            ws.Cell("E1").Value = "Искани средства";
            ws.Cell("F1").Value = "Верифицирана сума-БФП";
            ws.Cell("G1").Value = "Верифицирана сума-СФ";
            ws.Cell("H1").Value = "Сертифицирана сума-БФП";
            ws.Cell("I1").Value = "Сертифицирана сума-СФ";
            ws.Cell("J1").Value = "Номер на договор";
            ws.Cell("K1").Value = "Договор";
            ws.Cell("L1").Value = "Процедура";
            ws.Cell("M1").Value = "Статус";
            ws.Cell("N1").Value = "Тип";
            ws.Cell("O1").Value = "Въведен от";
            ws.Cell("P1").Value = "Дата на регистрация";

            var rngHeaders = ws.Range(1, 1, 1, 16);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 16).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReportPayment in certReportPayments)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReportPayment.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certReportPayment.PaymentVersionNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReportPayment.CheckedDate.HasValue ?
                    certReportPayment.CheckedDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReportPayment.PaymentType.HasValue ?
                    certReportPayment.PaymentType.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReportPayment.RequestedAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReportPayment.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReportPayment.ApprovedSelfAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReportPayment.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReportPayment.CertifiedSelfAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReportPayment.ContractRegNum;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certReportPayment.ContractName;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = certReportPayment.ProcedureName;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = certReportPayment.Status.GetEnumDescription();

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = certReportPayment.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "O").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "O").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "O").Value = certReportPayment.Source.GetEnumDescription();

                ws.Cell(rowIndex, "P").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "P").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "P").Value = certReportPayment.SubmitDate.HasValue ?
                    certReportPayment.SubmitDate.Value.ToString("dd.MM.yyyy") :
                    null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 10).AdjustToContents();
            ws.Columns(11, 12).Style.Alignment.SetWrapText();
            ws.Columns(11, 12).Width = 40;
            ws.Columns(12, 16).AdjustToContents();

            return workbook;
        }
    }
}
