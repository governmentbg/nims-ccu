using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    public class AnnualAccountReportCertifiedRevalidationFinancialCorrectionsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAuthorizer authorizer;

        public AnnualAccountReportCertifiedRevalidationFinancialCorrectionsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/annualAccountReports/{annualAccountReportId:int}/certifiedRevalidationFinancialCorrections/excelExport")]
        public HttpResponseMessage GetAnnualAccountReportCertifiedRevalidationFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var annualAccountReportRevalidationFinancialCorrections = this.annualAccountReportsRepository.GetAnnualAccountReportCertRevalidationFinancialCorrections(annualAccountReportId);

            var workbook = this.GetWorkbook(annualAccountReportRevalidationFinancialCorrections);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<AnnualAccountReportCertRevalidationFinancialCorrectionVO> certifiedFinancialCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(СО) преп. РОД ГСО");

            // Headers
            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Договор";
            ws.Cell("C1").Value = "Процедура";
            ws.Cell("D1").Value = "Номер на пакет";
            ws.Cell("E1").Value = "Корекция сума-БФП";
            ws.Cell("F1").Value = "Корекция сума-СФ";
            ws.Cell("G1").Value = "Статус";
            ws.Cell("H1").Value = "Пореден номер";
            ws.Cell("I1").Value = "Дата на създаване";
            ws.Cell("J1").Value = "Бележки";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var annualAccountReportFinancialCorrection in certifiedFinancialCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = annualAccountReportFinancialCorrection.ContractRegNum;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = annualAccountReportFinancialCorrection.ContractName;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = annualAccountReportFinancialCorrection.ProcedureName;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = annualAccountReportFinancialCorrection.ReportOrderNum;

                ws.Cell(rowIndex, "E").Value = annualAccountReportFinancialCorrection.CorrectedBfpTotalAmount;

                ws.Cell(rowIndex, "F").Value = annualAccountReportFinancialCorrection.CorrectedSelfAmount;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = annualAccountReportFinancialCorrection.Status.GetEnumDescription();

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = annualAccountReportFinancialCorrection.OrderNum;

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = annualAccountReportFinancialCorrection.CreateDate.ToString("dd.MM.yyyy");

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = annualAccountReportFinancialCorrection.Notes;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).AdjustToContents();
            ws.Columns(2, 3).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).Width = 40;
            ws.Columns(4, 10).AdjustToContents();

            return workbook;
        }
    }
}
