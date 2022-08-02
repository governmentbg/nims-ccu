using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Certificates
{
    public static class CertificateExtensions
    {
        private static readonly char[] PossibleSepartors = new char[2] { '+', ',' };

        public static CertificateSubjectInfo GetSubject(this X509Certificate2 certificate)
        {
            var subjectData = NormalizeSubject(certificate.Subject);

            return DeserializeSubject(subjectData);
        }

        private static char GetCurrentSubjectSeparator(string value)
        {
            var matchingHits = new List<int>();

            foreach (char separator in PossibleSepartors)
            {
                matchingHits.Add(value.Split(separator).Length);
            }

            return PossibleSepartors[matchingHits.IndexOf(matchingHits.Max())];
        }

        private static List<string> NormalizeSubject(string subject)
        {
            // Replace potentially danger character
            subject = subject.Replace('"', '|');

            char currentSeparator = GetCurrentSubjectSeparator(subject);

            var preparedData = subject.Split(currentSeparator).ToList();

            for (int m = preparedData.Count - 1; m >= 0; m--)
            {
                if (!preparedData[m].Contains("=") && m > 0)
                {
                    // Restore lines integrity
                    preparedData[m - 1] += ", " + preparedData[m];
                    preparedData.RemoveAt(m);
                }
            }

            return preparedData;
        }

        private static CertificateSubjectInfo DeserializeSubject(List<string> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            data.ForEach(a =>
            {
                var expression = a.Split('=');
                if (expression.Length == 2)
                {
                    sb.Append($"\"{expression[0].Trim()}\": \"{expression[1]}\",{Environment.NewLine}");
                }
            });

            sb.Append("}");

            return JsonConvert.DeserializeObject<CertificateSubjectInfo>(sb.ToString());
        }
    }
}
