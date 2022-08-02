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
    public class CertReportAdvancePaymentsExcelController : ApiController
    {
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public CertReportAdvancePaymentsExcelController(
            ICertReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/certReports/{certReportId:int}/advancePayments/excelExport")]
        public HttpResponseMessage GetCertReportAdvancePayments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportAdvancePayments = this.certReportsRepository.GetCertReportAdvancePayments(certReportId);

            var workbook = this.GetWorkbook(certReportAdvancePayments);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<CertReportAdvancePaymentVO> certReportAdvancePayments)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ИП по чл.131 към ДС");

            // Headers
            ws.Cell("A1").Value = "Номер на пакет";
            ws.Cell("B1").Value = "Номер на ИП";
            ws.Cell("C1").Value = "Дата на верификация";
            ws.Cell("D1").Value = "Тип на ИП";
            ws.Cell("E1").Value = "Искани средства";
            ws.Cell("F1").Value = "Верифицирана сума-БФП";
            ws.Cell("G1").Value = "Сертифицирана сума-БФП";
            ws.Cell("H1").Value = "Номер на договор";
            ws.Cell("I1").Value = "Договор";
            ws.Cell("J1").Value = "Процедура";
            ws.Cell("K1").Value = "Статус";
            ws.Cell("L1").Value = "Тип";
            ws.Cell("M1").Value = "Въведен от";
            ws.Cell("N1").Value = "Дата на регистрация";

            var rngHeaders = ws.Range(1, 1, 1, 14);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 14).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certReportAdvancePayment in certReportAdvancePayments)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certReportAdvancePayment.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certReportAdvancePayment.PaymentVersionNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certReportAdvancePayment.CheckedDate.HasValue ?
                    certReportAdvancePayment.CheckedDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certReportAdvancePayment.PaymentType.HasValue ?
                    certReportAdvancePayment.PaymentType.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certReportAdvancePayment.RequestedAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certReportAdvancePayment.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certReportAdvancePayment.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certReportAdvancePayment.ContractRegNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = certReportAdvancePayment.ContractName;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certReportAdvancePayment.ProcedureName;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certReportAdvancePayment.Status.GetEnumDescription();

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = certReportAdvancePayment.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = certReportAdvancePayment.Source.GetEnumDescription();

                ws.Cell(rowIndex, "N").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = certReportAdvancePayment.SubmitDate.HasValue ?
                    certReportAdvancePayment.SubmitDate.Value.ToString("dd.MM.yyyy") :
                    null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();
            ws.Columns(9, 10).Style.Alignment.SetWrapText();
            ws.Columns(9, 10).Width = 40;
            ws.Columns(10, 14).AdjustToContents();

            return workbook;
        }
    }
}
