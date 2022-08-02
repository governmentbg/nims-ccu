using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    public class CertReportPaymentCSDsExcelController : ApiController
    {
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IAuthorizer authorizer;

        public CertReportPaymentCSDsExcelController(
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IAuthorizer authorizer)
        {
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.authorizer = authorizer;
        }

        [HttpGet]
        [Route("api/certReports/{certReportId:int}/payments/{contractReportId:int}/csds/excelExport")]
        public HttpResponseMessage GetCertReportContractReportAttachedFinancialCSDBudgetItemsExcel(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(contractReportId, isAttachedToCertReport: true, certReportId: certReportId);
            var workbook = this.GetWorkbook(budgetItems);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<ContractReportFinancialCSDBudgetItemsVO> contractReportFinancialCSDBudgetItems)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("РОД");

            // Headers
            ws.Cell("A1").Value = "Разходооправдателен документ";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Ред от бюджета";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Дейност";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Статус";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Съгласие";
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = "Одобрена сума";
            ws.Range("F1", "I1").Merge();
            ws.Cell("F2").Value = "БФП - ЕС";
            ws.Cell("G2").Value = "БФП - НФ ";
            ws.Cell("H2").Value = "Собствено съфинансиране";
            ws.Cell("I2").Value = "Обща сума";
            ws.Cell("J1").Value = "Сертифицирана сума";
            ws.Range("J1", "M1").Merge();
            ws.Cell("J2").Value = "БФП - ЕС";
            ws.Cell("K2").Value = "БФП - НФ ";
            ws.Cell("L2").Value = "Собствено съфинансиране";
            ws.Cell("M2").Value = "Обща сума";

            var rngHeaders = ws.Range(1, 1, 2, 13);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 13).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var contractReportFinancialCSDBudgetItem in contractReportFinancialCSDBudgetItems)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportFinancialCSDBudgetItem.Type.GetEnumDescription() + " " +
                    contractReportFinancialCSDBudgetItem.Number + " " + "от" + " " +
                    contractReportFinancialCSDBudgetItem.Date.ToString("dd.MM.yyyy") + " " +
                    "(" + contractReportFinancialCSDBudgetItem.CompanyType.GetEnumDescription() + ")" +
                    contractReportFinancialCSDBudgetItem.CompanyUinType.GetEnumDescription() + " " +
                    contractReportFinancialCSDBudgetItem.CompanyUin + " " +
                    contractReportFinancialCSDBudgetItem.CompanyName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportFinancialCSDBudgetItem.BudgetDetailName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportFinancialCSDBudgetItem.ContractActivityName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportFinancialCSDBudgetItem.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportFinancialCSDBudgetItem.CostSupportingDocumentApproved.HasValue ?
                    (contractReportFinancialCSDBudgetItem.CostSupportingDocumentApproved.Value == true ? "Да" : "Не") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportFinancialCSDBudgetItem.ApprovedEuAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportFinancialCSDBudgetItem.ApprovedBgAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportFinancialCSDBudgetItem.ApprovedSelfAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReportFinancialCSDBudgetItem.ApprovedTotalAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReportFinancialCSDBudgetItem.CertifiedApprovedEuAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = contractReportFinancialCSDBudgetItem.CertifiedApprovedBgAmount;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = contractReportFinancialCSDBudgetItem.CertifiedApprovedSelfAmount;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = contractReportFinancialCSDBudgetItem.CertifiedApprovedTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 70;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Column(2).Width = 50;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Column(3).Width = 50;
            ws.Column(3).Style.Alignment.SetWrapText();
            ws.Column(4).AdjustToContents();
            ws.Column(5).Width = 10;
            ws.Column(5).Style.Alignment.SetWrapText();
            ws.Columns(6, 13).AdjustToContents();

            return workbook;
        }
    }
}
