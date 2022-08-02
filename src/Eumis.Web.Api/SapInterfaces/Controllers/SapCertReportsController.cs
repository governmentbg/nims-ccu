using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.SapInterfaces.Controllers
{
    [RoutePrefix("api/sapCertReports")]
    public class SapCertReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public SapCertReportsController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            ICertReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<SapCertReportVO> GetSapCertReports(int? certReportId = null)
        {
            this.authorizer.AssertCanDo(SapFileListActions.Search);

            return this.certReportsRepository.GetSapCertReports(certReportId);
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage ExportSapCertReports(int? certReportId)
        {
            this.authorizer.AssertCanDo(SapFileListActions.Search);

            var report = this.certReportsRepository.GetSapCertReports(certReportId);
            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("SAP");

            ws.Cell("A1").Value = "Номер на договор";
            ws.Cell("B1").Value = "Пореден номер на искане за плащане";
            ws.Cell("C1").Value = "Приоритетна ос на финансиране";
            ws.Cell("D1").Value = "Приоритетна ос на финансиране";
            ws.Cell("E1").Value = "Източник на финансиране";
            ws.Cell("F1").Value = "Сума";
            ws.Cell("G1").Value = "Дата на която е подписан Сертификата към комисията";
            ws.Cell("H1").Value = "Счетоводна година";
            ws.Cell("I1").Value = "Пореден номер на одобрения Сертификат";

            var rngHeaders = ws.Range("A1", "I1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Alignment.SetWrapText();
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var row = 2;
            foreach (var item in report)
            {
                ws.Cell(row, 1).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 1).DataType = XLCellValues.Text;
                ws.Cell(row, 1).Value = item.ContractRegNum;

                ws.Cell(row, 2).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 2).DataType = XLCellValues.Number;
                ws.Cell(row, 2).Value = item.PaymentVersionNum;

                ws.Cell(row, 3).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 3).DataType = XLCellValues.Text;
                ws.Cell(row, 3).Value = item.ProgrammePriorityCode;

                ws.Cell(row, 4).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 4).DataType = XLCellValues.Text;
                ws.Cell(row, 4).Value = item.ProgrammePriorityCode;

                ws.Cell(row, 5).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 5).DataType = XLCellValues.Text;
                ws.Cell(row, 5).Value = item.ProgrammePriorityCode;

                ws.Cell(row, 6).Style.NumberFormat.NumberFormatId = 2;
                ws.Cell(row, 6).DataType = XLCellValues.Number;
                ws.Cell(row, 6).Value = item.TotalAmount;

                ws.Cell(row, 7).Style.NumberFormat.Format = "@";
                ws.Cell(row, 7).DataType = XLCellValues.DateTime;
                ws.Cell(row, 7).Value = item.Date.HasValue ? item.Date.Value.ToString("dd.MM.yyyy") : null;

                ws.Cell(row, 8).Style.NumberFormat.NumberFormatId = 1;
                ws.Cell(row, 8).DataType = XLCellValues.Text;
                ws.Cell(row, 8).Value = item.Period;

                ws.Cell(row, 9).Style.NumberFormat.Format = "@";
                ws.Cell(row, 9).DataType = XLCellValues.Text;
                ws.Cell(row, 9).Value = item.CertReportNumber;

                row++;
            }

            ws.Columns(1, 9).AdjustToContents();

            return this.Request.CreateXmlResponse(workbook, "sap");
        }

        [HttpGet]
        [Route("exportTsv")]
        public HttpResponseMessage ExportSapCertReportsTsv(int? certReportId)
        {
            this.authorizer.AssertCanDo(SapFileListActions.Search);

            var report = this.certReportsRepository.GetSapCertReports(certReportId);

            MemoryStream ms = new MemoryStream(); // we should not dispose the stream
            using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8, 1024, true))
            {
                sw.Write("Номер на договор");
                sw.Write('\t');
                sw.Write("Пореден номер на искане за плащане");
                sw.Write('\t');
                sw.Write("Приоритетна ос на финансиране");
                sw.Write('\t');
                sw.Write("Фонд");
                sw.Write('\t');
                sw.Write("Сума");
                sw.Write('\t');
                sw.Write("Дата на която е подписан Сертификата към комисията");
                sw.Write('\t');
                sw.Write("Счетоводна година");
                sw.Write('\t');
                sw.Write("Пореден номер на одобрения Сертификат");
                sw.Write("\r\n");

                foreach (var item in report)
                {
                    sw.Write(item.ContractRegNum);
                    sw.Write('\t');
                    sw.Write(item.PaymentVersionNum.ToString());
                    sw.Write('\t');
                    sw.Write(item.ProgrammePriorityCode);
                    sw.Write('\t');
                    sw.Write(string.Empty);
                    sw.Write('\t');
                    sw.Write(item.TotalAmount.HasValue ? item.TotalAmount.Value.ToString("0.00") : string.Empty);
                    sw.Write('\t');
                    sw.Write(item.Date.HasValue ? item.Date.Value.ToString("dd.MM.yyyy") : string.Empty);
                    sw.Write('\t');
                    sw.Write(item.Period);
                    sw.Write('\t');
                    sw.Write(item.CertReportNumber);
                    sw.Write("\r\n");
                }

                sw.Flush();
            }

            return this.Request.CreateFileResponse(ms, "sap.tsv", "text/tab-separated-values");
        }
    }
}
