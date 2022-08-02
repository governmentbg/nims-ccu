using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    public class ProceduresExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;

        public ProceduresExcelController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.authorizer = authorizer;
        }

        [Route("api/procedures/excelExport")]
        public HttpResponseMessage GetEvalSessionSheets(int? programmeId = null, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(ProcedureListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProcedurePermissions.CanRead);

            var procedures = this.proceduresRepository.GetProcedures(programmeIds, programmeId, programmePriorityId);
            var workbook = this.GetWorkbook(procedures);

            return this.Request.CreateXmlResponse(workbook, "procedures");
        }

        private XLWorkbook GetWorkbook(IList<ProcedureVO> procedures)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Процедури");

            // Headers
            ws.Cell("A1").Value = "Код";
            ws.Cell("B1").Value = "Наименование";
            ws.Cell("C1").Value = "Оперативна програма";
            ws.Cell("D1").Value = "Дата на активиране";
            ws.Cell("E1").Value = "Краен срок за кандидатстване";
            ws.Cell("F1").Value = "Статус";
            ws.Cell("G1").Value = "Фин. от НФ";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var procedure in procedures)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = procedure.Code;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = procedure.Name;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = string.Join(",", procedure.ProgrammeNames);

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = procedure.ActivationDate.HasValue ?
                    procedure.ActivationDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = procedure.EndingDate.HasValue ?
                    procedure.EndingDate.Value.ToString("dd.MM.yyyy") :
                    null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = procedure.Status.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = procedure.BgAmount.HasValue ? procedure.BgAmount.Value : (decimal?)null;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 70;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Column(3).Width = 50;
            ws.Column(3).Style.Alignment.SetWrapText();
            ws.Columns(4, 7).AdjustToContents();

            return workbook;
        }
    }
}
