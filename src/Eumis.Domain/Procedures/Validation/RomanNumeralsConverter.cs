using System;

namespace Eumis.Domain.Procedures.Validation
{
    internal class RomanNumeralsConverter
    {
        public static int ConvertRomanToArabic(string romanNumber)
        {
            romanNumber = romanNumber.ToUpper();
            int arabicNumber = 0;
            int oldValue = 1000;

            // Loop through the Roman characters.
            for (int i = 0; i < romanNumber.Length; i++)
            {
                // See what the next character is worth.
                char ch = romanNumber[i];
                int newValue = 0;
                switch (ch)
                {
                    case 'I':
                        newValue = 1;
                        break;
                    case 'V':
                        newValue = 5;
                        break;
                    case 'X':
                        newValue = 10;
                        break;
                    case 'L':
                        newValue = 50;
                        break;
                    case 'C':
                        newValue = 100;
                        break;
                    case 'D':
                        newValue = 500;
                        break;
                    case 'M':
                        newValue = 1000;
                        break;
                }

                // See if this value is bigger than the previous one.
                if (newValue > oldValue)
                {
                    // new_value > old_value.
                    // Add this value to the result
                    // and subtract the previous one twice.
                    arabicNumber += newValue - (2 * oldValue);
                }
                else
                {
                    // new_value <= old_value. Add it to the result.
                    arabicNumber += newValue;
                }

                oldValue = newValue;
            }

            if ((arabicNumber < 1) || (arabicNumber > 3999))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(romanNumber),
                    "Roman number must be between I (1) and MMMCMXCIX (3999)");
            }

            return arabicNumber;
        }

        public static string ConvertArabicToRoman(int arabicNumber)
        {
            if ((arabicNumber < 1) || (arabicNumber > 3999))
            {
                throw new ArgumentOutOfRangeException(
                    "arabic_number",
                    "Arabic number must be between 1 and 3999");
            }

            // Thousands.
            string result = string.Empty;
            int digit = arabicNumber / 1000;
            arabicNumber = arabicNumber % 1000;
            result += new string('M', digit);

            // Hundreds.
            digit = arabicNumber / 100;
            arabicNumber = arabicNumber % 100;
            result += RomanDigits(digit, 'M', 'D', 'C');

            // Tens.
            digit = arabicNumber / 10;
            arabicNumber = arabicNumber % 10;
            result += RomanDigits(digit, 'C', 'L', 'X');

            // Ones.
            digit = arabicNumber;
            result += RomanDigits(digit, 'X', 'V', 'I');

            return result;
        }

        private static string RomanDigits(int arabicDigit, char tenChar, char fiveChar, char oneChar)
        {
            if (arabicDigit <= 3)
            {
                return new string(oneChar, arabicDigit);
            }

            if (arabicDigit == 4)
            {
                return oneChar.ToString() + fiveChar.ToString();
            }

            if (arabicDigit == 5)
            {
                return fiveChar.ToString();
            }

            if (arabicDigit <= 8)
            {
                return fiveChar + new string(oneChar, arabicDigit - 5);
            }

            return oneChar.ToString() + tenChar.ToString();
        }
    }
}
