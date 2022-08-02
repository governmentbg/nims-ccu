using System;
using System.Collections.Generic;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using PagedList;

namespace Eumis.Public.Web.Models.ActuallyPaidAmounts
{
    public class ActuallyPaidAmountsSearchVM
    {
        public string ProgrammeId { get; set; }

        public string ProgrammePriorityId { get; set; }

        public string ProcedureId { get; set; }

        public string GroupingLevel { get; set; }

        public string DateTo { get; set; }

        public bool ShowRes { get; set; }

        public IPagedList<ActuallyPaidAmountsVO> SearchResults { get; set; }

        public static void EncryptProperties(ActuallyPaidAmountsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.ProgrammePriorityId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammePriorityId);
            vm.ProcedureId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProcedureId);
            vm.GroupingLevel = ConfigurationBasedStringEncrypter.Encrypt(vm.GroupingLevel);
            vm.DateTo = ConfigurationBasedStringEncrypter.Encrypt(vm.DateTo);
        }
    }
}