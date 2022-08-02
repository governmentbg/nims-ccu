using ClosedXML.Excel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Eumis.Common.Api
{
    public static class EumisHttpRequestMessageExtensions
    {
        public static HttpResponseMessage CreateXmlResponse(this HttpRequestMessage request, XLWorkbook workbook, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);

            return request.CreateXmlResponse(ms, fileName);
        }

        public static HttpResponseMessage CreateXmlResponse(this HttpRequestMessage request, Stream workbookStream, string fileName)
        {
            return request.CreateFileResponse(workbookStream, $"{fileName}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Standartize the HttpResponseMessage extensions API and allow it to use the Request in the future.")]
        public static HttpResponseMessage CreateFileResponse(this HttpRequestMessage request, Stream stream, string fileName, string mimeType)
        {
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") { FileName = fileName };

            return result;
        }
    }
}
