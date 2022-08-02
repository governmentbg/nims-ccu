using System.Collections.Generic;
using System.Linq;
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

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    public class EvalSessionProjectsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAuthorizer authorizer;

        public EvalSessionProjectsExcelController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/evalSessions/{evalSessionId:int}/projectsExcelExport")]
        public HttpResponseMessage GetEvalSessionProjects(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var projects = this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId);
            var workbook = this.GetWorkbook(projects);

            var evalSessionNumber = this.evalSessionsRepository.GetEvalSessionNumber(evalSessionId);
            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_projects", evalSessionNumber));
        }

        private XLWorkbook GetWorkbook(IList<EvalSessionProjectsVO> projects)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add(WebApiTexts.EvalSessionSheets_ExportExcel_SheetName);

            var showAdminAdmissResult = projects.Any(p => p.IsPassedASD.HasValue);
            var showTechFinanceResult = projects.Any(p => p.IsPassedTFO.HasValue);
            var showComplexResult = projects.Any(p => p.IsPassedComplex.HasValue);

            // Headers
            ws.Cell("A1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_RegNumber;
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_Name;
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_PCCEA;
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_Candidate;
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_CA;
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_RegistrationDate;
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_RegStatus;
            ws.Range("G1", "G2").Merge();
            ws.Cell("H1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_Annulled;
            ws.Range("H1", "H2").Merge();
            ws.Cell("I1").Value = WebApiTexts.EvalSessionProjects_ExportExcel_WorkStatus;
            ws.Range("I1", "I2").Merge();

            var colIndex = 10;
            if (showAdminAdmissResult)
            {
                ws.Cell(1, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_ASD;
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Pass;
                ws.Cell(2, colIndex + 1).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Points;

                colIndex += 2;
            }

            if (showTechFinanceResult)
            {
                ws.Cell(1, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_TFA;
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Pass;
                ws.Cell(2, colIndex + 1).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Points;

                colIndex += 2;
            }

            if (showComplexResult)
            {
                ws.Cell(1, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_CA;
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Pass;
                ws.Cell(2, colIndex + 1).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Points;

                colIndex += 2;
            }

            ws.Cell(1, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Standing;
            ws.Range(1, colIndex, 1, colIndex + 1).Merge();
            ws.Cell(2, colIndex).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Number;
            ws.Cell(2, colIndex + 1).Value = WebApiTexts.EvalSessionProjects_ExportExcel_Status;

            var rngHeaders = ws.Range(1, 1, 2, colIndex + 1);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, colIndex + 1).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var project in projects)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = project.ProjectRegNumber;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = project.ProjectName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = project.ProjectKidCode;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = project.Company;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = project.CompanyKidCode;

                ws.Cell(rowIndex, "F").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = project.ProjectRegDate.HasValue ?
                    project.ProjectRegDate.Value.ToString("dd.MM.yyyy HH:mm") :
                    null;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = project.ProjectRegistrationStatus.HasValue ?
                    project.ProjectRegistrationStatus.Value.GetEnumDescription() :
                    null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = project.IsDeleted ? WebApiTexts.EvalSessionSheets_ExportExcel_Yes : WebApiTexts.EvalSessionSheets_ExportExcel_No;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = project.WorkStatus.HasValue ?
                    project.WorkStatus.Value.GetEnumDescription() :
                    null;

                colIndex = 10;
                if (showAdminAdmissResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.IsPassedASD.HasValue ?
                        (project.IsPassedASD.Value ? WebApiTexts.EvalSessionSheets_ExportExcel_Yes : WebApiTexts.EvalSessionSheets_ExportExcel_No) :
                        null;

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.PointsASD;

                    colIndex += 2;
                }

                if (showTechFinanceResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.IsPassedTFO.HasValue ?
                        (project.IsPassedTFO.Value ? WebApiTexts.EvalSessionSheets_ExportExcel_Yes : WebApiTexts.EvalSessionSheets_ExportExcel_No) :
                        null;

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.PointsTFO;

                    colIndex += 2;
                }

                if (showComplexResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.IsPassedComplex.HasValue ?
                        (project.IsPassedComplex.Value ? WebApiTexts.EvalSessionSheets_ExportExcel_Yes : WebApiTexts.EvalSessionSheets_ExportExcel_No) :
                        null;

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.PointsComplex;

                    colIndex += 2;
                }

                ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Number;
                ws.Cell(rowIndex, colIndex).Value = project.OrderNum;

                ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Text;
                ws.Cell(rowIndex, colIndex + 1).Value = project.StandingStatus.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 50;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Column(4).Width = 50;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(6, colIndex + 1).AdjustToContents();

            return workbook;
        }
    }
}
