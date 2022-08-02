using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    public class ContractContractCommunicationsExcelController : ApiController
    {
        private IContractsRepository contractsRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ContractContractCommunicationsExcelController(
            IContractsRepository contractsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("api/contracts/{contractId:int}/contractCommunications/excelExport")]
        public HttpResponseMessage GetContractCommunications(int contractId)
        {
            this.authorizer.AssertCanDo(ContractCommunicationListActions.Search);

            var contractRegNumber = this.contractsRepository.GetRegNumber(contractId);
            var contractCommunications = this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, ContractCommunicationType.Administrative);

            var workbook = this.GetWorkbook(contractCommunications);

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_communication", contractRegNumber));
        }

        [Route("api/contracts/contractCommunications/excelExport")]
        public HttpResponseMessage GetAllCommunications(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Source? source = null)
        {
            this.authorizer.AssertCanDo(ContractCommunicationListActions.Search);
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractCommunicationPermissions.CanRead);

            var contractCommunications = this.contractCommunicationXmlsRepository.GetAllCommunications(
                programmeIds,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                source);

            var workbook = this.GetAdminAuthorityWorkbook(contractCommunications);

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_communication", DateTime.Now));
        }

        private XLWorkbook GetAdminAuthorityWorkbook(IList<AdminAuthorityContractCommunicationVO> contractCommunications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("КД");

            // Headers
            ws.Cell("A1").Value = "Дата на изпращане";
            ws.Cell("B1").Value = "Дата на първо отваряне";
            ws.Cell("C1").Value = "Рег. номер договор";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Изпратено от";
            ws.Cell("F1").Value = "Рег. номер";
            ws.Cell("G1").Value = "Тема";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractCommunication in contractCommunications)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractCommunication.SendDate != null ?
                    contractCommunication.SendDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractCommunication.ReadDate != null ?
                    contractCommunication.ReadDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractCommunication.ContractRegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractCommunication.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractCommunication.Source.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractCommunication.RegNumber;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractCommunication.Subject;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetWorkbook(IList<ContractCommunicationVO> contractCommunications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("КД");

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

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractCommunication.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractCommunication.Subject;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractCommunication.SendDate != null ?
                    contractCommunication.SendDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractCommunication.ReadDate != null ?
                    contractCommunication.ReadDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();

            return workbook;
        }
    }
}
