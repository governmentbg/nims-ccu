using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Validation
{
    public static class BulstatValidator
    {
        private static int[] FIRST_SUM_9DIGIT_WEIGHTS = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static int[] SECOND_SUM_9DIGIT_WEIGHTS = { 3, 4, 5, 6, 7, 8, 9, 10 };
        private static int[] FIRST_SUM_13DIGIT_WEIGHTS = { 2, 7, 3, 5 };
        private static int[] SECOND_SUM_13DIGIT_WEIGHTS = { 4, 9, 5, 7 };

        private const int INVALID_THIRTHEENTH_VALUE = int.MinValue;

        public static bool ValidateBulstat(string idn)
        {
            if (idn.Length == 9)
                return Validate9DigitBulstat(idn);
            else if (idn.Length == 13)
                return Validate13DigitBulstat(idn);

            return false;
        }

        public static bool Validate9DigitBulstat(string idn)
        {
            if (string.IsNullOrWhiteSpace(idn) || idn.Length != 9)
            {
                return false;
            }

            List<int> digits = new List<int>();

            if (!GetDigits(idn, digits))
                return false;

            int ninthDigit = CalculateNinthDigitInBulstat(digits);

            if (ninthDigit != digits[8])
            {
                return false;
            }

            return true;
        }

        public static bool Validate13DigitBulstat(string idn)
        {
            if (string.IsNullOrWhiteSpace(idn) || idn.Length != 13)
            {
                return false;
            }

            List<int> digits = new List<int>();

            if (!GetDigits(idn, digits))
                return false;

            int thirteenthDigit = CalculateThirteenthDigitInBulstat(digits);

            if (thirteenthDigit != digits[12])
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEGN(string egn)
        {
            int[] digits = new int[10];
            int[] coeffs = new int[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int[] days = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (egn.Length != 10)
            {
                return false;
            }

            for (var i = 0; i < egn.Length; i++)
            {
                int digit;
                if (!int.TryParse(egn[i].ToString(), out digit))
                {
                    return false;
                }
                digits[i] = digit;
            }

            int dd = digits[4] * 10 + digits[5];
            int mm = digits[2] * 10 + digits[3];
            int yy = digits[0] * 10 + digits[1];
            int yyyy;

            if (mm >= 1 && mm <= 12) { yyyy = 1900 + yy; }
            else if (mm >= 21 && mm <= 32) { mm -= 20; yyyy = 1800 + yy; }
            else if (mm >= 41 && mm <= 52) { mm -= 40; yyyy = 2000 + yy; }
            else
            {
                return false;
            }

            int leapYear = 0;
            if (yyyy % 400 == 0) { leapYear = 1; }
            else if (yyyy % 100 == 0) { leapYear = 0; }
            else if (yyyy % 4 == 0) { leapYear = 1; }

            days[1] += leapYear == 1 ? 1 : 0;

            if (!(dd >= 1 && dd <= days[mm - 1]))
            {
                return false;
            }

            int checksum = 0;

            for (int j = 0; j < coeffs.Length; j++) { checksum += digits[j] * coeffs[j]; }
            checksum %= 11;
            if (10 == checksum) { checksum = 0; }

            if (digits[9] != checksum)
            {
                return false;
            }

            return true;
        }

        private static bool GetDigits(string idn, List<int> digits)
        {
            for (var i = 0; i < idn.Length; i++)
            {
                int digit;
                if (!int.TryParse(idn[i].ToString(), out digit))
                {
                    return false;
                }
                digits.Add(digit);
            }

            return true;
        }

        private static int CalculateNinthDigitInBulstat(List<int> digits)
        {
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum = sum + (digits[i] * FIRST_SUM_9DIGIT_WEIGHTS[i]);
            }
            int remainder = sum % 11;
            if (remainder != 10)
            {
                return remainder;
            }
            // remainder= 10
            int secondSum = 0;
            for (int i = 0; i < 8; i++)
            {
                secondSum = secondSum + (digits[i] * SECOND_SUM_9DIGIT_WEIGHTS[i]);
            }
            int secondRem = secondSum % 11;
            if (secondRem != 10)
            {
                return secondRem;
            }
            // secondRemainder= 10
            return 0;
        }

        private static int CalculateThirteenthDigitInBulstat(List<int> digits)
        {
            int ninthDigit = CalculateNinthDigitInBulstat(digits);
            if (ninthDigit != digits[8])
            {
                return INVALID_THIRTHEENTH_VALUE;
            }
            // 9thDigit is a correct checkSum. Continue with 13thDigit
            int sum = 0;
            for (int i = 8, j = 0; j < 4; i++, j++)
            {
                sum = sum + (digits[i] * FIRST_SUM_13DIGIT_WEIGHTS[j]);
            }
            int remainder = sum % 11;
            if (remainder != 10)
            {
                return remainder;
            }
            // remainder= 10
            int secondSum = 0;
            for (int i = 8, j = 0; j < 4; i++, j++)
            {
                secondSum = secondSum + (digits[i] * SECOND_SUM_13DIGIT_WEIGHTS[j]);
            }
            int secondRem = secondSum % 11;
            if (secondRem != 10)
            {
                return secondRem;
            }
            // secondRemainder= 10
            return 0;
        }
    }
}
