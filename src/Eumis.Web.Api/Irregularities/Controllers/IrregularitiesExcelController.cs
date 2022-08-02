using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    public class IrregularitiesExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IIrregularitiesRepository irregularitiesRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;

        public IrregularitiesExcelController(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IIrregularitiesRepository irregularitiesRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.irregularitiesRepository = irregularitiesRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
        }

        [Route("api/irregularities/excelExport")]
        public HttpResponseMessage GetIrregularities(
            Year? reportYear = null,
            Quarter? reportQuarter = null,
            int? programmeId = null,
            IrregularityCaseState? caseState = null)
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Search);

            var programmeIds = System.Array.Empty<int>();
            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(i => i == programmeId).ToArray();
            }

            var irregularities = this.irregularitiesRepository.GetIrrReport(programmeIds, reportYear, reportQuarter, caseState);
            var involvedPersons = this.irregularitiesRepository.GetReportInvolvedPersons(irregularities.Select(i => i.IrregularityVersionId).ToArray());
            var versionsData = this.irregularitiesRepository.GetVersionsData(irregularities.Select(i => i.IrregularityId).ToArray());

            var workbook = new XLWorkbook();
            this.AddIrregularitiesSheet(workbook, irregularities);
            this.AddInvolvedPersonsSheet(workbook, involvedPersons);
            this.AddVersionsDataSheet(workbook, versionsData);

            return this.Request.CreateXmlResponse(workbook, "irregularities");
        }

        [Route("api/irregularities/registerExcelExport")]
        public HttpResponseMessage GetIrrRegister(
            Year? reportYearFrom = null,
            Quarter? reportQuarterFrom = null,
            Year? reportYearTo = null,
            Quarter? reportQuarterTo = null)
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Search);
            var programmeIds = System.Array.Empty<int>();

            var report = this.irregularitiesRepository.GetIrrRegister(programmeIds, reportYearFrom, reportQuarterFrom, reportYearTo, reportQuarterTo);
            var involvedPersons = this.irregularitiesRepository.GetReportInvolvedPersons(report.Select(i => i.VersionId).ToArray());

            var workbook = new XLWorkbook();
            this.AddIrrRegisterSheet(workbook, report);
            this.AddInvolvedPersonsSheet(workbook, involvedPersons);

            return this.Request.CreateXmlResponse(workbook, "irregularities_register");
        }

        private void AddIrregularitiesSheet(XLWorkbook workbook, IList<IrrReportItemVO> irregularities)
        {
            var ws = workbook.Worksheets.Add("Нередности");

            // Headers
            ws.Cell("A1").Value = "Национален номер на случая";
            ws.Range("A1", "A3").Merge();
            ws.Cell("B1").Value = "Фонд";
            ws.Range("B1", "B3").Merge();
            ws.Cell("C1").Value = "Програма";
            ws.Range("C1", "D1").Merge();
            ws.Cell("C2").Value = "Код";
            ws.Range("C2", "C3").Merge();
            ws.Cell("D2").Value = "Наименование";
            ws.Range("D2", "D3").Merge();
            ws.Cell("E1").Value = "Проектно предложение";
            ws.Range("E1", "F1").Merge();
            ws.Cell("E2").Value = "Рег. номер";
            ws.Range("E2", "E3").Merge();
            ws.Cell("F2").Value = "Наименование";
            ws.Range("F2", "F3").Merge();
            ws.Cell("G1").Value = "Сигнал за нередност";
            ws.Range("G1", "K1").Merge();
            ws.Cell("G2").Value = "Номер";
            ws.Range("G2", "G3").Merge();
            ws.Cell("H2").Value = "Дата на регистриране в ИСУН";
            ws.Range("H2", "H3").Merge();
            ws.Cell("I2").Value = "Рег № на акта по чл. 14";
            ws.Range("I2", "I3").Merge();
            ws.Cell("J2").Value = "Дата на акта по чл. 14";
            ws.Range("J2", "J3").Merge();
            ws.Cell("K2").Value = "Източник на първия сигнал";
            ws.Range("K2", "K3").Merge();
            ws.Cell("L1").Value = "Основни данни";
            ws.Range("L1", "Z1").Merge();
            ws.Cell("L2").Value = "Дата на създаване";
            ws.Range("L2", "L3").Merge();
            ws.Cell("M2").Value = "Дата на въвеждане на информацията";
            ws.Range("M2", "M3").Merge();
            ws.Cell("N2").Value = "Докладващ орган";
            ws.Range("N2", "N3").Merge();
            ws.Cell("O2").Value = "Тримесечие";
            ws.Range("O2", "O3").Merge();
            ws.Cell("P2").Value = "Година";
            ws.Range("P2", "P3").Merge();
            ws.Cell("Q2").Value = "Подлежи на докладване до ОЛАФ";
            ws.Range("Q2", "Q3").Merge();
            ws.Cell("R2").Value = "Причина за недокладване до ОЛАФ";
            ws.Range("R2", "R3").Merge();
            ws.Cell("S2").Value = "Нова неправомерна практика";
            ws.Range("S2", "S3").Merge();
            ws.Cell("T2").Value = "Hеобходимост да се информират други страни";
            ws.Range("T2", "T3").Merge();
            ws.Cell("T2").Value = "Hеобходимост да се информират други страни";
            ws.Range("T2", "T3").Merge();
            ws.Cell("U2").Value = "Статус на процедурите";
            ws.Range("U2", "U3").Merge();
            ws.Cell("V2").Value = "Финансов статус";
            ws.Range("V2", "V3").Merge();
            ws.Cell("W2").Value = "Състояние на случая";
            ws.Range("W2", "W3").Merge();
            ws.Cell("X2").Value = "Дата на приключване";
            ws.Range("X2", "X3").Merge();
            ws.Cell("Y2").Value = "Рег. № на акта по чл. 30";
            ws.Range("Y2", "Y3").Merge();
            ws.Cell("Z2").Value = "Дата на акта по чл. 30";
            ws.Range("Z2", "Z3").Merge();
            ws.Cell("AA1").Value = "Нарушени разпоредби";
            ws.Range("AA1", "AE1").Merge();
            ws.Cell("AA2").Value = "Акт";
            ws.Range("AA2", "AA3").Merge();
            ws.Cell("AB2").Value = "Номер";
            ws.Range("AB2", "AB3").Merge();
            ws.Cell("AC2").Value = "Година";
            ws.Range("AC2", "AC3").Merge();
            ws.Cell("AD2").Value = "Нарушена разпоредба";
            ws.Range("AD2", "AD3").Merge();
            ws.Cell("AE2").Value = "Нарушена национална разпоредба";
            ws.Range("AE2", "AE3").Merge();
            ws.Cell("AF1").Value = "Данни за нередността";
            ws.Range("AF1", "AQ1").Merge();
            ws.Cell("AF2").Value = "Период на нередността от";
            ws.Range("AF2", "AF3").Merge();
            ws.Cell("AG2").Value = "Период на нередността до";
            ws.Range("AG2", "AG3").Merge();
            ws.Cell("AH2").Value = "Квалификация";
            ws.Range("AH2", "AH3").Merge();
            ws.Cell("AI2").Value = "Категория";
            ws.Range("AI2", "AI3").Merge();
            ws.Cell("AJ2").Value = "Вид";
            ws.Range("AJ2", "AJ3").Merge();
            ws.Cell("AK2").Value = "Приложени практики при извършване на нередността";
            ws.Range("AK2", "AK3").Merge();
            ws.Cell("AL2").Value = "Данни, декларирани от бенефициента";
            ws.Range("AL2", "AL3").Merge();
            ws.Cell("AM2").Value = "Констатации на администрацията";
            ws.Range("AM2", "AM3").Merge();
            ws.Cell("AN2").Value = "Компетентен орган, установил нередността";
            ws.Range("AN2", "AN3").Merge();
            ws.Cell("AO2").Value = "Административните процедури по случая";
            ws.Range("AO2", "AO3").Merge();
            ws.Cell("AP2").Value = "Наказателните процедури";
            ws.Range("AP2", "AP3").Merge();
            ws.Cell("AQ2").Value = "Момент на проверката";
            ws.Range("AQ2", "AQ3").Merge();
            ws.Cell("AR1").Value = "Суми";
            ws.Range("AR1", "BE1").Merge();
            ws.Cell("AR2").Value = "Процент на съфинансиране от ЕС";
            ws.Range("AR2", "AR3").Merge();
            ws.Cell("AS2").Value = "Разходи (лева)";
            ws.Range("AS2", "AW2").Merge();
            ws.Cell("AS3").Value = "ЕС";
            ws.Cell("AT3").Value = "НФ";
            ws.Cell("AU3").Value = "Общо БФП";
            ws.Cell("AV3").Value = "Собствено съфинансиране";
            ws.Cell("AW3").Value = "Обща сума";
            ws.Cell("AX2").Value = "Нередни разходи (лева)";
            ws.Range("AX2", "AZ2").Merge();
            ws.Cell("AX3").Value = "ЕС";
            ws.Cell("AY3").Value = "НФ";
            ws.Cell("AZ3").Value = "Обща сума";
            ws.Cell("BA2").Value = "Сертифицирани разходи от неверните (лева)";
            ws.Range("BA2", "BC2").Merge();
            ws.Cell("BA3").Value = "ЕС";
            ws.Cell("BB3").Value = "НФ";
            ws.Cell("BC3").Value = "Обща сума";
            ws.Cell("BD2").Value = "Десертифициране на нередния разход";
            ws.Range("BD2", "BD3").Merge();
            ws.Cell("BE2").Value = "Коментари за десертифицирането и по финансовата част";
            ws.Range("BE2", "BE3").Merge();
            ws.Cell("BF1").Value = "Процедура за налагане на санкциите";
            ws.Range("BF1", "BN1").Merge();
            ws.Cell("BF2").Value = "Тип";
            ws.Range("BF2", "BF3").Merge();
            ws.Cell("BG2").Value = "Вид";
            ws.Range("BG2", "BG3").Merge();
            ws.Cell("BH2").Value = "Статус";
            ws.Range("BH2", "BH3").Merge();
            ws.Cell("BI2").Value = "Начална дата";
            ws.Range("BI2", "BI3").Merge();
            ws.Cell("BJ2").Value = "Очаквана крайна дата";
            ws.Range("BJ2", "BJ3").Merge();
            ws.Cell("BK2").Value = "Крайна дата";
            ws.Range("BK2", "BK3").Merge();
            ws.Cell("BL2").Value = "Категория санкции";
            ws.Range("BL2", "BL3").Merge();
            ws.Cell("BM2").Value = "Вид санкции";
            ws.Range("BM2", "BM3").Merge();
            ws.Cell("BN2").Value = "Вид санкции";
            ws.Range("BN2", "BN3").Merge();
            ws.Cell("BO1").Value = "Коментари от докладващия орган";
            ws.Range("BO1", "BO3").Merge();

            var rngHeaders = ws.Range("A1", "BO3");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A3", "BO3").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 4;
            foreach (var irregularity in irregularities)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = irregularity.RegNumber;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = null;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = irregularity.ProgrammeCode;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = irregularity.ProgrammeName;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = irregularity.ProjectRegNumber;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = irregularity.ProjectName;

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = irregularity.SignalNumber;

                ws.Cell(rowIndex, "H").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = irregularity.SignalRegDate.HasValue ?
                    irregularity.SignalRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = irregularity.SignalActRegNum;

                ws.Cell(rowIndex, "J").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = irregularity.SignalActRegDate.HasValue ?
                    irregularity.SignalActRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = irregularity.SignalSource;

                ws.Cell(rowIndex, "L").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = irregularity.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "M").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = irregularity.ModifyDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = irregularity.Rapporteur.HasValue ?
                    irregularity.Rapporteur.GetEnumDescription() : null;

                ws.Cell(rowIndex, "O").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "O").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "O").Value = irregularity.ReportQuarter.GetEnumDescription();

                ws.Cell(rowIndex, "P").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "P").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "P").Value = irregularity.ReportYear.GetEnumDescription();

                ws.Cell(rowIndex, "Q").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "Q").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "Q").Value = irregularity.ShouldReportToOlaf ? "Да" : "Не";

                ws.Cell(rowIndex, "R").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "R").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "R").Value = irregularity.ReasonNotReportingToOlaf.HasValue ?
                    irregularity.ReasonNotReportingToOlaf.GetEnumDescription() : null;

                ws.Cell(rowIndex, "S").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "S").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "S").Value = irregularity.IsNewUnlawfulPractice.HasValue ?
                    (irregularity.IsNewUnlawfulPractice.Value ? "Да" : "Не") : null;

                ws.Cell(rowIndex, "T").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "T").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "T").Value = irregularity.ShouldInformOther.HasValue ?
                    (irregularity.ShouldInformOther.Value ? "Да" : "Не") : null;

                ws.Cell(rowIndex, "U").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "U").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "U").Value = irregularity.ProcedureStatus.HasValue ?
                    irregularity.ProcedureStatus.GetEnumDescription() : null;

                ws.Cell(rowIndex, "V").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "V").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "V").Value = irregularity.FinancialStatus;

                ws.Cell(rowIndex, "W").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "W").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "W").Value = irregularity.CaseState.HasValue ?
                    irregularity.CaseState.GetEnumDescription() : null;

                ws.Cell(rowIndex, "X").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "X").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "X").Value = irregularity.IrregularityEndDate.HasValue ?
                    irregularity.IrregularityEndDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "Y").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "Y").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "Y").Value = irregularity.EndingActRegNum;

                ws.Cell(rowIndex, "Z").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "Z").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "Z").Value = irregularity.EndingActDate.HasValue ?
                    irregularity.EndingActDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "AA").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AA").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AA").Value = irregularity.ImpairedRegulationAct.HasValue ?
                    irregularity.ImpairedRegulationAct.GetEnumDescription() : null;

                ws.Cell(rowIndex, "AB").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AB").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AB").Value = irregularity.ImpairedRegulationNum;

                ws.Cell(rowIndex, "AC").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "AC").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AC").Value = irregularity.ImpairedRegulationYear;

                ws.Cell(rowIndex, "AD").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AD").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AD").Value = irregularity.ImpairedRegulation;

                ws.Cell(rowIndex, "AE").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AE").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AE").Value = irregularity.ImpairedNationalRegulation;

                ws.Cell(rowIndex, "AF").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "AF").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AF").Value = irregularity.IrregularityDateFrom.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "AG").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "AG").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AG").Value = irregularity.IrregularityDateTo.HasValue ?
                    irregularity.IrregularityDateTo.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "AH").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AH").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AH").Value = irregularity.IrregularityClassification.HasValue ?
                    irregularity.IrregularityClassification.GetEnumDescription() : null;

                ws.Cell(rowIndex, "AI").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AI").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AI").Value = irregularity.IrregularityCategory;

                ws.Cell(rowIndex, "AJ").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AJ").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AJ").Value = irregularity.IrregularityType;

                ws.Cell(rowIndex, "AK").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AK").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AK").Value = irregularity.AppliedPractices;

                ws.Cell(rowIndex, "AL").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AL").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AL").Value = irregularity.BeneficiaryData;

                ws.Cell(rowIndex, "AM").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AM").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AM").Value = irregularity.AdminAscertainments;

                ws.Cell(rowIndex, "AN").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AN").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AN").Value = irregularity.IrregularityDetectedBy;

                ws.Cell(rowIndex, "AO").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AO").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AO").Value = irregularity.AdminProcedures;

                ws.Cell(rowIndex, "AP").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AP").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AP").Value = irregularity.PenaltyProcedures;

                ws.Cell(rowIndex, "AQ").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AQ").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AQ").Value = irregularity.CheckTime.HasValue ?
                    irregularity.CheckTime.GetEnumDescription() : null;

                ws.Cell(rowIndex, "AR").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AR").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AR").Value = irregularity.EUCoFinancingPercent;

                ws.Cell(rowIndex, "AS").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AS").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AS").Value = irregularity.ExpensesLvBfpEuAmount;

                ws.Cell(rowIndex, "AT").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AT").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AT").Value = irregularity.ExpensesLvBfpBgAmount;

                ws.Cell(rowIndex, "AU").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AU").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AU").Value = irregularity.ExpensesLvBfpTotalAmount;

                ws.Cell(rowIndex, "AV").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AV").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AV").Value = irregularity.ExpensesLvSelfAmount;

                ws.Cell(rowIndex, "AW").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AW").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AW").Value = irregularity.ExpensesLvTotalAmount;

                ws.Cell(rowIndex, "AX").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AX").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AX").Value = irregularity.IrregularExpensesLvEuAmount;

                ws.Cell(rowIndex, "AY").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AY").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AY").Value = irregularity.IrregularExpensesLvBgAmount;

                ws.Cell(rowIndex, "AZ").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AZ").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AZ").Value = irregularity.IrregularExpensesLvTotalAmount;

                ws.Cell(rowIndex, "BA").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "BA").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "BA").Value = irregularity.CertifiedExpensesLvEuAmount;

                ws.Cell(rowIndex, "BB").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "BB").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "BB").Value = irregularity.CertifiedExpensesLvBgAmount;

                ws.Cell(rowIndex, "BC").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "BC").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "BC").Value = irregularity.CertifiedExpensesLvTotalAmount;

                ws.Cell(rowIndex, "BD").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BD").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BD").Value = irregularity.ShouldDecertifyIrregularExpenses.HasValue ?
                    (irregularity.ShouldDecertifyIrregularExpenses.Value ? "Да" : "Не") : null;

                ws.Cell(rowIndex, "BE").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BE").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BE").Value = irregularity.DecertificationComments;

                ws.Cell(rowIndex, "BF").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BF").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BF").Value = irregularity.SanctionProcedureType.GetEnumDescription();

                ws.Cell(rowIndex, "BG").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BG").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BG").Value = irregularity.SanctionProcedureKind.HasValue ?
                    irregularity.SanctionProcedureKind.GetEnumDescription() : null;

                ws.Cell(rowIndex, "BH").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BH").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BH").Value = irregularity.SanctionProcedureStatus.HasValue ?
                    irregularity.SanctionProcedureStatus.GetEnumDescription() : null;

                ws.Cell(rowIndex, "BI").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "BI").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BI").Value = irregularity.SanctionProcedureStartDate.HasValue ?
                    irregularity.SanctionProcedureStartDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "BJ").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "BJ").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BJ").Value = irregularity.SanctionProcedureExpectedEndDate.HasValue ?
                    irregularity.SanctionProcedureExpectedEndDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "BK").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "BK").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BK").Value = irregularity.SanctionProcedureEndDate.HasValue ?
                    irregularity.SanctionProcedureEndDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "BL").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BL").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BL").Value = irregularity.SanctionCategory;

                ws.Cell(rowIndex, "BM").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BM").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BM").Value = irregularity.SanctionType;

                ws.Cell(rowIndex, "BN").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BN").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BN").Value = irregularity.Fines;

                ws.Cell(rowIndex, "BO").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "BO").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "BO").Value = irregularity.RapporteurComments;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Column(2).Width = 9;
            ws.Column(2).Style.Alignment.SetWrapText();
            ws.Columns(3, 6).AdjustToContents();
            ws.Column(7).Width = 9;
            ws.Column(8).Width = 23;
            ws.Column(9).Width = 15;
            ws.Column(10).Width = 13;
            ws.Column(11).Width = 50;
            ws.Column(12).Width = 12;
            ws.Column(13).Width = 19;
            ws.Column(14).Width = 15;
            ws.Column(15).Width = 13;
            ws.Column(16).Width = 9;
            ws.Column(17).Width = 22;
            ws.Column(18).Width = 23;
            ws.Column(19).Width = 22;
            ws.Column(20).Width = 23;
            ws.Columns(7, 20).Style.Alignment.SetWrapText();
            ws.Column(21).AdjustToContents();
            ws.Column(22).Width = 30;
            ws.Column(23).Width = 13;
            ws.Column(24).Width = 14;
            ws.Column(25).Width = 14;
            ws.Column(26).Width = 14;
            ws.Columns(22, 26).Style.Alignment.SetWrapText();
            ws.Column(27).AdjustToContents();
            ws.Column(28).Width = 14;
            ws.Column(29).Width = 8;
            ws.Column(30).Width = 14;
            ws.Column(31).Width = 23;
            ws.Column(32).Width = 16;
            ws.Column(33).Width = 16;
            ws.Columns(27, 33).Style.Alignment.SetWrapText();
            ws.Columns(34, 35).AdjustToContents();
            ws.Columns(36, 42).Width = 50;
            ws.Columns(36, 42).Style.Alignment.SetWrapText();
            ws.Column(43).AdjustToContents();
            ws.Column(44).Width = 16;
            ws.Column(44).Style.Alignment.SetWrapText();
            ws.Columns(45, 55).AdjustToContents();
            ws.Column(56).Width = 21;
            ws.Column(57).Width = 50;
            ws.Columns(56, 57).Style.Alignment.SetWrapText();
            ws.Columns(58, 60).AdjustToContents();
            ws.Column(61).Width = 11;
            ws.Column(62).Width = 12;
            ws.Column(63).Width = 11;
            ws.Column(64).Width = 16;
            ws.Column(65).Width = 25;
            ws.Column(66).Width = 50;
            ws.Column(67).Width = 50;
            ws.Columns(61, 67).Style.Alignment.SetWrapText();
        }

        private void AddIrrRegisterSheet(XLWorkbook workbook, IList<IrrRegisterItemVO> irregularities)
        {
            var ws = workbook.Worksheets.Add("Регистър нередности");

            // Headers
            ws.Cell("A1").Value = "Национален номер на случая";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Сигнал за нередност";
            ws.Range("B1", "D1").Merge();
            ws.Cell("B2").Value = "Номер";
            ws.Cell("C2").Value = "Рег № на акта по чл. 14";
            ws.Cell("D2").Value = "Дата на акта по чл. 14";
            ws.Cell("E1").Value = "Тримесечие на първоначално докладване";
            ws.Range("E1", "F1").Merge();
            ws.Cell("E2").Value = "Тримесечие";
            ws.Cell("F2").Value = "Година";
            ws.Cell("G1").Value = "Тримесечие на последващо докладване";
            ws.Range("G1", "H1").Merge();
            ws.Cell("G2").Value = "Тримесечие";
            ws.Cell("H2").Value = "Година";
            ws.Cell("I1").Value = "Програма";
            ws.Range("I1", "I2").Merge();
            ws.Cell("J1").Value = "Фонд";
            ws.Range("J1", "J2").Merge();
            ws.Cell("K1").Value = "Докладващ орган";
            ws.Range("K1", "K2").Merge();
            ws.Cell("L1").Value = "Проект";
            ws.Range("L1", "N1").Merge();
            ws.Cell("L2").Value = "Наименование";
            ws.Cell("M2").Value = "Номер от ИСУН";
            ws.Cell("N2").Value = "Друга регистрация";
            ws.Cell("O1").Value = "Квалификация на нередността";
            ws.Range("O1", "O2").Merge();
            ws.Cell("P1").Value = "Приложени практики при извършване на нередността";
            ws.Range("P1", "P2").Merge();
            ws.Cell("Q1").Value = "Обща сума на разходите (лв)";
            ws.Range("Q1", "U1").Merge();
            ws.Cell("Q2").Value = "ЕС";
            ws.Cell("R2").Value = "НФ";
            ws.Cell("S2").Value = "БФП";
            ws.Cell("T2").Value = "Собствено съфинансиране";
            ws.Cell("U2").Value = "Общо";
            ws.Cell("V1").Value = "Сума на нередния БФП (лв)";
            ws.Range("V1", "X1").Merge();
            ws.Cell("V2").Value = "ЕС";
            ws.Cell("W2").Value = "НФ";
            ws.Cell("X2").Value = "БФП";
            ws.Cell("Y1").Value = "Платена част от нередния разход (лв)";
            ws.Range("Y1", "AA1").Merge();
            ws.Cell("Y2").Value = "ЕС";
            ws.Cell("Z2").Value = "НФ";
            ws.Cell("AA2").Value = "БФП";
            ws.Cell("AB1").Value = "Статус на дълга";
            ws.Range("AB1", "AB2").Merge();
            ws.Cell("AC1").Value = "Административни процедури";
            ws.Range("AC1", "AC2").Merge();
            ws.Cell("AD1").Value = "Наказателни процедури";
            ws.Range("AD1", "AD2").Merge();
            ws.Cell("AE1").Value = "Състояние на случая";
            ws.Range("AE1", "AE2").Merge();
            ws.Cell("AF1").Value = "Рег. № на акта по чл. 30";
            ws.Range("AF1", "AF2").Merge();
            ws.Cell("AG1").Value = "Дата на акта по чл. 30";
            ws.Range("AG1", "AG2").Merge();
            ws.Cell("AH1").Value = "Коментари";
            ws.Range("AH1", "AH2").Merge();

            var rngHeaders = ws.Range("A1", "AH2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "AH2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var irregularity in irregularities)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = irregularity.RegNumber;

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = irregularity.SignalNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = irregularity.SignalActRegNum;

                ws.Cell(rowIndex, "D").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = irregularity.SignalActRegDate.HasValue ?
                    irregularity.SignalActRegDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = irregularity.FirstReportQuarter.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = irregularity.FirstReportYear.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = irregularity.ReportQuarter.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = irregularity.ReportYear.GetEnumDescription();

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = irregularity.ProgrammeCode;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = null;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = irregularity.Rapporteur.HasValue ? irregularity.Rapporteur.GetEnumDescription() : null;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = irregularity.ProjectName;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "M").Value = irregularity.ProjectNumber;

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = irregularity.ProjectOtherNumber;

                ws.Cell(rowIndex, "O").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "O").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "O").Value = irregularity.IrregularityClassification.HasValue ?
                    irregularity.IrregularityClassification.GetEnumDescription() : null;

                ws.Cell(rowIndex, "P").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "P").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "P").Value = irregularity.AppliedPractices;

                ws.Cell(rowIndex, "Q").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "Q").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "Q").Value = irregularity.ExpensesLvEuAmount;

                ws.Cell(rowIndex, "R").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "R").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "R").Value = irregularity.ExpensesLvBgAmount;

                ws.Cell(rowIndex, "S").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "S").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "S").Value = irregularity.ExpensesLvBfpAmount;

                ws.Cell(rowIndex, "T").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "T").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "T").Value = irregularity.ExpensesLvSelfAmount;

                ws.Cell(rowIndex, "U").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "U").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "U").Value = irregularity.ExpensesLvTotalAmount;

                ws.Cell(rowIndex, "V").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "V").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "V").Value = irregularity.IrregularExpensesLvEuAmount;

                ws.Cell(rowIndex, "W").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "W").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "W").Value = irregularity.IrregularExpensesLvBgAmount;

                ws.Cell(rowIndex, "X").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "X").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "X").Value = irregularity.IrregularExpensesLvBfpAmount;

                ws.Cell(rowIndex, "Y").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "Y").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "Y").Value = irregularity.PaidIrregularExpensesLvEuAmount;

                ws.Cell(rowIndex, "Z").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "Z").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "Z").Value = irregularity.PaidIrregularExpensesLvBgAmount;

                ws.Cell(rowIndex, "AA").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AA").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AA").Value = irregularity.PaidIrregularExpensesLvBfpAmount;

                ws.Cell(rowIndex, "AB").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AB").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AB").Value = irregularity.ContractDebtStatus.HasValue ?
                    irregularity.ContractDebtStatus.GetEnumDescription() : null;

                ws.Cell(rowIndex, "AC").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AC").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AC").Value = irregularity.AdminProcedures;

                ws.Cell(rowIndex, "AD").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AD").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AD").Value = irregularity.PenaltyProcedures;

                ws.Cell(rowIndex, "AE").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AE").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AE").Value = irregularity.CaseState.HasValue ?
                    irregularity.CaseState.GetEnumDescription() : null;

                ws.Cell(rowIndex, "AF").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AF").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AF").Value = irregularity.EndingActRegNum;

                ws.Cell(rowIndex, "AG").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "AG").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AG").Value = irregularity.EndingActDate.HasValue ?
                    irregularity.EndingActDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "AH").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "AH").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AH").Value = irregularity.RapporteurComments;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 3).AdjustToContents();
            ws.Column(4).Width = 12;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 10).AdjustToContents();
            ws.Column(11).Width = 12;
            ws.Column(12).Width = 35;
            ws.Columns(11, 12).Style.Alignment.SetWrapText();
            ws.Columns(13, 14).AdjustToContents();
            ws.Column(15).Width = 25;
            ws.Column(16).Width = 65;
            ws.Columns(15, 16).Style.Alignment.SetWrapText();
            ws.Columns(17, 28).AdjustToContents();
            ws.Columns(29, 30).Width = 65;
            ws.Columns(29, 30).Style.Alignment.SetWrapText();
            ws.Columns(31, 32).AdjustToContents();
            ws.Column(33).Width = 12;
            ws.Column(34).Width = 65;
            ws.Columns(33, 34).Style.Alignment.SetWrapText();
        }

        private void AddVersionsDataSheet(XLWorkbook workbook, IList<IrrReportVersionDataVO> versions)
        {
            var years = Enum.GetValues(typeof(Year)).Cast<Year>();
            var quarters = Enum.GetValues(typeof(Quarter)).Cast<Quarter>();
            var ws = workbook.Worksheets.Add("Уведомления");

            ws.Cell("A1").Value = "Национален номер на случая";
            ws.Range("A1", "A2").Merge();

            var column = 2;
            foreach (var year in years)
            {
                ws.Cell(1, column).Value = year.GetEnumDescription();
                ws.Range(1, column, 1, column + 3).Merge();

                foreach (var quarter in quarters)
                {
                    ws.Cell(2, column).Value = quarter.GetEnumDescription();
                    column++;
                }
            }

            var rngHeaders = ws.Range(1, 1, 2, column - 1);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, column - 1).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var rowIndex = 3;
            foreach (var version in versions.GroupBy(v => v.IrregularityNum))
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = version.Key;

                column = 2;
                foreach (var year in years)
                {
                    foreach (var quarter in quarters)
                    {
                        ws.Cell(rowIndex, column).Style.NumberFormat.NumberFormatId = 1;
                        ws.Cell(rowIndex, column).DataType = XLCellValues.Text;
                        ws.Cell(rowIndex, column).Value = version.Any(v => v.ReportYear == year && v.ReportQuarter == quarter) ? "✔" : null;
                        column++;
                    }
                }

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, column - 1).AdjustToContents();
        }

        private void AddInvolvedPersonsSheet(XLWorkbook workbook, IList<IrrReportInvolvedPersonVO> persons)
        {
            if (persons.Count == 0)
            {
                return;
            }

            var ws = workbook.Worksheets.Add("Замесени лица");

            // Headers
            ws.Cell("A1").Value = "Национален номер на случая";
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
                ws.Cell(rowIndex, "A").Value = person.IrregularityVersionNum;

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
