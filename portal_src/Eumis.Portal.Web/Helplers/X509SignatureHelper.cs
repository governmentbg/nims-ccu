using System;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using WAIS.Security.X509;

namespace Eumis.Portal.Web.Helpers
{
    public static class X509SignatureHelper
    {
        public static X509Certificate2 IsDetachedSignatureValid(byte[] signature)
        {
            try
            {
                //SubmissionState.Current.isunFile = File.ReadAllBytes("c:\\signedIsun.isun");
                ContentInfo contentInfo = new ContentInfo(SubmissionState.Current.isunFile.Content);

                // Create a new, detached SignedCms message.
                SignedCms signedCms = new SignedCms(contentInfo, true);

                // encodedMessage is the encoded message received from
                // the sender.
                signedCms.Decode(signature);

                // Verify the signature without validating the
                // certificate.
                signedCms.CheckSignature(true);

                return signedCms.Certificates[signedCms.Certificates.Count - 1];
            }
            catch
            {
                return null;
            }
        }

        //public static string XadesStampSignatures(string xml)
        //{
        //    try
        //    {
        //        string tmpXml = xml;
        //        return X509Signature.XadesStampSignatures(tmpXml);
        //    }
        //    catch (Exception ex)
        //    {
        //        ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
        //        logger.LogError("Error: Method: XadesStampSignatures", ex.Message);

        //        return xml;
        //    }
        //}

        //public static string XadesStampSignature(string xml, string xPath, IDictionary<string, string> xPathNamespaces)
        //{
        //    try
        //    {
        //        string tmpXml = xml;

        //        // TODO DELETE THIS LINE
        //        tmpXml = tmpXml.Replace("dsig:XPath", "XPath");

        //        return X509Signature.XadesStampSignature(tmpXml, xPath, xPathNamespaces);
        //    }
        //    catch (Exception ex)
        //    {
        //        ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
        //        logger.LogError("Error: Method: XadesStampSignature", ex.Message);

        //        return xml;
        //    }
        //}

        //public static IEnumerable<X509SignatureInfo> GetSignaturesInfo(string xml)
        //{
        //    try
        //    {
        //        return X509Signature.GetSignaturesInfo(xml);
        //    }
        //    catch (Exception ex)
        //    {
        //        ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
        //        logger.LogError("Error: Method: GetSignaturesInfo", ex.Message);

        //        return null;
        //    }
        //}

        public static bool IsSignatureValid(string signatureId)
        {
            try
            {
                string xml = AppContext.Current.Xml;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.PreserveWhitespace = true;
                xmlDocument.LoadXml(xml);

                XmlElement xmlElement = null;
                XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");

                if (nodeList.Count == 1)
                {
                    xmlElement = (XmlElement)nodeList[0];
                }
                else
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        if (nodeList[i].Attributes["Id"] != null)
                        {
                            if (nodeList[i].Attributes["Id"].Value == signatureId)
                            {
                                xmlElement = (XmlElement)nodeList[i];
                                break;
                            }
                        }
                        else
                        {
                            XmlNode subNode = nodeList[i].FirstChild;

                            if (subNode != null && subNode.Attributes["Id"] != null)
                            {
                                if (subNode.Attributes["Id"].Value == signatureId)
                                {
                                    xmlElement = (XmlElement)subNode;
                                    break;
                                }
                            }
                        }
                    }
                }

                return X509Signature.CheckSignatureValidity(xmlDocument, xmlElement, null);
            }
            catch
            {
                // ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
                // logger.LogError("Error: Method: IsSignatureValid", ex.Message);

                return false;
            }
        }

        //public static IEnumerable<ITimeStampInfo> GetXadesStampInfo(string xml)
        //{
        //    try
        //    {
        //        return X509Signature.GetXadesStampInfo(xml);
        //    }
        //    catch (Exception ex)
        //    {
        //        ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
        //        logger.LogError("Error: Method: GetXadesStampInfo", ex.Message);

        //        return null;
        //    }
        //}

        //public static ITimeStampInfo GetFromXMLDigitalSignatureXadesStampInfo(R_0009_000004.XMLDigitalSignature signature)
        //{
        //    try
        //    {
        //        var serializer = new Components.DocumentSerializer.DocumentSerializerImpl();
        //        var partialXml = serializer.XmlSerializeObjectToString(signature);
        //        var partialInfo = X509Signature.GetXadesStampInfo(partialXml);
        //        if (partialInfo != null && partialInfo.Count() > 0)
        //            return partialInfo.First();
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ICommunicationLogger logger = DependencyResolver.Current.GetService<ICommunicationLogger>();
        //        logger.LogError("Error: Method: GetFromPartialXmlXadesStampInfo", ex.Message);

        //        return null;
        //    }
        //}

        public static DateTime? ExtractSigningTimeFromXml(string xml)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.PreserveWhitespace = true;
                xmlDocument.LoadXml(xml);

                DateTime signatureTime = DateTime.MinValue;
                var astElments = xmlDocument.GetElementsByTagName("ApplicationSigningTime");
                foreach (var ast in astElments)
                {
                    string d = ((XmlElement)ast).InnerText;
                    if (!string.IsNullOrEmpty(d))
                    {
                        DateTime.TryParse(d, out signatureTime);
                    }
                }

                var drosdElments = xmlDocument.GetElementsByTagName("DocumentReceiptOrSigningDate");
                foreach (var drosd in drosdElments)
                {
                    string d = ((XmlElement)drosd).InnerText;
                    if (!string.IsNullOrEmpty(d))
                    {
                        DateTime.TryParse(d, out signatureTime);
                    }
                }

                if (signatureTime != DateTime.MinValue)
                    return signatureTime;
            }
            catch
            {
                return null;
            }

            return null;
        }

        public static DateTime? ExtractSigningTimeFromXml()
        {
            if (AppContext.Current != null && !string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                return ExtractSigningTimeFromXml(AppContext.Current.Xml);
            }

            return null;
        }

        //public static string GetCertificateIdentifier(IRioApplication application)
        //{
        //    string personOrEntityIdentifier = string.Empty;

        //    IUesFactory _uesFactory = DependencyResolver.Current.GetService<IUesFactory>();

        //    if (application is IHeaderFooterDocumentsRioApplication)
        //    {
        //        var hfdra = (IHeaderFooterDocumentsRioApplication)application;

        //        var model = hfdra.ElectronicAdministrativeServiceFooter.XMLDigitalSignature;

        //        if (model != null && model.Signature != null && model.Signature.KeyInfo != null)
        //        {
        //            if (model.Signature.KeyInfo.X509DataCollection != null && model.Signature.KeyInfo.X509DataCollection.Count > 0)
        //            {
        //                foreach (var x509Certificate in model.Signature.KeyInfo.X509DataCollection)
        //                {
        //                    X509Certificate2 certificate = new X509Certificate2(x509Certificate.X509Certificate);

        //                    if (certificate != null)
        //                    {
        //                        UesBase uesBase = _uesFactory.GetUes(certificate);

        //                        if (uesBase != null)
        //                        {
        //                            personOrEntityIdentifier = uesBase.PersonalIdentifier;

        //                            if (!string.IsNullOrEmpty(personOrEntityIdentifier))
        //                            {
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return personOrEntityIdentifier;
        //}
    }
}