using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Eumis.Public.Common.Helpers
{
    public static class Helper
    {
        public static List<int> GetIdListFromString(string idList)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(idList))
            {
                string[] values = idList.Split(',');

                foreach (string s in values)
                {
                    int i;
                    if (int.TryParse(s, out i))
                    {
                        ids.Add(i);
                    }
                }
            }

            return ids;
        }

        public static List<string> GetEmailListFromString(string input, char delimiter)
        {
            List<string> emails = new List<string>();

            if (!string.IsNullOrEmpty(input))
            {
                string[] values = input.Split(delimiter);

                foreach (var val in values)
                {
                    if (EmailCheck(val.Trim()))
                    {
                        emails.Add(val.Trim());
                    }
                }
            }

            return emails;
        }

        public static bool EmailCheck(string email)
        {
            string emailRegex = @"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$";

            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailRegex);
        }

        public static string GetStringFromIdList(List<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            if (ids.Any())
            {
                foreach (var id in ids)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }

                    sb.Append(id);
                }
            }

            return sb.ToString();
        }

        public static string ToNotNullString(string value)
        {
            return value != null ? value : string.Empty;
        }

        public static string FormatName(string firstName, string secondName, string lastName)
        {
            return string.Format("{0} {1} {2}", ToNotNullString(firstName), ToNotNullString(secondName), ToNotNullString(lastName)).Trim();
        }

        public static Tuple<string, string, string> SplitNames(string fullName)
        {
            string firstName = string.Empty;
            string secondName = string.Empty;
            string lastName = string.Empty;

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var splitName = fullName.Split(new char[] { ' ' });

                firstName = splitName[0];
                if (splitName.Length > 1)
                {
                    lastName = splitName[splitName.Length - 1];

                    if (splitName.Length > 2)
                    {
                        for (int i = 1; i < splitName.Length - 1; i++)
                        {
                            secondName = secondName + splitName[i] + ' ';
                        }

                        secondName = secondName.Trim();
                    }
                }
            }

            return new Tuple<string, string, string>(firstName, secondName, lastName);
        }

        public static string AnonymizeName(string fullName)
        {
            if (fullName == null)
            {
                return fullName;
            }

            var names = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (names.Length > 0)
            {
                return names[0];
            }
            else
            {
                return fullName;
            }
        }

        #region CastToSqlDbValue methods

        public static object CastToSqlDbValue(string value)
        {
            return value != null ? value : SqlString.Null;
        }

        public static object CastToSqlDbValue(int value)
        {
            return value;
        }

        public static object CastToSqlDbValue(int? value)
        {
            return value.HasValue ? value.Value : SqlInt32.Null;
        }

        public static object CastToSqlDbValue(bool value)
        {
            return value;
        }

        public static object CastToSqlDbValue(bool? value)
        {
            return value.HasValue ? value.Value : SqlBoolean.Null;
        }

        public static object CastToSqlDbValue(DateTime value)
        {
            return value;
        }

        public static object CastToSqlDbValue(DateTime? value)
        {
            return value.HasValue ? value.Value : SqlDateTime.Null;
        }

        public static object CastToSqlDbValue(Guid value)
        {
            return value;
        }

        public static object CastToSqlDbValue(Guid? value)
        {
            return value.HasValue ? value.Value : SqlGuid.Null;
        }

        public static object CastToSqlDbValue(byte[] value)
        {
            return value != null ? value : SqlBinary.Null;
        }

        #endregion

        public static byte[] StringToVersion(string version)
        {
            return Convert.FromBase64String(version);
        }

        public static string VersionToString(byte[] version)
        {
            return Convert.ToBase64String(version);
        }

        public static string CalculateSHA1(byte[] content)
        {
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] hashBytes = cryptoTransformSHA1.ComputeHash(content);
            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        public static string GetDetailedExceptionInfo(Exception ex)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (ex != null)
            {
                GetExceptionInfo(strBuilder, ex);
            }

            return strBuilder.ToString();
        }

        public static string RemoveWhiteSpaces(this string value)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                // Trim each line
                result = string.Join(
                    Environment.NewLine,
                    value.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                         .Select(l => l.Trim()));

                // Remove new lines
                result = result.Replace(Environment.NewLine, string.Empty).Trim();
            }

            return result;
        }

        private static void GetExceptionInfo(StringBuilder stringBuilder, Exception exception)
        {
            stringBuilder.AppendFormat("Exception type: {0}\n", exception.GetType().FullName);
            stringBuilder.AppendFormat("Message: {0}\n", exception.Message);
            stringBuilder.AppendFormat("Stack trace:\n{0}\n", exception.StackTrace);
            if (exception.InnerException != null)
            {
                stringBuilder.AppendFormat("\n\nInner Exception:\n");
                GetExceptionInfo(stringBuilder, exception.InnerException);
            }
        }

        public static string DateToBgFormatWithoutAbbr(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }

        public static string DateToBgFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г.");
        }

        public static string DateToBgFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd.MM.yyyy г.");
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeToBgFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г. HH:mm:ss ч.");
        }

        public static string DateTimeToBgFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd.MM.yyyy г. HH:mm:ss ч.");
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeToBgFormatWithoutSeconds(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г. HH:mm ч.");
        }

        public static string DateTimeToBgFormatWithoutSeconds(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd.MM.yyyy г. HH:mm ч.");
            }
            else
            {
                return null;
            }
        }
    }
}
