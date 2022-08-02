using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Eumis.Documents.Validation.Shared
{
    public class IbanValidator
    {
        private static readonly Regex cyrillicSymbols = new Regex(@"[а-яА-Я]");

        public static ValidationResult Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return ValidationResult.ValueMissing;

            if (value.Length < Lengths.Select(l => l.Value).Min())
                return ValidationResult.ValueTooSmall;

            if (cyrillicSymbols.IsMatch(value))
            {
                return ValidationResult.ValueFoundCyrillic;
            }

            var countryCode = value.Substring(0, 2).ToUpper();

            int lengthForCountryCode;

            var countryCodeKnown = Lengths.TryGetValue(countryCode, out lengthForCountryCode);
            if (!countryCodeKnown)
            {
                return ValidationResult.CountryCodeNotKnown;
            }

            value = Regex.Replace(value, @"\s+", "");

            if (countryCode == "BG")
            {
                if (!baeCodes.Contains(value.Substring(4, 8).ToUpper()))
                {
                    return ValidationResult.BaeCodeNotKnown;
                }
            }

            if (value.Length < lengthForCountryCode)
                return ValidationResult.ValueTooSmall;

            if (value.Length > lengthForCountryCode)
                return ValidationResult.ValueTooBig;

            Regex regexForCountryCode;

            var countryRegex = Regexs.TryGetValue(countryCode, out regexForCountryCode);
            if (!countryRegex)
            {
                return ValidationResult.CountryCodeNotKnown;
            }

            if (!regexForCountryCode.IsMatch(value))
            {
                return ValidationResult.ValueWrongStructure;
            }

            value = value.ToUpper();
            var newIban = value.Substring(4) + value.Substring(0, 4);

            newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

            var remainder = BigInteger.Parse(newIban) % 97;

            if (remainder != 1)
                return ValidationResult.ValueFailsModule97Check;

            return ValidationResult.IsValid;
        }

        public enum ValidationResult
        {
            IsValid,
            ValueMissing,
            ValueTooSmall,
            ValueTooBig,
            ValueFoundCyrillic,
            ValueWrongStructure,
            ValueFailsModule97Check,
            CountryCodeNotKnown,
            BaeCodeNotKnown
        }

        private static readonly IDictionary<string, int> Lengths = new Dictionary<string, int>
        {
            {"AL", 28},
            {"AD", 24},
            {"AT", 20},
            {"AZ", 28},
            {"BE", 16},
            {"BH", 22},
            {"BY", 28},
            {"BA", 20},
            {"BR", 29},
            {"BG", 22},
            {"CR", 21},
            {"HR", 21},
            {"CY", 28},
            {"CZ", 24},
            {"DK", 18},
            {"DO", 28},
            {"TL", 23},
            {"EE", 20},
            {"FO", 18},
            {"FI", 18},
            {"FR", 27},
            {"GE", 22},
            {"DE", 22},
            {"GI", 23},
            {"GR", 27},
            {"GL", 18},
            {"GT", 28},
            {"HU", 28},
            {"IS", 26},
            {"IE", 22},
            {"IL", 23},
            {"IT", 27},
            {"JO", 30},
            {"KZ", 20},
            {"XK", 20},
            {"KW", 30},
            {"LV", 21},
            {"LB", 28},
            {"LI", 21},
            {"LT", 20},
            {"LU", 20},
            {"MK", 19},
            {"MT", 31},
            {"MR", 27},
            {"MU", 30},
            {"MC", 27},
            {"MD", 24},
            {"ME", 22},
            {"NL", 18},
            {"NO", 15},
            {"PK", 24},
            {"PS", 29},
            {"PL", 28},
            {"PT", 25},
            {"QA", 29},
            {"RO", 24},
            {"SM", 27},
            {"SA", 24},
            {"RS", 22},
            {"SK", 24},
            {"SI", 19},
            {"ES", 24},
            {"SE", 24},
            {"CH", 21},
            {"TN", 24},
            {"TR", 26},
            {"AE", 23},
            {"GB", 22},
            {"VA", 22},
            {"VG", 24}
        };

        private static readonly IDictionary<string, Regex> Regexs = new Dictionary<string, Regex>
        {
            {"AL", new Regex(@"AL[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([a-zA-Z0-9]{4}\s?){4}\s?")},
            {"AD", new Regex(@"AD[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([a-zA-Z0-9]{4}\s?){3}\s?")},
            {"AT", new Regex(@"AT[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}\s?")},
            {"AZ", new Regex(@"AZ[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){5}\s?")},
            {"BE", new Regex(@"BE[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}\s?")},
            {"BH", new Regex(@"BH[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{2})\s?")},
            {"BY", new Regex(@"BY[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){5}\s?")},
            {"BA", new Regex(@"BA[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}\s?")},
            {"BR", new Regex(@"BR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}([0-9]{3})([a-zA-Z]{1}\s?)([a-zA-Z0-9]{1})\s?")},
            {"BG", new Regex(@"BG[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){1}([0-9]{2})([a-zA-Z0-9]{2}\s?)([a-zA-Z0-9]{4}\s?){1}([a-zA-Z0-9]{2})\s?")},
            {"CR", new Regex(@"CR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{2})\s?")},
            {"HR", new Regex(@"HR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{1})\s?")},
            {"CY", new Regex(@"CY[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([a-zA-Z0-9]{4}\s?){4}\s?")},
            {"CZ", new Regex(@"CZ[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}\s?")},
            {"DK", new Regex(@"DK[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"DO", new Regex(@"DO[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){5}\s?")},
            {"TL", new Regex(@"TL[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{3})\s?")},
            {"EE", new Regex(@"EE[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}\s?")},
            {"FO", new Regex(@"FO[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"FI", new Regex(@"FI[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"FR", new Regex(@"FR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([0-9]{2})([a-zA-Z0-9]{2}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{1})([0-9]{2})\s?")},
            {"GE", new Regex(@"GE[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{2})([0-9]{2}\s?)([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"DE", new Regex(@"DE[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{2})\s?")},
            {"GI", new Regex(@"GI[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{3})\s?")},
            {"GR", new Regex(@"GR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([0-9]{3})([a-zA-Z0-9]{1}\s?)([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{3})\s?")},
            {"GL", new Regex(@"GL[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"GT", new Regex(@"GT[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([a-zA-Z0-9]{4}\s?){5}\s?")},
            {"HU", new Regex(@"HU[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){6}\s?")},
            {"IS", new Regex(@"IS[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}([0-9]{2})\s?")},
            {"IE", new Regex(@"IE[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"IL", new Regex(@"IL[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{3})\s?")},
            {"IT", new Regex(@"IT[a-zA-Z0-9]{2}\s?([a-zA-Z]{1})([0-9]{3}\s?)([0-9]{4}\s?){1}([0-9]{3})([a-zA-Z0-9]{1}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{3})\s?")},
            {"JO", new Regex(@"JO[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){5}([0-9]{2})\s?")},
            {"KZ", new Regex(@"KZ[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{1})([a-zA-Z0-9]{3}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{2})\s?")},
            {"XK", new Regex(@"XK[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([0-9]{4}\s?){2}([0-9]{2})([0-9]{2}\s?)\s?")},
            {"KW", new Regex(@"KW[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){5}([a-zA-Z0-9]{2})\s?")},
            {"LV", new Regex(@"LV[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{1})\s?")},
            {"LB", new Regex(@"LB[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([a-zA-Z0-9]{4}\s?){5}\s?")},
            {"LI", new Regex(@"LI[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([0-9]{1})([a-zA-Z0-9]{3}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{1})\s?")},
            {"LT", new Regex(@"LT[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}\s?")},
            {"LU", new Regex(@"LU[a-zA-Z0-9]{2}\s?([0-9]{3})([a-zA-Z0-9]{1}\s?)([a-zA-Z0-9]{4}\s?){3}\s?")},
            {"MK", new Regex(@"MK[a-zA-Z0-9]{2}\s?([0-9]{3})([a-zA-Z0-9]{1}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{1})([0-9]{2})\s?")},
            {"MT", new Regex(@"MT[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){1}([0-9]{1})([a-zA-Z0-9]{3}\s?)([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{3})\s?")},
            {"MR", new Regex(@"MR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}([0-9]{3})\s?")},
            {"MU", new Regex(@"MU[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){4}([0-9]{3})([a-zA-Z]{1}\s?)([a-zA-Z]{2})\s?")},
            {"MC", new Regex(@"MC[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([0-9]{2})([a-zA-Z0-9]{2}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{1})([0-9]{2})\s?")},
            {"MD", new Regex(@"MD[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{2})([a-zA-Z0-9]{2}\s?)([a-zA-Z0-9]{4}\s?){4}\s?")},
            {"ME", new Regex(@"ME[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{2})\s?")},
            {"NL", new Regex(@"NL[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){2}([0-9]{2})\s?")},
            {"NO", new Regex(@"NO[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){2}([0-9]{3})\s?")},
            {"PK", new Regex(@"PK[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){4}\s?")},
            {"PS", new Regex(@"PS[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){5}([0-9]{1})\s?")},
            {"PL", new Regex(@"PL[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){6}\s?")},
            {"PT", new Regex(@"PT[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}([0-9]{1})\s?")},
            {"QA", new Regex(@"QA[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){5}([a-zA-Z0-9]{1})\s?")},
            {"RO", new Regex(@"RO[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([a-zA-Z0-9]{4}\s?){4}\s?")},
            {"SM", new Regex(@"SM[a-zA-Z0-9]{2}\s?([a-zA-Z]{1})([0-9]{3}\s?)([0-9]{4}\s?){1}([0-9]{3})([a-zA-Z0-9]{1}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{3})\s?")},
            {"SA", new Regex(@"SA[a-zA-Z0-9]{2}\s?([0-9]{2})([a-zA-Z0-9]{2}\s?)([a-zA-Z0-9]{4}\s?){4}\s?")},
            {"RS", new Regex(@"RS[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){4}([0-9]{2})\s?")},
            {"SK", new Regex(@"SK[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}\s?")},
            {"SI", new Regex(@"SI[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){3}([0-9]{3})\s?")},
            {"ES", new Regex(@"ES[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}\s?")},
            {"SE", new Regex(@"SE[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}\s?")},
            {"CH", new Regex(@"CH[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([0-9]{1})([a-zA-Z0-9]{3}\s?)([a-zA-Z0-9]{4}\s?){2}([a-zA-Z0-9]{1})\s?")},
            {"TN", new Regex(@"TN[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){5}\s?")},
            {"TR", new Regex(@"TR[a-zA-Z0-9]{2}\s?([0-9]{4}\s?){1}([0-9]{1})([a-zA-Z0-9]{3}\s?)([a-zA-Z0-9]{4}\s?){3}([a-zA-Z0-9]{2})\s?")},
            {"AE", new Regex(@"AE[a-zA-Z0-9]{2}\s?([0-9]{3})([0-9]{1}\s?)([0-9]{4}\s?){3}([0-9]{3})\s?")},
            {"GB", new Regex(@"GB[a-zA-Z0-9]{2}\s?([a-zA-Z]{4}\s?){1}([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"VA", new Regex(@"VA[a-zA-Z0-9]{2}\s?([0-9]{3})([0-9]{1}\s?)([0-9]{4}\s?){3}([0-9]{2})\s?")},
            {"VG", new Regex(@"VG[a-zA-Z0-9]{2}\s?([a-zA-Z0-9]{4}\s?){1}([0-9]{4}\s?){4}\s?")}
        };

        public static readonly HashSet<string> baeCodes = new HashSet<string>
        {
            "INTF4001",
            "BUIN9561",
            "BUIN7680",
            "BUIN7855",
            "BUIN7006",
            "BUIN7001",
            "BUIN7004",
            "BUIN7750",
            "BUIN7470",
            "BUIN8012",
            "BUIN7005",
            "BUIN7082",
            "BUIN7068",
            "BUIN7444",
            "BUIN7220",
            "BUIN7145",
            "BUIN7604",
            "BUIN8035",
            "BUIN7003",
            "BUIN8092",
            "BUIN7661",
            "BUIN7660",
            "BUIN7250",
            "BUIN7015",
            "BUIN8090",
            "BUIN7009",
            "BUIN7650",
            "BUIN8016",
            "BUIN7007",
            "BUIN7067",
            "BUIN7069",
            "BUIN7002",
            "BUIN7903",
            "BUIN7770",
            "BUIN7008",
            "BUIN7545",
            "BUIN7014",
            "BUIN7562",
            "BPEF9290",
            "BNPA9440",
            "STSA9300",
            "STSA8300",
            "BGUS9160",
            "NASB9620",
            "BNBG9661",
            "VGAG9876",
            "VPAY4011",
            "TTBB9400",
            "INGB9145",
            "EAPS4008",
            "ESPY4004",
            "IORT9120",
            "IORT8049",
            "IORT8132",
            "IORT7378",
            "IORT8165",
            "IORT7377",
            "IORT8164",
            "IORT8043",
            "IORT8166",
            "IORT7373",
            "IORT8131",
            "IORT8095",
            "IORT8167",
            "IORT8088",
            "IORT8130",
            "IORT8023",
            "IORT8046",
            "IORT8103",
            "IORT8266",
            "IORT6091",
            "IORT8111",
            "IORT7380",
            "IORT8139",
            "IORT7375",
            "IORT8163",
            "IORT8116",
            "IORT7379",
            "IORT8137",
            "IORT8127",
            "IORT8047",
            "IORT8029",
            "IORT8019",
            "IORT7371",
            "IORT8045",
            "IORT8094",
            "IORT8102",
            "IORT8168",
            "IORT8038",
            "IORT8128",
            "IORT7376",
            "IORT8138",
            "IORT8031",
            "IORT8048",
            "IORT8129",
            "IORT8006",
            "IABG9470",
            "IABG7458",
            "IABG7475",
            "IABG7479",
            "IABG7431",
            "IABG7496",
            "IABG7494",
            "IABG7498",
            "IABG8123",
            "IABG7495",
            "IABG7497",
            "IABG7490",
            "IABG7456",
            "IABG8074",
            "IABG8118",
            "IABG7474",
            "IABG7096",
            "IABG7478",
            "IABG7473",
            "IABG7095",
            "IABG7460",
            "IABG7471",
            "IABG7648",
            "IABG7459",
            "IABG7488",
            "IABG7491",
            "IABG7432",
            "IABG7433",
            "IABG7093",
            "IABG7097",
            "IABG8098",
            "IABG7477",
            "IABG7094",
            "IABG8429",
            "IABG8428",
            "MYFN4012",
            "UBBS9200",
            "UBBS7207",
            "UBBS7242",
            "UBBS7243",
            "UBBS7222",
            "UBBS7504",
            "UBBS8442",
            "UBBS7823",
            "UBBS7342",
            "UBBS7122",
            "UBBS7070",
            "UBBS7822",
            "UBBS7267",
            "UBBS8505",
            "UBBS8121",
            "UBBS8141",
            "UBBS7284",
            "UBBS7343",
            "UBBS7369",
            "UBBS7142",
            "UBBS8562",
            "UBBS8427",
            "UBBS7125",
            "UBBS8423",
            "UBBS7426",
            "UBBS8161",
            "UBBS7270",
            "UBBS7282",
            "UBBS8201",
            "UBBS7224",
            "UBBS7523",
            "UBBS8888",
            "UBBS8221",
            "UBBS8383",
            "UBBS7126",
            "UBBS8241",
            "UBBS8261",
            "UBBS8246",
            "UBBS8281",
            "UBBS7821",
            "UBBS8503",
            "UBBS7543",
            "UBBS7826",
            "UBBS7370",
            "UBBS8341",
            "UBBS8448",
            "UBBS7827",
            "UBBS7525",
            "UBBS7566",
            "UBBS7269",
            "UBBS8381",
            "UBBS7825",
            "UBBS7824",
            "UBBS7268",
            "UBBS7828",
            "UBBS8501",
            "UBBS7820",
            "UBBS8521",
            "UBBS8002",
            "UBBS7428",
            "UBBS8541",
            "UBBS7368",
            "SOMB9130",
            "PATC4002",
            "PRCB9230",
            "FINV9150",
            "RZBB9155",
            "CITI9250",
            "TBIB9310",
            "TCZB9350",
            "TEXI9545",
            "CREX9260",
            "TRUD4005",
            "TRIV4003",
            "DEMI9240",
            "UNCR9660",
            "UNCR8115",
            "UNCR8135",
            "UNCR7630",
            "UNCR7527",
            "UNCR7000",
            "CECB9790",
            "BPBI9920",
            "BPBI8898",
            "BPBI7922",
            "BPBI7937",
            "BPBI7945",
            "BPBI7931",
            "BPBI7948",
            "BPBI7944",
            "BPBI7929",
            "BPBI7938",
            "BPBI7932",
            "BPBI7925",
            "BPBI7928",
            "BPBI7933",
            "BPBI7930",
            "BPBI7947",
            "BPBI7923",
            "BPBI7924",
            "BPBI7949",
            "BPBI7921",
            "BPBI7941",
            "BPBI7115",
            "BPBI7936",
            "BPBI7927",
            "BPBI7940",
            "BPBI7942",
            "BPBI7939",
            "BPBI7935",
            "BPBI7943",
            "BPBI7926",
            "BPBI7946",
            "BPBI7934",
            "BPBI8170"
        };
    }
}
