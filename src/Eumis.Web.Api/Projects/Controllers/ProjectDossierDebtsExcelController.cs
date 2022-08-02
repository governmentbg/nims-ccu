using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Core.Relations;
using Eumis.Data.Debts.Repositories;
using Eumis.Domain.Debts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierDebtsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractDebtsRepository contractDebtsRepository;
        private ICorrectionDebtsRepository correctionDebtsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierDebtsExcelController(
            IAuthorizer authorizer,
            IContractDebtsRepository contractDebtsRepository,
            ICorrectionDebtsRepository correctionDebtsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.contractDebtsRepository = contractDebtsRepository;
            this.correctionDebtsRepository = correctionDebtsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractDebts/excelExport")]
        public HttpResponseMessage GetContractDebts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractDebts = this.contractDebtsRepository.GetContractDebtsForProjectDossier(contractId);
            var workbook = this.GetContractDebtWorkbook(contractDebts);

            return this.Request.CreateXmlResponse(workbook, "contractDebts");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/correctionDebts/excelExport")]
        public HttpResponseMessage GetCorrectionDebts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var correctionDebts = this.correctionDebtsRepository.GetCorrectionDebtsForProjectDossier(contractId);
            var workbook = this.GetCorrectionDebtWorkbook(correctionDebts);

            return this.Request.CreateXmlResponse(workbook, "correctionDebts");
        }

        private XLWorkbook GetContractDebtWorkbook(IList<ContractDebtVO> contractDebts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Дългове към договор");

            // Headers
            ws.Cell("A1").Value = "Пореден номер";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Номер на дълга";
            ws.Cell("D1").Value = "Актуален статус";
            ws.Cell("E1").Value = "Дата на регистрация";
            ws.Cell("F1").Value = "Дата на последна актуализация";
            ws.Cell("G1").Value = "Главница Общо";
            ws.Cell("H1").Value = "Лихва Общо";
            ws.Cell("I1").Value = "№ ДС";

            var rngHeaders = ws.Range(1, 1, 1, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractDebt in contractDebts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractDebt.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractDebt.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractDebt.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractDebt.ExecutionStatus.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractDebt.RegDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractDebt.ModifyDate.HasValue ?
                    contractDebt.ModifyDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractDebt.TotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractDebt.TotalInterestAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractDebt.CertReportNumber;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 9).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetCorrectionDebtWorkbook(IList<CorrectionDebtVO> correctionDebts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Дългове по ФКСП");

            // Headers
            ws.Cell("A1").Value = "Пореден номер";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Номер на дълга по ФКСП";
            ws.Cell("D1").Value = "Дата на регистрация";
            ws.Cell("E1").Value = "Дата на последна актуализация";
            ws.Cell("F1").Value = "Обща дължина сума";
            ws.Cell("G1").Value = "Обща сертифицирана сума";
            ws.Cell("H1").Value = "Обща възстановена сума";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var correctionDebt in correctionDebts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = correctionDebt.OrderNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = correctionDebt.CorrectionRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = correctionDebt.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = correctionDebt.RegDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = correctionDebt.ModifyDate.HasValue ?
                    correctionDebt.ModifyDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = correctionDebt.DebtTotalAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = correctionDebt.CertTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = correctionDebt.ReimbursedTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }
    }
}
