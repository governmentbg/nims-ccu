using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Eumis.Components
{
    /// <summary>
    /// Временна имплементация на компонента за подписване на xml документи
    /// </summary>
    public class FakeDocumentSigner : IDocumentSigner
    {
        #region Public

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentSigner"></param>
        public FakeDocumentSigner(IDocumentSigner documentSigner)
        {
            _documentSigner = documentSigner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="signatureXPath"></param>
        /// <param name="signatureXPathNamespaces"></param>
        /// <param name="certificateName"></param>
        /// <returns></returns>
        public string Sign(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, string certificateName)
        {
            return _documentSigner.Sign(xml, signatureXPath, signatureXPathNamespaces, certificateName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="signatureXPath"></param>
        /// <param name="signatureXPathNamespaces"></param>
        /// <returns></returns>
        public bool HasValidSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, out X509Certificate2 signingCertificate)
        {
            signingCertificate = null;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="signatureXPath"></param>
        /// <param name="signatureXPathNamespaces"></param>
        /// <returns></returns>
        public bool HasSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces)
        {
            return true;
        }

        #endregion

        #region Private

        IDocumentSigner _documentSigner;

        #endregion        
    }
}
