using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Beneficiary;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class BeneficiaryController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public BeneficiaryController(
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
                    "beneficiary",
                    "companyType",
                    "companyLegalType",
                    "companyUin",
                    "page",
            })]
        public virtual ActionResult Index(
                string beneficiary = "",
                string companyType = "",
                string companyLegalType = "",
                string companyUin = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            BeneficiarySearchVM vm = new BeneficiarySearchVM()
            {
                Beneficiary = beneficiary,
                CompanyType = companyType,
                CompanyLegalType = companyLegalType,
                CompanyUin = companyUin,
                ShowRes = showRes,
            };

            int temp;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;

            if (int.TryParse(companyType, out temp))
            {
                companyTypeId = temp;
            }

            if (int.TryParse(companyLegalType, out temp))
            {
                companyLegalTypeId = temp;
            }

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var beneficiaries = this.umisRepository.GetContractBeneficiaries(beneficiary, companyTypeId, companyLegalTypeId, companyUin, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractBeneficiaryVO>(beneficiaries.Results, innerPage, Configuration.MaxItemsPerPage, beneficiaries.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(BeneficiarySearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            BeneficiarySearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;
            string companyUin = string.Empty;

            if (!string.IsNullOrEmpty(this.Request.QueryString["beneficiary"]))
            {
                name = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["beneficiary"]);
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

            var beneficiaries = this.umisRepository.GetContractBeneficiaries(name, companyTypeId, companyLegalTypeId, companyUin, 0, null);

            var template = new ExportTemplate("beneficiaries");
            template.Sheet.Name = "beneficiaries";

            var table = new ExportTable(Texts.Global_Beneficiaries);
            var headerRow = new ExportRow();

            for (int i = 0; i < 4; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Beneficiary_Index_Name;
            headerRow.Cells[1].Value = Texts.Beneficiary_Index_ProjectsCount;
            headerRow.Cells[2].Value = Texts.Global_Contracted;
            headerRow.Cells[3].Value = Texts.Global_Payed;

            table.Rows.Add(headerRow);

            foreach (var beneficiary in beneficiaries.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(beneficiary.TransFullName.ToExportCell());
                row.Cells.Add(beneficiary.ContractsCount.ToExportCell());
                row.Cells.Add(beneficiary.ContractedAmount.ToExportCell());
                row.Cells.Add(beneficiary.PaidAmount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 800 },
                { 2, 100 },
                { 3, 200 },
                { 4, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}