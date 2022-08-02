using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Eumis.Common.Helpers
{
    /// <summary>
    /// Модел за постване на ePay плащане
    /// </summary>
    public class ePayPost
    {
        /// <summary>
        /// Url при успешно изпращане
        /// </summary>
        public string URLOK { get; set; }

        /// <summary>
        /// Url при неуспешно изпращане
        /// </summary>
        public string URLCANCEL { get; set; }

        /// <summary>
        /// Кодиране
        /// </summary>
        public string ENCODED { get; set; }

        /// <summary>
        /// Сума
        /// </summary>
        public string CHECKSUM { get; set; }

        public string ePayUrl { get; set; }
    }

    /// <summary>
    /// Модел на данните за постване при ePay плащане
    /// </summary>
    public class ePayPostData
    {
        /// <summary>
        /// Фактура
        /// </summary>
        public string INVOICE { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string STATUS { get; set; }

        /// <summary>
        /// Време на плащане
        /// </summary>
        public DateTime PAYTIME { get; set; }

        /// <summary>
        /// Код на транзакцията
        /// </summary>
        public string STAN { get; set; }

        /// <summary>
        /// Идентификатор на банка
        /// </summary>
        public string BCODE { get; set; }
    }

    /// <summary>
    /// Модел на ePay отговор
    /// </summary>
    public class ePayReplay
    {
        /// <summary>
        /// Фактура
        /// </summary>
        public string INVOICE { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string STATUS { get; set; }
    }

    public class ePayHelper
    {
        public static ePayPost GetePayPost(string KIN, string EMAIL, decimal? EXPIREDAYS, string SECRETKEY, string invoice, decimal tariff, string descr)
        {
            ePayPost vm = new ePayPost();

            string amount = tariff.ToString("0.00", new System.Globalization.CultureInfo("en-US"));
            string ePayRequest = GetePayRequest(invoice, KIN, EMAIL, Convert.ToDouble((object) EXPIREDAYS), amount, descr);
            string encodedTo64 = EncodeTo64(ePayRequest);

            vm.ENCODED = encodedTo64;

            string encodedToEncodeHMACSHA1 = EncodeHMACSHA1(encodedTo64, SECRETKEY);

            vm.CHECKSUM = HttpUtility.UrlEncode(encodedToEncodeHMACSHA1);
            vm.URLOK = System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("ePayUrlOk") + invoice;
            vm.URLCANCEL = System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("ePayUrlFail");
            vm.ePayUrl = System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("ePaySendUrl");

            return vm;
        }

        public static string GetePayRequest(string invoice, string min, string email, double expireDays, string amount, string descr)
        {
            string exp_date = DateTime.Now.AddDays(expireDays).ToString("dd.MM.yyyy");
            string sum = amount;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("MIN={0}", min);
            sb.AppendLine();
            sb.AppendFormat("EMAIL={0}", email);
            sb.AppendLine();
            sb.AppendFormat("INVOICE={0}", invoice);
            sb.AppendLine();
            sb.AppendFormat("AMOUNT={0}", sum);
            sb.AppendLine();
            sb.AppendFormat("EXP_TIME={0}", exp_date);
            sb.AppendLine();
            sb.AppendFormat("DESCR={0}", descr);
            sb.AppendLine();
            sb.AppendFormat("ENCODING=UTF-8");

            return sb.ToString(); ;
        }

        public static string EncodeHMACSHA1(string message, string key)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
            string result = ByteToHex(hashmessage);
            return result;
        }

        public static ePayPostData DecodeePayPostData(string encoded, string checksum)
        {
            encoded = HttpUtility.UrlDecode(encoded);
            checksum = HttpUtility.UrlDecode(checksum);

            string ePayResponse = DecodeFrom64(encoded);
            ePayPostData result = GetePayPostData(ePayResponse);

            return result;
        }

        private static ePayPostData GetePayPostData(string ePayResponce)
        {
            /* if (preg_match("/^INVOICE=(\d+):STATUS=(PAID|DENIED|EXPIRED)(:PAY_TIME=(\d+):STAN=(\d+):BCODE=([0-9a-zA-Z]+))?$/", $line, $regs)) {
                 $invoice  = $regs[1];
                 $status   = $regs[2];
                 $pay_date = $regs[4]; # XXX if PAID
                 $stan     = $regs[5]; # XXX if PAID
                 $bcode    = $regs[6]; # XXX if PAID */

            ePayPostData vm = new ePayPostData();

            string[] postElements = ePayResponce.Split(new string[] { ":", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> ePayPostElements
                = postElements.Select(p => new { Key = p.Split('=').First(), Value = p.Split('=').Last() })
                                .AsEnumerable()
                                .ToDictionary(k => k.Key, v => v.Value);

            foreach (var item in ePayPostElements)
            {
                switch (item.Key)
                {
                    case "INVOICE":
                        vm.INVOICE = item.Value;
                        break;
                    case "STATUS":
                        vm.STATUS = item.Value;
                        break;
                    case "PAY_TIME":
                        vm.PAYTIME = DateTime.ParseExact(item.Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "STAN":
                        vm.STAN = item.Value;
                        break;
                    case "BCODE":
                        vm.BCODE = item.Value;
                        break;
                    default:
                        break;
                }
            }

            return vm;
        }

        private static string ByteToHex(byte[] buff)
        {
            string sbinary = string.Empty;
            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
            = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
            = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private static string DecodeFrom64(string toDecode)
        {
            byte[] decodedAsBytes = System.Convert.FromBase64String(toDecode);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(decodedAsBytes);

            return returnValue;
        }
    }
}