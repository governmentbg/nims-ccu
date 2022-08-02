using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    public class ActuallyPaidAmountsExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActuallyPaidAmountsRepository actuallyPaidAmountsRepository;
        private IPermissionsRepository permissionsRepository;
        private IAuthorizer authorizer;

        public ActuallyPaidAmountsExcelController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActuallyPaidAmountsRepository actuallyPaidAmountsRepository,
            IPermissionsRepository permissionsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actuallyPaidAmountsRepository = actuallyPaidAmountsRepository;
            this.permissionsRepository = permissionsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/actuallyPaidAmounts/excelExport")]
        public HttpResponseMessage GetActuallyPaidAmounts(int? contractId = null, PaymentReason? paymentReason = null)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            var actuallyPaidAmounts = this.actuallyPaidAmountsRepository.GetActuallyPaidAmounts(programmeIds, this.accessContext.UserId, contractId, paymentReason);

            var workbook = this.GetWorkbook(actuallyPaidAmounts);

            return this.Request.CreateXmlResponse(workbook, "actuallyPaidAmounts");
        }

        private XLWorkbook GetWorkbook(IList<ActuallyPaidAmountVO> actuallyPaidAmounts)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("РИС");

            // Headers
            ws.Cell("A1").Value = "Програма";
            ws.Cell("B1").Value = "№ договор";
            ws.Cell("C1").Value = "Номер на ИП";
            ws.Cell("D1").Value = "Тип на ИП";
            ws.Cell("E1").Value = "Фонд";
            ws.Cell("F1").Value = "Статус";
            ws.Cell("G1").Value = "Номер";
            ws.Cell("H1").Value = "Основание за плащане";
            ws.Cell("I1").Value = "Дата на плащане";
            ws.Cell("J1").Value = "Сума";

            var rngHeaders = ws.Range(1, 1, 1, 10);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var actuallyPaidAmount in actuallyPaidAmounts)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = actuallyPaidAmount.ProgrammeName;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = actuallyPaidAmount.ContractRegNumber;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = actuallyPaidAmount.ContractReportPaymentNum;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = actuallyPaidAmount.ContractReportPaymentType.GetEnumDescription();

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = actuallyPaidAmount.ContractReportPaymentType.GetEnumDescription();

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = actuallyPaidAmount.Status.GetEnumDescription();

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = actuallyPaidAmount.RegNumber;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = actuallyPaidAmount.PaymentReason.GetEnumDescription();

                ws.Cell(rowIndex, "I").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = actuallyPaidAmount.PaymentDate.HasValue ?
                    actuallyPaidAmount.PaymentDate.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "J").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "J").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "J").Value = actuallyPaidAmount.PaidBfpTotalAmount;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 40;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).AdjustToContents();
            ws.Column(4).Width = 40;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 10).AdjustToContents();

            return workbook;
        }
    }
}
