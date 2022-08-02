using System;
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
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    public class EvalSessionCommunicationsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IAuthorizer authorizer;

        public EvalSessionCommunicationsExcelController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/evalSessions/{evalSessionId:int}/communicationsExcelExport")]
        public HttpResponseMessage GetEvalSessionCommunications(
            int evalSessionId,
            int? projectId = null,
            ProjectCommunicationStatus? statusId = null,
            DateTime? questionDateFrom = null,
            DateTime? questionDateTo = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var communications = this.projectCommunicationsRepository.GetProjectCommunicationsForEvalSession(evalSessionId, projectId, statusId, questionDateFrom, questionDateTo);
            var workbook = this.GetWorkbook(communications);

            var evalSessionNumber = this.evalSessionsRepository.GetEvalSessionNumber(evalSessionId);
            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_communications", evalSessionNumber));
        }

        private XLWorkbook GetWorkbook(IList<EvalSessionCommunicationVO> communications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("К");

            // Headers
            ws.Cell("A1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_DateCreated;
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_Status;
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_Number;
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_QuestionDate;
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_ReplyTerm;
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_AnswerDate;
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_OpenDate;
            ws.Range("G1", "G2").Merge();
            ws.Cell("H1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_ProjecProposal;
            ws.Range("H1", "I1").Merge();
            ws.Cell("H2").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_RegNumber;
            ws.Cell("I2").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_Name;
            ws.Cell("J1").Value = WebApiTexts.EvalSessionCommunications_ExportExcel_Candidate;
            ws.Range("J1", "J2").Merge();

            var rngHeaders = ws.Range(1, 1, 2, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var communication in communications)
            {
                ws.Cell(rowIndex, "A").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = communication.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = communication.Status.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = communication.RegNumber;

                ws.Cell(rowIndex, "D").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = communication.QuestionDate.HasValue ?
                    communication.QuestionDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "E").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = communication.QuestionEndingDate.HasValue ?
                    communication.QuestionEndingDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "F").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = communication.AnswerDate.HasValue ?
                    communication.AnswerDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "G").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = communication.QuestionReadDate.HasValue ?
                    communication.QuestionReadDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = communication.ProjectRegNumber;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = communication.ProjectName;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = communication.CompanyName;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();
            ws.Columns(8, 9).Width = 50;
            ws.Columns(8, 9).Style.Alignment.SetWrapText();

            return workbook;
        }
    }
}
