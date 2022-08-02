using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Excel;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    public class IrregularitySignalsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IIrregularitySignalsRepository irregularitySignalsRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;

        public IrregularitySignalsExcelController(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IIrregularitySignalsRepository irregularitySignalsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
        }

        [Route("api/irregularitySignals/registerExcelExport")]
        public HttpResponseMessage GetIrregularitySignalRegister(int? irregularitySignalId = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Search);
            var programmeIds = System.Array.Empty<int>();

            var report = this.irregularitySignalsRepository.GetIrregularitySignalRegister(programmeIds, this.accessContext.UserId, irregularitySignalId);
            var involvedPersons = this.irregularitySignalsRepository.GetSignalReportInvolvedPersons(report.Select(i => i.IrregularitySignalId).ToArray());

            var workbook = new XLWorkbook();
            this.AddIrrSignalRegisterSheet(workbook, report);
            this.AddInvolvedPersonsSheet(workbook, involvedPersons);

            return this.Request.CreateXmlResponse(workbook, "irregularity_signal_register");
        }

        private void AddIrrSignalRegisterSheet(XLWorkbook workbook, IList<IrregularitySignalRegisterItemVO> irrSignals)
        {
            var ws = workbook.Worksheets.Add("Регистър сигнали за нередности");

            // Headers
            ws.Cell("A1").Value = "№ на сигнала";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Дата на регистриране на сигнала в деловодството на структурата";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Програма";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Фонд";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Име на проекта";
            ws.Range("E1", "E2").Merge();
            ws.Cell("F1").Value = "Номер на проекта";
            ws.Range("F1", "F2").Merge();
            ws.Cell("G1").Value = "Описание на нарушението";
            ws.Range("G1", "G2").Merge();
            ws.Cell("H1").Value = "Източник на сигнала";
            ws.Range("H1", "H2").Merge();
            ws.Cell("I1").Value = "Предприети действия по сигнала";
            ws.Range("I1", "I2").Merge();
            ws.Cell("J1").Value = "Състояние на сигнала (активен или приключен)";
            ws.Range("J1", "J2").Merge();
            ws.Cell("K1").Value = "Рег. №  и дата на акта по чл. 14 ";
            ws.Range("K1", "K2").Merge();
            ws.Cell("L1").Value = "Резултат от проверката";
            ws.Range("L1", "M1").Merge();
            ws.Cell("L2").Value = "Установена нередност (да/не)";
            ws.Cell("M2").Value = "Номер на установената нередност";
            ws.Cell("N1").Value = "Коментари";
            ws.Range("N1", "N2").Merge();

            var rngHeaders = ws.Range("A1", "N2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "N2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var irrSignal in irrSignals)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = irrSignal.IrregularitySignalRegNumber;

                ws.Cell(rowIndex, "B").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = irrSignal.MASystemRegDate.HasValue ?
                    irrSignal.MASystemRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = irrSignal.ProgrammeName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = string.IsNullOrEmpty(irrSignal.ContractName) ? irrSignal.ProjectName : irrSignal.ContractName;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = string.IsNullOrEmpty(irrSignal.ContractRegNumber) ? irrSignal.ProjectRegNumber : irrSignal.ContractRegNumber;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = irrSignal.ViolationDesrc.TruncateWithEllipsis(ExcelHelper.CellLengthLimit);

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = irrSignal.SignalSource;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = irrSignal.Actions.TruncateWithEllipsis(ExcelHelper.CellLengthLimit);

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = irrSignal.Status.GetEnumDescription();

                ws.Cell(rowIndex, "K").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = irrSignal.ActRegNum + " " + (irrSignal.ActRegDate.HasValue ?
                    irrSignal.ActRegDate.Value.ToString("dd.MM.yyyy") : null);

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = irrSignal.IsIrregularityFound ? "Да" : "Не";

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = irrSignal.IrregularityRegNumber;

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "N").Value = irrSignal.Comment.TruncateWithEllipsis(ExcelHelper.CellLengthLimit);

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 10;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).AdjustToContents();
            ws.Column(4).Width = 10;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 6).AdjustToContents();
            ws.Column(7).Width = 40;
            ws.Column(8).Width = 40;
            ws.Column(9).Width = 40;
            ws.Columns(7, 9).Style.Alignment.SetWrapText();
            ws.Columns(10, 11).AdjustToContents();
            ws.Column(12).Width = 15;
            ws.Column(13).Width = 20;
            ws.Column(14).Width = 40;
            ws.Columns(12, 14).AdjustToContents();
        }

        private void AddInvolvedPersonsSheet(XLWorkbook workbook, IList<IrregularitySignalRegisterInvolvedPersonVO> persons)
        {
            if (persons.Count == 0)
            {
                return;
            }

            var ws = workbook.Worksheets.Add("Замесени лица");

            // Headers
            ws.Cell("A1").Value = "№ на сигнала";
            ws.Cell("B1").Value = "Тип";
            ws.Cell("C1").Value = "УИН";
            ws.Cell("D1").Value = "Име";
            ws.Cell("E1").Value = "Презиме";
            ws.Cell("F1").Value = "Фамилия";
            ws.Cell("G1").Value = "Наименование на фирмата";
            ws.Cell("H1").Value = "Търговско име";
            ws.Cell("I1").Value = "Име на холдинга";

            var rngHeaders = ws.Range("A1", "I1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var person in persons)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = person.IrregularitySignalRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = person.LegalType.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = person.Uin;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = person.FirstName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = person.MiddleName;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = person.LastName;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = person.CompanyName;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = person.TradeName;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = person.HoldingName;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 9).AdjustToContents();
        }
    }
}
