using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;
using Eumis.Domain.NonAggregates;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    public class UsersExcelController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUsersRepository usersRepository;
        private IUserClaimsContext currentUserClaimsContext;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;

        public UsersExcelController(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            UserClaimsContextFactory userClaimsContextFactory,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("api/users/excelExport")]
        public HttpResponseMessage GetUsers(
            string username = null,
            string fullname = null,
            int? userOrganizationId = null,
            bool? active = null,
            bool? deleted = null,
            bool? locked = null,
            bool? hasAcceptedGDPRDeclaration = null,
            bool exact = false)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Search);

            IList<UserVO> users;

            if (this.currentUserClaimsContext.IsSuperUser)
            {
                users = this.usersRepository.GetUsers(username, fullname, userOrganizationId, active, deleted, locked, hasAcceptedGDPRDeclaration, exact);
            }
            else
            {
                // only super users can view/search users of other UserTypes
                users = this.usersRepository.GetUsers(username, fullname, this.currentUserClaimsContext.UserOrganizationId, active, deleted, locked, hasAcceptedGDPRDeclaration, exact);
            }

            var workbook = this.GetWorkbook(users);

            return this.Request.CreateXmlResponse(workbook, "users");
        }

        private XLWorkbook GetWorkbook(IList<UserVO> users)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Потребители");

            // Headers
            ws.Cell("A1").Value = "Потребителско име";
            ws.Cell("B1").Value = "Организация";
            ws.Cell("C1").Value = "Група";
            ws.Cell("D1").Value = "Име";
            ws.Cell("E1").Value = "E-mail";
            ws.Cell("F1").Value = "Статус";
            ws.Cell("G1").Value = "Декларация - Л.Д.";

            var rngHeaders = ws.Range(1, 1, 1, 7);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var user in users)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = user.Username;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = user.UserOrganization;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = user.UserType;

                ws.Cell(rowIndex, "D").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = user.Fullname;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = user.Email;

                ws.Cell(rowIndex, "F").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value =
                    (user.IsActive ? "Активен" : "Неактивен") + (user.IsLocked ? ", Заключен" : string.Empty) + (user.IsDeleted ? ", Изтрит" : string.Empty);

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "G").Value = user.HasAcceptedGDPRDeclaration ? "Да" : "Не";

                rowIndex++;
            }

            // Style cells
            ws.Columns(1, 6).AdjustToContents();

            return workbook;
        }
    }
}
