using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ContractAccessCodes.Controllers
{
    public class ContractAccessCodesExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IUserClaimsContext currentUserClaimsContext;
        private IContractAccessCodesRepository contractAccessCodesRepository;

        public ContractAccessCodesExcelController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory,
            IContractAccessCodesRepository contractAccessCodesRepository)
        {
            this.authorizer = authorizer;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }

            this.contractAccessCodesRepository = contractAccessCodesRepository;
        }

        [Route("api/contractAccessCodes/excelExport")]
        public HttpResponseMessage GetContractRegistrations()
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            var isSuperUser = this.currentUserClaimsContext.IsSuperUser;

            var contractAccessCodes = this.contractAccessCodesRepository.GetContractAccessCodes(isSuperUser);

            var workbook = this.GetWorkbook(contractAccessCodes, isSuperUser);

            return this.Request.CreateXmlResponse(workbook, "contractAccessCodes");
        }

        private XLWorkbook GetWorkbook(IList<ContractAccessCodeVO> contractAccessCodes, bool isSuperUser)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Кодове за достъп");

            // Headers
            ws.Cell("A1").Value = "Ел. поща";
            ws.Cell("B1").Value = "Код";
            ws.Cell("C1").Value = "Име";
            ws.Cell("D1").Value = "Фамилия";
            ws.Cell("E1").Value = "Активен";
            ws.Cell("F1").Value = "Договор";
            ws.Cell("G1").Value = "Дата на създаване";
            ws.Cell("H1").Value = "Дата на последна промяна";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var accessCode in contractAccessCodes)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = accessCode.Email;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = accessCode.Code;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = accessCode.FirstName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = accessCode.LastName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = accessCode.IsActive ? "Да" : "Не";

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = accessCode.ContractRegNumber;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.DateTime;
                ws.Cell(rowIndex, "G").Value = accessCode.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "H").DataType = XLCellValues.DateTime;
                ws.Cell(rowIndex, "H").Value = accessCode.ModifyDate.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            if (!isSuperUser)
            {
                ws.Column("B").Delete();
            }

            return workbook;
        }
    }
}
