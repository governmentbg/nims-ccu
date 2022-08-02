﻿using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Eumis.Common.Helpers
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

            return (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailRegex));
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
            return value != null ? value : String.Empty;
        }

        public static string RemoveNewLines(this string value)
        {
            value = value.Replace("\n", String.Empty);
            value = value.Replace("\r", String.Empty);
            
            return value;
        }

        public static string FormatName(string firstName, string secondName, string lastName)
        {
            return String.Format("{0} {1} {2}", ToNotNullString(firstName), ToNotNullString(secondName), ToNotNullString(lastName)).Trim();
        }

        public static Tuple<string, string, string> SplitNames(string fullName)
        {
            string firstName = String.Empty;
            string secondName = String.Empty;
            string lastName = String.Empty;

            if (!String.IsNullOrWhiteSpace(fullName))
            {
                var splitName = fullName.Split(new Char[] { ' ' });

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
            return BitConverter.ToString(hashBytes).Replace("-", "");
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
            String result = String.Empty;

            if (!String.IsNullOrWhiteSpace(value))
            {
                // Trim each line
                result = String.Join(Environment.NewLine,
                    value.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                         .Select(l => l.Trim()));

                // Remove new lines
                result = result.Replace(Environment.NewLine, String.Empty).Trim();
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

        #region DateFormat

        public static string DateToBgFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г.");
        }

        public static string DateToEnFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string DateToBgFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateToBgFormat(dateTime.Value);
            }

            return null;
        }

        public static string DateToEnFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateToEnFormat(dateTime.Value);
            }

            return null;
        }

        public static string DateFormat(DateTime? dateTime)
        {
            if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return DateToEnFormat(dateTime);
            }

            return DateToBgFormat(dateTime);
        }

        #endregion

        #region DateTimeFormat

        public static string DateTimeToBgFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г. HH:mm:ss ч.");
        }

        public static string DateTimeToEnFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string DateTimeToBgFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToBgFormat(dateTime.Value);
            }

            return null;
        }

        public static string DateTimeToEnFormat(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToEnFormat(dateTime.Value);
            }

            return null;
        }

        public static string DateTimeFormat(DateTime? dateTime)
        {
            if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return DateTimeToEnFormat(dateTime);
            }

            return DateTimeToBgFormat(dateTime);
        }

        public static string ToISO8601Format(this DateTime dateTime)
        {
            return dateTime.ToString("o", CultureInfo.InvariantCulture);
        }

        #endregion

        #region DateTimeFormatWithoutSeconds

        public static string DateTimeToBgFormatWithoutSeconds(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г. HH:mm ч.");
        }

        public static string DateTimeToEnFormatWithoutSeconds(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }

        public static string DateTimeToBgFormatWithoutSeconds(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToBgFormatWithoutSeconds(dateTime.Value);
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeToEnFormatWithoutSeconds(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToEnFormatWithoutSeconds(dateTime.Value);
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeFormatWithoutSeconds(DateTime? dateTime)
        {
            if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return DateTimeToEnFormatWithoutSeconds(dateTime);
            }

            return DateTimeToBgFormatWithoutSeconds(dateTime);
        }

        #endregion

        #region DateTimeFormatOnlyDate

        public static string DateTimeToBgFormatOnlyDate(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy г.");
        }

        public static string DateTimeToEnFormatOnlyDate(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string DateTimeToBgFormatOnlyDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToBgFormatOnlyDate(dateTime.Value);
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeToEnFormatOnlyDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToEnFormatOnlyDate(dateTime.Value);
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeFormatOnlyDate(DateTime? dateTime)
        {
            if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return DateTimeToEnFormatOnlyDate(dateTime);
            }

            return DateTimeToBgFormatOnlyDate(dateTime);
        }

        #endregion
    }
}
