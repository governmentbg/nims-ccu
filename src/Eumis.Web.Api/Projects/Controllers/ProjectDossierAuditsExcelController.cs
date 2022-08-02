using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Json;
using Eumis.Data.Audits.Repositories;
using Eumis.Data.Audits.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Data.Debts.Repositories;
using Eumis.Domain.Debts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    public class ProjectDossierAuditsExcelController : ApiController
    {
        private IAuthorizer authorizer;
        private IAuditsRepository auditsRepository;
        private IRelationsRepository relationsRepository;

        public ProjectDossierAuditsExcelController(
            IAuthorizer authorizer,
            IAuditsRepository auditsRepository,
            IRelationsRepository relationsRepository)
        {
            this.authorizer = authorizer;
            this.auditsRepository = auditsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("api/projectDossier/{projectId}/contract/{contractId}/internalEnvironmentAudits/excelExport")]
        public HttpResponseMessage GetInternalEnvironmentAudits(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var internalЕnvironmentAudits = this.auditsRepository.GetInternalЕnvironmentAuditsForProjectDossier(contractId);
            var workbook = this.GetInternalEnvironmentAuditWorkbook(internalЕnvironmentAudits);

            return this.Request.CreateXmlResponse(workbook, "internalЕnvironmentAudits");
        }

        private XLWorkbook GetInternalEnvironmentAuditWorkbook(IList<InternalЕnvironmentAuditVO> internalЕnvironmentAudits)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Одити – вътрешна среда");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "Номер на договор";
            ws.Cell("C1").Value = "Институция, извършваща одита";
            ws.Cell("D1").Value = "Тип";
            ws.Cell("E1").Value = "Вид";
            ws.Cell("F1").Value = "Ниво";
            ws.Cell("G1").Value = "Констатации";
            ws.Cell("H1").Value = "Изпълнени ли са препоръките";
            ws.Cell("I1").Value = "Финансово изражение";

            var rngHeaders = ws.Range(1, 1, 1, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            int maxCellTextLength = 32767;

            // Content
            int rowIndex = 2;
            foreach (var internalЕnvironmentAudit in internalЕnvironmentAudits)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = internalЕnvironmentAudit.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = internalЕnvironmentAudit.ContractRegNum;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = internalЕnvironmentAudit.AuditInstitution.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = internalЕnvironmentAudit.AuditType.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = internalЕnvironmentAudit.AuditKind.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = internalЕnvironmentAudit.Level.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                var untrimmedAscertainmentsValue = string.Join(Environment.NewLine, internalЕnvironmentAudit.Ascertainments.OrderBy(a => a.Id).Select(a => a.AscertainmentContent));
                ws.Cell(rowIndex, "G").Value = untrimmedAscertainmentsValue.Substring(0, Math.Min(untrimmedAscertainmentsValue.Length, maxCellTextLength));

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = string.Join(Environment.NewLine, internalЕnvironmentAudit.RecommendationsFulfilledStatuses.OrderBy(a => a.Id).Select(s => s.RecommendationsFulfilledStatus ?? "---"));

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = string.Join(Environment.NewLine, internalЕnvironmentAudit.RecommendationsFulfilledStatuses.OrderBy(a => a.Id).Select(s => s.IsFinancial ?? "---"));

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();
            ws.Column(7).Width = 100;
            ws.Column(7).Style.Alignment.SetWrapText();
            ws.Columns(8, 9).AdjustToContents();

            return workbook;
        }

        private XLWorkbook GetTechnicalReportAuditWorkbook(IList<TechnicalReportAuditVO> technicalReportAudits)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Одити – Технически отчет");

            // Headers
            ws.Cell("A1").Value = "Номер на ТО";
            ws.Cell("B1").Value = "Институция";
            ws.Cell("C1").Value = "Тип";
            ws.Cell("D1").Value = "Вид";
            ws.Cell("E1").Value = "Номер на окончателния доклад";
            ws.Cell("F1").Value = "Дата на окончателния доклад";

            var rngHeaders = ws.Range(1, 1, 1, 6);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var technicalReportAudit in technicalReportAudits)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = technicalReportAudit.VersionNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = technicalReportAudit.InstitutionName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = technicalReportAudit.TypeDesc;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = technicalReportAudit.KindDesc;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = technicalReportAudit.FinalReportNumber;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = technicalReportAudit.FinalReportDate.HasValue ?
                    technicalReportAudit.FinalReportDate.Value.ToString("dd.MM.yyyy") : null;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();

            return workbook;
        }
    }
}
