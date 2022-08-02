using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Domain.NonAggregates;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Companies.Controllers
{
    public class CompaniesExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICompaniesRepository companiesRepository;
        private IAuthorizer authorizer;

        public CompaniesExcelController(
            IUnitOfWork unitOfWork,
            ICompaniesRepository companiesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.companiesRepository = companiesRepository;
            this.authorizer = authorizer;
        }

        [Route("api/companies/excelExport")]
        public HttpResponseMessage GetCompanies(
            string name = null,
            UinType? uinTypeId = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Search);

            var companies = this.companiesRepository.GetCompanies(name, uinTypeId, uin);

            var workbook = this.GetWorkbook(companies);

            return this.Request.CreateXmlResponse(workbook, "companies");
        }

        private XLWorkbook GetWorkbook(IList<CompaniesVO> companies)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Кандидати");

            // Headers
            ws.Cell("A1").Value = "Име";
            ws.Cell("B1").Value = "Идентификатор";
            ws.Cell("C1").Value = "Булстат/ЕИК/ЕГН";
            ws.Cell("D1").Value = "Седалище и адрес на управление";
            ws.Cell("E1").Value = "Адрес за кореспонденция";

            var rngHeaders = ws.Range(1, 1, 1, 5);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var company in companies)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = company.Name;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = company.UinType.GetEnumDescription();

                ws.Cell(rowIndex, "C").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = company.Uin;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = company.Seat;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = company.Corr;

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 5).AdjustToContents();

            return workbook;
        }
    }
}
