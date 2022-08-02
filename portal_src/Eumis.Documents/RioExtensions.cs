using System;
using System.Collections.Generic;
using System.Linq;
using R_10018;

namespace Eumis.Documents
{
    public static class RioExtensions
    {
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
    }
}
