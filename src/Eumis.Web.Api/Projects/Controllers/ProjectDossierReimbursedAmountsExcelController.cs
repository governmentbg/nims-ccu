using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Core.Relations;
using Eumis.Data.FIReimbursedAmounts.Repositories;
using Eumis.Data.FIReimbursedAmounts.ViewObjects;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierReimbursedAmountsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository;
        private IContractReimbursedAmountsRepository contractReimbursedAmountsRepository;
        private IFIReimbursedAmountsRepository fiReimbursedAmountsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierReimbursedAmountsExcelController(
            IAuthorizer authorizer,
            IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository,
            IContractReimbursedAmountsRepository contractReimbursedAmountsRepository,
            IFIReimbursedAmountsRepository fiReimbursedAmountsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.debtReimbursedAmountsRepository = debtReimbursedAmountsRepository;
            this.contractReimbursedAmountsRepository = contractReimbursedAmountsRepository;
            this.fiReimbursedAmountsRepository = fiReimbursedAmountsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/debtReimbursedAmounts/excelExport")]
        public HttpResponseMessage GetDebtReimbursedAmounts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var debtReimbursedAmounts = this.debtReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
            var workbook = this.GetDebtReimbursedAmountWorkbook(debtReimbursedAmounts);

            return this.Request.CreateXmlResponse(workbook, "debtReimbursedAmounts");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReimbursedAmounts/excelExport")]
        public HttpResponseMessage GetContarctReimbursedAmounts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReimbursedAmounts = this.contractReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
            var workbook = this.GetContractReimbursedAmountWorkbook(contractReimbursedAmounts);

            return this.Request.CreateXmlResponse(workbook, "contractReimbursedAmounts");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/fiReimbursedAmounts/excelExport")]
        public HttpResponseMessage GetFiReimbursedAmounts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var fiReimbursedAmounts = this.fiReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
            var workbook = this.GetFIReimbursedAmountWorkbook(fiReimbursedAmounts);

            return this.Request.CreateXmlResponse(workbook, "fiReimbursedAmounts");
        }

        private XLWorkbook GetDebtReimbursedAmountWorkbook(IList<DebtReimbursedAmountVO> debtReimbursedAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Възстановени суми по дългове");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "№ дълг";
            ws.Cell("C1").Value = "Статус";
            ws.Cell("D1").Value = "Номер";
            ws.Cell("E1").Value = "Вид";
            ws.Cell("F1").Value = "Начин на възстановяване";
            ws.Cell("G1").Value = "Дата на плащане";
            ws.Cell("H1").Value = "Главница-Финансиране от ЕС";
            ws.Cell("I1").Value = "Главница-Финансиране от НФ";
            ws.Cell("J1").Value = "Главница-Общо";
            ws.Cell("K1").Value = "Лихва-Финансиране от ЕС";
            ws.Cell("L1").Value = "Лихва-Финансиране от НФ";
            ws.Cell("M1").Value = "Лихва-Общо";

            var rngHeaders = ws.Range(1, 1, 1, 13);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 13).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var debtReimbursedAmount in debtReimbursedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = debtReimbursedAmount.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = debtReimbursedAmount.DebtRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = debtReimbursedAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = debtReimbursedAmount.RegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = debtReimbursedAmount.Type.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = debtReimbursedAmount.Reimbursement.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = debtReimbursedAmount.ReimbursementDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = debtReimbursedAmount.PrincipalEuAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = debtReimbursedAmount.PrincipalBgAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = debtReimbursedAmount.PrincipalTotalAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = debtReimbursedAmount.InterestEuAmount;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = debtReimbursedAmount.InterestBgAmount;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = debtReimbursedAmount.InterestTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 13).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReimbursedAmountWorkbook(IList<ContractReimbursedAmountVO> contractReimbursedAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Възстановени суми по договор");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "№ договор";
            ws.Cell("C1").Value = "Статус";
            ws.Cell("D1").Value = "Номер";
            ws.Cell("E1").Value = "Вид";
            ws.Cell("F1").Value = "Начин на възстановяване";
            ws.Cell("G1").Value = "Дата на плащане";
            ws.Cell("H1").Value = "Главница-Финансиране от ЕС";
            ws.Cell("I1").Value = "Главница-Финансиране от НФ";
            ws.Cell("J1").Value = "Главница-Общо";
            ws.Cell("K1").Value = "Лихва-Финансиране от ЕС";
            ws.Cell("L1").Value = "Лихва-Финансиране от НФ";
            ws.Cell("M1").Value = "Лихва-Общо";

            var rngHeaders = ws.Range(1, 1, 1, 13);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 13).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReimbursedAmount in contractReimbursedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReimbursedAmount.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReimbursedAmount.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReimbursedAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReimbursedAmount.RegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReimbursedAmount.Type.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReimbursedAmount.Reimbursement.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReimbursedAmount.ReimbursementDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReimbursedAmount.PrincipalEuAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReimbursedAmount.PrincipalBgAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReimbursedAmount.PrincipalTotalAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = contractReimbursedAmount.InterestEuAmount;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = contractReimbursedAmount.InterestBgAmount;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = contractReimbursedAmount.InterestTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 13).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetFIReimbursedAmountWorkbook(IList<FIReimbursedAmountVO> fiReimbursedAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Възстановени суми по ФИ");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "№ договор";
            ws.Cell("C1").Value = "Статус";
            ws.Cell("D1").Value = "Номер";
            ws.Cell("E1").Value = "Суми, възстановени на ФИ";
            ws.Cell("F1").Value = "Начин на възстановяване";
            ws.Cell("G1").Value = "Дата на плащане";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var fiReimbursedAmount in fiReimbursedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = fiReimbursedAmount.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = fiReimbursedAmount.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = fiReimbursedAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = fiReimbursedAmount.RegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = fiReimbursedAmount.Type.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = fiReimbursedAmount.Reimbursement.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = fiReimbursedAmount.ReimbursementDate.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }
    }
}
