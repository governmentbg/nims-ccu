using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierApprovedAmountsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierApprovedAmountsExcelController(
            IAuthorizer authorizer,
            IContractReportsRepository contractReportsRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportApprovedAmounts/excelExport")]
        public HttpResponseMessage GetContractReportApprovedAmounts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var approvedAmounts = this.contractReportsRepository.GetContractReportApprovedAmountsForProjectDossier(contractId);
            var workbook = this.GetContractReportApprovedAmountWorkbook(approvedAmounts);

            return this.Request.CreateXmlResponse(workbook, "approvedAmounts");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCorrections = this.contractReportCorrectionsRepository.GetContractReportCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCorrectionWorkbook(contractReportCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportFinancialCorrections/excelExport")]
        public HttpResponseMessage GetContractReportFinancialCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportFinancialCorrections = this.contractReportFinancialCorrectionsRepository.GetContractReportFinancialCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportFinancialCorrectionWorkbook(contractReportFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportFinancialCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportFinancialCSDs/excelExport")]
        public HttpResponseMessage GetContractReportFinancialCSDs(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportFinancialCSDs = this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDsForProjectDossier(contractId);
            var workbook = this.GetContractReportFinancialCSDWorkbook(contractReportFinancialCSDs);

            return this.Request.CreateXmlResponse(workbook, "contractReportFinancialCSDs");
        }

        private XLWorkbook GetContractReportApprovedAmountWorkbook(IList<ContractReportApprovedAmountVO> approvedAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПОД");

            // Headers
            ws.Cell("A1").Value = "Верифицирана сума";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Договор";
            ws.Cell("D1").Value = "Процедура";
            ws.Cell("E1").Value = "Статус";
            ws.Cell("F1").Value = "Пореден номер";
            ws.Cell("G1").Value = "Тип";
            ws.Cell("H1").Value = "Въведен от";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var approvedAmount in approvedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = approvedAmount.ApprovedAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = approvedAmount.ContractRegNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = approvedAmount.ContractName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = approvedAmount.ProcedureName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = approvedAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = approvedAmount.OrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = approvedAmount.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = approvedAmount.Source.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCorrectionWorkbook(IList<ContractReportCorrectionVO> contractReportCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Коригиране на ВС на други нива");

            // Headers
            ws.Cell("A1").Value = "Коригирана одобрена сума-БФП";
            ws.Cell("B1").Value = "Коригирана одобрена сума-СФ";
            ws.Cell("C1").Value = "Програма";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Номер";
            ws.Cell("F1").Value = "Вид";
            ws.Cell("G1").Value = "Дата";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCorrection in contractReportCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCorrection.CorrectedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCorrection.CorrectedApprovedSelfAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCorrection.ProgrammeName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCorrection.RegNumber;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCorrection.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportFinancialCorrectionWorkbook(IList<ContractReportFinancialCorrectionVO> contractReportFinancialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Коригиране на ВС ниво РОД");

            // Headers
            ws.Cell("A1").Value = "Коригирана одобрена сума-БФП";
            ws.Cell("B1").Value = "Коригирана одобрена сума-СФ";
            ws.Cell("C1").Value = "Номер на договор";
            ws.Cell("D1").Value = "Договор";
            ws.Cell("E1").Value = "Процедура";
            ws.Cell("F1").Value = "Номер на пакет";
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
            foreach (var contractReportFinancialCorrection in contractReportFinancialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportFinancialCorrection.CorrectedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportFinancialCorrection.CorrectedApprovedSelfAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportFinancialCorrection.ContractRegNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportFinancialCorrection.ContractName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportFinancialCorrection.ProcedureName;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportFinancialCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportFinancialCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportFinancialCorrection.OrderNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReportFinancialCorrection.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReportFinancialCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 10).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportFinancialCSDWorkbook(IList<ContractReportFinancialCSDsVO> contractReportFinancialCSDs)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("РОД");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Разходооправдателен документ";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Номер и дата на пакет";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Номер и дата на ИП";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Размер на РОД";
            ws.Range("E1", "F1").Merge();
            ws.Cell("E2").Value = "БФП";
            ws.Cell("F2").Value = "Общо";
            ws.Cell("G1").Value = "Одобрен размер на РОД";
            ws.Range("G1", "H1").Merge();
            ws.Cell("G2").Value = "БФП";
            ws.Cell("H2").Value = "Общо";
            ws.Cell("I1").Value = "Корекция на РОД";
            ws.Range("I1", "J1").Merge();
            ws.Cell("I2").Value = "БФП";
            ws.Cell("J2").Value = "Общо";

            var rngHeaders = ws.Range(1, 1, 2, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var contractReportFinancialCSD in contractReportFinancialCSDs)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportFinancialCSD.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportFinancialCSD.Type.GetEnumDescription() + " " +
                    contractReportFinancialCSD.Number + " " + "от" + " " +
                    contractReportFinancialCSD.Date.ToString("dd.MM.yyyy") + " " +
                    "(" + contractReportFinancialCSD.CompanyType.GetEnumDescription() + ")" +
                    contractReportFinancialCSD.CompanyUinType.GetEnumDescription() + " " +
                    contractReportFinancialCSD.CompanyUin + " " +
                    contractReportFinancialCSD.CompanyName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportFinancialCSD.ReportNum +
                    (contractReportFinancialCSD.ReportSubmitDate.HasValue ? " " + contractReportFinancialCSD.ReportSubmitDate.Value.ToString("dd.MM.yyyy") : string.Empty);

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportFinancialCSD.PaymentVersionNum + "." +
                    contractReportFinancialCSD.PaymentVersionSubNum +
                    (contractReportFinancialCSD.PaymentSubmitDate.HasValue ? " (" +
                    contractReportFinancialCSD.PaymentSubmitDate.Value.ToString("dd.MM.yyyy") + ")" : string.Empty);

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportFinancialCSD.BfpTotalAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportFinancialCSD.TotalAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportFinancialCSD.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportFinancialCSD.ApprovedTotalAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReportFinancialCSD.CorrectedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReportFinancialCSD.CorrectedApprovedTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 10;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Column(2).Width = 22;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Columns(3, 10).Width = 15;
            ws.Columns(3, 10).Style.Alignment.SetWrapText();

            return workbook;
        }
    }
}
