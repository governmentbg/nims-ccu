using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Registrations.ViewObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Registrations.Controllers
{
    public class RegistrationsExcelController : ApiController
    {
        private IRegistrationsRepository registrationsRepository;
        private IAuthorizer authorizer;

        public RegistrationsExcelController(IRegistrationsRepository registrationsRepository, IAuthorizer authorizer)
        {
            this.registrationsRepository = registrationsRepository;
            this.authorizer = authorizer;
        }

        [Route("api/registrations/excelExport")]
        public HttpResponseMessage GetRegistrations()
        {
            this.authorizer.AssertCanDo(RegistrationListActions.Search);

            var registrations = this.registrationsRepository.GetRegistrations();

            var workbook = this.GetWorkbook(registrations);

            return this.Request.CreateXmlResponse(workbook, "registrations");
        }

        private XLWorkbook GetWorkbook(IList<RegistrationsVO> registrations)
        {
            var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Профили за кандидатстване");

            // Headers
            worksheet.Cell("A1").Value = "Е-mail";
            worksheet.Cell("B1").Value = "Собствено име";
            worksheet.Cell("C1").Value = "Фамилия";
            worksheet.Cell("D1").Value = "Телефон";

            var rngHeaders = worksheet.Range(1, 1, 1, 4);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            worksheet.Range(1, 1, 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var registration in registrations)
            {
                worksheet.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                worksheet.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                worksheet.Cell(rowIndex, "A").Value = registration.Email;

                worksheet.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                worksheet.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                worksheet.Cell(rowIndex, "B").Value = registration.FirstName;

                worksheet.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                worksheet.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                worksheet.Cell(rowIndex, "C").Value = registration.LastName;

                worksheet.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                worksheet.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                worksheet.Cell(rowIndex, "D").Value = registration.Phone;

                rowIndex++;
            }

            // Styles
            worksheet.Columns(1, 4).AdjustToContents();

            return workbook;
        }
    }
}
