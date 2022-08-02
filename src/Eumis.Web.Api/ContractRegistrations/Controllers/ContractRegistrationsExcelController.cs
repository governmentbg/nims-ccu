using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.ContractRegistrations.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ContractRegistrations.Controllers
{
    public class ContractRegistrationsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractRegistrationsRepository contractRegistrationsRepository;
        private IAuthorizer authorizer;

        public ContractRegistrationsExcelController(
            IUnitOfWork unitOfWork,
            IContractRegistrationsRepository contractRegistrationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.contractRegistrationsRepository = contractRegistrationsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/contractRegistrations/excelExport")]
        public HttpResponseMessage GetContractRegistrations()
        {
            this.authorizer.AssertCanDo(ContractRegistrationListActions.Search);

            var contractRegistrations = this.contractRegistrationsRepository.GetContractRegistrations(null, null, null, null, null, null);

            var workbook = this.GetWorkbook(contractRegistrations);

            return this.Request.CreateXmlResponse(workbook, "contractRegs");
        }

        private XLWorkbook GetWorkbook(IList<ContractRegistrationsVO> contractRegistrations)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПДД");

            // Headers
            ws.Cell("A1").Value = "Ел. поща";
            ws.Cell("B1").Value = "Собствено име";
            ws.Cell("C1").Value = "Фамилия";
            ws.Cell("D1").Value = "Телефон";
            ws.Cell("E1").Value = "Договори";

            var rngHeaders = ws.Range(1, 1, 1, 5);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractRegistration in contractRegistrations)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractRegistration.Email;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractRegistration.FirstName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractRegistration.LastName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractRegistration.Phone;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = string.Join(", ", contractRegistration.Contracts.Select(c => c.RegNumber));

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();
            ws.Column(5).Width = 100;
            ws.Column(5).Style.Alignment.SetWrapText();

            return workbook;
        }
    }
}
