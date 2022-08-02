using System;
using System.Collections.Generic;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Web.Models.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammesSearchVM
    {
        public string ProgrammeId { get; set; }

        public string Year { get; set; }

        public string IawpType { get; set; }

        public bool ShowRes { get; set; }

        public IList<IndicativeAnnualWorkingProgrammeVO> SearchResults { get; set; }

        public static void EncryptProperties(IndicativeAnnualWorkingProgrammesSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.Year = ConfigurationBasedStringEncrypter.Encrypt(vm.Year);
            vm.IawpType = ConfigurationBasedStringEncrypter.Encrypt(vm.IawpType);
        }
    }
}
