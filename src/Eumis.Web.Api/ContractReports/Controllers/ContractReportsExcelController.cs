using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    public class ContractReportsExcelController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IAuthorizer authorizer;

        public ContractReportsExcelController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IContractReportsRepository contractReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/contractReports/excelExport")]
        public HttpResponseMessage GetContractReports(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractReportPermissions.CanRead);

            var contractReports = this.contractReportsRepository.GetContractReports(programmeIds, contractRegNum, procedureId, contractReportNum);

            var workbook = this.GetWorkbook(contractReports);

            return this.Request.CreateXmlResponse(workbook, "reports");
        }

        private XLWorkbook GetWorkbook(IList<ContractReportVO> contractReports)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПОД");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Договор";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Пореден номер";
            ws.Cell("F1").Value = "Тип";
            ws.Cell("G1").Value = "Въведен от";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReport in contractReports)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReport.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReport.ContractName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReport.ProcedureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReport.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReport.OrderNum;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReport.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReport.Source.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 50;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Column(3).Width = 50;
            ws.Column(3).Style.Alignment.SetWrapText();
            ws.Columns(4, 7).AdjustToContents();

            return workbook;
        }
    }
}
