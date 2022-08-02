using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Eumis.IntegrationRegiX.Host.Helpers
{
    public static class XmlHelper
    {
        public static XmlElement SerializeToXmlElement<T>(T request)
        {
            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(typeof(T)).Serialize(writer, request);
            }

            return doc.DocumentElement;
        }

        public static T DeserializeFromXmlElement<T>(this XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new XmlNodeReader(element));
        }

        public static XmlElement GetElement(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }
    }
}
