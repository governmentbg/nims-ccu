using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    public class ProgrammeApplicationDocumentsExcelController : ApiController
    {
        private IProgrammesRepository programmesRepository;
        private IAuthorizer authorizer;

        public ProgrammeApplicationDocumentsExcelController(
            IProgrammesRepository programmesRepository,
            IAuthorizer authorizer)
        {
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("api/programmes/{programmeId}/applicationDocuments/excelExport")]
        public HttpResponseMessage GetProgrammeApplicationDocuments(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var documents = this.programmesRepository.GetProgrammeApplicationDocuments(programmeId);
            var workbook = this.GetWorkbook(documents);

            return this.Request.CreateXmlResponse(workbook, "applicationDocuments");
        }

        private XLWorkbook GetWorkbook(IList<ProgrammeApplicationDocumentsVO> documents)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Документи от кандидатите");

            // Headers
            ws.Cell("A1").Value = "Наименование";
            ws.Cell("B1").Value = "Активен";
            ws.Cell("C1").Value = "Разширение";
            ws.Cell("D1").Value = "Електронен подпис";

            var rngHeaders = ws.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var document in documents)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = document.Name;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = document.IsActive ? "Да" : "Не";

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = document.Extension;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = document.IsSignatureRequired ? "Да" : "Не";

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();

            return workbook;
        }
    }
}
