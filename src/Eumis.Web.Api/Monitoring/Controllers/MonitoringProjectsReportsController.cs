using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Core;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/projects")]
    public class MonitoringProjectsReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringProjectsReportsController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IMonitoringReportsRepository monitoringReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.monitoringReportsRepository = monitoringReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProjectsReportItem> GetProjectsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetProjectsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
        }

        [Route("export")]
        public HttpResponseMessage GetProjectsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetProjectsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Проектни предложения");

            // Headers
            ws.Cell("A1").Value = "Оперативна програма";
            ws.Cell("B1").Value = "Процедура";
            ws.Cell("C1").Value = "Рег. номер";
            ws.Cell("D1").Value = "Име";
            ws.Cell("E1").Value = "Дата и час на регистрация";
            ws.Cell("F1").Value = "Дата и час на получаване";
            ws.Cell("G1").Value = "Начин на получаване";
            ws.Cell("H1").Value = "ЕИК";
            ws.Cell("I1").Value = "Име на организацията";
            ws.Cell("J1").Value = "Тип на организацията";
            ws.Cell("K1").Value = "Вид на организацията";
            ws.Cell("L1").Value = "Код по КИД 2008 на организацията";
            ws.Cell("M1").Value = "Адрес на организацията";
            ws.Cell("N1").Value = "Адрес за кореспонденция";
            ws.Cell("O1").Value = "Е-mail за контакт";
            ws.Cell("P1").Value = "Категория на предприятие";
            ws.Cell("Q1").Value = "Продължителност на проекта";
            ws.Cell("R1").Value = "Място на изпълнение на проекта";
            ws.Cell("S1").Value = "Код по КИД 2008 на проекта";
            ws.Cell("T1").Value = "Общ размер на БФП (лв.) на подаденото проектно предложение(първа версия на проектното предложение)";
            ws.Cell("U1").Value = "Общ размер на съфинансиране(лв.) на подаденото проектно предложение(първа версия на проектното предложение)";
            ws.Cell("V1").Value = "Размер на Одобрената БФП(последна версия на проектното предложение)";
            ws.Cell("W1").Value = "Оценка ОАСД";
            ws.Cell("X1").Value = "Оценка ТФО";
            ws.Cell("Y1").Value = "Комплексна оценка";
            ws.Cell("Z1").Value = "Статус на проекта от оценката";

            var rngHeaders = ws.Range("A1", "Y1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var data in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = data.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.Procedure;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.RegNumber;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.Name;

                ws.Cell(row, 5).Style.NumberFormat.Format = "@";
                ws.Cell(row, 5).DataType = XLCellValues.DateTime;
                ws.Cell(row, 5).Value = data.RegDate.ToString("dd.MM.yyyy HH:mm");

                ws.Cell(row, 6).Style.NumberFormat.Format = "@";
                ws.Cell(row, 6).DataType = XLCellValues.DateTime;
                ws.Cell(row, 6).Value = data.RecieveDate.ToString("dd.MM.yyyy HH:mm");

                ws.Cell(row, 7).Style.NumberFormat.Format = "@";
                ws.Cell(row, 7).DataType = XLCellValues.DateTime;
                ws.Cell(row, 7).Value = data.RecieveType.GetEnumDescription();

                ws.Cell(row, 8).Style.NumberFormat.Format = "@";
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = data.CompanyUin;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = data.CompanyName;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 10).DataType = XLCellValues.Text;
                ws.Cell(row, 10).Value = data.CompanyType;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 11).DataType = XLCellValues.Text;
                ws.Cell(row, 11).Value = data.CompanyLegalType;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 12).DataType = XLCellValues.Text;
                ws.Cell(row, 12).Value = data.CompanyKidCode;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 13).DataType = XLCellValues.Text;
                ws.Cell(row, 13).Value = data.CompanyAddress;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 14).DataType = XLCellValues.Text;
                ws.Cell(row, 14).Value = data.CompanyCorrespondenceAddress;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 15).DataType = XLCellValues.Text;
                ws.Cell(row, 15).Value = data.CompanyEmail;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 16).DataType = XLCellValues.Text;
                ws.Cell(row, 16).Value = data.CompanySizeType;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.ProjectDuration;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 18).DataType = XLCellValues.Text;
                ws.Cell(row, 18).Value = data.ProjectPlace;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 19).DataType = XLCellValues.Text;
                ws.Cell(row, 19).Value = data.ProjectKidCode;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.InitialTotalBfpAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.InitialCoFinancingAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.ActualTotalBfpAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 23).DataType = XLCellValues.Text;
                ws.Cell(row, 23).Value = data.IsAdminAdmissPassed.HasValue ? data.IsAdminAdmissPassed.GetEnumDescription() : null;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 24).DataType = XLCellValues.Text;
                ws.Cell(row, 24).Value = data.IsTechFinancePassed.HasValue ? data.IsTechFinancePassed.GetEnumDescription() : null;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 25).DataType = XLCellValues.Text;
                ws.Cell(row, 25).Value = data.IsComplexPassed.HasValue ? data.IsComplexPassed.GetEnumDescription() : null;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 26).DataType = XLCellValues.Text;
                ws.Cell(row, 26).Value = data.StandingStatus.HasValue ? data.StandingStatus.GetEnumDescription() : null;

                row++;
            }

            ws.Columns(1, 2).Width = 40;
            ws.Columns(1, 2).Style.Alignment.SetWrapText();
            ws.Column(3).AdjustToContents();
            ws.Column(4).Width = 40;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 6).Width = 16;
            ws.Column(7).Width = 20;
            ws.Columns(5, 7).Style.Alignment.SetWrapText();
            ws.Column(8).AdjustToContents();
            ws.Column(9).Width = 40;
            ws.Column(10).Width = 21;
            ws.Column(11).Width = 30;
            ws.Column(12).Width = 40;
            ws.Columns(9, 12).Style.Alignment.SetWrapText();
            ws.Column(13).Width = 40;
            ws.Column(13).Style.Alignment.SetWrapText();
            ws.Column(14).Width = 40;
            ws.Column(14).Style.Alignment.SetWrapText();
            ws.Column(15).AdjustToContents();
            ws.Column(16).Width = 18;
            ws.Column(17).Width = 22;
            ws.Column(18).Width = 30;
            ws.Columns(19, 20).Width = 24;
            ws.Columns(21, 23).Width = 20;
            ws.Column(24).Width = 20;
            ws.Columns(16, 24).Style.Alignment.SetWrapText();
            ws.Columns(24, 26).AdjustToContents();

            return this.Request.CreateXmlResponse(workbook, "projects");
        }
    }
}
