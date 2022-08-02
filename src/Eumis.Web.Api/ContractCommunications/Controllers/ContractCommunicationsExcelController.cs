using ClosedXML.Excel;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    public abstract class ContractCommunicationsExcelController : ApiController
    {
        private ContractCommunicationType type;
        private IContractsRepository contractsRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;

        public ContractCommunicationsExcelController(
            ContractCommunicationType type,
            IContractsRepository contractsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository)
        {
            this.type = type;
            this.contractsRepository = contractsRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
        }

        protected abstract void AssertPermissions(int contractId);

        [Route("excelExport")]
        public HttpResponseMessage GetContractCommunications(int contractId)
        {
            this.AssertPermissions(contractId);

            var contractRegNumber = this.contractsRepository.GetRegNumber(contractId);
            var contractCommunications = this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, this.type);

            var workbook = this.GetWorkbook(contractCommunications);

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_communication", contractRegNumber));
        }

        private XLWorkbook GetWorkbook(IList<ContractCommunicationVO> contractCommunications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("КД");

            // Headers
            ws.Cell("A1").Value = "Статус";
            ws.Cell("B1").Value = "Изпратено от";
            ws.Cell("C1").Value = "Тема";
            ws.Cell("D1").Value = "Рег. номер";
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
                ws.Cell(rowIndex, "C").Value = contractCommunication.Subject;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractCommunication.RegNumber;

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
