using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReportIndicators.ViewObjects;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportIndicators.Controllers
{
    public class ContractReportIndicatorsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;

        public ContractReportIndicatorsExcelController(
            IAuthorizer authorizer,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository)
        {
            this.authorizer = authorizer;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
        }

        [Route("api/contractReports/{contractReportId:int}/indicators/excelExport")]

        public HttpResponseMessage GetContractReportIndicators(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            var contractRegNum = this.contractsRepository.GetRegNumber(contractReport.ContractId);
            var contractReportIndicators = this.contractReportIndicatorsRepository.GetContractReportIndicators(contractReportId);

            var workbook = this.GetWorkbook(contractReportIndicators);

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_{1}_indicators", contractRegNum, contractReport.OrderNum));
        }

        private XLWorkbook GetWorkbook(IList<ContractReportIndicatorsVO> contractReportIndicators)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ВИ");

            // Headers
            ws.Cell("A1").Value = "Индикатор";
            ws.Range("A1", "A2").Merge();

            ws.Cell("B1").Value = "Статус";
            ws.Range("B1", "B2").Merge();

            ws.Cell("C1").Value = "Одобрение";
            ws.Range("C1", "C2").Merge();

            ws.Cell("D1").Value = "Отчетени стойности";
            ws.Range("D1", "F1").Merge();
            ws.Cell("D2").Value = "Ст-ст за периода";
            ws.Cell("E2").Value = "Ст-ст с натрупване";
            ws.Cell("F2").Value = "Остатък спрямо договора ";

            ws.Cell("G1").Value = "Одобрени стойности";
            ws.Range("G1", "J1").Merge();
            ws.Cell("G2").Value = "Ст-ст за периода";
            ws.Cell("H2").Value = "Ст-ст с натрупване";
            ws.Cell("I2").Value = "Остатък спрямо договора ";

            var rngHeaders = ws.Range(1, 1, 2, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var contractReportIndicator in contractReportIndicators)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportIndicator.IndicatorName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportIndicator.Status.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportIndicator.Approval.HasValue ?
                    contractReportIndicator.Approval.GetEnumDescription() : null;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportIndicator.PeriodAmount;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportIndicator.CumulativeAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportIndicator.ResidueAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportIndicator.ApprovedPeriodAmount.HasValue ?
                    contractReportIndicator.ApprovedPeriodAmount.Value : (decimal?)null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportIndicator.ApprovedCumulativeAmount.HasValue ?
                    contractReportIndicator.ApprovedCumulativeAmount.Value : (decimal?)null;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReportIndicator.ApprovedResidueAmount.HasValue ?
                    contractReportIndicator.ApprovedResidueAmount.Value : (decimal?)null;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 50;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 9).AdjustToContents();

            return workbook;
        }
    }
}
