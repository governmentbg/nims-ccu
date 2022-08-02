using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/beneficiaryProjectsContracts")]
    public class MonitoringBeneficiaryProjectsContractsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringBeneficiaryProjectsContractsController(
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
        public IList<BeneficiaryProjectsContractsReportItem> GetBeneficiaryProjectsContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            return this.monitoringReportsRepository.GetBeneficiaryProjectsContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                companyTypeId,
                companyLegalTypeId);
        }

        [Route("export")]
        public HttpResponseMessage GetBeneficiaryProjectsContractsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var report = this.monitoringReportsRepository.GetBeneficiaryProjectsContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                companyTypeId,
                companyLegalTypeId);

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Бенефициенти");

            // Headers
            ws.Cell("A1").Value = "Име на бенефициент";
            ws.Cell("B1").Value = "ЕИК на бенефициент";
            ws.Cell("C1").Value = "Тип на организацията";
            ws.Cell("D1").Value = "Вид на организацията";
            ws.Cell("E1").Value = "Брой подадени проектни предложения";
            ws.Cell("F1").Value = "Обща стойност на подадените проектни предложения";
            ws.Cell("G1").Value = "Стойност нa БФП на подадените проектни предложения";
            ws.Cell("H1").Value = "Стойност нa собственото финансиране на подадените проектни предложения";
            ws.Cell("I1").Value = "Брой сключени договори за предоставяне на БФП";
            ws.Cell("J1").Value = "Обща стойност на проектите по сключените договори за предоставяне на БФП";
            ws.Cell("K1").Value = "БФП на сключените договори за предоставяне на БФП – ЕС";
            ws.Cell("L1").Value = "БФП на сключените договори за предоставяне на БФП - НФ";
            ws.Cell("M1").Value = "Стойност нa собственото финансиране по сключените договори за предоставяне на БФП";
            ws.Cell("N1").Value = "Обща стойност нa реално изплатените суми";
            ws.Cell("O1").Value = "Стойност нa финансирането от ЕС нa реално изплатените суми";
            ws.Cell("P1").Value = "Стойност нa НФ нa реално изплатените суми";
            ws.Cell("Q1").Value = "Подадени сигнали за нередности срещу бенефициента";
            ws.Cell("R1").Value = "Активни сигнали";
            ws.Cell("S1").Value = "Брой регистрирани нередности";
            ws.Cell("T1").Value = "Абсолютна стойност на наложената финансова корекция - Общо";
            ws.Cell("U1").Value = "Абсолютна стойност на наложената финансова корекция - БФП";
            ws.Cell("V1").Value = "Абсолютна стойност на наложената финансова корекция - Собствено финансиране";
            ws.Cell("W1").Value = "Стойност на извършените финансови корекции - Общо";
            ws.Cell("X1").Value = "Стойност на извършените финансови корекции - БФП";
            ws.Cell("Y1").Value = "Стойност на извършените финансови корекции - Собствено финансиране";

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
                ws.Cell(row, 1).Value = data.CompanyName;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = data.CompanyUin;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = data.CompanyType;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = data.CompanyLegalType;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = data.ProjectsCount;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = data.ProjectsTotalAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = data.ProjectsBfpAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = data.ProjectsSelfAmount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = data.ContractsCount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = data.ContractsTotalAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = data.ContractsEuAmount;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = data.ContractsBgAmount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = data.ContractsSelfAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = data.ActuallyPaidTotalAmount;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = data.ActuallyPaidEuAmount;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = data.ActuallyPaidBgAmount;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = data.IrregularitySignalsCount;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = data.IrregularitySignalsActiveCount;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = data.IrregularitiesCount;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = data.FinancialCorrectionTotalAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = data.FinancialCorrectionBfpAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = data.FinancialCorrectionSelfAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = data.CorrectionsTotalAmount;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = data.CorrectionsBfpAmount;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = data.CorrectionsSelfAmount;

                row++;
            }

            ws.Column(1).Width = 50;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 5).AdjustToContents();
            ws.Columns(6, 25).Width = 25;
            ws.Columns(6, 25).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "beneficiaries");
        }
    }
}
