using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    public class ContractsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;

        public ContractsExcelController(
            IUnitOfWork unitOfWork,
            IContractsRepository contractsRepository,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractsRepository;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.authorizer = authorizer;
        }

        [Route("api/contracts/excelExport")]
        public HttpResponseMessage GetEvalSessionSheets(int? programmePriorityId = null, int? procedureId = null)
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            var contracts = this.contractsRepository.GetContracts(programmeIds, programmePriorityId, procedureId, true, userId: this.accessContext.UserId);
            var workbook = this.GetWorkbook(contracts);

            return this.Request.CreateXmlResponse(workbook, "contracts");
        }

        private XLWorkbook GetWorkbook(IList<ContractVO> contracts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Договори");

            // Headers
            ws.Cell("A1").Value = "Процедура";
            ws.Cell("B1").Value = "Номер";
            ws.Cell("C1").Value = "Дата на сключване";
            ws.Cell("D1").Value = "Наименование";
            ws.Cell("E1").Value = "Статус на изпълнение";
            ws.Cell("F1").Value = "Бенефициент ";
            ws.Cell("G1").Value = "КО по КИД 2008";
            ws.Cell("H1").Value = "БФП - Общо";
            ws.Cell("I1").Value = "Собств. съфинансиране";

            var rngHeaders = ws.Range(1, 1, 1, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contract in contracts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contract.ProcedureName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contract.RegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contract.ContractDate.HasValue ?
                    contract.ContractDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contract.Name;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contract.ExecutionStatus.HasValue ?
                    contract.ExecutionStatus.Value.GetEnumDescription() : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contract.Company;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contract.CompanyKidCode;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contract.TotalBfpAmount.HasValue ? contract.TotalBfpAmount.Value : (decimal?)null;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contract.TotalSelfAmount.HasValue ? contract.TotalSelfAmount.Value : (decimal?)null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 3).AdjustToContents();
            ws.Column(4).Width = 50;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 9).AdjustToContents();

            return workbook;
        }
    }
}
