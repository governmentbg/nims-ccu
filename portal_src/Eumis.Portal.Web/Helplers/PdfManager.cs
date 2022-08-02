using SautinSoft;

namespace Eumis.Portal.Web.Helpers
{
    public class PdfManagerSettings
    {
        public float MarginLeft { get; set; }
        public float MarginRight { get; set; }
        public float MarginTop { get; set; }
        public float MarginBottom { get; set; }
        public bool IsLandscape { get; set; }
    }

    public static class PdfManager
    {
        static readonly string PDF_METAMORPHOSIS_SERIAL = "10024747414";
        static readonly object htmlLocker = new object();

        public static byte[] ConvertHtmlToPdf(byte[] input, string baseUrl = "", PdfManagerSettings settings = null)
        {
            try
            {
                lock (htmlLocker)
                {
                    PdfMetamorphosis pdfConverter = new PdfMetamorphosis();
                    pdfConverter.SetSerial(PDF_METAMORPHOSIS_SERIAL);

                    //pdfConverter.UnicodeOptions.DetectFontsDirectory = 
                    //    PdfMetamorphosis.CUnicodeOptions.eUnicodeDetectFontsDirectory.Custom;

                    pdfConverter.UnicodeOptions.FontsDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);

                    pdfConverter.HtmlOptions.BaseUrl = baseUrl;

                    if (settings != null)
                    {
                        pdfConverter.PageStyle.PageMarginLeft.mm(settings.MarginLeft);
                        pdfConverter.PageStyle.PageMarginRight.mm(settings.MarginRight);
                        pdfConverter.PageStyle.PageMarginTop.mm(settings.MarginTop);
                        pdfConverter.PageStyle.PageMarginBottom.mm(settings.MarginBottom);

                        if (settings.IsLandscape)
                            pdfConverter.PageStyle.PageOrientation.Landscape();
                        else
                            pdfConverter.PageStyle.PageOrientation.Portrait();
                    }

                    byte[] pdf = pdfConverter.HtmlToPdfConvertStringToByte(System.Text.UTF8Encoding.UTF8.GetString(input));

                    return pdf;

                    // PdfVision pdfConverter = new PdfVision();
                    // pdfConverter.Serial = PDF_PDFVISION_SERIAL;
                    // 
                    // pdfConverter.PageStyle.PageSize.A4();
                    // pdfConverter.ImageStyle.CompressionType = PdfVision.CImageStyle.eCompressionType.Fastest;
                    // pdfConverter.PageStyle.PageMarginBottom = new PdfVision.CSizeType() { ValuePx = 20 };
                    // pdfConverter.PageStyle.PageMarginLeft = new PdfVision.CSizeType() { ValuePx = 30 };
                    // pdfConverter.PageStyle.PageMarginRight = new PdfVision.CSizeType() { ValuePx = 30 };
                    // pdfConverter.PageStyle.PageMarginTop = new PdfVision.CSizeType() { ValuePx = 15 };
                    // 
                    // byte[] pdf = pdfConverter.ConvertHtmlStringToPDFStream(UTF8Encoding.UTF8.GetString(input));
                    // 
                    // return pdf;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}