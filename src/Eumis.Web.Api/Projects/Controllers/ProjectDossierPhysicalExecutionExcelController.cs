using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReportIndicators.ViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierPhysicalExecutionExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractsRepository contractsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierPhysicalExecutionExcelController(
            IAuthorizer authorizer,
            IContractsRepository contractsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.contractsRepository = contractsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/physicalExecutionActivities/excelExport")]
        public HttpResponseMessage GetPhysicalExecutionActivities(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractPhysicalExecutionActivities = this.contractsRepository.GetContractPhysicalExecutionActivitiesForProjectDossier(contractId);
            var workbook = this.GetPhysicalExecutionActivityWorkbook(contractPhysicalExecutionActivities);

            return this.Request.CreateXmlResponse(workbook, "contractPhysicalExecutionActivities");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/physicalExecutionIndicators/excelExport")]
        public HttpResponseMessage GetPhysicalExecutionIndicators(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractPhysicalExecutionIndicators = this.contractReportIndicatorsRepository.GetContractPhysicalExecutionIndicatorsForProjectDossier(contractId);
            var workbook = this.GetPhysicalExecutionIndicatorWorkbook(contractPhysicalExecutionIndicators);

            return this.Request.CreateXmlResponse(workbook, "contractPhysicalExecutionIndicators");
        }

        private XLWorkbook GetPhysicalExecutionActivityWorkbook(IList<ContractPhysicalExecutionActivityVO> contractPhysicalExecutionActivities)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Дейности по проекта");

            // Headers
            ws.Cell("A1").Value = "Номер на догово";
            ws.Cell("B1").Value = "Дейност";
            ws.Cell("C1").Value = "Статус";
            ws.Cell("D1").Value = "Актуална начална дата";
            ws.Cell("E1").Value = "Актуална крайна дата";
            ws.Cell("F1").Value = "Договорена стойност";
            ws.Cell("G1").Value = "Отчетена стойност";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractPhysicalExecutionActivity in contractPhysicalExecutionActivities)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractPhysicalExecutionActivity.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractPhysicalExecutionActivity.ActivityName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractPhysicalExecutionActivity.StatusDesc;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractPhysicalExecutionActivity.StartDate.HasValue ?
                    contractPhysicalExecutionActivity.StartDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractPhysicalExecutionActivity.EndDate.HasValue ?
                    contractPhysicalExecutionActivity.EndDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractPhysicalExecutionActivity.Amount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractPhysicalExecutionActivity.TotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetPhysicalExecutionIndicatorWorkbook(IList<ContractPhysicalExecutionIndicatorVO> contractPhysicalExecutionIndicators)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Индикатори");

            // Headers
            ws.Cell("A1").Value = "Номер на догово";
            ws.Cell("B1").Value = "Индикатор";
            ws.Cell("C1").Value = "Мерна единица";
            ws.Cell("D1").Value = "Базова стойност";
            ws.Cell("E1").Value = "Целева стойност";
            ws.Cell("F1").Value = "Отчетена";
            ws.Cell("G1").Value = "Одобрена";
            ws.Cell("H1").Value = "Коригирана одобрена стойност";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractPhysicalExecutionIndicator in contractPhysicalExecutionIndicators)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractPhysicalExecutionIndicator.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractPhysicalExecutionIndicator.IndicatorName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractPhysicalExecutionIndicator.MeasureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractPhysicalExecutionIndicator.BaseTotal;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractPhysicalExecutionIndicator.TargetTotal;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractPhysicalExecutionIndicator.CumulativeAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractPhysicalExecutionIndicator.ApprovedCumulativeAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractPhysicalExecutionIndicator.CorrectedApprovedCumulativeAmountTotal;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }
    }
}
