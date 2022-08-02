using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Subcontractor;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class SubcontractorController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public SubcontractorController(
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
                    "subcontractor",
                    "companyUin",
                    "page",
            })]
        public virtual ActionResult Index(
                string subcontractor = "",
                string companyUin = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            SubcontractorSearchVM vm = new SubcontractorSearchVM()
            {
                Subcontractor = subcontractor,
                CompanyUin = companyUin,
                ShowRes = showRes,
            };

            if (showRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var subcontractors = this.umisRepository.GetContractSubcontractors(ContractSubcontractType.Subcontractor, subcontractor, companyUin, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractSubcontractorVO>(subcontractors.Results, innerPage, Configuration.MaxItemsPerPage, subcontractors.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(SubcontractorSearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            SubcontractorSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            string companyUin = string.Empty;

            if (!string.IsNullOrEmpty(this.Request.QueryString["subcontractor"]))
            {
                name = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["subcontractor"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyUin"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyUin"]);
            }

            var subcontractors = this.umisRepository.GetContractSubcontractors(ContractSubcontractType.Subcontractor, name, companyUin, 0, null);

            var template = new ExportTemplate("subcontractors");
            template.Sheet.Name = "subcontractors";
            var table = new ExportTable(Texts.Global_Subcontractors);
            var headerRow = new ExportRow();

            for (int i = 0; i < 2; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Subcontractor_Index_Name;
            headerRow.Cells[1].Value = Texts.Subcontractor_Index_ContractsCount;

            table.Rows.Add(headerRow);

            foreach (var subcontractor in subcontractors.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(subcontractor.TransFullName.ToExportCell());
                row.Cells.Add(subcontractor.ContractsCount.ToExportCell());

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