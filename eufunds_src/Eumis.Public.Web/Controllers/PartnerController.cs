using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Partner;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class PartnerController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public PartnerController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "partner",
                    "companyType",
                    "companyLegalType",
                    "companyUin",
                    "page",
            })]
        public virtual ActionResult Index(
                string partner = "",
                string companyType = "",
                string companyLegalType = "",
                string companyUin = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            PartnerSearchVM vm = new PartnerSearchVM()
            {
                Partner = partner,
                CompanyType = companyType,
                CompanyLegalType = companyLegalType,
                CompanyUin = companyUin,
                ShowRes = showRes,
            };

            int temp;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;
            if (showRes)
            {
                if (int.TryParse(companyType, out temp))
                {
                    companyTypeId = temp;
                }

                if (int.TryParse(companyLegalType, out temp))
                {
                    companyLegalTypeId = temp;
                }

                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var partners = this.umisRepository.GetContractPartners(partner, companyTypeId, companyLegalTypeId, companyUin, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractPartnerVO>(partners.Results, innerPage, Configuration.MaxItemsPerPage, partners.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(PartnerSearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            PartnerSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;
            string companyUin = string.Empty;

            if (!string.IsNullOrEmpty(this.Request.QueryString["partner"]))
            {
                name = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["partner"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyType"]))
            {
                companyTypeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyType"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyLegalType"]))
            {
                companyLegalTypeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyLegalType"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyUin"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyUin"]);
            }

            var partners = this.umisRepository.GetContractPartners(name, companyTypeId, companyLegalTypeId, companyUin, 0, null);

            var template = new ExportTemplate("partners");
            template.Sheet.Name = "partners";

            var table = new ExportTable(Texts.Global_Partners);
            var headerRow = new ExportRow();

            for (int i = 0; i < 2; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Partner_Index_Name;
            headerRow.Cells[1].Value = Texts.Partner_Index_ProjectsCount;

            table.Rows.Add(headerRow);

            foreach (var partner in partners.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(partner.TransFullName.ToExportCell());
                row.Cells.Add(partner.ContractsCount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 800 },
                { 2, 100 },
            };

            return template;
        }
    }
}