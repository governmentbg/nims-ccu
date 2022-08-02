using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Indicators
{
    public class IndicatorsSearchVM
    {
        /// <summary>
        /// Gets or sets operational program name.
        /// </summary>
        public string ProgrammeId { get; set; }

        public bool ShowRes { get; set; }

        public IEnumerable<SelectListItem> Programs { get; set; }

        public IPagedList<StatisticIndicatorVO> SearchResults { get; set; }

        public static void EncryptProperties(IndicatorsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
        }
    }
}