using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IProjectsRepository projectsRepository;
        private IAuthorizer authorizer;

        public ProjectsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IProjectsRepository projectsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.projectsRepository = projectsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/projects/excelExport")]

        public HttpResponseMessage GetProjectRegistrations(
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string projectNumber = null)
        {
            this.authorizer.AssertCanDo(ProjectListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectPermissions.CanRead);

            var projectsInMemory = this.projectsRepository.GetProjectRegistrations(programmeIds, programmePriorityId, procedureId, fromDate, toDate, projectNumber)
                .ToList();

            var directionsInMemory = this.projectsRepository.GetProjectDirections(projectsInMemory.Select(s => s.ProjectId).ToArray())
                .GroupBy(s => s.ProjectId);

            var mappedDirectionsToProjects = (from project in projectsInMemory
                                              join direction in directionsInMemory on project.ProjectId equals direction.Key into g
                                              from direction in g.DefaultIfEmpty()
                                              select new ProjectRegistrationsWithDirectionsVO
                                              {
                                                  CompanyKidCode = project.CompanyKidCode,
                                                  CompanyNameBg = project.CompanyNameBg,
                                                  CompanyNameEn = project.CompanyNameEn,
                                                  CompanySizeTypeBg = project.CompanySizeTypeBg,
                                                  CompanySizeTypeEn = project.CompanySizeTypeEn,
                                                  CompanyUin = project.CompanyUin,
                                                  CompanyUinType = project.CompanyUinType,
                                                  KidCode = project.KidCode,
                                                  NameBg = project.NameBg,
                                                  NameEn = project.NameEn,
                                                  ProjectId = project.ProjectId,
                                                  ProcedureId = project.ProcedureId,
                                                  ProcedureNameBg = project.ProcedureNameBg,
                                                  ProcedureNameEn = project.ProcedureNameEn,
                                                  ProjectTypeBg = project.ProjectTypeBg,
                                                  ProjectTypeEn = project.ProjectTypeEn,
                                                  RegDate = project.RegDate,
                                                  RegistrationStatus = project.RegistrationStatus,
                                                  RegNumber = project.RegNumber,
                                                  Directions = direction != null ? direction.Select(s => new DirectionItemVO { Direction = s.Direction, SubDirection = s.SubDirection }).ToList() : new List<DirectionItemVO>(),
                                              }).ToList();

            var workbook = this.GetWorkbook(mappedDirectionsToProjects);

            return this.Request.CreateXmlResponse(workbook, "projects");
        }

        private XLWorkbook GetWorkbook(IList<ProjectRegistrationsWithDirectionsVO> projects)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ПП");

            // Headers
            ws.Cell("A1").Value = "Процедура";
            ws.Range("A1", "A2").Merge();

            ws.Cell("B1").Value = "Номер на ПП";
            ws.Range("B1", "B2").Merge();

            ws.Cell("C1").Value = "Наименование";
            ws.Range("C1", "C2").Merge();

            ws.Cell("D1").Value = "Кандидат";
            ws.Range("D1", "F1").Merge();
            ws.Cell("D2").Value = "Име";
            ws.Cell("E2").Value = "Булстат/ЕИК/ЕГН";
            ws.Cell("F2").Value = "Идентификатор";

            ws.Cell("G1").Value = "Дата на регистрация";
            ws.Range("G1", "G2").Merge();

            ws.Cell("H1").Value = "Регистрационен статус";
            ws.Range("H1", "H2").Merge();

            ws.Cell("I1").Value = "Тип";
            ws.Range("I1", "I2").Merge();

            ws.Cell("J1").Value = "Направления";
            ws.Range("J1", "J2").Merge();

            ws.Cell("K1").Value = "Поднаправления";
            ws.Range("K1", "K2").Merge();

            var rngHeaders = ws.Range(1, 1, 2, 11);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, 11).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var project in projects)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = project.ProcedureName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = project.RegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = project.Name;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = project.CompanyName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = project.CompanyUinType.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = project.CompanyUin;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = project.RegDate.HasValue ?
                    project.RegDate.Value.ToString("dd.MM.yyyy HH:mm") :
                    null;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = project.RegistrationStatus.GetEnumDescription();

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = project.ProjectType;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = string.Join(Environment.NewLine, project.Directions.Select(s => s.Direction).ToList());

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = string.Join(Environment.NewLine, project.Directions.Select(s => s.SubDirection).ToList());

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 11).AdjustToContents();

            return workbook;
        }
    }
}
