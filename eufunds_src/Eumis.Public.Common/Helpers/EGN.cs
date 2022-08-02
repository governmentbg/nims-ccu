using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Common.Helpers
{
    public class EGN
    {
        private int[] coef = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
        private DateTime birthDate;
        private Gender sex;
        private string eGNError;

        public EGN(string egn)
        {
            if (string.IsNullOrWhiteSpace(egn) || egn.Length != 10)
            {
                this.EGNError = "Invalid EGN length";
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                if (!char.IsDigit(egn, i))
                {
                    this.EGNError = "EGN must contain digits only";
                    return;
                }
            }

            int yy = int.Parse(egn.Substring(0, 2));
            int mm = int.Parse(egn.Substring(2, 2));
            int dd = int.Parse(egn.Substring(4, 2));

            if (mm >= 21 && mm <= 32)
            {
                mm -= 20;
                yy += 1800;
            }
            else if (mm >= 41 && mm <= 52)
            {
                mm -= 40;
                yy += 2000;
            }
            else
            {
                yy += 1900;
            }

            try
            {
                this.birthDate = new DateTime(yy, mm, dd);
            }
            catch
            {
                this.EGNError = "Invalid date in EGN";
                return;
            }

            if (Convert.ToInt32(egn.Substring(8, 1)) % 2 == 0)
            {
                this.sex = Gender.Male;
            }
            else
            {
                this.sex = Gender.Female;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(egn.Substring(i, 1)) * this.coef[i];
            }

            int rem = sum % 11;
            if (rem == 10)
            {
                rem = 0;
            }

            if (rem != int.Parse(egn.Substring(9, 1)))
            {
                this.EGNError = "Invalid EGN checksum";
                return;
            }
        }

        public enum Gender
        {
            /// <summary>
            /// Стойност по подразбиране: пола на лицето не е инициализиран
            /// </summary>
            None,

            /// <summary>
            /// Лицето е мъж
            /// </summary>
            Male,

            /// <summary>
            /// Лицето е жена
            /// </summary>
            Female,
        }

        /// <summary>
        /// Gets на раждане на лицето със съответното ЕГН.
        /// </summary>
        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }
        }

        /// <summary>
        /// Gets полът на лицето със съответното ЕГН.
        /// </summary>
        public Gender Sex
        {
            get
            {
                return this.sex;
            }
        }

        public string EGNError { get => this.eGNError; set => this.eGNError = value; }

        public static DateTime DateFromEGNInternal(string egn)
        {
            EGN e = new EGN(egn);
            if (!e.IsValid())
            {
                return DateTime.MinValue;
            }
            else
            {
                return e.BirthDate;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this.EGNError);
        }
    }
}
