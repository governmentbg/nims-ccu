using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReportCertCorrections.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierCertifiedAmountsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierCertifiedAmountsExcelController(
            IAuthorizer authorizer,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.contractReportCertAuthorityFinancialCorrectionsRepository = contractReportCertAuthorityFinancialCorrectionsRepository;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertifiedAmounts/excelExport")]
        public HttpResponseMessage GetContractReportCertifiedAmounts(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var certifiedAmounts = this.contractReportsRepository.GetContractReportCertifiedAmountsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertifiedAmountWorkbook(certifiedAmounts);

            return this.Request.CreateXmlResponse(workbook, "certifiedAmounts");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportRevalidations/excelExport")]
        public HttpResponseMessage GetContractReportRevalidations(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportRevalidations = this.contractReportRevalidationsRepository.GetContractReportRevalidationsForProjectDossier(contractId);
            var workbook = this.GetContractReportRevalidationWorkbook(contractReportRevalidations);

            return this.Request.CreateXmlResponse(workbook, "contractReportRevalidations");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportFinancialRevalidations/excelExport")]
        public HttpResponseMessage GetContractReportFinancialRevalidations(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportFinancialRevalidations = this.contractReportFinancialRevalidationsRepository.GetContractReportFinancialRevalidationsForProjectDossier(contractId);
            var workbook = this.GetContractReportFinancialRevalidationWorkbook(contractReportFinancialRevalidations);

            return this.Request.CreateXmlResponse(workbook, "contractReportFinancialRevalidations");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertifiedAmountFinancialCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCertifiedAmountFinancialCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCertifiedAmountFinancialCorrections = this.contractReportFinancialCorrectionsRepository.GetContractReportCertifiedAmountFinancialCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertifiedAmountFinancialCorrectionWorkbook(contractReportCertifiedAmountFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportFinancialCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertifiedAmountCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCertifiedAmountCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCertifiedAmountFinancialCorrections = this.contractReportCorrectionsRepository.GetContractReportCertifiedAmountCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertifiedAmountCorrectionWorkbook(contractReportCertifiedAmountFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertAuthorityFinancialCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCertAuthorityFinancialCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCertAuthorityFinancialCorrections = this.contractReportCertAuthorityFinancialCorrectionsRepository.GetContractReportCertAuthorityFinancialCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertAuthorityFinancialCorrectionWorkbook(contractReportCertAuthorityFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportCertAuthorityFinancialCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertAuthorityCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCertAuthorityCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCertAuthorityCorrections = this.contractReportCertAuthorityCorrectionsRepository.GetContractReportCertAuthorityCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertAuthorityCorrectionWorkbook(contractReportCertAuthorityCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportCertAuthorityCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportCertCorrections/excelExport")]
        public HttpResponseMessage GetContractReportCertCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportCertCorrections = this.contractReportCertCorrectionsRepository.GetContractReportCertCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportCertCorrectionWorkbook(contractReportCertCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportCertCorrections");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractReportFinancialCertCorrections/excelExport")]
        public HttpResponseMessage GetContractReportFinancialCertCorrections(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractReportFinancialCertCorrections = this.contractReportFinancialCertCorrectionsRepository.GetContractReportFinancialCertCorrectionsForProjectDossier(contractId);
            var workbook = this.GetContractReportFinancialCertCorrectionWorkbook(contractReportFinancialCertCorrections);

            return this.Request.CreateXmlResponse(workbook, "contractReportFinancialCertCorrections");
        }

        private XLWorkbook GetContractReportCertifiedAmountWorkbook(IList<ContractReportCertifiedAmountVO> certifiedAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПОД");

            // Headers
            ws.Cell("A1").Value = "Сертифицирана сума";
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
            foreach (var certifiedAmount in certifiedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certifiedAmount.CertifiedApprovedAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certifiedAmount.ContractRegNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certifiedAmount.ContractName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certifiedAmount.ProcedureName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certifiedAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certifiedAmount.OrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certifiedAmount.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = certifiedAmount.Source.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportRevalidationWorkbook(IList<ContractReportRevalidationVO> contractReportRevalidations)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Преп. на ВС на други нива");

            // Headers
            ws.Cell("A1").Value = "Преп. сума БФП";
            ws.Cell("B1").Value = "Преп. сума СФ";
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
            foreach (var contractReportRevalidation in contractReportRevalidations)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportRevalidation.BfpTotalAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportRevalidation.SelfAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportRevalidation.ProgrammeName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportRevalidation.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportRevalidation.RegNumber;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportRevalidation.Type.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportRevalidation.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportFinancialRevalidationWorkbook(IList<ContractReportFinancialRevalidationVO> contractReportFinancialRevalidations)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Преп. на ВС на ниво РОД");

            // Headers
            ws.Cell("A1").Value = "Преп. сума БФП";
            ws.Cell("B1").Value = "Преп. сума СФ";
            ws.Cell("C1").Value = "Номер на договор";
            ws.Cell("D1").Value = "Договор";
            ws.Cell("E1").Value = "Процедура";
            ws.Cell("F1").Value = "Номер на пакет";
            ws.Cell("G1").Value = "Статус";
            ws.Cell("H1").Value = "Пореден номер";
            ws.Cell("I1").Value = "Дата на създаване";
            ws.Cell("J1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportFinancialRevalidation in contractReportFinancialRevalidations)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportFinancialRevalidation.TotalRevalidatedBfpTotalAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportFinancialRevalidation.TotalRevalidatedSelfAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportFinancialRevalidation.ContractRegNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportFinancialRevalidation.ContractName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportFinancialRevalidation.ProcedureName;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportFinancialRevalidation.ReportOrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportFinancialRevalidation.Status.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportFinancialRevalidation.OrderNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReportFinancialRevalidation.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReportFinancialRevalidation.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 10).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCertifiedAmountFinancialCorrectionWorkbook(IList<ContractReportCertifiedAmountFinancialCorrectionVO> contractReportCertifiedAmountFinancialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Кор. на ВС на ниво РОД");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Номер на пакет";
            ws.Cell("C1").Value = "Пореден номер";
            ws.Cell("D1").Value = "Сертифицирана коригирана сума - БФП";
            ws.Cell("E1").Value = "Сертифицирана коригирана сума - СФ";
            ws.Cell("F1").Value = "№ на ДС";
            ws.Cell("G1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCertifiedAmountFinancialCorrection in contractReportCertifiedAmountFinancialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCertifiedAmountFinancialCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCertifiedAmountFinancialCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCertifiedAmountFinancialCorrection.OrderNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCertifiedAmountFinancialCorrection.CertifiedCorrectedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCertifiedAmountFinancialCorrection.CertifiedCorrectedApprovedSelfAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCertifiedAmountFinancialCorrection.CertReportNumber;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCertifiedAmountFinancialCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCertifiedAmountCorrectionWorkbook(IList<ContractReportCertifiedAmountCorrectionVO> contractReportCertifiedAmountCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Кор. на ВС на на други нива");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Номер на пакет";
            ws.Cell("C1").Value = "Пореден номер";
            ws.Cell("D1").Value = "Сертифицирана коригирана сума - БФП";
            ws.Cell("E1").Value = "Сертифицирана коригирана сума - СФ";
            ws.Cell("F1").Value = "№ на ДС";
            ws.Cell("G1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCertifiedAmountCorrection in contractReportCertifiedAmountCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCertifiedAmountCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCertifiedAmountCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCertifiedAmountCorrection.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCertifiedAmountCorrection.CertifiedCorrectedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCertifiedAmountCorrection.CertifiedCorrectedApprovedSelfAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCertifiedAmountCorrection.CertReportNumber;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCertifiedAmountCorrection.Description;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCertAuthorityFinancialCorrectionWorkbook(IList<ContractReportCertAuthorityFinancialCorrectionVO> contractReportCertAuthorityFinancialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(СС) на ниво РОД");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Номер на пакет";
            ws.Cell("C1").Value = "Пореден номер";
            ws.Cell("D1").Value = "Коригирана сертифицирана сума - БФП";
            ws.Cell("E1").Value = "Коригирана сертифицирана сума - СФ";
            ws.Cell("F1").Value = "№ на ГСО";
            ws.Cell("G1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCertAuthorityFinancialCorrection in contractReportCertAuthorityFinancialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCertAuthorityFinancialCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCertAuthorityFinancialCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCertAuthorityFinancialCorrection.OrderNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCertAuthorityFinancialCorrection.CertifiedApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCertAuthorityFinancialCorrection.CertifiedApprovedSelfAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCertAuthorityFinancialCorrection.AnnualAccountReportOrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCertAuthorityFinancialCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCertAuthorityCorrectionWorkbook(IList<ContractReportCertAuthorityCorrectionVO> contractReportCertAuthorityCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(СС)");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Номер на пакет";
            ws.Cell("C1").Value = "Пореден номер";
            ws.Cell("D1").Value = "Коригирана сертифицирана сума - БФП";
            ws.Cell("E1").Value = "Коригирана сертифицирана сума - СФ";
            ws.Cell("F1").Value = "№ на ГСО";
            ws.Cell("G1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCertAuthorityCorrection in contractReportCertAuthorityCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCertAuthorityCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCertAuthorityCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCertAuthorityCorrection.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCertAuthorityCorrection.CertifiedBfpTotalAmount;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCertAuthorityCorrection.CertifiedSelfAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCertAuthorityCorrection.AnnualAccountReportOrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCertAuthorityCorrection.Description;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportCertCorrectionWorkbook(IList<ContractReportCertCorrectionVO> contractReportCertCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Изр. на СС на други нива");

            // Headers
            ws.Cell("A1").Value = "Кор. БФП";
            ws.Cell("B1").Value = "Кор. СФ";
            ws.Cell("C1").Value = "Програма";
            ws.Cell("D1").Value = "Номер";
            ws.Cell("E1").Value = "Статус";
            ws.Cell("F1").Value = "Вид";
            ws.Cell("G1").Value = "Дата";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportCertCorrection in contractReportCertCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportCertCorrection.BfpTotalAmount;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportCertCorrection.SelfAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportCertCorrection.ProgrammeName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportCertCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportCertCorrection.RegNumber;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportCertCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportCertCorrection.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractReportFinancialCertCorrectionWorkbook(IList<ContractReportFinancialCertCorrectionVO> contractReportFinancialCertCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Изр. на СС на ниво РОД");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Договор";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Номер на пакет";
            ws.Cell("E1").Value = "Статус";
            ws.Cell("F1").Value = "Пореден номер";
            ws.Cell("G1").Value = "Дата на създаване";
            ws.Cell("H1").Value = "Бележка";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReportFinancialCertCorrection in contractReportFinancialCertCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReportFinancialCertCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReportFinancialCertCorrection.ContractName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReportFinancialCertCorrection.ProcedureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReportFinancialCertCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReportFinancialCertCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReportFinancialCertCorrection.OrderNum;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReportFinancialCertCorrection.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReportFinancialCertCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }
    }
}
