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
    public class AnnualAccountReportCertifiedCorrectionsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IAnnualAccountReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public AnnualAccountReportCertifiedCorrectionsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IAnnualAccountReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/annualAccountReports/{annualAccountReportId:int}/certifiedCorrections/excelExport")]
        public HttpResponseMessage GetAnnualAccountReportCertifiedCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var certReportCorrections = this.certReportsRepository.GetAnnualAccountReportCertCorrections(annualAccountReportId);

            var workbook = this.GetWorkbook(certReportCorrections);

            return this.Request.CreateXmlResponse(workbook, "export");
        }

        private XLWorkbook GetWorkbook(IList<ContractReportCertAuthorityCorrectionVO> certifiedCorrections)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Корекции(ВС) към ДС");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "Статус";
            ws.Cell("C1").Value = "Номер";
            ws.Cell("J1").Value = "Тип";
            ws.Cell("K1").Value = "Дата";

            var rngHeaders = ws.Range(1, 1, 1, 11);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 11).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certAuthorityCorrection in certifiedCorrections)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certAuthorityCorrection.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = certAuthorityCorrection.StatusDescr.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certAuthorityCorrection.RegNumber;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = certAuthorityCorrection.Type.GetEnumDescription();

                ws.Cell(rowIndex, "K").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = certAuthorityCorrection.Date.ToString("dd.MM.yyyy");

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 11).AdjustToContents();

            return workbook;
        }
    }
}
