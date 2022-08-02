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
    public class ContractReportChecksExcelController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IAuthorizer authorizer;

        public ContractReportChecksExcelController(
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

        [Route("api/contractReportChecks/excelExport")]
        public HttpResponseMessage GetContractReportChecks(
            string contractRegNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportCheckListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            var contractReports = this.contractReportsRepository.GetContractReportChecksContractReportsExcelExport(programmeIds, contractRegNum);

            var workbook = this.GetWorkbook(contractReports);

            return this.Request.CreateXmlResponse(workbook, "reportChecks");
        }

        private XLWorkbook GetWorkbook(IList<ContractReportExcelVO> contractReports)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ППОД");

            // Headers
            ws.Cell("A1").Value = "Процедура";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Договор";
            ws.Cell("D1").Value = "Пореден номер";
            ws.Cell("E1").Value = "Тип";
            ws.Cell("F1").Value = "Дата на представяне";
            ws.Cell("G1").Value = "Статус";
            ws.Cell("H1").Value = "Верифицирани средства-БФП";
            ws.Cell("I1").Value = "Верифицирани средства-СФ";
            ws.Cell("J1").Value = "Дата на верификация";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var contractReport in contractReports)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = contractReport.ProcedureName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = contractReport.ContractRegNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = contractReport.ContractName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = contractReport.OrderNum;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = contractReport.ReportType.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = contractReport.SubmitDate.HasValue ? contractReport.SubmitDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = contractReport.Status.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = contractReport.ApprovedBfpTotalAmount;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = contractReport.ApprovedSelfAmount;

                ws.Cell(rowIndex, "J").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = contractReport.CheckedDate.HasValue ? contractReport.CheckedDate.Value.ToString("dd.MM.yyyy") : null;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 50;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Column(2).AdjustToContents();
            ws.Column(3).Width = 50;
            ws.Column(3).Style.Alignment.SetWrapText();
            ws.Columns(4, 10).AdjustToContents();

            return workbook;
        }
    }
}
