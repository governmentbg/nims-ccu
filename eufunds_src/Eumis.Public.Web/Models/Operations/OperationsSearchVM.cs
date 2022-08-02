using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Operations
{
    public class OperationsSearchVM
    {
        /// <summary>
        /// Gets or sets operational program name.
        /// </summary>
        public string ProgrammeId { get; set; }

        /// <summary>
        /// Gets or sets start date from.
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// Gets or sets end date to.
        /// </summary>
        public string DateTo { get; set; }

        public bool ShowRes { get; set; }

        public IEnumerable<SelectListItem> Programs { get; set; }

        public IPagedList<StatisticContractVO> SearchResults { get; set; }

        public static void EncryptProperties(OperationsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.DateFrom = ConfigurationBasedStringEncrypter.Encrypt(vm.DateFrom);
            vm.DateTo = ConfigurationBasedStringEncrypter.Encrypt(vm.DateTo);
        }
    }
}