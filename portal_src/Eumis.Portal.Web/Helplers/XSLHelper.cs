using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using Eumis.Components;
using WAIS.Security.X509;

namespace Eumis.Portal.Web.Helpers
{
    public class XSLHelper
    {
        public static string XmlToHtml(string xml, string templatePath)
        {
            string signatureXml = GenerateSignatureXml(xml);

            XmlDocument signatureXmlDocument = new XmlDocument();
            signatureXmlDocument.LoadXml(signatureXml);

            System.Xml.Xsl.XsltArgumentList xsltArgumentList = new System.Xml.Xsl.XsltArgumentList();
            xsltArgumentList.AddParam("SignatureXML", string.Empty, signatureXmlDocument.CreateNavigator().Select("/."));

            string html = string.Empty;
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                XPathDocument xPathDocument = new XPathDocument(xmlStream);
                XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
                xslCompiledTransform.Load(templatePath);

                using (MemoryStream htmlStream = new MemoryStream())
                {
                    xslCompiledTransform.Transform(xPathDocument, xsltArgumentList, htmlStream);
                    html = Encoding.UTF8.GetString(htmlStream.ToArray());
                }
            }

            return html;
        }

        private static string GenerateSignatureXml(string xml)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                xmlDocument.PreserveWhitespace = true;

                IDocumentSerializer serializer = new DocumentSerializer();

                DocumentSignatures documentSignatures =
                    new DocumentSignatures() { Signatures = new List<Signature>() };

                var signatureInfos = X509Signature.GetSignaturesInfo(xmlDocument, null);

                foreach (var signatureInfo in signatureInfos)
                {
                    string signatureUniqueID = string.Empty;

                    XmlElement xe = signatureInfo.SignatureElement;
                    while (xe != null)
                    {
                        if (xe.Name == "Official")
                        {
                            if (xe.Attributes["SignatureUniqueID"] != null)
                            {
                                signatureUniqueID = xe.Attributes["SignatureUniqueID"].Value;
                                break;
                            }
                        }

                        if (xe.ParentNode is XmlElement)
                            xe = (XmlElement)xe.ParentNode;
                        else
                            xe = null;
                    }

                    Signature signature = new Signature() { TimeStampInfos = new List<TimeStampInfo>() };
                    signature.SignatureUniqueID = signatureUniqueID;
                    signature.SignatureTime = X509SignatureHelper.ExtractSigningTimeFromXml(xml);
                    signature.IsValid = signatureInfo.IsSignatureValid;

                    signature.SigningCertificateData = new SigningCertificateData
                    {
                        SerialNumber = signatureInfo.SigningCertificate.SerialNumber,
                        ValidFrom = DateTime.Parse(signatureInfo.SigningCertificate.GetEffectiveDateString()),
                        ValidTo = DateTime.Parse(signatureInfo.SigningCertificate.GetExpirationDateString()),
                        Issuer = signatureInfo.SigningCertificate.Issuer,
                        Subject = signatureInfo.SigningCertificate.Subject,
                        SubjectAlternativeName = signatureInfo.SigningCertificate.Extensions["Subject Alternative Name"] != null ?
                                                    signatureInfo.SigningCertificate.Extensions["Subject Alternative Name"].Format(false) : string.Empty,
                    };

                    var timeStampInfos = X509Signature.GetXadesStampInfo(signatureInfo.SignatureElement.OuterXml);

                    foreach (var timeStampInfo in timeStampInfos)
                    {
                        signature.TimeStampInfos.Add(new TimeStampInfo
                        {
                            TimeStampTime = timeStampInfo.TimeStampTime,
                            SigningCertificateData = new SigningCertificateData
                            {
                                SerialNumber = timeStampInfo.SignerCertificate.SerialNumber,
                                ValidFrom = DateTime.Parse(timeStampInfo.SignerCertificate.GetEffectiveDateString()),
                                ValidTo = DateTime.Parse(timeStampInfo.SignerCertificate.GetExpirationDateString()),
                                Issuer = timeStampInfo.SignerCertificate.Issuer,
                                Subject = timeStampInfo.SignerCertificate.Subject,
                                SubjectAlternativeName = timeStampInfo.SignerCertificate.Extensions["Subject Alternative Name"] != null ?
                                                            timeStampInfo.SignerCertificate.Extensions["Subject Alternative Name"].Format(false) : string.Empty,
                            }
                        });
                    }

                    documentSignatures.Signatures.Add(signature);
                }

                return serializer.XmlSerializeObjectToString(documentSignatures);
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    [XmlRoot("DocumentSignatures")]
    public class DocumentSignatures
    {
        [XmlElement(ElementName = "Signature")]
        public List<Signature> Signatures { get; set; }
    }

    public class Signature
    {
        [XmlAttribute()]
        public string SignatureUniqueID { get; set; }

        public DateTime? SignatureTime { get; set; }

        public bool IsValid { get; set; }

        public SigningCertificateData SigningCertificateData { get; set; }

        [XmlElement(ElementName = "TimeStampInfo")]
        public List<TimeStampInfo> TimeStampInfos { get; set; }
    }

    public class SigningCertificateData
    {
        public string SerialNumber { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Issuer { get; set; }

        public string Subject { get; set; }

        public string SubjectAlternativeName { get; set; }
    }

    public class TimeStampInfo
    {
        public DateTime TimeStampTime { get; set; }

        public SigningCertificateData SigningCertificateData { get; set; }
    }
}