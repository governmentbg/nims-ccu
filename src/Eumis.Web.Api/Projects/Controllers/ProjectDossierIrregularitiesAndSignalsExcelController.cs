using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Core.Relations;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierIrregularitiesAndSignalsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IIrregularitiesRepository irregularitiesRepository;
        private IIrregularitySignalsRepository irregularitySignalsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierIrregularitiesAndSignalsExcelController(
            IAuthorizer authorizer,
            IIrregularitiesRepository irregularitiesRepository,
            IIrregularitySignalsRepository irregularitySignalsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.irregularitiesRepository = irregularitiesRepository;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/irregularities/excelExport")]
        public HttpResponseMessage GetIrregularities(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var irregularities = this.irregularitiesRepository.GetIrregularitiesForProjectDossier(contractId);
            var workbook = this.GetIrregularityWorkbook(irregularities);

            return this.Request.CreateXmlResponse(workbook, "irregularities");
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/irregularitySignals/excelExport")]
        public HttpResponseMessage GetIrregularitySignals(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var irregularitySignals = this.irregularitySignalsRepository.GetIrregularitySignalsForProjectDossier(projectId, contractId);
            var workbook = this.GetIrregularitySignalWorkbook(irregularitySignals);

            return this.Request.CreateXmlResponse(workbook, "irregularitySignals");
        }

        private XLWorkbook GetIrregularityWorkbook(IList<IrregularityVO> irregularities)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Нередности");

            // Headers
            ws.Cell("A1").Value = "№ сигнал";
            ws.Cell("B1").Value = "Програма";
            ws.Cell("C1").Value = "№ договор";
            ws.Cell("D1").Value = "Национален номер";
            ws.Cell("E1").Value = "Бенефициент";

            var rngHeaders = ws.Range(1, 1, 1, 5);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var irregularitie in irregularities)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = irregularitie.SignalNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = irregularitie.ProgrammeName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = irregularitie.ContractRegNumber;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = irregularitie.RegNumber;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = irregularitie.Company;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 5).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetIrregularitySignalWorkbook(IList<IrregularitySignalVO> irregularitySignals)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Сигнали за нередности");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "№ договор/ПП";
            ws.Cell("C1").Value = "Статус";
            ws.Cell("D1").Value = "Установена нередност";

            var rngHeaders = ws.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var irregularitySignal in irregularitySignals)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = irregularitySignal.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = irregularitySignal.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = irregularitySignal.Status.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = irregularitySignal.IsIrregularityFound ? "Да" : "Не";

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();

            return workbook;
        }
    }
}
