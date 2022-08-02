using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;

namespace Eumis.Print.AnswerRegistration
{
    internal class ContractReportSAPDataTemplate : ITemplate
    {
        public byte[] Print(PrintType printType, JObject item)
        {
            switch (printType)
            {
                case PrintType.PDF:
                    return this.PrintPdf(item);
                default:
                    throw new ArgumentException();
            }
        }

        private byte[] PrintPdf(JObject context)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (Document doc = new Document(iTextSharp.text.PageSize.A4, 70, 70, 70, 70))
            using (PdfWriter pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                BaseFont baseFontArial = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font fontHeader = new Font(baseFontArial, 14, Font.BOLD);
                Font fontParagraphRegular = new Font(baseFontArial, 12, Font.NORMAL);
                Font fontParagraphBold = new Font(baseFontArial, 12, Font.BOLD);

                var nfi = (NumberFormatInfo)CultureInfo.GetCultureInfo("bg-BG").NumberFormat.Clone();

                var currentDate = DateTime.Now;

                doc.Open();

                var items = context.Property("items").Values();

                foreach (var item in items)
                {
                    doc.NewPage();

                    Paragraph paragraphImages = new Paragraph();

                    PdfPTable tableImages = new PdfPTable(2)
                    {
                        WidthPercentage = 100f,
                    };

                    Stream streamEuLogo = assembly.GetManifestResourceStream("Eumis.Print.ProjectRegistration.Images.eu_logo.jpg");
                    Image imageEuLogo = Image.GetInstance(streamEuLogo);
                    imageEuLogo.ScaleAbsolute(180, 52);

                    PdfPCell cellEuLogo = new PdfPCell(imageEuLogo)
                    {
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                        Border = Rectangle.NO_BORDER,
                    };
                    tableImages.AddCell(cellEuLogo);

                    Stream streamOpLogo = assembly.GetManifestResourceStream("Eumis.Print.ProjectRegistration.Images.op_logo.jpg");
                    Image imageOpLogo = Image.GetInstance(streamOpLogo);
                    imageOpLogo.ScaleAbsolute(210, 52);

                    PdfPCell cellOpLogo = new PdfPCell(imageOpLogo)
                    {
                        HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
                        Border = Rectangle.NO_BORDER,
                    };
                    tableImages.AddCell(cellOpLogo);

                    paragraphImages.Add(tableImages);

                    paragraphImages.Add(new Phrase(Environment.NewLine));
                    paragraphImages.Add(new Phrase(Environment.NewLine));

                    doc.Add(paragraphImages);

                    // Paragraph Header
                    Paragraph paragraphHeader = new Paragraph
                    {
                        Alignment = Element.ALIGN_CENTER,
                    };

                    Phrase phraseIsun = new Phrase(@"ИСУН 2020", fontHeader);
                    paragraphHeader.Add(phraseIsun);

                    paragraphHeader.Add(new Phrase(Environment.NewLine));
                    paragraphHeader.Add(new Phrase(Environment.NewLine));

                    Phrase phraseRegNumberHeader = new Phrase(@"ДАННИ ЗА САП", fontHeader);
                    paragraphHeader.Add(phraseRegNumberHeader);

                    paragraphHeader.Add(new Phrase(Environment.NewLine));
                    paragraphHeader.Add(new Phrase(Environment.NewLine));

                    doc.Add(paragraphHeader);

                    // Paragraph SAP Data
                    Paragraph paragraphSAPData = new Paragraph();

                    PdfPTable tableSAPData = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                    };

                    tableSAPData.AddCell(new Phrase(@"Код на оперативна програма", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["programmeCode"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Код от ИСУН на приоритетна ос", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["programmePriorityCode"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Фонд", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["financeSourceDescr"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Код на процедура от ИСУН", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["procedureCode"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Код на договор от ИСУН", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["contractCode"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Номер на пакет", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["orderNum"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Номер на искане за плащане", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["paymentOrderNum"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Идентификатор", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["companyUin"] + " (" + (string)item["companyUinType"] + ")", fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Име на бенефициент", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["companyName"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Одобрена сума за плащане от крайната проверка на искането за плащане, в т.ч.:", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["paidBfpTotalAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Национално финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["paidBgAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Европейско финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["paidEuAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Кръстосано финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["paidCrossAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Верифицирана сума, в т.ч.:", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["approvedBfpTotalAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Национално финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["approvedBgAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Европейско финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["approvedEuAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"-Кръстосано финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["approvedCrossAmount"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Валута", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["currency"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Дата на искането", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(DateTime.Parse(item["submitDate"].ToString()).ToString("dd.MM.yyyy г."), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Дата на верификация", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(DateTime.Parse(item["checkedDate"].ToString()).ToString("dd.MM.yyyy г."), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Тип на пакет", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["reportType"], fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Тип на искане за плащане", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase((string)item["paymentType"], fontParagraphRegular));

                    paragraphSAPData.Add(tableSAPData);
                    doc.Add(paragraphSAPData);

                    // Paragraph Footer
                    Paragraph paragraphFooter = new Paragraph();

                    PdfPTable tableFooter = new PdfPTable(1)
                    {
                        WidthPercentage = 100,
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                    };

                    PdfPCell cellFooter = new PdfPCell
                    {
                        Border = Rectangle.NO_BORDER,
                    };
                    cellFooter.AddElement(new Phrase(@"Данните са от ИСУН към дата: " + currentDate.ToString("dd.MM.yyyy г. HH.mm.ss"), fontParagraphRegular));
                    cellFooter.AddElement(new Phrase(Environment.NewLine));
                    cellFooter.AddElement(new Phrase(@"Подпис на лицето отговорно за предоставяне на информацията: ", fontParagraphRegular));

                    tableFooter.AddCell(cellFooter);

                    paragraphFooter.Add(new Phrase(Environment.NewLine));
                    paragraphFooter.Add(tableFooter);

                    doc.Add(paragraphFooter);
                }

                doc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
