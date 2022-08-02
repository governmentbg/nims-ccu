using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Eumis.Rio
{
    public static class RioExtensions
    {
        public static int? GetPrivateNomId<T>(this T doc, Func<T, Eumis.Rio.PrivateNomenclature> getter, Func<Guid, int> getNomIdByGid)
            where T : class
        {
            var xmlNom = doc.Get(getter);
            if (xmlNom == null || string.IsNullOrEmpty(xmlNom.Id))
            {
                return null;
            }

            Guid gid;
            if (!Guid.TryParse(xmlNom.Id, out gid))
            {
                return null;
            }

            return getNomIdByGid(gid);
        }

        public static int? GetPublicNomId<T>(this T doc, Func<T, Eumis.Rio.PublicNomenclature> getter, Func<string, int> getNomIdByCode)
            where T : class
        {
            var xmlNom = doc.Get(getter);
            if (xmlNom == null || string.IsNullOrEmpty(xmlNom.Code))
            {
                return null;
            }

            return getNomIdByCode(xmlNom.Code);
        }

        public static Nullable<TEnum> GetEnum<T, TEnum>(this T obj, Func<T, string> getter)
            where T : class
            where TEnum : struct
        {
            string enumString = Get(obj, getter);

            TEnum parseEnum;
            if (!string.IsNullOrEmpty(enumString) &&
                Enum.TryParse<TEnum>(enumString, true, out parseEnum))
            {
                return parseEnum;
            }
            else
            {
                return null;
            }
        }

        public static TResult Get<T, TResult>(this T obj, Func<T, TResult> getter)
            where T : class
        {
            try
            {
                return getter(obj);
            }
            catch (NullReferenceException)
            {
                return default(TResult);
            }
        }

        public static IEnumerable<AttachedDocument> GetFiles<T>(this T obj, Func<T, IEnumerable<AttachedDocument>> selector)
            where T : class
        {
            return obj
                .Get(selector)
                .Where(ad => !string.IsNullOrWhiteSpace(ad.AttachedDocumentContent.BlobContentId));
        }

        public static IEnumerable<AttachedDocument> GetFiles<T1, T2>(this T1 obj, Func<T1, IEnumerable<T2>> preSelector, Func<T2, IEnumerable<AttachedDocument>> selector)
            where T1 : class
            where T2 : class
        {
            return obj
                .Get(preSelector)
                .SelectMany(i => i.Get(selector))
                .Where(ad => !string.IsNullOrWhiteSpace(ad.AttachedDocumentContent.BlobContentId));
        }

        public static T Deserialize<T>(string xml) where T : class
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
