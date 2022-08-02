using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Eumis.Components
{
    /// <summary>
    /// Интерфейс на компонент извършващ подписването на документи
    /// </summary>
    public interface IDocumentSigner
    {
        /// <summary>
        /// Подписва документ в XML формат
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <param name="certificateName">име на сертификата в Local Computer Certificate Store</param>
        /// <returns>подписан XML</returns>
        string Sign(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, string certificateName);

        /// <summary>
        /// Проверява валидността на подписа на подписан XML
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <returns>true ако подписа е валиден, иначе false</returns>
        bool HasValidSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, out X509Certificate2 signingCertificate);

        /// <summary>
        /// Проверява наличност на подписа на подписан XML
        /// </summary>
        /// <param name="xml">XML съдържанието на документа</param>
        /// <param name="signatureXPath">път до XML елемента в който ще се съдържа електронния подпис</param>
        /// <param name="signatureXPathNamespaces">XML namespace-и използвани в signatureXPath</param>
        /// <returns>true ако има подписа, иначе false</returns>
        bool HasSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces);

        //string EncryptEsoedMessage(string esoedDocument);
        //string DecryptEsoedMessage(string esoedMessage);

        //string SignSubmissionRequestMessage(string docXml);
        //bool VerifySubmissionRequestMessage(string responseSignatureXml);
    }
}
