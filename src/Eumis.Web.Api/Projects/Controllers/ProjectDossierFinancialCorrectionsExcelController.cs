using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Core.Relations;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierFinancialCorrectionsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierFinancialCorrectionsExcelController(
            IAuthorizer authorizer,
            IFinancialCorrectionsRepository financialCorrectionsRepository,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/financialCorrections/excelExport")]
        public HttpResponseMessage GetFinancialCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var financialCorrections = this.financialCorrectionsRepository.GetFinancialCorrectionsForProjectDossier(contractId);
            var workbook = this.GetFinancialCorrectionWorkbook(financialCorrections);

            return this.Request.CreateXmlResponse(workbook, "financialCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/flatFinancialCorrections/excelExport")]
        public HttpResponseMessage GetFlatFinancialCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var flatFinancialCorrections = this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionsForProjectDossier(contractId);
            var workbook = this.GetFlatFinancialCorrectionWorkbook(flatFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "flatFinancialCorrections");
        }

        private XLWorkbook GetFinancialCorrectionWorkbook(IList<FinancialCorrectionVO> financialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Финансови корекции");

            // Headers
            ws.Cell("A1").Value = "Пореден номер";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Номер на договор";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Дата на налагане";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Договор с изпълнител";
            ws.Range("D1", "E1").Merge();
            ws.Cell("D2").Value = "Номер";
            ws.Cell("E2").Value = "Изпълнител";
            ws.Cell("F1").Value = "Основание за налагане";
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = "% на наложената финансова корекция";
            ws.Range("G1", "G2").Merge();
            ws.Cell("H1").Value = "Стойност на наложената финансова корекция - Общо";
            ws.Range("H1", "H2").Merge();

            var rngHeaders = ws.Range(1, 1, 2, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var financialCorrection in financialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = financialCorrection.FinancialCorrectionVersionOrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = financialCorrection.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = financialCorrection.ImpositionDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = financialCorrection.ContractContractNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = financialCorrection.ContractContractorCompany;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = financialCorrection.ImposingReason;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = financialCorrection.FinancialCorrectionVersionPercent;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = financialCorrection.FinancialCorrectionVersionTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 10;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 6).AdjustToContents();
            ws.Column(7).Width = 20;
            ws.Column(7).Style.Alignment.SetWrapText();
            ws.Column(8).Width = 15;
            ws.Column(8).Style.Alignment.SetWrapText();

            return workbook;
        }

        private XLWorkbook GetFlatFinancialCorrectionWorkbook(IList<FlatFinancialCorrectionVO> flatFinancialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Финансови корекции за СП");

            // Headers
            ws.Cell("A1").Value = "Пореден номер";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Наименование";
            ws.Cell("D1").Value = "Ниво";
            ws.Cell("E1").Value = "Тип";
            ws.Cell("F1").Value = "Статус";
            ws.Cell("G1").Value = "Дата на налагане";
            ws.Cell("H1").Value = "Номер на решението за налагане";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var flatFinancialCorrection in flatFinancialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = flatFinancialCorrection.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = flatFinancialCorrection.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = flatFinancialCorrection.Name;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = flatFinancialCorrection.Level.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = flatFinancialCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = flatFinancialCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = flatFinancialCorrection.ImpositionDate.HasValue ?
                    flatFinancialCorrection.ImpositionDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = flatFinancialCorrection.ImpositionNumber;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }
    }
}
