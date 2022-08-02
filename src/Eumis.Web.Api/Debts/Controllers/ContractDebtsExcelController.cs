using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Debts.Repositories;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/contractDebtsExcelExport")]
    public class ContractDebtsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;

        public ContractDebtsExcelController(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
        }

        [Route("report")]
        public HttpResponseMessage GetDebts(Month month, Year year, int? programmeId = null)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);
            if (programmeId != null)
            {
                if (Array.IndexOf(programmeIds, programmeId) == -1)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                }

                programmeIds = new int[] { programmeId.Value };
            }

            var dateFrom = new DateTime((int)year, (int)month, 1);
            var dateToMonth = month == Month.December ? Month.January : month + 1;
            var dateToYear = month == Month.December ? (int)year + 1 : (int)year;
            var dateTo = new DateTime(dateToYear, (int)dateToMonth, 1).AddDays(-1);

            var debts = this.contractDebtsRepository.GetContractDebtReport(programmeIds, dateFrom, dateTo);

            var workbook = new XLWorkbook();
            this.AddDebtsSheet(workbook, debts);

            return this.Request.CreateXmlResponse(workbook, "debts_report");
        }

        private void AddDebtsSheet(XLWorkbook workbook, IList<ContractDebtReportVO> debts)
        {
            var ws = workbook.Worksheets.Add("Книга на длъжниците");

            // Headers
            ws.Cell("A1").Value = "Статус";
            ws.Range("A1", "A3").Merge();
            ws.Cell("B1").Value = "Дълг №";
            ws.Range("B1", "B3").Merge();
            ws.Cell("C1").Value = "Бенефициент";
            ws.Range("C1", "C3").Merge();
            ws.Cell("D1").Value = "№ на Договор";
            ws.Range("D1", "D3").Merge();
            ws.Cell("E1").Value = "Дата на регистрация";
            ws.Range("E1", "E3").Merge();
            ws.Cell("F1").Value = "Дата на последна актуализация";
            ws.Range("F1", "F3").Merge();
            ws.Cell("G1").Value = "Нередност №";
            ws.Range("G1", "G3").Merge();
            ws.Cell("H1").Value = "Финансова корекция №";
            ws.Range("H1", "H3").Merge();
            ws.Cell("I1").Value = "Дължима сума";
            ws.Range("I1", "O1").Merge();
            ws.Cell("I2").Value = "Главница";
            ws.Range("I2", "K2").Merge();
            ws.Cell("I3").Value = "EС";
            ws.Cell("J3").Value = "НФ";
            ws.Cell("K3").Value = "Общо";
            ws.Cell("L2").Value = "Лихва";
            ws.Range("L2", "N2").Merge();
            ws.Cell("L3").Value = "EС";
            ws.Cell("M3").Value = "НФ";
            ws.Cell("N3").Value = "Общо";
            ws.Cell("O2").Value = "Общо";
            ws.Range("O2", "O3").Merge();
            ws.Cell("P1").Value = "Новорегистрирани дългове през месеца (главница)";
            ws.Range("P1", "R1").Merge();
            ws.Cell("P2").Value = "EС";
            ws.Range("P2", "P3").Merge();
            ws.Cell("Q2").Value = "НФ";
            ws.Range("Q2", "Q3").Merge();
            ws.Cell("R2").Value = "Общо";
            ws.Range("R2", "R3").Merge();
            ws.Cell("S1").Value = "Възстановена сума през месеца";
            ws.Range("S1", "Z1").Merge();
            ws.Cell("S2").Value = "Главница";
            ws.Range("S2", "U2").Merge();
            ws.Cell("S3").Value = "EС";
            ws.Cell("T3").Value = "НФ";
            ws.Cell("U3").Value = "Общо";
            ws.Cell("V2").Value = "Лихва";
            ws.Range("V2", "X2").Merge();
            ws.Cell("V3").Value = "EС";
            ws.Cell("W3").Value = "НФ";
            ws.Cell("X3").Value = "Общо";
            ws.Cell("Y2").Value = "Общо";
            ws.Range("Y2", "Y3").Merge();
            ws.Cell("Z2").Value = "Дата на възстановяване";
            ws.Range("Z2", "Z3").Merge();
            ws.Cell("AA1").Value = "Прихваната сума през месеца";
            ws.Range("AA1", "AH1").Merge();
            ws.Cell("AA2").Value = "Главница";
            ws.Range("AA2", "AC2").Merge();
            ws.Cell("AA3").Value = "EС";
            ws.Cell("AB3").Value = "НФ";
            ws.Cell("AC3").Value = "Общо";
            ws.Cell("AD2").Value = "Лихва";
            ws.Range("AD2", "AF2").Merge();
            ws.Cell("AD3").Value = "EС";
            ws.Cell("AE3").Value = "НФ";
            ws.Cell("AF3").Value = "Общо";
            ws.Cell("AG2").Value = "Общо";
            ws.Range("AG2", "AG3").Merge();
            ws.Cell("AH2").Value = "Дата на прихващане";
            ws.Range("AH2", "AH3").Merge();
            ws.Cell("AI1").Value = "Лихва, натрупана през месеца";
            ws.Range("AI1", "AK1").Merge();
            ws.Cell("AI2").Value = "EС";
            ws.Range("AI2", "AI3").Merge();
            ws.Cell("AJ2").Value = "НФ";
            ws.Range("AJ2", "AJ3").Merge();
            ws.Cell("AK2").Value = "Общо";
            ws.Range("AK2", "AK3").Merge();
            ws.Cell("AL1").Value = "Дължимо към края на месеца";
            ws.Range("AL1", "AR1").Merge();
            ws.Cell("AL2").Value = "Главница";
            ws.Range("AL2", "AN2").Merge();
            ws.Cell("AL3").Value = "EС";
            ws.Cell("AM3").Value = "НФ";
            ws.Cell("AN3").Value = "Общо";
            ws.Cell("AO2").Value = "Лихва";
            ws.Range("AO2", "AQ2").Merge();
            ws.Cell("AO3").Value = "EС";
            ws.Cell("AP3").Value = "НФ";
            ws.Cell("AQ3").Value = "Общо";
            ws.Cell("AR2").Value = "Общо";
            ws.Range("AR2", "AR3").Merge();
            ws.Cell("AS1").Value = "ДС и ДДР №, в който сумата е включена първоначално";
            ws.Range("AS1", "AS3").Merge();
            ws.Cell("AT1").Value = "ДС и ДДР №, от който сумата е приспадната";
            ws.Range("AT1", "AT3").Merge();

            var rngHeaders = ws.Range("A1", "AT3");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A3", "AT3").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            StringBuilder builder = new StringBuilder();

            // Content
            int rowIndex = 4;
            foreach (var debt in debts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = debt.ExecutionStatus.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = debt.RegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = debt.Company;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = debt.ContractNumber;

                ws.Cell(rowIndex, "E").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = debt.RegDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "F").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = debt.ModifyDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "G").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = debt.IrregularityNum;

                ws.Cell(rowIndex, "H").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = debt.FinancialCorrectionNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "I").Value = debt.DebtPrincipalBfpEuAmount;

                ws.Cell(rowIndex, "J").Style.DateFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "J").Value = debt.DebtPrincipalBfpBgAmount;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "K").Value = debt.DebtPrincipalTotalAmount;

                ws.Cell(rowIndex, "L").Style.DateFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "L").Value = debt.DebtInterestBfpEuAmount;

                ws.Cell(rowIndex, "M").Style.DateFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "M").Value = debt.DebtInterestBfpBgAmount;

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "N").Value = debt.DebtInterestTotalAmount;

                ws.Cell(rowIndex, "O").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "O").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "O").Value = debt.DebtTotalAmount;

                ws.Cell(rowIndex, "P").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "P").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "P").Value = debt.NewDebtPrincipalBfpEuAmount;

                ws.Cell(rowIndex, "Q").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "Q").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "Q").Value = debt.NewDebtPrincipalBfpBgAmount;

                ws.Cell(rowIndex, "R").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "R").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "R").Value = debt.NewDebtPrincipalTotalAmount;

                ws.Cell(rowIndex, "S").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "S").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "S").Value = debt.RaPrincipalBfpEuAmount;

                ws.Cell(rowIndex, "T").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "T").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "T").Value = debt.RaPrincipalBfpBgAmount;

                ws.Cell(rowIndex, "U").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "U").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "U").Value = debt.RaPrincipalBfpTotalAmount;

                ws.Cell(rowIndex, "V").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "V").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "V").Value = debt.RaInterestBfpEuAmount;

                ws.Cell(rowIndex, "W").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "W").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "W").Value = debt.RaInterestBfpBgAmount;

                ws.Cell(rowIndex, "X").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "X").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "X").Value = debt.RaInterestBfpTotalAmount;

                ws.Cell(rowIndex, "Y").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "Y").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "Y").Value = debt.RaBfpTotalAmount;

                ws.Cell(rowIndex, "Z").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "Z").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "Z").Value = debt.ReimbursementDate.HasValue ?
                    debt.ReimbursementDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "AA").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AA").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AA").Value = debt.DaPrincipalBfpEuAmount;

                ws.Cell(rowIndex, "AB").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AB").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AB").Value = debt.DaPrincipalBfpBgAmount;

                ws.Cell(rowIndex, "AC").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AC").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AC").Value = debt.DaPrincipalBfpTotalAmount;

                ws.Cell(rowIndex, "AD").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AD").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AD").Value = debt.DaInterestBfpEuAmount;

                ws.Cell(rowIndex, "AE").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AE").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AE").Value = debt.DaInterestBfpBgAmount;

                ws.Cell(rowIndex, "AF").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AF").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AF").Value = debt.DaInterestBfpTotalAmount;

                ws.Cell(rowIndex, "AG").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AG").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AG").Value = debt.DaBfpTotalAmount;

                ws.Cell(rowIndex, "AH").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "AH").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AH").Value = debt.DeductionDate.HasValue ?
                    debt.DeductionDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "AI").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AI").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AI").Value = debt.InterestBfpEuAmount;

                ws.Cell(rowIndex, "AJ").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AJ").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AJ").Value = debt.InterestBfpBgAmount;

                ws.Cell(rowIndex, "AK").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AK").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AK").Value = debt.InterestTotalAmount;

                ws.Cell(rowIndex, "AL").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AL").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AL").Value = debt.RemainingDebtPrincipalBfpEuAmount;

                ws.Cell(rowIndex, "AM").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AM").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AM").Value = debt.RemainingDebtPrincipalBfpBgAmount;

                ws.Cell(rowIndex, "AN").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AN").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AN").Value = debt.RemainingDebtPrincipalBfpTotalAmount;

                ws.Cell(rowIndex, "AO").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AO").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AO").Value = debt.RemainingDebtInterestBfpEuAmount;

                ws.Cell(rowIndex, "AP").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AP").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AP").Value = debt.RemainingDebtInterestBfpBgAmount;

                ws.Cell(rowIndex, "AQ").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AQ").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AQ").Value = debt.RemainingDebtInterestBfpTotalAmount;

                ws.Cell(rowIndex, "AR").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "AR").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "AR").Value = debt.RemainingDebtBfpTotalAmount;

                builder.Clear();
                foreach (var item in debt.PaymentsCertReports)
                {
                    builder.Append(string.Format("ИП{0}-ДС{1};", item.PaymentOrderNumber, item.CertReportOrderNumber));
                }

                ws.Cell(rowIndex, "AS").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AS").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AS").Value = builder.ToString();

                builder.Clear();
                foreach (var item in debt.CorrectionsCertReports)
                {
                    builder.Append(string.Format("ИП{0}-ДС{1};", item.PaymentOrderNumber, item.CertReportOrderNumber));
                }

                ws.Cell(rowIndex, "AT").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "AT").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "AT").Value = builder.ToString();

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 4).AdjustToContents();
            ws.Columns(5, 6).Width = 14;
            ws.Columns(7, 8).Width = 35;
            ws.Columns(5, 8).Style.Alignment.SetWrapText();
            ws.Columns(9, 25).AdjustToContents();
            ws.Column(26).Width = 16;
            ws.Column(26).Style.Alignment.SetWrapText();
            ws.Columns(27, 33).AdjustToContents();
            ws.Columns(34, 37).Width = 14;
            ws.Columns(34, 37).Style.Alignment.SetWrapText();
            ws.Columns(38, 44).AdjustToContents();
            ws.Columns(45, 46).Width = 18;
            ws.Columns(45, 46).Style.Alignment.SetWrapText();
        }
    }
}
