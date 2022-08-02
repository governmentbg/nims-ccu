using System;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Web.Models.OperationalProgram
{
    public class OpSearchVM
    {
        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string HeaderDate
        {
            get
            {
                return string.IsNullOrEmpty(this.DateTo)
                    ? Helper.DateToBgFormatWithoutAbbr(DateTime.Now)
                    : this.DateTo;
            }
        }

        public OperationalProgramsVO OperationalPrograms { get; set; }

        public static void EncryptProperties(OpSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.DateFrom = ConfigurationBasedStringEncrypter.Encrypt(vm.DateFrom);
            vm.DateTo = ConfigurationBasedStringEncrypter.Encrypt(vm.DateTo);
        }
    }
}