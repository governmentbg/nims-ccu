using System;
using System.Security.Cryptography;
using System.Text;

namespace Eumis.Public.Common.Helpers
{
    public class PasswordGenerator
    {
        private const int DefaultMinimum = 8;
        private const int DefaultMaximum = 9;
        private const int UBoundDigit = 61;

        private RNGCryptoServiceProvider rng;
        private int minSize;
        private int maxSize;
        private bool hasRepeating;
        private bool hasConsecutive;
        private bool hasSymbols;
        private string exclusionSet;
        private char[] pwdCharArray = "0123456789".ToCharArray();

        // private char[] pwdCharArray = ("abcdefghijklmnopqrstuvwxyz" +
        //                                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        //                                "0123456789").ToCharArray();
        public PasswordGenerator()
        {
            this.Minimum = DefaultMinimum;
            this.Maximum = DefaultMaximum;
            this.ConsecutiveCharacters = false;
            this.RepeatCharacters = true;
            this.ExcludeSymbols = false;
            this.Exclusions = null;

            this.rng = new RNGCryptoServiceProvider();
        }

        public string Exclusions
        {
            get { return this.exclusionSet; }
            set { this.exclusionSet = value; }
        }

        public int Minimum
        {
            get
            {
                return this.minSize;
            }

            set
            {
                this.minSize = value;
                if (this.minSize < PasswordGenerator.DefaultMinimum)
                {
                    this.minSize = PasswordGenerator.DefaultMinimum;
                }
            }
        }

        public int Maximum
        {
            get
            {
                return this.maxSize;
            }

            set
            {
                this.maxSize = value;
                if (this.minSize >= this.maxSize)
                {
                    this.maxSize = PasswordGenerator.DefaultMaximum;
                }
            }
        }

        public bool ExcludeSymbols
        {
            get { return this.hasSymbols; }
            set { this.hasSymbols = value; }
        }

        public bool RepeatCharacters
        {
            get { return this.hasRepeating; }
            set { this.hasRepeating = value; }
        }

        public bool ConsecutiveCharacters
        {
            get { return this.hasConsecutive; }
            set { this.hasConsecutive = value; }
        }

        protected int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            // Assumes lBound >= 0 && lBound < uBound

            // returns an int >= lBound and < uBound
            uint urndnum;
            byte[] rndnum = new byte[4];
            if (lBound == uBound - 1)
            {
                // test for degenerate case where only lBound can be returned
                return lBound;
            }

            uint xcludeRndBase = uint.MaxValue -
                (uint.MaxValue % (uint)(uBound - lBound));

            do
            {
                this.rng.GetBytes(rndnum);
                urndnum = System.BitConverter.ToUInt32(rndnum, 0);
            }
            while (urndnum >= xcludeRndBase);

            return (int)(urndnum % (uBound - lBound)) + lBound;
        }

        protected char GetRandomCharacter()
        {
            int upperBound = this.pwdCharArray.GetUpperBound(0);

            if (this.ExcludeSymbols == true)
            {
                upperBound = PasswordGenerator.UBoundDigit;
            }

            int randomCharPosition = this.GetCryptographicRandomNumber(
                this.pwdCharArray.GetLowerBound(0), upperBound);

            char randomChar = this.pwdCharArray[randomCharPosition];

            return randomChar;
        }

        public string Generate()
        {
            // Pick random length between minimum and maximum
            int pwdLength = this.GetCryptographicRandomNumber(
                this.Minimum,
                this.Maximum);

            StringBuilder pwdBuffer = new StringBuilder();
            pwdBuffer.Capacity = this.Maximum;

            // Generate random characters
            char lastCharacter, nextCharacter;

            // Initial dummy character flag
            lastCharacter = nextCharacter = '\n';

            for (int i = 0; i < pwdLength; i++)
            {
                nextCharacter = this.GetRandomCharacter();

                if (this.ConsecutiveCharacters == false)
                {
                    while (lastCharacter == nextCharacter)
                    {
                        nextCharacter = this.GetRandomCharacter();
                    }
                }

                if (this.RepeatCharacters == false)
                {
                    string temp = pwdBuffer.ToString();
                    int duplicateIndex = temp.IndexOf(nextCharacter);
                    while (duplicateIndex != -1)
                    {
                        nextCharacter = this.GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                if (this.Exclusions != null)
                {
                    while (this.Exclusions.IndexOf(nextCharacter) != -1)
                    {
                        nextCharacter = this.GetRandomCharacter();
                    }
                }

                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }

            if (pwdBuffer != null)
            {
                return pwdBuffer.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
