using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Web.Api.Projects.DataObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierExcelController(
            IAuthorizer authorizer,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/projectVersions/excelExport")]
        public HttpResponseMessage GetProjectVersions(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var projectVersions = this.projectVersionXmlsRepository.GetProjectVersions(projectId);
            var workbook = this.GetProjectVersionWorkbook(projectVersions);

            return this.Request.CreateXmlResponse(workbook, "projectVersions");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/projectCommunications/excelExport")]
        public HttpResponseMessage GetProjectCommunications(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var communications = this.projectCommunicationsRepository.GetProjectCommunications(projectId);
            var workbook = this.GetCommunicationWorkbook(communications);

            return this.Request.CreateXmlResponse(workbook, "projectCommunications");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/projectEvaulations/excelExport")]
        public HttpResponseMessage GetProjectEvaulations(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var evalSessionEvaluations = this.evalSessionsRepository.GetProjectEvalSessionEvaluations(projectId);
            var workbook = this.GetEvalSessionEvaluationWorkbook(evalSessionEvaluations);

            return this.Request.CreateXmlResponse(workbook, "projectEvaulations");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/projectStandings/excelExport")]
        public HttpResponseMessage GetProjectStandings(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var evalSessionProjectStandings = this.evalSessionsRepository.GetProjectEvalSessionProjectStandings(projectId);
            var workbook = this.GetEvalSessionProjectStandingWorkbook(evalSessionProjectStandings);

            return this.Request.CreateXmlResponse(workbook, "projectStandings");
        }

        private XLWorkbook GetProjectVersionWorkbook(IList<ProjectVersionVO> projectVersions)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("История на промените");

            // Headers
            ws.Cell("A1").Value = "Статус";
            ws.Cell("B1").Value = "Бележка";
            ws.Cell("C1").Value = "Дата на създаване";
            ws.Cell("D1").Value = "Дата на последна промяна";

            var rngHeaders = ws.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var projectVersion in projectVersions)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = projectVersion.Status.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = projectVersion.CreateNote;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = projectVersion.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = projectVersion.ModifyDate.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 50;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Columns(3, 4).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetCommunicationWorkbook(IList<CommunicationVO> communications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Комуникация с кандидата");

            // Headers
            ws.Cell("A1").Value = "Номер на сесия";
            ws.Cell("B1").Value = "Дата на изпращане";
            ws.Cell("C1").Value = "Рег. номер";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Краен срок за отговор";
            ws.Cell("F1").Value = "Дата на отговор";

            var rngHeaders = ws.Range(1, 1, 1, 6);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var communication in communications)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = communication.SessionNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = communication.QuestionDate.HasValue ?
                    communication.QuestionDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = communication.RegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = communication.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = communication.QuestionEndingDate.HasValue ?
                    communication.QuestionEndingDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = communication.AnswerDate.HasValue ?
                    communication.AnswerDate.Value.ToString("dd.MM.yyyy") : null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetEvalSessionEvaluationWorkbook(IList<EvalSessionEvaluationVO> evalSessionEvaluations)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Обобщени оценки");

            // Headers
            ws.Cell("A1").Value = "Номер на сесия";
            ws.Cell("B1").Value = "Тип на етап";
            ws.Cell("C1").Value = "Тип на обобщаването";
            ws.Cell("D1").Value = "Преминава";
            ws.Cell("E1").Value = "Точки";
            ws.Cell("F1").Value = "Коментар";
            ws.Cell("G1").Value = "Дата на създаване";
            ws.Cell("H1").Value = "Анулиран";
            ws.Cell("I1").Value = "Причина за анулиране";

            var rngHeaders = ws.Range(1, 1, 1, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var evalSessionEvaluation in evalSessionEvaluations)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = evalSessionEvaluation.EvalSessionNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = evalSessionEvaluation.EvalTableTypeName.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = evalSessionEvaluation.CalculationType.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = evalSessionEvaluation.EvalIsPassed.HasValue ?
                    evalSessionEvaluation.EvalIsPassed.Value ? "Да" : "Не" : null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = evalSessionEvaluation.EvalPoints;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = evalSessionEvaluation.EvalNote;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = evalSessionEvaluation.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = evalSessionEvaluation.IsDeleted ? "Да" : "Не";

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = evalSessionEvaluation.IsDeletedNote;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 9).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetEvalSessionProjectStandingWorkbook(IList<EvalSessionProjectStandingVO> evalSessionProjectStandings)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Класиране");

            // Headers
            ws.Cell("A1").Value = "Номер на сесия";
            ws.Cell("B1").Value = "Предварително";
            ws.Cell("C1").Value = "Пореден номер";
            ws.Cell("D1").Value = "Статус";
            ws.Cell("E1").Value = "Одобрено БФП (лв.)";
            ws.Cell("F1").Value = "Тип";
            ws.Cell("G1").Value = "Бележки";
            ws.Cell("H1").Value = "Дата на създаване";
            ws.Cell("I1").Value = "Анулиран";
            ws.Cell("J1").Value = "Причина за анулиране";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var evalSessionProjectStanding in evalSessionProjectStandings)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = evalSessionProjectStanding.EvalSessionNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = evalSessionProjectStanding.IsPreliminary ? "Да" : "Не";

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = evalSessionProjectStanding.OrderNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = evalSessionProjectStanding.Status.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = evalSessionProjectStanding.GrandAmount;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = evalSessionProjectStanding.Type.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = evalSessionProjectStanding.Notes;

                ws.Cell(rowIndex, "H").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = evalSessionProjectStanding.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = evalSessionProjectStanding.IsDeleted ? "Да" : "Не";

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = evalSessionProjectStanding.IsDeletedNote;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();
            ws.Column(7).Width = 50;
            ws.Column(7).Style.Alignment.SetWrapText();
            ws.Columns(8, 10).AdjustToContents();

            return workbook;
        }
    }
}
