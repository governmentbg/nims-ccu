using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    public class ContractReimbursedAmountsExcelController : ApiController
    {
        private IContractReimbursedAmountsRepository contractReimbursedAmountsRepository;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;

        public ContractReimbursedAmountsExcelController(
            IContractReimbursedAmountsRepository contractReimbursedAmountsRepository,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IAuthorizer authorizer)
        {
            this.contractReimbursedAmountsRepository = contractReimbursedAmountsRepository;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.authorizer = authorizer;
        }

        [Route("api/contarctReimbursedAmounts/excelExport")]
        public HttpResponseMessage GetContarctReimbursedAmounts(int? contractId = null, ReimbursementType? type = null)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            var reimbursedAmounts = this.contractReimbursedAmountsRepository.GetReimbursedAmounts(programmeIds, this.accessContext.UserId, contractId, type);
            var workbook = this.GetWorkbook(reimbursedAmounts);

            return this.Request.CreateXmlResponse(workbook, "contarctReimbursedAmounts");
        }

        private XLWorkbook GetWorkbook(IList<ContractReimbursedAmountVO> reimbursedAmounts)
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
            foreach (var reimbursedAmount in reimbursedAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = reimbursedAmount.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = reimbursedAmount.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = reimbursedAmount.StatusDescr.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = reimbursedAmount.RegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = reimbursedAmount.Type.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = reimbursedAmount.Reimbursement.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = reimbursedAmount.ReimbursementDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = reimbursedAmount.PrincipalEuAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = reimbursedAmount.PrincipalBgAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = reimbursedAmount.PrincipalTotalAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = reimbursedAmount.InterestEuAmount;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = reimbursedAmount.InterestBgAmount;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = reimbursedAmount.InterestTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 13).AdjustToContents();

            return workbook;
        }
    }
}
