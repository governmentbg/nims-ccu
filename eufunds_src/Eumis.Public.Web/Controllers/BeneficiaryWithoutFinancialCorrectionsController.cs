using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.BeneficiaryWithoutFinancialCorrections;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class BeneficiaryWithoutFinancialCorrectionsController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public BeneficiaryWithoutFinancialCorrectionsController(
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
                    "seat",
                    "page",
            })]
        public virtual ActionResult Index(
                string beneficiary = "",
                string companyType = "",
                string companyLegalType = "",
                string companyUin = "",
                string seat = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            BeneficiaryWithoutFinancialCorrectionsSearchVM vm = new BeneficiaryWithoutFinancialCorrectionsSearchVM()
            {
                Beneficiary = beneficiary,
                CompanyType = companyType,
                CompanyLegalType = companyLegalType,
                CompanyUin = companyUin,
                Seat = seat,
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

                var beneficiaries = this.umisRepository.GetContractBeneficiariesWithoutFinancialCorrections(beneficiary, companyTypeId, companyLegalTypeId, companyUin, seat, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractBeneficiaryWithoutFinancialCorrectionsVO>(beneficiaries.Results, innerPage, Configuration.MaxItemsPerPage, beneficiaries.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(BeneficiaryWithoutFinancialCorrectionsSearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            BeneficiaryWithoutFinancialCorrectionsSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;
            string companyUin = string.Empty;
            string seat = string.Empty;

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

            if (!string.IsNullOrEmpty(this.Request.QueryString["seat"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["seat"]);
            }

            var beneficiaries = this.umisRepository.GetContractBeneficiariesWithoutFinancialCorrections(name, companyTypeId, companyLegalTypeId, companyUin, seat, 0, null);

            var template = new ExportTemplate("beneficiariesWithoutFC");
            template.Sheet.Name = "beneficiariesWithoutFC";

            var table = new ExportTable(Texts.BeneficiaryWithoutFC_Index_ShortName);
            var headerRow = new ExportRow();

            for (int i = 0; i < 6; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.BeneficiaryWithoutFC_Index_Beneficiary;
            headerRow.Cells[1].Value = Texts.BeneficiaryWithoutFC_Index_CompanyUin;
            headerRow.Cells[2].Value = Texts.BeneficiaryWithoutFC_Index_CompanyType;
            headerRow.Cells[3].Value = Texts.BeneficiaryWithoutFC_Index_CompanyLegalType;
            headerRow.Cells[4].Value = Texts.BeneficiaryWithoutFC_Index_Seat;
            headerRow.Cells[5].Value = Texts.BeneficiaryWithoutFC_Index_ProjectsCount;

            table.Rows.Add(headerRow);

            foreach (var beneficiary in beneficiaries.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(beneficiary.TransFullName.ToExportCell());
                row.Cells.Add(beneficiary.UinAnonymized.ToExportCell());
                row.Cells.Add(beneficiary.CompanyTypeName.ToExportCell());
                row.Cells.Add(beneficiary.CompanyLegalTypeName.ToExportCell());
                row.Cells.Add(beneficiary.Seat.ToExportCell());
                row.Cells.Add(beneficiary.ContractsCount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 800 },
                { 2, 200 },
                { 3, 200 },
                { 4, 200 },
                { 5, 500 },
                { 6, 300 },
            };

            return template;
        }
    }
}
