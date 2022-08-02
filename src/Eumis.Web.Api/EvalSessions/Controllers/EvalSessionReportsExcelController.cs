using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    public class EvalSessionReportsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionReportsRepository evalSessionReportsRepository;
        private IAuthorizer authorizer;

        public EvalSessionReportsExcelController(
            IUnitOfWork unitOfWork,
            IEvalSessionReportsRepository evalSessionReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionReportsRepository = evalSessionReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/evalSessions/{evalSessionId:int}/reports/{evalSessionReportId:int}/excelExport")]
        public HttpResponseMessage GetEvalSessionReport(int evalSessionId, int evalSessionReportId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var report = this.evalSessionReportsRepository.Find(evalSessionReportId);
            var projects = report.Projects
                .OrderBy(p => p.StandingStatus)
                .ThenBy(p => p.StandingNum)
                .ToList();

            var workbook = this.GetWorkbook(projects);

            return this.Request.CreateXmlResponse(workbook, report.RegNumber);
        }

        private XLWorkbook GetWorkbook(IList<EvalSessionReportProject> projects)
        {
            var workbook = new XLWorkbook();

            this.AddProjectsSheet(workbook, projects);
            this.AddPartnersSheet(workbook, projects);

            return workbook;
        }

        private void AddProjectsSheet(XLWorkbook workbook, IList<EvalSessionReportProject> projects)
        {
            var ws = workbook.Worksheets.Add("ПП");

            var showAdminAdmissResult = projects.Any(p => p.HasAdminAdmiss);
            var showTechFinanceResult = projects.Any(p => p.HasTechFinance);
            var showComplexResult = projects.Any(p => p.HasComplex);

            // Headers
            ws.Cell("A1").Value = "ПП рег. номер";
            ws.Range("A1", "A2").Merge();
            ws.Cell("B1").Value = "Дата и час на рег.";
            ws.Range("B1", "B2").Merge();
            ws.Cell("C1").Value = "Дата и час на получаване";
            ws.Range("C1", "C2").Merge();
            ws.Cell("D1").Value = "Начин на получаване";
            ws.Range("D1", "D2").Merge();
            ws.Cell("E1").Value = "Кандидат";
            ws.Range("E1", "I1").Merge();
            ws.Cell("E2").Value = "УИН";
            ws.Cell("F2").Value = "Наименование";
            ws.Cell("G2").Value = "Код на организацията по КИД 2008";
            ws.Cell("H2").Value = "Ел.поща за контакт";
            ws.Cell("I2").Value = "Адрес за кореспонденция";
            ws.Cell("I1").Value = "Проектно предложение";
            ws.Range("J1", "O1").Merge();
            ws.Cell("J2").Value = "Наименование";
            ws.Cell("K2").Value = "Код на проекта по КИД 2008";
            ws.Cell("L2").Value = "Категория на предприятието";
            ws.Cell("M2").Value = "Продължителност в месеци";
            ws.Cell("N2").Value = "Място на изпълнение";
            ws.Cell("O2").Value = "Общ размер на БФП (лв.)";
            ws.Cell("P2").Value = "Общ размер на съфинансиране (лв.)";

            var colIndex = 17;
            if (showAdminAdmissResult)
            {
                ws.Cell(1, colIndex).Value = "Резултат от ОАСД";
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = "Статус";
                ws.Cell(2, colIndex + 1).Value = "Точки";

                colIndex += 2;
            }

            if (showTechFinanceResult)
            {
                ws.Cell(1, colIndex).Value = "Резултат от ТФО";
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = "Статус";
                ws.Cell(2, colIndex + 1).Value = "Точки";

                colIndex += 2;
            }

            if (showComplexResult)
            {
                ws.Cell(1, colIndex).Value = "Резултат от КО";
                ws.Range(1, colIndex, 1, colIndex + 1).Merge();
                ws.Cell(2, colIndex).Value = "Статус";
                ws.Cell(2, colIndex + 1).Value = "Точки";

                colIndex += 2;
            }

            ws.Cell(1, colIndex).Value = "Класиране";
            ws.Range(1, colIndex, 1, colIndex + 2).Merge();
            ws.Cell(2, colIndex).Value = "Пореден номер";
            ws.Cell(2, colIndex + 1).Value = "Статус";
            ws.Cell(2, colIndex + 2).Value = "БФП след корекции (лв.)";

            var rngHeaders = ws.Range(1, 1, 2, colIndex + 2);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(2, 1, 2, colIndex + 2).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 3;
            foreach (var project in projects)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = project.RegNumber;

                ws.Cell(rowIndex, "B").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = project.RegDate.ToString("dd.MM.yyyy HH:mm");

                ws.Cell(rowIndex, "C").Style.DateFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = project.RecieveDate.ToString("dd.MM.yyyy HH:mm");

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = project.RecieveType.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = project.CompanyUin;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = project.CompanyName;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = project.CompanyKidCode == null ? null : string.Format("{0} {1}", project.CompanyKidCode.Code, project.CompanyKidCode.Name);

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = project.RegEmail;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = project.RegEmail;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = project.Correspondence;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = project.Name;

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").Value = project.ProjectKidCode == null ? null : string.Format("{0} {1}", project.ProjectKidCode.Code, project.ProjectKidCode.Name);
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = null;

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "M").Value = project.Duration;

                ws.Cell(rowIndex, "N").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "N").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "N").Value = project.ProjectPlace;

                ws.Cell(rowIndex, "O").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "O").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "O").Value = project.GrandAmount ?? 0;

                ws.Cell(rowIndex, "P").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "P").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "P").Value = project.CoFinancingAmount ?? 0;

                colIndex = 17;
                if (showAdminAdmissResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.AdminAdmissResult.GetEnumDescription();

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.AdminAdmissPoints;

                    colIndex += 2;
                }

                if (showTechFinanceResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.TechFinanceResult.GetEnumDescription();

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.TechFinancePoints;

                    colIndex += 2;
                }

                if (showComplexResult)
                {
                    ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, colIndex).Value = project.ComplexResult.GetEnumDescription();

                    ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, colIndex + 1).Value = project.ComplexPoints;

                    colIndex += 2;
                }

                ws.Cell(rowIndex, colIndex).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, colIndex).DataType = XLCellValues.Number;
                ws.Cell(rowIndex, colIndex).Value = project.StandingNum;

                ws.Cell(rowIndex, colIndex + 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, colIndex + 1).DataType = XLCellValues.Text;
                ws.Cell(rowIndex, colIndex + 1).Value = project.StandingStatus.GetEnumDescription();

                ws.Cell(rowIndex, colIndex + 2).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, colIndex + 2).DataType = XLCellValues.Number;
                ws.Cell(rowIndex, colIndex + 2).Value = project.StandingGrandAmount;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();
            ws.Column(7).Width = 50;
            ws.Column(7).Style.Alignment.SetWrapText();
            ws.Columns(8, 10).AdjustToContents();
            ws.Column(11).Width = 50;
            ws.Columns(12, 13).Width = 24;
            ws.Column(14).Width = 70;
            ws.Columns(15, 16).Width = 24;
            ws.Columns(11, 16).Style.Alignment.SetWrapText();
            ws.Columns(17, colIndex + 2).AdjustToContents();
        }

        private void AddPartnersSheet(XLWorkbook workbook, IList<EvalSessionReportProject> projects)
        {
            if (!projects.Any(p => p.Partners.Count != 0))
            {
                return;
            }

            var ws = workbook.Worksheets.Add("Партньори");

            // Headers
            ws.Cell("A1").Value = "ПП рег. номер";
            ws.Cell("B1").Value = "УИН";
            ws.Cell("C1").Value = "Наименование";
            ws.Cell("D1").Value = "Вид на организацията";
            ws.Cell("E1").Value = "Адрес";
            ws.Cell("F1").Value = "Финансово участие";
            ws.Cell("G1").Value = "Представител";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var project in projects)
            {
                foreach (var partner in project.Partners)
                {
                    ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "A").Value = project.RegNumber;

                    ws.Cell(rowIndex, "B").Style.NumberFormat.Format = "@";
                    ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "B").Value = partner.PartnerUin;

                    ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "C").Value = partner.PartnerName;

                    ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "D").Value = partner.PartnerLegalType.Name;

                    ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "E").Value = partner.PartnerAddress;

                    ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(rowIndex, "F").DataType = XLCellValues.Number;
                    ws.Cell(rowIndex, "F").Value = partner.PartnerFinancialContribution ?? 0;

                    ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                    ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                    ws.Cell(rowIndex, "G").Value = partner.PartnerRepresentative;

                    rowIndex++;
                }
            }

            // Style cells
            ws.Columns(1, 7).AdjustToContents();
        }
    }
}
