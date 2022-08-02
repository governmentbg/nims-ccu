using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    public class EvalSessionSheetsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAuthorizer authorizer;

        public EvalSessionSheetsExcelController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/evalSessions/{evalSessionId:int}/sheetsExcelExport")]
        public HttpResponseMessage GetEvalSessionSheets(
            int evalSessionId,
            int? project = null,
            ProcedureEvalTableType? evalTableType = null,
            int? distribution = null,
            int? assessor = null,
            [FromUri] EvalSessionSheetStatus[] statuses = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var sheets = this.evalSessionsRepository.GetEvalSessionSheets(evalSessionId, project, evalTableType, distribution, assessor, null, statuses);
            var workbook = this.GetWorkbook(sheets);

            var evalSessionNumber = this.evalSessionsRepository.GetEvalSessionNumber(evalSessionId);
            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_sheets", evalSessionNumber));
        }

        private XLWorkbook GetWorkbook(IList<EvalSessionSheetsVO> sheets)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add(WebApiTexts.EvalSessionSheets_ExportExcel_SheetName);

            // Headers
            ws.Cell("A1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_ProjectProposal;
            ws.Range("A1", "B1").Merge();
            ws.Cell("A2").Value = WebApiTexts.EvalSessionSheets_ExportExcel_RegNumber;
            ws.Cell("B2").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Name;
            ws.Cell("C1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Member;
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_StageType;
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_DistributionType;
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Status;
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Pass;
            ws.Range("G1", "G2").Merge();
            ws.Cell("H1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Points;
            ws.Range("H1", "H2").Merge();
            ws.Cell("I1").Value = WebApiTexts.EvalSessionSheets_ExportExcel_Comment;
            ws.Range("I1", "I2").Merge();

            var rngHeaders = ws.Range(1, 1, 2, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var sheet in sheets)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = sheet.ProjectRegNumber;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = sheet.ProjectName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = sheet.Assessor;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = sheet.EvalTableTypeName.HasValue ?
                    sheet.EvalTableTypeName.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = sheet.DistributionType.HasValue ?
                    sheet.DistributionType.Value.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = sheet.Status.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = sheet.EvalIsPassed.HasValue ?
                    (sheet.EvalIsPassed.Value ? WebApiTexts.EvalSessionSheets_ExportExcel_Yes : WebApiTexts.EvalSessionSheets_ExportExcel_No) :
                    null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "H").Value = sheet.EvalPoints;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = sheet.EvalNote;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 50;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Column(3).AdjustToContents();
            ws.Column(4).Width = 10;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Column(5).Width = 17;
            ws.Column(5).Style.Alignment.SetWrapText();
            ws.Column(7).Width = 11;
            ws.Column(7).Style.Alignment.SetWrapText();
            ws.Column(8).AdjustToContents();
            ws.Column(9).Width = 30;
            ws.Column(9).Style.Alignment.SetWrapText();

            return workbook;
        }
    }
}
