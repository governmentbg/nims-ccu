using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Eumis.Components;
using Eumis.Components.Web;
using System.IO;
using Eumis.Documents.Mappers;
using Eumis.Portal.Model.Repositories;
using Eumis.Common.Linq;
using Eumis.Portal.Model.Entities;
using Eumis.Documents.Enums;
using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using Eumis.Common;
using System.Threading.Tasks;
using System.Net.Http;
using ClosedXML.Excel;
using System.Globalization;
using System.Text.RegularExpressions;
using Eumis.Portal.Web.Helpers;
using System.Net.Http.Headers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public class ExcelController : ApiController
    {
        #region Team members

        [HttpPost]
        public async Task<object> ReadTeamMembers()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, new { message = Eumis.Portal.Web.Resources.Excel.InvalidFile }));

            var provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents.Last();
            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            var byteArray = await file.ReadAsByteArrayAsync();

            // Read Excel
            Stream stream = new MemoryStream(byteArray);
            IXLWorksheet ws = null;

            try
            {
                var wb = new XLWorkbook(stream);
                ws = wb.Worksheets.First();
            }
            catch
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, new { message = Eumis.Portal.Web.Resources.Excel.InvalidFile }));
            }

            var team = new List<R_10057.TechnicalReportTeamMember>();

            try
            {
                ExtractMembers(ws, out team);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, new { message = e.Message }));
            }

            return Request.CreateResponse(HttpStatusCode.OK, team);
        }

        [HttpGet]
        public HttpResponseMessage ExportTeamMembers()
        {
            if (AppContext.Current == null)
                return null;

            var technicalReprot = (R_10044.TechnicalReport)AppContext.Current.Document;

            var workbook = this.GetWorkbook(technicalReprot.Team.TeamMemberCollection);

            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);

            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            ms.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(ms);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") { FileName = "TeamMembers.xlsx" };

            return result;
        }

        private XLWorkbook GetWorkbook(IList<R_10057.TechnicalReportTeamMember> team)
        {
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("РИС");

            // Headers
            ws.Cell("A1").Value = "Име по документ за самоличност";
            ws.Cell("B1").Value = "Позиция по проекта";
            ws.Cell("C1").Value = "Идентификатор";
            ws.Cell("D1").Value = "ЕГН/ Чуждестранно лице";
            ws.Cell("E1").Value = "Тип на ангажимента";
            ws.Cell("F1").Value = "Дата";
            ws.Cell("G1").Value = "Отработени часове";
            ws.Cell("H1").Value = "Извършена дейност";
            ws.Cell("I1").Value = "Конкретни резултати";

            var rngHeaders = ws.Range(1, 1, 1, 9);
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            ws.Range(1, 1, 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Double;

            // Content
            int rowIndex = 2;
            foreach (var teamMember in team)
            {
                ws.Cell(rowIndex, "A").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "A").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "A").Value = teamMember.Name;

                ws.Cell(rowIndex, "B").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "B").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "B").Value = teamMember.Position;

                ws.Cell(rowIndex, "C").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "C").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "C").Value = teamMember.UinType.Name;

                ws.Cell(rowIndex, "D").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "D").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "D").Value = teamMember.Uin;

                ws.Cell(rowIndex, "E").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "E").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "E").Value = teamMember.CommitmentType.Description;

                ws.Cell(rowIndex, "F").Style.NumberFormat.Format = "@";
                ws.Cell(rowIndex, "F").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "F").Value = teamMember.Date != null ?
                    teamMember.Date.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(rowIndex, "G").Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(rowIndex, "G").DataType = XLCellValues.Number;
                ws.Cell(rowIndex, "G").Value = teamMember.Hours;

                ws.Cell(rowIndex, "H").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "H").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "H").Value = teamMember.Activity;

                ws.Cell(rowIndex, "I").Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(rowIndex, "I").DataType = XLCellValues.Text;
                ws.Cell(rowIndex, "I").Value = teamMember.Result;

                rowIndex++;
            }

            // Style cells
            ws.Column(1).Width = 40;
            ws.Column(1).Style.Alignment.SetWrapText();
            ws.Columns(2, 3).AdjustToContents();
            ws.Column(4).Width = 40;
            ws.Column(4).Style.Alignment.SetWrapText();
            ws.Columns(5, 9).AdjustToContents();

            return workbook;
        }

        private void ExtractMembers(IXLWorksheet ws, out List<R_10057.TechnicalReportTeamMember> team)
        {
            team = new List<R_10057.TechnicalReportTeamMember>();

            // Look for the first row used
            var firstRowUsed = ws.FirstRowUsed();

            // Narrow down the row so that it only includes the used part
            var currentRow = firstRowUsed.RowUsed();

            // Move to the next row (it now has the titles)
            currentRow = currentRow.RowBelow();

            int totalRowsCount = ws.LastRowUsed().RowNumber();

            while (currentRow.RowNumber() <= totalRowsCount)
            {
                if (!currentRow.IsEmpty())
                    team.Add(ConvertToTeamMember(currentRow));

                currentRow = currentRow.RowBelow();
            }
        }

        private R_10057.TechnicalReportTeamMember ConvertToTeamMember(IXLRangeRow row)
        {
            int rowNumber = row.RowNumber();

            R_10057.TechnicalReportTeamMember result = new R_10057.TechnicalReportTeamMember();

            result.Name = row.Cell(1).GetString();
            result.Position = row.Cell(2).GetString();

            // Uin type
            var uinString = Regex.Replace(row.Cell(3).GetString(), @"\s+", string.Empty);
            if (!String.IsNullOrWhiteSpace(uinString))
            {
                var uin = new UinTypeNomenclature().GetTechnicalReportTeamItems()
                    .FirstOrDefault(e => Regex.Replace(e.Name, @"\s+", string.Empty)
                        .Equals(uinString, StringComparison.OrdinalIgnoreCase));
                if (uin != null)
                {
                    result.UinType = new R_10000.PrivateNomenclature()
                    {
                        Id = uin.Value,
                        Name = uin.Name,
                        NameEN = uin.NameEN
                    };
                }
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.TechnicalReportTeamMember.UinType, rowNumber));
            }

            result.Uin = row.Cell(4).GetString();

            // Commitment type
            var commitmentString = Regex.Replace(row.Cell(5).GetString(), @"\s+", string.Empty);
            if (!String.IsNullOrWhiteSpace(commitmentString))
            {
                var commitment = new CommitmentTypeNomenclature().GetItems()
                    .FirstOrDefault(e => Regex.Replace(e.Name, @"\s+", string.Empty)
                        .Equals(commitmentString, StringComparison.OrdinalIgnoreCase));
                if (commitment != null)
                {
                    result.CommitmentType = new R_09991.EnumNomenclature()
                    {
                        Value = commitment.Value,
                        Description = commitment.Name
                    };
                }
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.TechnicalReportTeamMember.CommitmentType, rowNumber));
            }

            // Date
            var dateString = row.Cell(6).GetString();
            if (!String.IsNullOrWhiteSpace(dateString))
            {
                DateTime date;
                if (DateTime.TryParse(dateString, new CultureInfo("bg-BG"), DateTimeStyles.None, out date))
                    result.Date = date;
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.TechnicalReportTeamMember.Date, rowNumber));
            }

            // Hours
            var hoursString = row.Cell(7).GetString();
            if (!String.IsNullOrWhiteSpace(hoursString))
            {
                Decimal hours;
                if (Decimal.TryParse(hoursString, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out hours))
                    result.Hours = hours;
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.TechnicalReportTeamMember.Hours, rowNumber));
            }

            result.Activity = row.Cell(8).GetString();
            result.Result = row.Cell(9).GetString();

            return result;
        }

        #endregion

        #region Cost supporting documents

        [HttpPost]
        public async Task<object> ReadCostSupportingDocuments()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, new { message = Eumis.Portal.Web.Resources.Excel.InvalidFile }));

            var provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents.Last();
            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            var byteArray = await file.ReadAsByteArrayAsync();

            // Read Excel
            Stream stream = new MemoryStream(byteArray);
            IXLWorksheet ws = null;

            try
            {
                var wb = new XLWorkbook(stream);
                ws = wb.Worksheets.First();
            }
            catch
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, new { message = Eumis.Portal.Web.Resources.Excel.InvalidFile }));
            }

            var docs = new List<R_10066.CostSupportingDocument>();

            try
            {
                ExtractDocs(ws, out docs);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, new { message = e.Message }));
            }

            return Request.CreateResponse(HttpStatusCode.OK, docs);
        }

        private void ExtractDocs(IXLWorksheet ws, out List<R_10066.CostSupportingDocument> docs)
        {
            docs = new List<R_10066.CostSupportingDocument>();

            // Look for the first row used
            var firstRowUsed = ws.FirstRowUsed();

            // Narrow down the row so that it only includes the used part
            var currentRow = firstRowUsed.RowUsed();

            // Move to the next row (it now has the titles)
            currentRow = currentRow.RowBelow();

            int totalRowsCount = ws.LastRowUsed().RowNumber();

            while (currentRow.RowNumber() <= totalRowsCount)
            {
                if (!currentRow.IsEmpty())
                    docs.Add(ConvertToCostSupportingDocument(currentRow));

                currentRow = currentRow.RowBelow();
            }
        }

        private R_10066.CostSupportingDocument ConvertToCostSupportingDocument(IXLRangeRow row)
        {
            int rowNumber = row.RowNumber();

            R_10066.CostSupportingDocument result = new R_10066.CostSupportingDocument();

            // Cost supporting document type
            var docTypeString = Regex.Replace(row.Cell(1).GetString(), @"\s+", string.Empty);
            if (!String.IsNullOrWhiteSpace(docTypeString))
            {
                var type = new CostSupportingDocumentTypeNomenclature().GetItems()
                    .FirstOrDefault(e => Regex.Replace(e.Name, @"\s+", string.Empty)
                        .Equals(docTypeString, StringComparison.OrdinalIgnoreCase));
                if (type != null)
                {
                    result.CostSupportingDocumentType = new R_09991.EnumNomenclature()
                    {
                        Value = type.Value,
                        Description = type.Name
                    };
                }
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.CostSupportingDocument.Type, rowNumber));
            }

            result.CostSupportingDocumentDescription = row.Cell(2).GetString();
            result.Number = row.Cell(3).GetString();

            // Date
            var dateString = row.Cell(4).GetString();
            if (!String.IsNullOrWhiteSpace(dateString))
            {
                DateTime date;
                if (DateTime.TryParse(dateString, new CultureInfo("bg-BG"), DateTimeStyles.None, out date))
                    result.Date = date;
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.CostSupportingDocument.Date, rowNumber));
            }

            var paymentDateString = row.Cell(5).GetString();
            if (!String.IsNullOrWhiteSpace(paymentDateString))
            {
                DateTime paymentDate;
                if (DateTime.TryParse(paymentDateString, new CultureInfo("bg-BG"), DateTimeStyles.None, out paymentDate))
                    result.PaymentDate = paymentDate;
                else
                    throw new Exception(String.Format(Eumis.Portal.Web.Resources.Excel.InvalidFieldRow, Eumis.Portal.Web.Views.Shared.App_LocalResources.CostSupportingDocument.PaymentDate, rowNumber));
            }

            result.Load();

            return result;
        }

        #endregion

        #region FinanceSources

        [HttpGet]
        public HttpResponseMessage DownloadFinanceSources()
        {
            if (AppContext.Current == null)
                return null;

            var financeReprot = (R_10043.FinanceReport)AppContext.Current.Document;

            IDocumentSerializer serializer = new DocumentSerializer();

            var ms = new MemoryStream(serializer.XmlSerializeObjectToBytes(null));

            var content = new HttpResponseMessage(HttpStatusCode.OK)
                             {
                                 Content = new StreamContent(ms)
                             };

            content.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            content.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            content.Content.Headers.ContentDisposition.FileName = Path.GetFileName("FinanceSources.xml");

            return content;
        }

        #endregion

        #region FinanceBudget

        [HttpGet]
        public HttpResponseMessage DownloadFinanceBudget()
        {
            if (AppContext.Current == null)
                return null;

            var financeReprot = (R_10043.FinanceReport)AppContext.Current.Document;

            IDocumentSerializer serializer = new DocumentSerializer();

            var ms = new MemoryStream(serializer.XmlSerializeObjectToBytes(financeReprot.FinanceBudget));

            var content = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(ms)
            };

            content.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            content.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            content.Content.Headers.ContentDisposition.FileName = Path.GetFileName("FinanceBudget.xml");

            return content;
        }

        #endregion
    }
}
