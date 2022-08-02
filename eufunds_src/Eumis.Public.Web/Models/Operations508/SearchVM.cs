using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.Models.Operations508
{
    public class SearchVM
    {
        public string ProgrammeId { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public bool ShowRes { get; set; }

        public IEnumerable<SelectListItem> Programs { get; set; }

        public IPagedList<Operations508ReportVO> SearchResults { get; set; }

        public static void EncryptProperties(SearchVM vm)
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
