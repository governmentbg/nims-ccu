using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Contractor;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ContractorController : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;

        public ContractorController(
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
                    "contractor",
                    "companyUin",
                    "page",
            })]
        public virtual ActionResult Index(
                string contractor = "",
                string companyUin = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ContractorSearchVM vm = new ContractorSearchVM()
            {
                Contractor = contractor,
                CompanyUin = companyUin,
                ShowRes = showRes,
            };

            if (showRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contractors = this.umisRepository.GetContractContractors(contractor, companyUin, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractContractorVO>(contractors.Results, innerPage, Configuration.MaxItemsPerPage, contractors.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ContractorSearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            ContractorSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            string companyUin = string.Empty;

            if (!string.IsNullOrEmpty(this.Request.QueryString["contractor"]))
            {
                name = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["contractor"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyUin"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyUin"]);
            }

            var contractors = this.umisRepository.GetContractContractors(name, companyUin, 0, null);

            var template = new ExportTemplate("contractors");
            template.Sheet.Name = "contractors";

            var table = new ExportTable(Texts.Global_Contractors);
            var headerRow = new ExportRow();

            for (int i = 0; i < 2; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Contractor_Index_Name;
            headerRow.Cells[1].Value = Texts.Contractor_Index_ContractsCount;

            table.Rows.Add(headerRow);

            foreach (var contractor in contractors.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(contractor.TransFullName.ToExportCell());
                row.Cells.Add(contractor.ContractsCount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 800 },
                { 2, 250 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);

            return template;
        }
    }
}