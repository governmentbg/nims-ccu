using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Eumis.Common;
using Eumis.Common.Crypto;

namespace Eumis.Domain.Core
{
    public class RioXmlDocument<T>
        where T : class
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        [SuppressMessage("", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Utility method using private access to the class.")]
        public static string GetHash(string xml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var dummy = new RioXmlDocument<T>();
            dummy.SetXml(xmlDoc);

            return dummy.Hash;
        }

        public virtual void SetXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new DomainValidationException("xml cannot be null or empty");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            this.SetXml(xmlDoc);
        }

        public void SetXml(T document)
        {
            if (document == null)
            {
                throw new DomainValidationException("xml cannot be null or empty");
            }

            var xmlDoc = this.SerializeToXmlDocument(document);
            this.SetXml(xmlDoc);
        }

        public T GetDocument()
        {
            return this.Deserialize(this.Xml);
        }

        public void SetXml(XmlDocument xmlDoc)
        {
            // remove xml declaration (required for storing it in as an xml column in sql)
            if (xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                xmlDoc.RemoveChild(xmlDoc.FirstChild);
            }

            this.Xml = xmlDoc.OuterXml;
            this.Hash = CryptoUtils.GetSha256XMLHash(xmlDoc.OuterXml).Truncate(10);
        }

        private T Deserialize(XmlDocument xmlDoc)
        {
            using (XmlReader xmlReader = new XmlNodeReader(xmlDoc))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(xmlReader);
            }
        }

        private T Deserialize(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        private XmlDocument SerializeToXmlDocument(T document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document should not be null.");
            }

            var xmlDoc = new XmlDocument();
            using (var writer = xmlDoc.CreateNavigator().AppendChild())
            {
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(writer, document);
            }

            return xmlDoc;
        }
    }
}
