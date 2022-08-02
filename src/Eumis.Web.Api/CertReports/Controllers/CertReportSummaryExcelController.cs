using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.ViewObjects.SummaryVOs;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    public class CertReportSummaryExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private ICertReportsRepository certReportsRepository;

        public CertReportSummaryExcelController(
            IAuthorizer authorizer,
            ICertReportsRepository certReportsRepository)
        {
            this.authorizer = authorizer;
            this.certReportsRepository = certReportsRepository;
        }

        [Route("api/certReports/{certReportId:int}/summary/annex4A/excelExport")]
        public HttpResponseMessage GetAnnex4AExcel(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var report = this.certReportsRepository.GetAnnex4A(certReportId);

            var workbook = this.GetWorkbook(report);

            return this.Request.CreateXmlResponse(workbook, "annex4a");
        }

        private XLWorkbook GetWorkbook(CertReportAnnex4aResultVO report)
        {
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Приложение № 4А");

            // Headers
            ws.Cell("A1").Value = "№ на договор";
            ws.Range("A1", "A2").Merge();

            ws.Cell("B1").Value = "№ на искане за плащане от бенефициента";
            ws.Range("B1", "B2").Merge();

            ws.Cell("C1").Value = "Договор с изпълнител / еднотипни разходи / разходи за организация и управление";
            ws.Range("C1", "G1").Merge();
            ws.Cell("C2").Value = "№ / дата";
            ws.Cell("D2").Value = "Изпълнител";
            ws.Cell("E2").Value = "Стойност на договора без ДДС";
            ws.Cell("F2").Value = "Стойност на ДДС в случай, че е допустим разход";
            ws.Cell("G2").Value = "Обща стойност на договора";

            ws.Cell("H1").Value = "Отчетени с искането за плащане разходи по договора с изпълнител / еднотипни разходи / разходи за организация и управление";
            ws.Range("H1", "I1").Merge();
            ws.Cell("H2").Value = "БФП";
            ws.Cell("I2").Value = "Собствен принос";

            ws.Cell("J1").Value = "Неверифицирани разходи поради финансови корекции";
            ws.Range("J1", "K1").Merge();
            ws.Cell("J2").Value = "БФП";
            ws.Cell("K2").Value = "Собствен принос";

            ws.Cell("L1").Value = "Неверифицирани разходи поради други причини";
            ws.Range("L1", "M1").Merge();
            ws.Cell("L2").Value = "БФП";
            ws.Cell("M2").Value = "Собствен принос";

            ws.Cell("N1").Value = "Верифицирани разходи";
            ws.Range("N1", "O1").Merge();
            ws.Cell("N2").Value = "БФП";
            ws.Cell("O2").Value = "Собствен принос";

            ws.Cell("P1").Value = "Препотвърдени разходи";
            ws.Range("P1", "Q1").Merge();
            ws.Cell("P2").Value = "БФП";
            ws.Cell("Q2").Value = "Собствен принос";

            ws.Cell("R1").Value = "Възстановени суми, с които се намяляват верифицираните разходи в колони 14 и 15, включително собствен принос, свързан с тях";
            ws.Range("R1", "S1").Merge();
            ws.Cell("R2").Value = "БФП";
            ws.Cell("S2").Value = "Собствен принос";

            ws.Cell("T1").Value = "Корекции, с които се намаляват верифицираните разходи в колони 14 и 15";
            ws.Range("T1", "U1").Merge();
            ws.Cell("T2").Value = "БФП";
            ws.Cell("U2").Value = "Собствен принос";

            ws.Cell("V1").Value = "Възстановени суми, с които се намаляват вече включени в Заявление за плащане и/или Годишен счетоводен отчет разходи, включително собствен принос, свързан с тях";
            ws.Range("V1", "W1").Merge();
            ws.Cell("V2").Value = "БФП";
            ws.Cell("W2").Value = "Собствен принос";

            ws.Cell("X1").Value = "Корекции, с които се намаляват вече включени в Заявление за плащане и/или Годишен счетоводен отчет разходи";
            ws.Range("X1", "Y1").Merge();
            ws.Cell("X2").Value = "БФП";
            ws.Cell("Y2").Value = "Собствен принос";

            ws.Cell("Z1").Value = "Включени в ДСДДР разходи";
            ws.Range("Z1", "AA1").Merge();
            ws.Cell("Z2").Value = "БФП";
            ws.Cell("AA2").Value = "Собствен принос";

            ws.Cell("AB1").Value = "Задържани от СО разходи от заявление за плащане към ЕК";
            ws.Range("AB1", "AC1").Merge();
            ws.Cell("AB2").Value = "БФП";
            ws.Cell("AC2").Value = "Собствен принос";

            ws.Cell("AD1").Value = "Разходи, включени от СО в заявление за плащане към ЕК";
            ws.Range("AD1", "AE1").Merge();
            ws.Cell("AD2").Value = "БФП";
            ws.Cell("AE2").Value = "Собствен принос";

            ws.Cell("AF1").Value = "Несертифицирани с Годишен счетоводен отчет разходи";
            ws.Range("AF1", "AG1").Merge();
            ws.Cell("AF2").Value = "БФП";
            ws.Cell("AG2").Value = "Собствен принос";

            ws.Cell("AH1").Value = "Сертифицирани с Годишен счетоводен отчет разходи";
            ws.Range("AH1", "AI1").Merge();
            ws.Cell("AH2").Value = "БФП";
            ws.Cell("AI2").Value = "Собствен принос";

            ws.Cell("AJ1").Value = "Номер и дата на ДС и ДДР";
            ws.Range("AJ1", "AJ2").Merge();

            for (int i = 1; i <= 36; i++)
            {
                ws.Cell(3, i).Value = i;
            }

            var rngHeaders = ws.Range("A1", "AJ3");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.WrapText = true;
            ws.Range("A3", "AJ3").Style.Border.BottomBorder = XLBorderStyleValues.Double;
            ws.Row(1).Height = 75;

            // Data
            const int precision2WithSeparator = 4;

            var row = 4;
            foreach (var item in report.Items)
            {
                ws.Cell(row, "A").Value = item.ContractRegNumber;

                ws.Cell(row, "B").Value = item.PaymentVersionNum;
                ws.Cell(row, "B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, "C").Value = item.ContractNumber;
                ws.Cell(row, "C").Style.Alignment.WrapText = true;

                ws.Cell(row, "D").Value = item.ContractContractor;
                ws.Cell(row, "D").Style.Alignment.WrapText = true;

                ws.Cell(row, "E").Value = item.ContractAmountWithoutVAT;
                ws.Cell(row, "E").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "F").Value = item.ContractVATAmountIfEligible;
                ws.Cell(row, "F").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "G").Value = item.ContractTotalAmount;
                ws.Cell(row, "G").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "H").Value = item.BfpTotalAmount;
                ws.Cell(row, "H").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "I").Value = item.SelfAmount;
                ws.Cell(row, "I").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "J").Value = item.UnapprovedByCorrectionBfpTotalAmount;
                ws.Cell(row, "J").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "K").Value = item.UnapprovedByCorrectionSelfAmount;
                ws.Cell(row, "K").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "L").Value = item.UnapprovedBfpTotalAmount;
                ws.Cell(row, "L").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "M").Value = item.UnapprovedSelfAmount;
                ws.Cell(row, "M").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "N").Value = item.ApprovedBfpTotalAmount;
                ws.Cell(row, "N").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "O").Value = item.ApprovedSelfAmount;
                ws.Cell(row, "O").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "P").Value = item.RevalidatedBfpTotalAmount;
                ws.Cell(row, "P").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "Q").Value = item.RevalidatedSelfAmount;
                ws.Cell(row, "Q").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "T").Value = item.CorrectedApprovedBfpTotalAmountNoCert;
                ws.Cell(row, "T").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "U").Value = item.CorrectedApprovedSelfAmountNoCert;
                ws.Cell(row, "U").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "X").Value = item.CorrectedApprovedBfpTotalAmountCert;
                ws.Cell(row, "X").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "Y").Value = item.CorrectedApprovedSelfAmountCert;
                ws.Cell(row, "Y").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "Z").Value = item.IncludedInCertReportBfpTotalAmount;
                ws.Cell(row, "Z").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "AA").Value = item.IncludedInCertReportSelfAmount;
                ws.Cell(row, "AA").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

                ws.Cell(row, "AJ").Value = item.CertReportNumber;

                row++;
            }

            ws.Column("A").AdjustToContents();
            ws.Column("C").Width = 30;
            ws.Column("D").Width = 30;
            ws.Columns("E", "AI").Width = 17;

            // Footer
            ws.Cell(row, "A").Value = "ОБЩО:";

            ws.Cell(row, "E").Value = report.TotalContractAmountWithoutVAT;
            ws.Cell(row, "E").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "F").Value = report.TotalContractVATAmountIfEligible;
            ws.Cell(row, "F").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "G").Value = report.TotalContractTotalAmount;
            ws.Cell(row, "G").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "H").Value = report.TotalBfpTotalAmount;
            ws.Cell(row, "H").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "I").Value = report.TotalSelfAmount;
            ws.Cell(row, "I").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "J").Value = report.TotalUnapprovedByCorrectionBfpTotalAmount;
            ws.Cell(row, "J").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "K").Value = report.TotalUnapprovedByCorrectionSelfAmount;
            ws.Cell(row, "K").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "L").Value = report.TotalUnapprovedBfpTotalAmount;
            ws.Cell(row, "L").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "M").Value = report.TotalUnapprovedSelfAmount;
            ws.Cell(row, "M").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "N").Value = report.TotalApprovedBfpTotalAmount;
            ws.Cell(row, "N").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "O").Value = report.TotalApprovedSelfAmount;
            ws.Cell(row, "O").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "P").Value = report.TotalRevalidatedBfpTotalAmount;
            ws.Cell(row, "P").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "Q").Value = report.TotalRevalidatedSelfAmount;
            ws.Cell(row, "Q").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "T").Value = report.TotalCorrectedApprovedBfpTotalAmountNoCert;
            ws.Cell(row, "T").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "U").Value = report.TotalCorrectedApprovedSelfAmountNoCert;
            ws.Cell(row, "U").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "X").Value = report.TotalCorrectedApprovedBfpTotalAmountCert;
            ws.Cell(row, "X").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "Y").Value = report.TotalCorrectedApprovedSelfAmountCert;
            ws.Cell(row, "Y").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "Z").Value = report.TotalIncludedInCertReportBfpTotalAmount;
            ws.Cell(row, "Z").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            ws.Cell(row, "AA").Value = report.TotalIncludedInCertReportSelfAmount;
            ws.Cell(row, "AA").Style.NumberFormat.NumberFormatId = precision2WithSeparator;

            var footer = ws.Range($"A{row}", $"AJ{row}");
            footer.Style.Font.Bold = true;
            footer.Style.Border.TopBorder = XLBorderStyleValues.Double;

            return workbook;
        }
    }
}
