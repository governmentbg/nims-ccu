using System;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;

namespace Eumis.Print.AnswerRegistration
{
    internal class AnswerRegistrationTemplate : ITemplate
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

                doc.Open();

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

                Phrase phraseIsun = new Phrase(@"СУНИ", fontHeader);
                paragraphHeader.Add(phraseIsun);

                paragraphHeader.Add(new Phrase(Environment.NewLine));
                paragraphHeader.Add(new Phrase(Environment.NewLine));

                Phrase phraseRegNumberHeader = new Phrase(@"РЕГИСТРАЦИОНЕН НОМЕР НА ОТГОВОР", fontHeader);
                paragraphHeader.Add(phraseRegNumberHeader);

                paragraphHeader.Add(new Phrase(Environment.NewLine));
                paragraphHeader.Add(new Phrase(Environment.NewLine));

                Phrase phraseProgramme = new Phrase(@"ОТГОВОРНА ОРГАНИЗАЦИЯ", fontHeader);
                paragraphHeader.Add(phraseProgramme);

                paragraphHeader.Add(new Phrase(Environment.NewLine));

                Phrase phraseProgrammeName = new Phrase(((string)context["programmeName"]).ToUpper(), fontHeader);
                paragraphHeader.Add(phraseProgrammeName);

                paragraphHeader.Add(new Phrase(Environment.NewLine));
                paragraphHeader.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphHeader);

                // Paragraph Procedure
                Paragraph paragraphProcedure = new Paragraph();

                Phrase phraseProcedure = new Phrase(@"БЮДЖЕТ: ", fontParagraphRegular);
                paragraphProcedure.Add(phraseProcedure);

                Phrase phraseProcedureDescription = new Phrase((string)context["procedureName"], fontParagraphBold);
                paragraphProcedure.Add(phraseProcedureDescription);

                paragraphProcedure.Add(new Phrase(Environment.NewLine));
                paragraphProcedure.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphProcedure);

                // Paragraph Project
                Paragraph paragraphProject = new Paragraph();

                Phrase phraseProject = new Phrase(@"Проект: ", fontParagraphRegular);
                paragraphProject.Add(phraseProject);

                Phrase phraseProjectDescription = new Phrase((string)context["projectName"], fontParagraphBold);
                paragraphProject.Add(phraseProjectDescription);

                paragraphProject.Add(new Phrase(Environment.NewLine));
                paragraphProject.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphProject);

                // Paragraph RegistrationData
                Paragraph paragraphRegistrationData = new Paragraph();

                Phrase phraseRegNumber = new Phrase(@"Регистрационен номер: ", fontParagraphRegular);
                paragraphRegistrationData.Add(phraseRegNumber);

                Phrase phraseRegNumberDescription = new Phrase((string)context["regNumber"], fontParagraphBold);
                paragraphRegistrationData.Add(phraseRegNumberDescription);

                paragraphRegistrationData.Add(new Phrase(Environment.NewLine));

                Phrase phraseRegDate = new Phrase(@"Дата на регистрация: ", fontParagraphRegular);
                paragraphRegistrationData.Add(phraseRegDate);

                Phrase phraseRegDateDescription = new Phrase(string.Format("{0:dd.MM.yyyy}г.", (DateTime)context["regDate"]), fontParagraphBold);
                paragraphRegistrationData.Add(phraseRegDateDescription);

                paragraphRegistrationData.Add(new Phrase(Environment.NewLine));
                paragraphRegistrationData.Add(new Phrase(Environment.NewLine));

                doc.Add(paragraphRegistrationData);

                // Paragraph CompanyData
                Paragraph paragraphCompanyData = new Paragraph();

                var answerXmlHashDescription = (string)context["answerXmlHash"];
                if (!string.IsNullOrWhiteSpace(answerXmlHashDescription))
                {
                    Phrase phraseAnswerXmlHash = new Phrase(@"Код на отговора: ", fontParagraphRegular);
                    paragraphCompanyData.Add(phraseAnswerXmlHash);

                    Phrase phraseAnswerXmlHashDescription = new Phrase(answerXmlHashDescription, fontParagraphBold);
                    paragraphCompanyData.Add(phraseAnswerXmlHashDescription);

                    paragraphCompanyData.Add(new Phrase(Environment.NewLine));
                }

                Phrase phraseCompany = new Phrase(@"Кандидат: ", fontParagraphRegular);
                paragraphCompanyData.Add(phraseCompany);

                Phrase phraseCompanyDescription = new Phrase((string)context["companyName"], fontParagraphBold);
                paragraphCompanyData.Add(phraseCompanyDescription);

                paragraphCompanyData.Add(new Phrase(Environment.NewLine));

                Phrase phraseEik = new Phrase((string)context["uinType"] + ": ", fontParagraphRegular);
                paragraphCompanyData.Add(phraseEik);

                Phrase phraseEikDescription = new Phrase((string)context["uin"], fontParagraphBold);
                paragraphCompanyData.Add(phraseEikDescription);

                doc.Add(paragraphCompanyData);

                // Paragraph Footer
                Paragraph paragraphFooter = new Paragraph();

                PdfPTable tableFooter = new PdfPTable(1)
                {
                    WidthPercentage = 45,
                    HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
                };

                PdfPCell cellFooter = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                };
                cellFooter.AddElement(new Phrase(@"Предал: ", fontParagraphRegular));
                cellFooter.AddElement(new Phrase(Environment.NewLine));
                cellFooter.AddElement(new Phrase(@"Приел: ", fontParagraphRegular));

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
