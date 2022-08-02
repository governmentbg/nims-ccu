using System;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;

namespace Eumis.Print.MonitorstatRequestDeclaration
{
    internal class MonitorstatRequestDeclarationTemplate : ITemplate
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
            using (var memoryStream = new MemoryStream())
            using (var doc = new Document(PageSize.A4, 70, 70, 70, 70))
            using (PdfWriter pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                BaseFont baseFontArial = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var fontHeader = new Font(baseFontArial, 14, Font.BOLD);
                var fontParagraphRegular = new Font(baseFontArial, 12, Font.NORMAL);
                var fontParagraphBold = new Font(baseFontArial, 12, Font.BOLD);
                var fontSmallRegular = new Font(baseFontArial, 8, Font.NORMAL);
                var fontSmallBold = new Font(baseFontArial, 8, Font.BOLD);

                doc.Open();

                var paragraphImages = new Paragraph();

                var tableImages = new PdfPTable(2)
                {
                    WidthPercentage = 100f,
                };

                Stream streamEuLogo = assembly.GetManifestResourceStream("Eumis.Print.MonitorstatRequestDeclaration.Images.eu_logo.jpg");
                Image imageEuLogo = Image.GetInstance(streamEuLogo);
                imageEuLogo.ScaleAbsolute(77, 52);

                var cellEuLogo = new PdfPCell(imageEuLogo)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.NO_BORDER,
                };
                tableImages.AddCell(cellEuLogo);

                Stream streamOpLogo = assembly.GetManifestResourceStream("Eumis.Print.MonitorstatRequestDeclaration.Images.op_logo.jpg");
                Image imageOpLogo = Image.GetInstance(streamOpLogo);
                imageOpLogo.ScaleAbsolute(206, 52);

                var cellOpLogo = new PdfPCell(imageOpLogo)
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.NO_BORDER,
                };
                tableImages.AddCell(cellOpLogo);

                paragraphImages.Add(tableImages);

                paragraphImages.Add(new Phrase(Environment.NewLine));
                paragraphImages.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphImages);

                // Paragraph Header
                var paragraphHeader = new Paragraph
                {
                    Alignment = Element.ALIGN_CENTER,
                };

                paragraphHeader.Add(new Phrase(Environment.NewLine));
                paragraphHeader.Add(new Phrase(Environment.NewLine));

                var phraseDeclaration = new Phrase("ДЕКЛАРАЦИЯ", fontHeader);
                paragraphHeader.Add(phraseDeclaration);

                paragraphHeader.Add(new Phrase(Environment.NewLine));
                paragraphHeader.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphHeader);

                // Paragraph Candidate Data
                var paragraphCandidateData = new Paragraph
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                };

                var phraseFrom = new Phrase("От кандидат ", fontParagraphRegular);
                paragraphCandidateData.Add(phraseFrom);

                var phraseFromData = new Phrase((string)context["companyName"], fontParagraphRegular);
                paragraphCandidateData.Add(phraseFromData);

                var phraseBulstat = new Phrase(" с ЕИК/БУЛСТАТ ", fontParagraphRegular);
                paragraphCandidateData.Add(phraseBulstat);

                var phraseBulstatData = new Phrase((string)context["companyUin"], fontParagraphRegular);
                paragraphCandidateData.Add(phraseBulstatData);

                paragraphCandidateData.Add(new Phrase(Environment.NewLine));
                paragraphCandidateData.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphCandidateData);

                // Paragraph Center
                var paragraphCenter = new Paragraph
                {
                    Alignment = Element.ALIGN_CENTER,
                };

                var phraseText = new Phrase("декларирам, че", fontParagraphBold);
                paragraphCenter.Add(phraseText);

                doc.Add(paragraphCenter);

                // Paragraph Declaration
                var paragraphDeclaration = new Paragraph()
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                };

                var phraseConsent = new Phrase((string)context["consent"], fontParagraphRegular);
                paragraphDeclaration.Add(phraseConsent);

                paragraphDeclaration.Add(new Phrase(Environment.NewLine));
                paragraphDeclaration.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphDeclaration);

                // Paragraph Warning
                var paragraphWarning = new Paragraph
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                };

                var phraseWarning = new Phrase("Известна ми е наказателната отговорност, която нося по чл. 313 от НК за деклариране на неверни данни.", fontParagraphBold);
                paragraphWarning.Add(phraseWarning);

                paragraphWarning.Add(new Phrase(Environment.NewLine));
                paragraphWarning.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphWarning);

                // Paragraph Footer
                var paragraphFooter = new Paragraph
                {
                    SpacingBefore = 20,
                };

                var tableFooter = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                };

                var cellLeft = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                };
                cellLeft.AddElement(new Phrase("Дата: ", fontParagraphRegular));
                cellLeft.AddElement(new Phrase(((DateTime)context["regDate"]).ToString("dd.MM.yyyy"), fontParagraphRegular));

                tableFooter.AddCell(cellLeft);

                var cellRight = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                };

                cellRight.AddElement(new Phrase("Подпис на деклариращия:", fontParagraphRegular));

                var signaturesTable = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                };
                signaturesTable.DefaultCell.Border = Rectangle.NO_BORDER;

                foreach (var sig in context["signatures"])
                {
                    signaturesTable.AddCell(new Phrase("Данни за сертификат", fontSmallBold));
                    signaturesTable.AddCell(new Phrase($"Сериен номер: " + (string)sig["serialNumber"], fontSmallRegular));
                    signaturesTable.AddCell(new Phrase("Валиден от: " + (string)sig["effectiveDate"], fontSmallRegular));
                    signaturesTable.AddCell(new Phrase("Валиден до: " + (string)sig["expirationDate"], fontSmallRegular));

                    signaturesTable.AddCell(new Phrase("Информация за издателя", fontSmallBold));
                    signaturesTable.AddCell(new Phrase((string)sig["issuer"], fontSmallRegular));

                    signaturesTable.AddCell(new Phrase("Информация за автора и титуляра", fontSmallBold));
                    signaturesTable.AddCell(new Phrase((string)sig["subject"], fontSmallRegular));

                    signaturesTable.AddCell(new Phrase(Environment.NewLine));
                }

                cellRight.AddElement(signaturesTable);

                tableFooter.AddCell(cellRight);

                paragraphFooter.Add(tableFooter);

                doc.Add(paragraphFooter);

                doc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
