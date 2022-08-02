using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierContractExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IContractsRepository contractsRepository;
        private IRegOfferXmlsRepository regOfferXmlsRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierContractExcelController(
            IAuthorizer authorizer,
            IContractVersionsRepository contractVersionsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IContractsRepository contractsRepository,
            IRegOfferXmlsRepository regOfferXmlsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.contractsRepository = contractsRepository;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractVersions/excelExport")]
        public HttpResponseMessage GetContractVersions(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractVersions = this.contractVersionsRepository.GetContractVersions(contractId);
            var workbook = this.GetContractVersionWorkbook(contractVersions);

            return this.Request.CreateXmlResponse(workbook, "contractVersions");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractProcurements/excelExport")]
        public HttpResponseMessage GetContractProcurements(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractProcurements = this.contractProcurementsRepository.GetContractProcurements(contractId);
            var workbook = this.GetContractProcurementWorkbook(contractProcurements);

            return this.Request.CreateXmlResponse(workbook, "contractProcurements");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractSpendingPlans/excelExport")]
        public HttpResponseMessage GetContractSpendingPlans(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractSpendingPlans = this.contractSpendingPlansRepository.GetContractSpendingPlans(contractId);
            var workbook = this.GetContractSpendingPlanWorkbook(contractSpendingPlans);

            return this.Request.CreateXmlResponse(workbook, "contractSpendingPlans");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractOffers/excelExport")]
        public HttpResponseMessage GetContractOffers(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            if (this.contractsRepository.FindWithoutIncludes(contractId).ProjectId != projectId)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var contractOffers = this.regOfferXmlsRepository.GetAllForContract(contractId);
            var workbook = this.GetContractOfferWorkbook(contractOffers);

            return this.Request.CreateXmlResponse(workbook, "contractOffers");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/contractCommunications/excelExport")]
        public HttpResponseMessage GetContractCommunications(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contractCommunications = this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, ContractCommunicationType.Administrative);
            var workbook = this.GetContractCommunicationWorkbook(contractCommunications);

            return this.Request.CreateXmlResponse(workbook, "contractCommunications");
        }

        private XLWorkbook GetContractVersionWorkbook(IList<ContractVersionVO> contractVersions)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Договор");

            // Headers
            ws.Cell("A1").Value = "№ на версия";
            ws.Cell("B1").Value = "Тип";
            ws.Cell("C1").Value = "Рег. номер";
            ws.Cell("D1").Value = "Бележки";
            ws.Cell("E1").Value = "БФП";
            ws.Cell("F1").Value = "Дата на сключване";
            ws.Cell("G1").Value = "Статус";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractVersion in contractVersions)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractVersion.VersionNumber;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractVersion.VersionType.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractVersion.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractVersion.CreateNote;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractVersion.TotalBfpAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractVersion.ContractDate.HasValue ?
                    contractVersion.ContractDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractVersion.Status.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 3).AdjustToContents();
            ws.Column(4).Width = 100;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractProcurementWorkbook(IList<ContractProcurementVO> contractProcurements)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПИИСД");

            // Headers
            ws.Cell("A1").Value = "Източник";
            ws.Cell("B1").Value = "Статус";
            ws.Cell("C1").Value = "Дата на създаване";
            ws.Cell("D1").Value = "Дата на промяна";

            var rngHeaders = ws.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractProcurement in contractProcurements)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractProcurement.Source.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractProcurement.Status.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractProcurement.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractProcurement.ModifyDate.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractSpendingPlanWorkbook(IList<ContractSpendingPlanVO> contractSpendingPlans)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПРС");

            // Headers
            ws.Cell("A1").Value = "Източник";
            ws.Cell("B1").Value = "Статус";
            ws.Cell("C1").Value = "Дата на създаване";
            ws.Cell("D1").Value = "Дата на промяна";

            var rngHeaders = ws.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractSpendingPlan in contractSpendingPlans)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractSpendingPlan.Source.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractSpendingPlan.Status.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractSpendingPlan.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractSpendingPlan.ModifyDate.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractOfferWorkbook(IList<ContractOfferVO> contractOffers)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Оферти");

            // Headers
            ws.Cell("A1").Value = "Дата на подаване";
            ws.Cell("B1").Value = "Прогнозна стойност съгласно обявление";
            ws.Cell("C1").Value = "Предмет на процедурата";

            var rngHeaders = ws.Range(1, 1, 1, 3);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractOffer in contractOffers)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractOffer.SubmitDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractOffer.ProcurementPlanExpectedAmount;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractOffer.ProcurementPlanSubject;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 3).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetContractCommunicationWorkbook(IList<ContractCommunicationVO> contractCommunications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Кореспонденция");

            // Headers
            ws.Cell("A1").Value = "Статус";
            ws.Cell("B1").Value = "Изпратено от";
            ws.Cell("C1").Value = "Рег. номер";
            ws.Cell("D1").Value = "Тема";
            ws.Cell("E1").Value = "Дата на изпращане";
            ws.Cell("F1").Value = "Дата на първо отваряне";

            var rngHeaders = ws.Range(1, 1, 1, 6);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractCommunication in contractCommunications)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractCommunication.Status.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractCommunication.Source.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractCommunication.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractCommunication.Subject;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractCommunication.SendDate.HasValue ?
                    contractCommunication.SendDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractCommunication.ReadDate.HasValue ?
                    contractCommunication.ReadDate.Value.ToString("dd.MM.yyyy") : null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();

            return workbook;
        }
    }
}
