using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Eumis.Print.ContractVersionSAPData
{
    public class ContractVersionSAPDataTemplate : ITemplate
    {
        public byte[] Print(PrintType printType, JObject context)
        {
            switch (printType)
            {
                case PrintType.PDF:
                    return this.PrintPdf(context);
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

                var item = context.Property("item").Value;

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

                tableSAPData.AddCell(new Phrase(@"Кодове на приоритетни оси", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Join(", ", item["programmePriorityCodes"]), fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Код на процедура", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase((string)item["procedureCode"], fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Код на проектно предложение", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase((string)item["projectRegNumber"], fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Код на договор/анекс", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase((string)item["contractRegNumber"], fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Идентификатор на бенефициент", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase((string)item["companyUinType"] + ": " + (string)item["companyUin"], fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Наименование на бенефициент", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase((string)item["companyName"], fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Дата на стартиране", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(DateTime.Parse(item["startDate"].ToString()).ToString("dd.MM.yyyy г."), fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Дата на приключване", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(DateTime.Parse(item["completionDate"].ToString()).ToString("dd.MM.yyyy г."), fontParagraphRegular));

                paragraphSAPData.Add(tableSAPData);

                tableSAPData = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                };

                tableSAPData.AddCell(new PdfPCell(new Phrase(@"Стойност на бюджета:", fontParagraphRegular)) { Colspan = 3 });

                tableSAPData.AddCell(new Phrase(@"Валута", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(@"Лева(BGN)", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(@"Евро(EUR)", fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Общо", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["totalAmountBGN"].ToString())), fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["totalAmountEUR"].ToString())), fontParagraphRegular));

                tableSAPData.AddCell(new PdfPCell(new Phrase(@"Финансиране от ЕС", fontParagraphRegular)) { Colspan = 3 });

                foreach (var budgetEU in item["euAmounts"])
                {
                    tableSAPData.AddCell(new Phrase($@"  -{budgetEU["financeSource"]}", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(budgetEU["amountBGN"].ToString())), fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(budgetEU["amountEUR"].ToString())), fontParagraphRegular));
                }

                tableSAPData.AddCell(new Phrase(@"Национално финансиране", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["bgAmountBGN"].ToString())), fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["bgAmountEUR"].ToString())), fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Общо БФП/ФИ", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["bfpTotalAmountBGN"].ToString())), fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["bfpTotalAmountEUR"].ToString())), fontParagraphRegular));

                tableSAPData.AddCell(new Phrase(@"Собствено финансиране", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["totalSelfAmountBGN"].ToString())), fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(item["totalSelfAmountEUR"].ToString())), fontParagraphRegular));

                tableSAPData.AddCell(new PdfPCell(new Phrase(@"Стойност по приоритетни оси:", fontParagraphRegular)) { Colspan = 3 });

                tableSAPData.AddCell(new Phrase(@"Валута", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(@"Лева(BGN)", fontParagraphRegular));
                tableSAPData.AddCell(new Phrase(@"Евро(EUR)", fontParagraphRegular));

                foreach (var programmeBudget in item["programmePriorityBudgets"])
                {
                    tableSAPData.AddCell(new Phrase(@"Код на приоритетна ос", fontParagraphRegular));
                    tableSAPData.AddCell(new PdfPCell(new Phrase((string)programmeBudget["programmePriorityCode"], fontParagraphRegular)) { Colspan = 2 });

                    tableSAPData.AddCell(new Phrase(@"Общо", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["totalAmountBGN"].ToString())), fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["totalAmountEUR"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new PdfPCell(new Phrase(@"Финансиране от ЕС", fontParagraphRegular)) { Colspan = 3 });

                    foreach (var budgetEU in programmeBudget["euAmounts"])
                    {
                        tableSAPData.AddCell(new Phrase($@"  -{budgetEU["financeSource"]}", fontParagraphRegular));
                        tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(budgetEU["amountBGN"].ToString())), fontParagraphRegular));
                        tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(budgetEU["amountEUR"].ToString())), fontParagraphRegular));
                    }

                    tableSAPData.AddCell(new Phrase(@"Национално финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["bgAmountBGN"].ToString())), fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["bgAmountEUR"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Общо БФП/ФИ", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["bfpTotalAmountBGN"].ToString())), fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["bfpTotalAmountEUR"].ToString())), fontParagraphRegular));

                    tableSAPData.AddCell(new Phrase(@"Собствено финансиране", fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["totalSelfAmountBGN"].ToString())), fontParagraphRegular));
                    tableSAPData.AddCell(new Phrase(string.Format(nfi, "{0:0,0.00}", decimal.Parse(programmeBudget["totalSelfAmountEUR"].ToString())), fontParagraphRegular));
                }

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

                doc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
