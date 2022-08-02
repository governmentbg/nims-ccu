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
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Prognoses.Repositories;
using Eumis.Data.Prognoses.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    [RoutePrefix("api/prognosisReports")]
    public class PrognosisReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IPrognosesRepository prognosesRepository;
        private IProgrammesRepository programmesRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IAuthorizer authorizer;

        public PrognosisReportsController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IPrognosesRepository prognosesRepository,
            IProgrammesRepository programmesRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.prognosesRepository = prognosesRepository;
            this.programmesRepository = programmesRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.authorizer = authorizer;
        }

        [Route("yearlyPrognoses")]
        public IList<PrognosisYearlyReportVO> GetYearlyReport(int programmeId, [FromUri]Year[] years)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewYearlyReport, programmeId);

            return this.prognosesRepository.GetYearlyPrognosisReport(programmeId, years);
        }

        [Route("yearlyPrognoses/excel")]
        public HttpResponseMessage GetYearlyReportExcel(int programmeId, [FromUri]Year[] years)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewYearlyReport, programmeId);

            var workbook = new XLWorkbook();

            var report = this.prognosesRepository.GetYearlyPrognosisReport(programmeId, years);
            if (report.Count == 0)
            {
                var ws = workbook.Worksheets.Add("Годишни прогнози");
                ws.Cell("A1").Value = "Няма въведени приоритетни оси за избраната програма, по които да се направи справката.";
                ws.Range("A1", "J1").Merge();
            }

            return this.Request.CreateXmlResponse(workbook, "yearly_report");
        }

        [Route("monthlyPrognoses")]
        public IList<PrognosisMonthlyReportVO> GetMonthlyReport(int programmeId, Year year, [FromUri]Month[] months)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewMonthlyReport, programmeId);

            return this.prognosesRepository.GetMonthlyPrognosisReport(programmeId, year, months);
        }

        [Route("monthlyPrognoses/excel")]
        public HttpResponseMessage GetMonthlyReportExcel(int programmeId, Year year, [FromUri]Month[] months)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewMonthlyReport, programmeId);

            var workbook = new XLWorkbook();

            var report = this.prognosesRepository.GetMonthlyPrognosisReport(programmeId, year, months);
            if (report.Count == 0)
            {
                var ws = workbook.Worksheets.Add("Годишни прогнози");
                ws.Cell("A1").Value = "Няма въведени приоритетни оси за избраната програма, по които да се направи справката.";
                ws.Range("A1", "J1").Merge();
            }

            return this.Request.CreateXmlResponse(workbook, "monthly_report");
        }

        [Route("programmePriorityPrognoses")]
        public IList<PrognosisProgrammePriorityReportVO> GetProgrammePriorityReport(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewProgrammePriorityReport, programmePriorityId);

            return this.prognosesRepository.GetProgrammePriorityPrognosisReport(programmePriorityId);
        }

        [Route("programmePriorityPrognoses/excel")]
        public HttpResponseMessage GetProgrammePriorityReportExcel(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewProgrammePriorityReport, programmePriorityId);

            var report = this.prognosesRepository.GetProgrammePriorityPrognosisReport(programmePriorityId);

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Prognoses");

            ws.Cell("A1").Value = "Priority axis";
            ws.Cell("B1").Value = "Year";
            ws.Cell("C1").Value = "Quarter";
            ws.Cell("D1").Value = "N+3 with advances";
            ws.Cell("E1").Value = "N+3 without advance";

            ws.Cell("F1").Value = "Prognosed contracted amounts in euro";
            ws.Cell("G1").Value = "Contracted amounts in euro";
            ws.Cell("H1").Value = "Percent contracted";

            ws.Cell("I1").Value = "Prognosed approved amounts in euro";
            ws.Cell("J1").Value = "Approved amounts in euro";
            ws.Cell("K1").Value = "Percent approved";

            ws.Cell("L1").Value = "Prognosed certified amounts in euro";
            ws.Cell("M1").Value = "Certified amounts in euro";
            ws.Cell("N1").Value = "Percent certified amounts";

            var rngHeaders = ws.Range("A1", "N1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.ProgrammePriority;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = item.Year.GetEnumDescription();

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.Quarter;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = item.NextThreeWithAdvances;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = item.NextThreeWithoutAdvances;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = item.PrognosedContractedBfpAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = item.ContractsBfpAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = item.ContractedPercent;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = item.PrognosedApprovedBfpAmount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = item.ApprovedBfpAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = item.ApprovedPercent;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = item.PrognosedCertifiedBfpAmount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = item.CertifiedBfpAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = item.CertifiedPercent;

                row++;
            }

            ws.Column(1).Width = 35;
            ws.Column(2).Width = 10;
            ws.Column(3).Width = 13;
            ws.Columns(4, 7).Width = 20;
            ws.Column(8).Width = 13;
            ws.Columns(9, 10).Width = 20;
            ws.Column(11).Width = 13;
            ws.Columns(12, 13).Width = 20;
            ws.Column(14).Width = 13;
            ws.Columns(1, 14).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "programme_priority_report");
        }

        [Route("programmePrognoses")]
        public IList<PrognosisProgrammeReportVO> GetProgrammeReport(int programmeId)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewProgrammeReport, programmeId);

            return this.prognosesRepository.GetProgrammePrognosisReport(programmeId);
        }

        [Route("programmePrognoses/excel")]
        public HttpResponseMessage GetProgrammeReportExcel(int programmeId)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.ViewProgrammeReport, programmeId);

            var report = this.prognosesRepository.GetProgrammePrognosisReport(programmeId);

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Prognoses");

            ws.Cell("A1").Value = "Programme";
            ws.Cell("B1").Value = "Year";
            ws.Cell("C1").Value = "Quarter";
            ws.Cell("D1").Value = "N+3 with advances";
            ws.Cell("E1").Value = "N+3 without advance";

            ws.Cell("F1").Value = "Prognosed contracted amounts in euro";
            ws.Cell("G1").Value = "Contracted amounts in euro";
            ws.Cell("H1").Value = "Percent contracted";

            ws.Cell("I1").Value = "Prognosed approved amounts in euro";
            ws.Cell("J1").Value = "Approved amounts in euro";
            ws.Cell("K1").Value = "Percent approved";

            ws.Cell("L1").Value = "Prognosed certified amounts in euro";
            ws.Cell("M1").Value = "Certified amounts in euro";
            ws.Cell("N1").Value = "Percent certified amounts";

            var rngHeaders = ws.Range("A1", "N1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.Programme;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = item.Year.GetEnumDescription();

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.Quarter;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = item.NextThreeWithAdvances;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 5).DataType = XLCellValues.Number;
                ws.Cell(row, 5).Value = item.NextThreeWithoutAdvances;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = item.PrognosedContractedBfpAmount;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = item.ContractsBfpAmount;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = item.ContractedPercent;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = item.PrognosedApprovedBfpAmount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = item.ApprovedBfpAmount;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = item.ApprovedPercent;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = item.PrognosedCertifiedBfpAmount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = item.CertifiedBfpAmount;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = item.CertifiedPercent;

                row++;
            }

            ws.Column(1).Width = 35;
            ws.Column(2).Width = 10;
            ws.Column(3).Width = 13;
            ws.Columns(4, 7).Width = 20;
            ws.Column(8).Width = 13;
            ws.Columns(9, 10).Width = 20;
            ws.Column(11).Width = 13;
            ws.Columns(12, 13).Width = 20;
            ws.Column(14).Width = 13;
            ws.Columns(1, 14).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "programme_report");
        }

        [Route("summary")]
        public IList<PrognosisSummaryReportVO> GetSummaryReport()
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Search);

            return this.prognosesRepository.GetPrognosisSummaryReport();
        }

        [Route("summary/excel")]
        public HttpResponseMessage GetSummaryReportExcel()
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Search);

            var report = this.prognosesRepository.GetPrognosisSummaryReport();

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value = "Procedure";
            ws.Range("A1", "G1").Merge();
            ws.Cell("A2").Value = "Name";
            ws.Cell("B2").Value = "Programme";
            ws.Cell("C2").Value = "Priority axis";
            ws.Cell("D2").Value = "Total budget in euro";
            ws.Cell("E2").Value = "Listing date";
            ws.Cell("F2").Value = "Eu financing percent";
            ws.Cell("G2").Value = "Percent of procedure's budget compared to all procedures";

            ws.Cell("H1").Value = "Registered projects";
            ws.Range("H1", "H2").Merge();
            ws.Cell("I1").Value = "Approved projects";
            ws.Range("I1", "I2").Merge();
            ws.Cell("J1").Value = "Approved projects budget in euro";
            ws.Range("J1", "J2").Merge();
            ws.Cell("K1").Value = "Percent approved projects budget compared to procedure's budget";
            ws.Range("K1", "K2").Merge();

            ws.Cell("L1").Value = "Contracts";
            ws.Range("L1", "L2").Merge();
            ws.Cell("M1").Value = "Contracts budget in euro";
            ws.Range("M1", "M2").Merge();
            ws.Cell("N1").Value = "Contracts budget financed by EU in euro";
            ws.Range("N1", "N2").Merge();
            ws.Cell("O1").Value = "Contracts budget financed by EU compared to procedure's budget financed by EU";
            ws.Range("O1", "O2").Merge();
            ws.Cell("P1").Value = "Prognosed contracted amounts";
            ws.Range("P1", "P2").Merge();
            ws.Cell("Q1").Value = "Percent contracted";
            ws.Range("Q1", "Q2").Merge();

            ws.Cell("R1").Value = "Intermediate and final payment amounts in euro";
            ws.Range("R1", "R2").Merge();
            ws.Cell("S1").Value = "Approved intermediate and final payment amounts in euro";
            ws.Range("S1", "S2").Merge();

            ws.Cell("T1").Value = "Approved amounts in euro";
            ws.Range("T1", "T2").Merge();
            ws.Cell("U1").Value = "Paid amounts in euro";
            ws.Range("U1", "U2").Merge();
            ws.Cell("V1").Value = "Prognosed approved amounts in euro";
            ws.Range("V1", "V2").Merge();
            ws.Cell("W1").Value = "Percent approved";
            ws.Range("W1", "W2").Merge();

            ws.Cell("X1").Value = "Requested amounts in euro";
            ws.Range("X1", "X2").Merge();
            ws.Cell("Y1").Value = "Certified amounts in euro";
            ws.Range("Y1", "Y2").Merge();
            ws.Cell("Z1").Value = "Percent certified compared to procedure's budget";
            ws.Range("Z1", "Z2").Merge();
            ws.Cell("AA1").Value = "Prognosed certified amounts in euro";
            ws.Range("AA1", "AA2").Merge();
            ws.Cell("AB1").Value = "Percent certified amounts";
            ws.Range("AB1", "AB2").Merge();

            var rngHeaders = ws.Range("A1", "AB2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range("A2", "AB2").Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 3;
            foreach (var procedure in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = procedure.ProcedureNameAlt;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Text;
                ws.Cell(row, 2).Value = procedure.Programme;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = procedure.ProgrammePriority;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 4).DataType = XLCellValues.Number;
                ws.Cell(row, 4).Value = procedure.ProcedureBudget;

                ws.Cell(row, 5).Style.NumberFormat.Format = "@";
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = null;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = procedure.EuPercent;

                ws.Cell(row, 7).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 7).DataType = XLCellValues.Number;
                ws.Cell(row, 7).Value = procedure.ProcedurePercent;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 8).DataType = XLCellValues.Number;
                ws.Cell(row, 8).Value = procedure.ProjectsCount;

                ws.Cell(row, 9).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 9).DataType = XLCellValues.Number;
                ws.Cell(row, 9).Value = procedure.ApprovedProjectsCount;

                ws.Cell(row, 10).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 10).DataType = XLCellValues.Number;
                ws.Cell(row, 10).Value = procedure.ApprovedProjectsBudget;

                ws.Cell(row, 11).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 11).DataType = XLCellValues.Number;
                ws.Cell(row, 11).Value = procedure.ApprovedProjectsPercent;

                ws.Cell(row, 12).Style.NumberFormat.NumberFormatId = 3;
                ws.Cell(row, 12).DataType = XLCellValues.Number;
                ws.Cell(row, 12).Value = procedure.ContractsCount;

                ws.Cell(row, 13).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 13).DataType = XLCellValues.Number;
                ws.Cell(row, 13).Value = procedure.ContractsBfpBudget;

                ws.Cell(row, 14).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 14).DataType = XLCellValues.Number;
                ws.Cell(row, 14).Value = procedure.ContractsEuBudget;

                ws.Cell(row, 15).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 15).DataType = XLCellValues.Number;
                ws.Cell(row, 15).Value = procedure.ContractsEuPercent;

                ws.Cell(row, 16).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 16).DataType = XLCellValues.Number;
                ws.Cell(row, 16).Value = procedure.PrognosedContractedBfpAmount;

                ws.Cell(row, 17).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 17).DataType = XLCellValues.Number;
                ws.Cell(row, 17).Value = procedure.PrognosedContractedPercent;

                ws.Cell(row, 18).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 18).DataType = XLCellValues.Number;
                ws.Cell(row, 18).Value = procedure.PaymentsBfpAmount;

                ws.Cell(row, 19).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 19).DataType = XLCellValues.Number;
                ws.Cell(row, 19).Value = procedure.ApprovedPaymentsBfpAmount;

                ws.Cell(row, 20).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 20).DataType = XLCellValues.Number;
                ws.Cell(row, 20).Value = procedure.ApprovedBfpAmount;

                ws.Cell(row, 21).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 21).DataType = XLCellValues.Number;
                ws.Cell(row, 21).Value = procedure.ActuallyPaidAmount;

                ws.Cell(row, 22).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 22).DataType = XLCellValues.Number;
                ws.Cell(row, 22).Value = procedure.PrognosedApprovedBfpAmount;

                ws.Cell(row, 23).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 23).DataType = XLCellValues.Number;
                ws.Cell(row, 23).Value = procedure.PrognosedApprovedPercent;

                ws.Cell(row, 24).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 24).DataType = XLCellValues.Number;
                ws.Cell(row, 24).Value = procedure.RequestedBfpAmount;

                ws.Cell(row, 25).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 25).DataType = XLCellValues.Number;
                ws.Cell(row, 25).Value = procedure.CertifiedBfpAmount;

                ws.Cell(row, 26).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 26).DataType = XLCellValues.Number;
                ws.Cell(row, 26).Value = procedure.CertifiedPercent;

                ws.Cell(row, 27).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 27).DataType = XLCellValues.Number;
                ws.Cell(row, 27).Value = procedure.PrognosedCertifiedBfpAmount;

                ws.Cell(row, 28).Style.NumberFormat.NumberFormatId = 10;
                ws.Cell(row, 28).DataType = XLCellValues.Number;
                ws.Cell(row, 28).Value = procedure.PrognosedCertifiedPercent;

                row++;
            }

            ws.Columns(1, 3).Width = 35;
            ws.Column(4).Width = 20;
            ws.Columns(5, 7).Width = 13;
            ws.Columns(8, 9).Width = 11;
            ws.Column(10).Width = 20;
            ws.Column(11).Width = 17;
            ws.Column(12).Width = 11;
            ws.Columns(13, 16).Width = 20;
            ws.Column(17).Width = 13;
            ws.Columns(18, 22).Width = 20;
            ws.Column(23).Width = 13;
            ws.Columns(24, 25).Width = 20;
            ws.Column(26).Width = 13;
            ws.Column(27).Width = 20;
            ws.Column(28).Width = 13;
            ws.Columns(1, 28).Style.Alignment.SetWrapText();

            return this.Request.CreateXmlResponse(workbook, "summary_report");
        }

        private XLWorkbook AddYearlyReportWorksheet(
            XLWorkbook workbook,
            Year year,
            IList<PrognosisYearlyReportVO> ppPrognoses)
        {
            var ws = workbook.Worksheets.Add(string.Format("Прогнози за {0}г.", year.GetEnumDescription()));

            // Headers
            ws.Range("A1", "A3").Merge();

            var column = 2;
            foreach (var ppPrognosis in ppPrognoses)
            {
                ws.Cell(1, column).Value = string.Format("{0} {1}", ppPrognosis.ProgrammePriorityCode, ppPrognosis.ProgrammePriorityName);
                ws.Range(1, column, 1, column + 3).Merge();

                ws.Cell(2, column).Value = "авансови";
                ws.Range(2, column, 2, column + 1).Merge();
                ws.Cell(3, column).Value = "авансови, които не подлежат на верификация";
                ws.Cell(3, column + 1).Value = "авансови, които подлежат на верификация съгласно чл. 131 от Регламент ЕС № 1303/2013";

                ws.Cell(2, column + 2).Value = "междинни";
                ws.Range(2, column + 2, 3, column + 2).Merge();

                ws.Cell(2, column + 3).Value = "окончателни";
                ws.Range(2, column + 3, 3, column + 3).Merge();

                column += 4;
            }

            var rngHeaders = ws.Range(1, 2, 3, column - 1);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(3, 2, 3, column - 1).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            ws.Cell(4, 1).Value = "Общо за годината";
            ws.Cell(4, 1).Style.Font.Bold = true;

            column = 2;
            foreach (var ppPrognosis in ppPrognoses)
            {
                var total = ppPrognosis.ReportItems.Where(p => p.Year == year);

                ws.Cell(4, column).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(4, column).DataType = XLCellValues.Number;
                ws.Cell(4, column).Value = total.Sum(p => p.AdvancePaymentAmount);

                ws.Cell(4, column + 1).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(4, column + 1).DataType = XLCellValues.Number;
                ws.Cell(4, column + 1).Value = total.Sum(p => p.AdvanceVerPaymentAmount);

                ws.Cell(4, column + 2).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(4, column + 2).DataType = XLCellValues.Number;
                ws.Cell(4, column + 2).Value = total.Sum(p => p.IntermediatePaymentAmount);

                ws.Cell(4, column + 3).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(4, column + 3).DataType = XLCellValues.Number;
                ws.Cell(4, column + 3).Value = total.Sum(p => p.FinalPaymentAmount);

                column += 4;
            }

            var row = 5;

            var quarters = Enum.GetValues(typeof(Quarter))
                .Cast<Quarter>();
            foreach (var quarter in quarters)
            {
                ws.Cell(row, 1).Value = quarter.GetEnumDescription();
                ws.Cell(row, 1).Style.Font.Bold = true;

                column = 2;
                foreach (var ppPrognosis in ppPrognoses)
                {
                    var total = ppPrognosis.ReportItems.Where(p => p.Year == year && p.Quarter == quarter);

                    ws.Cell(row, column).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column).DataType = XLCellValues.Number;
                    ws.Cell(row, column).Value = total.Sum(p => p.AdvancePaymentAmount);

                    ws.Cell(row, column + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 1).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 1).Value = total.Sum(p => p.AdvanceVerPaymentAmount);

                    ws.Cell(row, column + 2).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 2).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 2).Value = total.Sum(p => p.IntermediatePaymentAmount);

                    ws.Cell(row, column + 3).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 3).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 3).Value = total.Sum(p => p.FinalPaymentAmount);

                    column += 4;
                }

                row++;
            }

            var verticalHeaders = ws.Range(4, 1, row - 1, 1);
            verticalHeaders.Style.Alignment.SetWrapText();
            verticalHeaders.Style.Border.RightBorder = XLBorderStyleValues.Double;

            ws.Column(1).Width = 16;
            ws.Columns(2, column - 1).Width = 23;
            ws.Columns(2, column - 1).Style.Alignment.SetWrapText();

            return workbook;
        }

        private XLWorkbook AddMonthlyReportWorksheet(
            XLWorkbook workbook,
            Year year,
            Month[] months,
            IList<PrognosisMonthlyReportVO> ppPrognoses)
        {
            var ws = workbook.Worksheets.Add("Месечни прогнози");

            // Headers
            ws.Range("A1", "A3").Merge();

            var column = 2;
            foreach (var ppPrognosis in ppPrognoses)
            {
                ws.Cell(1, column).Value = string.Format("{0} {1}", ppPrognosis.ProgrammePriorityCode, ppPrognosis.ProgrammePriorityName);
                ws.Range(1, column, 1, column + 3).Merge();

                ws.Cell(2, column).Value = "авансови";
                ws.Range(2, column, 2, column + 1).Merge();
                ws.Cell(3, column).Value = "авансови, които не подлежат на верификация";
                ws.Cell(3, column + 1).Value = "авансови, които подлежат на верификация съгласно чл. 131 от Регламент ЕС № 1303/2013";

                ws.Cell(2, column + 2).Value = "междинни";
                ws.Range(2, column + 2, 3, column + 2).Merge();

                ws.Cell(2, column + 3).Value = "окончателни";
                ws.Range(2, column + 3, 3, column + 3).Merge();

                column += 4;
            }

            var rngHeaders = ws.Range(1, 2, 3, column - 1);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(3, 2, 3, column - 1).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 4;
            foreach (var month in months.OrderBy(m => (int)m))
            {
                ws.Cell(row, 1).Style.NumberFormat.Format = "@";
                ws.Cell(row, 1).Value = string.Format("{0} {1}", month.GetEnumDescription(), year.GetEnumDescription());
                ws.Cell(row, 1).Style.Font.Bold = true;

                column = 2;
                foreach (var ppPrognosis in ppPrognoses)
                {
                    var total = ppPrognosis.ReportItems.Where(p => p.Month == month);

                    ws.Cell(row, column).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column).DataType = XLCellValues.Number;
                    ws.Cell(row, column).Value = total.Sum(p => p.AdvancePaymentAmount);

                    ws.Cell(row, column + 1).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 1).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 1).Value = total.Sum(p => p.AdvanceVerPaymentAmount);

                    ws.Cell(row, column + 2).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 2).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 2).Value = total.Sum(p => p.IntermediatePaymentAmount);

                    ws.Cell(row, column + 3).Style.NumberFormat.NumberFormatId = 2;
                    ws.Cell(row, column + 3).DataType = XLCellValues.Number;
                    ws.Cell(row, column + 3).Value = total.Sum(p => p.FinalPaymentAmount);

                    column += 4;
                }

                row++;
            }

            var verticalHeaders = ws.Range(4, 1, row - 1, 1);
            verticalHeaders.Style.Alignment.SetWrapText();
            verticalHeaders.Style.Border.RightBorder = XLBorderStyleValues.Double;

            ws.Column(1).Width = 18;
            ws.Columns(2, column - 1).Width = 23;
            ws.Columns(2, column - 1).Style.Alignment.SetWrapText();

            return workbook;
        }
    }
}
