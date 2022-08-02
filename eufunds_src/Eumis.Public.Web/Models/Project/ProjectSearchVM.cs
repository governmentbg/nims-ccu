using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Project
{
    public class ProjectSearchVM
    {
        /// <summary>
        /// Gets or sets Start year from.
        /// </summary>
        public string StFrom { get; set; }

        /// <summary>
        /// Gets or sets Start year to.
        /// </summary>
        public string StTo { get; set; }

        /// <summary>
        /// Gets or sets End year from.
        /// </summary>
        public string EndFrom { get; set; }

        /// <summary>
        /// Gets or sets end year to.
        /// </summary>
        public string EndTo { get; set; }

        /// <summary>
        /// Gets or sets programme.
        /// </summary>
        public string Prog { get; set; }

        /// <summary>
        /// Gets or sets priority.
        /// </summary>
        public string Prior { get; set; }

        /// <summary>
        /// Gets or sets procedure.
        /// </summary>
        public string Proc { get; set; }

        /// <summary>
        /// Gets or sets beneficiary.
        /// </summary>
        public string Ben { get; set; }

        /// <summary>
        /// Gets or sets partner.
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// Gets or sets contractor.
        /// </summary>
        public string Con { get; set; }

        /// <summary>
        /// Gets or sets region.
        /// </summary>
        public string Reg { get; set; }

        /// <summary>
        /// Gets or sets Uin.
        /// </summary>
        public string Uin { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show results.
        /// </summary>
        public bool ShowRes { get; set; }

        public bool IsProgrammeSelected { get; set; }

        public bool IsRegionSelected { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }

        public ProjectsSummarizedDataVO SummarizedSearchResult { get; set; }

        public IPagedList<ContractVO> SearchResults { get; set; }

        public static void EncryptProperties(ProjectSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.StFrom = ConfigurationBasedStringEncrypter.Encrypt(vm.StFrom);
            vm.StTo = ConfigurationBasedStringEncrypter.Encrypt(vm.StTo);
            vm.EndFrom = ConfigurationBasedStringEncrypter.Encrypt(vm.EndFrom);
            vm.EndTo = ConfigurationBasedStringEncrypter.Encrypt(vm.EndTo);

            vm.Prog = ConfigurationBasedStringEncrypter.Encrypt(vm.Prog);
            vm.Prior = ConfigurationBasedStringEncrypter.Encrypt(vm.Prior);
            vm.Proc = ConfigurationBasedStringEncrypter.Encrypt(vm.Proc);
            vm.Ben = ConfigurationBasedStringEncrypter.Encrypt(vm.Ben);
            vm.Part = ConfigurationBasedStringEncrypter.Encrypt(vm.Part);
            vm.Con = ConfigurationBasedStringEncrypter.Encrypt(vm.Con);
            vm.Uin = ConfigurationBasedStringEncrypter.Encrypt(vm.Uin);
            vm.Name = ConfigurationBasedStringEncrypter.Encrypt(vm.Name);
        }
    }
}
