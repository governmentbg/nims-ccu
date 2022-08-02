using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    public class ProjectManagingAuthorityCommunicationsExcelController : ApiController
    {
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ProjectManagingAuthorityCommunicationsExcelController(
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("api/projectManagingAuthorityCommunications/excelExport")]
        public HttpResponseMessage GetAllCommunications(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            ProjectManagingAuthorityCommunicationSource? source = null)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationListActions.Search);
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            var contractCommunications = this.projectManagingAuthorityCommunicationsRepository.GetAllCommunications(
                programmeIds,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                source);

            var workbook = this.GetWorkbook(contractCommunications);

            return this.Request.CreateXmlResponse(workbook, string.Format("{0}_communication", DateTime.Now));
        }

        private XLWorkbook GetWorkbook(IList<ProjectManagingAuthorityCommunicationVO> projectCommunications)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Комуникация към ПП");

            // Headers
            ws.Cell("A1").Value = "Дата на изпращане";
            ws.Cell("B1").Value = "Дата на първо отваряне";
            ws.Cell("C1").Value = "Дата на отговор";
            ws.Cell("D1").Value = "Рег. номер на ПП";
            ws.Cell("E1").Value = "Статус";
            ws.Cell("F1").Value = "Изпратено от";
            ws.Cell("G1").Value = "Рег. номер";
            ws.Cell("H1").Value = "Тема";

            var rngHeaders = ws.Range(1, 1, 1, 8);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var projectCommunication in projectCommunications)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = projectCommunication.QuestionSendDate != null ?
                    projectCommunication.QuestionSendDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = projectCommunication.QuestionReadDate != null ?
                    projectCommunication.QuestionReadDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = projectCommunication.AnswerDate != null ?
                    projectCommunication.AnswerDate.Value.ToString("dd.MM.yyyy HH:mm") : null;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = projectCommunication.ProjectRegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = projectCommunication.Status.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = projectCommunication.Source.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = projectCommunication.RegNumber;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = projectCommunication.Subject.GetEnumDescription();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 8).AdjustToContents();

            return workbook;
        }
    }
}
