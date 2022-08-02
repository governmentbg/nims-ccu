using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Web.Models.Admin
{
    public class OPStatisticsSearchVM
    {
        /// <summary>
        /// Gets or sets operational program name.
        /// </summary>
        public string ProgrammeId { get; set; }

        public bool ShowRes { get; set; }

        public IEnumerable<SelectListItem> Programs { get; set; }

        public OPStatisticsVO SearchResult { get; set; }

        public static void EncryptProperties(OPStatisticsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
        }
    }
}