using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    public class CertAuthorityChecksExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;
        private IPermissionsRepository permissionsRepository;
        private IAuthorizer authorizer;

        public CertAuthorityChecksExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            ICertAuthorityChecksRepository certAuthorityChecksRepository,
            IPermissionsRepository permissionsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
            this.permissionsRepository = permissionsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/certAuthorityChecks/excelExport")]
        public HttpResponseMessage GetCertAuthorityChecks(CertAuthorityCheckStatus? status = null, CertAuthorityCheckType? type = null)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            var certAuthorityChecks = this.certAuthorityChecksRepository.GetCertAuthorityCheks(status, type);

            var workbook = this.GetWorkbook(certAuthorityChecks);

            return this.Request.CreateXmlResponse(workbook, "certAuthorityChecks");
        }

        private XLWorkbook GetWorkbook(IList<CertAuthorityCheckVO> certAuthorityChecks)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("РИС");

            // Headers
            ws.Cell("A1").Value = "Статус";
            ws.Cell("B1").Value = "Пореден номер";
            ws.Cell("C1").Value = "Тип";
            ws.Cell("D1").Value = "Период - от";
            ws.Cell("E1").Value = "Период - до";
            ws.Cell("F1").Value = "Вид на проверяваната институция";
            ws.Cell("G1").Value = "Наименование на проверявания";
            ws.Cell("H1").Value = "ОП";
            ws.Cell("I1").Value = "Обхват на проверката";
            ws.Cell("J1").Value = "Проверяван проект";
            ws.Cell("K1").Value = "Констатации";
            ws.Cell("L1").Value = "Препоръки";
            ws.Cell("M1").Value = "Статус на препоръките";

            var rngHeaders = ws.Range(1, 1, 1, 13);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 13).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var certAuthorityCheck in certAuthorityChecks)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = certAuthorityCheck.Status.GetEnumDescription();

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "B").Value = certAuthorityCheck.CheckNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = certAuthorityCheck.Type.GetEnumDescription();

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = certAuthorityCheck.DateFrom.HasValue ?
                    certAuthorityCheck.DateFrom.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "E").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = certAuthorityCheck.DateTo.HasValue ?
                    certAuthorityCheck.DateTo.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = certAuthorityCheck.SubjectType.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = certAuthorityCheck.SubjectName;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .ProgrammeShortNames);

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .ItemCodes);

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .ProjectCodes);

                ws.Cell(rowIndex, "K").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "K").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "K").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .Ascertainments.Select(a => a.Ascertainment));

                ws.Cell(rowIndex, "L").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "L").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "L").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .Ascertainments.Select(a => a.Recommendation));

                ws.Cell(rowIndex, "M").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "M").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "M").Value = string.Join(Environment.NewLine, certAuthorityCheck
                    .RecommendationExecutionStatuses.Select(s => s.RecommendationExecutionStatus == null ? "---" : s.RecommendationExecutionStatus.GetEnumDescription()));

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 15;
            ws.Column(2).Width = 10;
            ws.Columns(3, 5).Width = 15;
            ws.Columns(6, 13).AdjustToContents();

            return workbook;
        }
    }
}
