using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Eumis.Components;
using System.Security.Cryptography.Xml;

namespace Eumis.Components
{
    /// <summary>
    /// Имплементация на компонент извършващ подписването на документи
    /// </summary>
    public class DocumentSigner : IDocumentSigner
    {
        #region Public

        public DocumentSigner()
        {

        }

        /// <summary>
        /// Подписва документ в XML формат
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <param name="certificateName">име на сертификата в Local Computer Certificate Store</param>
        /// <returns>подписан XML</returns>
        public string Sign(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, string certificateName)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("Xml document can not be null or empty.");
            if (string.IsNullOrEmpty(signatureXPath))
                throw new ArgumentException("Signature XPath can not be null or empty.");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            if (signatureXPathNamespaces != null)
            {
                foreach (var ns in signatureXPathNamespaces)
                {
                    xmlNamespaceManager.AddNamespace(ns.Key, ns.Value);
                }
            }

            MakeXPath(xmlDocument, signatureXPath, xmlNamespaceManager);

            XmlElement xmlDigitalSignature = GenerateSignature(xmlDocument, certificateName);

            XmlNode node = xmlDocument.SelectSingleNode(signatureXPath, xmlNamespaceManager);
            node.AppendChild(xmlDocument.ImportNode(xmlDigitalSignature, true));

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, DocumentSerializerSettings.DefaultEncoding))
                {
                    xmlDocument.WriteTo(xmlWriter);

                    xmlWriter.Flush();
                    ms.Seek(0, System.IO.SeekOrigin.Begin);

                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ms, DocumentSerializerSettings.DefaultEncoding))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Проверява валидността на подписа на подписан XML
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <returns>true ако подписа е валиден, иначе false</returns>
        public bool HasValidSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, out X509Certificate2 signingCertificate)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("Xml document can not be null or empty.");
            if (string.IsNullOrEmpty(signatureXPath))
                throw new ArgumentException("Signature XPath can not be null or empty.");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            if (signatureXPathNamespaces != null)
            {
                foreach (var ns in signatureXPathNamespaces)
                {
                    xmlNamespaceManager.AddNamespace(ns.Key, ns.Value);
                }
            }

            return VerifyXml(xmlDocument, signatureXPath, xmlNamespaceManager, out signingCertificate);
        }

        /// <summary>
        /// Проверява наличност на подписа на подписан XML
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <returns>true ако има подписа, иначе false</returns>
        public bool HasSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("Xml document can not be null or empty.");
            if (string.IsNullOrEmpty(signatureXPath))
                throw new ArgumentException("Signature XPath can not be null or empty.");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            if (signatureXPathNamespaces != null)
            {
                foreach (var ns in signatureXPathNamespaces)
                {
                    xmlNamespaceManager.AddNamespace(ns.Key, ns.Value);
                }
            }

            XmlNode node = xmlDocument.SelectSingleNode(signatureXPath, xmlNamespaceManager);

            if (node == null)
            {
                return false;
            }

            xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            XmlNode signatureNode = node.SelectSingleNode("//ds:Signature", xmlNamespaceManager);

            if (!(signatureNode is XmlElement))
            {
                return false;
            }

            return true;
        }

        //public string SignSubmissionRequestMessage(string docXml)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(docXml);

        //    XmlElement xmlDigitalSignature = GenerateSignature(xmlDocument, _portalConfigurationManager.EncryptionCertificateName);

        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, DocumentSerializerSettings.DefaultEncoding))
        //        {
        //            xmlDigitalSignature.WriteTo(xmlWriter);

        //            xmlWriter.Flush();
        //            ms.Seek(0, System.IO.SeekOrigin.Begin);

        //            using (System.IO.StreamReader sr = new System.IO.StreamReader(ms, DocumentSerializerSettings.DefaultEncoding))
        //            {
        //                return sr.ReadToEnd();
        //            }
        //        }
        //    }
        //}

        //public bool VerifySubmissionRequestMessage(string responseSignatureXml)
        //{
        //    X509Certificate2 cert = GetX509Certificate(_portalConfigurationManager.DavidCertificateName);

        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(responseSignatureXml);

        //    SignedXml signedXml = new SignedXml();
        //    signedXml.LoadXml(xmlDocument.DocumentElement);

        //    Signature sig = new Signature();
        //    sig.LoadXml(xmlDocument.DocumentElement);

        //    return signedXml.CheckSignature(cert, true);
        //}

        //public string EncryptEsoedMessage(string esoedDocument)
        //{
        //    X509Certificate2 cert = GetX509Certificate(_portalConfigurationManager.EncryptionCertificateName);

        //    Esoed esoed = _communicationMessageHelper.ParseEsoedMessage(esoedDocument);

        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(esoedDocument);

        //    Encrypt(xmlDocument, cert);

        //    esoed.EncryptedData = xmlDocument.DocumentElement;

        //    return _communicationMessageHelper.CreateEsoedMessage(esoed);
        //}

        //public string DecryptEsoedMessage(string esoedMessage)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(esoedMessage);

        //    Decrypt(xmlDocument);

        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, DocumentSerializerSettings.DefaultEncoding))
        //        {
        //            xmlDocument.WriteTo(xmlWriter);

        //            xmlWriter.Flush();
        //            ms.Seek(0, System.IO.SeekOrigin.Begin);

        //            using (System.IO.StreamReader sr = new System.IO.StreamReader(ms, DocumentSerializerSettings.DefaultEncoding))
        //            {
        //                return sr.ReadToEnd();
        //            }
        //        }
        //    }
        //}

        #endregion

        #region Private

        private X509Certificate2 GetX509Certificate(string certificateName)
        {
            X509Certificate2 x509Certificate;
            X509Store keyStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            keyStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection x509Certificates = keyStore.Certificates
                .Find(X509FindType.FindBySubjectName, certificateName, false);
            if (x509Certificates.Count > 0)
            {
                x509Certificate = x509Certificates[0];
            }
            else
            {
                throw new Exception("x509 Certificate does not exist. Name: " + certificateName);
            }

            return x509Certificate;
        }

        private XmlElement GenerateSignature(XmlDocument xmlDocument, string certificateName)
        {
            X509Certificate2 certificate = GetX509Certificate(certificateName);

            if (certificate.PrivateKey == null)
                throw new MissingFieldException("Private Key is missing.");

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));

            SignedXml signedXml = new SignedXml(xmlDocument);
            signedXml.KeyInfo = keyInfo;
            signedXml.SigningKey = certificate.PrivateKey;
            Reference reference = new Reference();
            reference.Uri = string.Empty;
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            return signedXml.GetXml();
        }

        private bool VerifyXml(XmlDocument xmlDocument, string signatureXPath, XmlNamespaceManager xmlNamespaceManager, out X509Certificate2 signingCertificate)
        {
            if (xmlDocument == null)
                throw new ArgumentException("xmlDocument");

            signingCertificate = null;

            SignedXml signedXml = new SignedXml(xmlDocument);
            XmlNode node = xmlDocument.SelectSingleNode(signatureXPath, xmlNamespaceManager);

            if (node == null)
            {
                return false;
            }

            xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            XmlNode signatureNode = node.SelectSingleNode("//ds:Signature", xmlNamespaceManager);

            if (!(signatureNode is XmlElement))
            {
                return false;
            }

            signedXml.LoadXml((XmlElement)signatureNode);
            bool isValid = signedXml.CheckSignature();

            if (isValid)
            {
                if (signedXml.KeyInfo != null)
                {
                    var dsaPublicKeys = signedXml.KeyInfo.OfType<DSAKeyValue>().Select(kv => kv.Key.ToXmlString(false));
                    var rsaPublicKeys = signedXml.KeyInfo.OfType<RSAKeyValue>().Select(kv => kv.Key.ToXmlString(false));
                    var publicKeys = dsaPublicKeys.Concat(rsaPublicKeys);
                    string singingKey = publicKeys.SingleOrDefault();

                    if (singingKey != null)
                    {
                        signingCertificate =
                            signedXml.KeyInfo
                            .OfType<KeyInfoX509Data>()
                            .SelectMany(ki => ki.Certificates.OfType<X509Certificate2>())
                            .Where(cert => cert.PublicKey.Key.ToXmlString(false).Equals(singingKey))
                            .SingleOrDefault();
                    }
                }
            }

            return isValid;
        }

        private void MakeXPath(XmlDocument doc, string xpath, XmlNamespaceManager xmlNamespaceManager)
        {
            List<string> partsOfXPath = xpath.Trim('/').Split('/').ToList();
            MakeXPath(doc, doc, partsOfXPath, xmlNamespaceManager);
        }

        private void MakeXPath(XmlDocument doc, XmlNode parent, List<string> partsOfXPath, XmlNamespaceManager xmlNamespaceManager)
        {
            string nextNodeInXPathWithNS = partsOfXPath.FirstOrDefault();
            if (string.IsNullOrEmpty(nextNodeInXPathWithNS))
                return;

            string nextNodeInXPath;
            string nextNodeInXPathNamespacePrefix;
            if (nextNodeInXPathWithNS.Contains(':'))
            {
                string[] nodeNameParts = nextNodeInXPathWithNS.Trim().Split(':');
                nextNodeInXPath = nodeNameParts.Last();
                nextNodeInXPathNamespacePrefix = nodeNameParts.First();
            }
            else
            {
                nextNodeInXPath = nextNodeInXPathWithNS;
                nextNodeInXPathNamespacePrefix = null;
            }

            XmlNode node = null;
            if (parent == null)
            {
                doc.AppendChild(doc.CreateElement(nextNodeInXPath));
                node = doc.DocumentElement;
            }
            else
            {
                node = parent.SelectSingleNode(nextNodeInXPathWithNS, xmlNamespaceManager);
                if (node == null)
                {
                    if (nextNodeInXPathNamespacePrefix != null)
                        node = parent.AppendChild(doc.CreateElement(nextNodeInXPath, xmlNamespaceManager.LookupNamespace(nextNodeInXPathNamespacePrefix)));
                    else
                        node = parent.AppendChild(doc.CreateElement(nextNodeInXPath));
                }
            }

            MakeXPath(doc, node, partsOfXPath.Skip(1).ToList(), xmlNamespaceManager);
        }

        private void Encrypt(XmlDocument xmlDocument, X509Certificate2 cert)
        {
            // Check the arguments.  
            if (xmlDocument == null)
                throw new ArgumentNullException("xmlDocument");
            if (cert == null)
                throw new ArgumentNullException("cert");

            ////////////////////////////////////////////////
            // Find the root element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////

            XmlElement elementToEncrypt = xmlDocument.DocumentElement;

            // Throw an XmlException if the element was not found.
            if (elementToEncrypt == null)
            {
                throw new XmlException("The root element was not found");

            }

            //////////////////////////////////////////////////
            // Create a new instance of the EncryptedXml class 
            // and use it to encrypt the XmlElement with the 
            // X.509 Certificate.
            //////////////////////////////////////////////////

            EncryptedXml eXml = new EncryptedXml();

            // Encrypt the element.
            EncryptedData edElement = eXml.Encrypt(elementToEncrypt, cert);


            ////////////////////////////////////////////////////
            // Replace the element from the original XmlDocument
            // object with the EncryptedData element.
            ////////////////////////////////////////////////////

            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        private void Decrypt(XmlDocument xmlDocumant)
        {
            // Check the arguments.  
            if (xmlDocumant == null)
                throw new ArgumentNullException("xmlDocumant");

            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml(xmlDocumant);

            // Decrypt the XML document.
            exml.DecryptDocument();
        }

        #endregion

    }
}
