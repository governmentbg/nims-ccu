using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Member;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class MemberController : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;

        public MemberController(
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
                    "member",
                    "companyUin",
                    "page",
            })]
        public virtual ActionResult Index(
                string member = "",
                string companyUin = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            MemberSearchVM vm = new MemberSearchVM()
            {
                Member = member,
                CompanyUin = companyUin,
                ShowRes = showRes,
            };

            if (showRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var members = this.umisRepository.GetContractSubcontractors(ContractSubcontractType.Member, member, companyUin, offset, Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractSubcontractorVO>(members.Results, innerPage, Configuration.MaxItemsPerPage, members.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(MemberSearchVM vm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            MemberSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            string name = string.Empty;
            string companyUin = string.Empty;

            if (!string.IsNullOrEmpty(this.Request.QueryString["member"]))
            {
                name = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["member"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyUin"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyUin"]);
            }

            var members = this.umisRepository.GetContractSubcontractors(ContractSubcontractType.Member, name, companyUin, 0, null);

            var template = new ExportTemplate("members");
            template.Sheet.Name = "members";

            var table = new ExportTable(Texts.Global_Members);
            var headerRow = new ExportRow();

            for (int i = 0; i < 2; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Member_Index_Name;
            headerRow.Cells[1].Value = Texts.Member_Index_ContractsCount;

            table.Rows.Add(headerRow);

            foreach (var member in members.Results)
            {
                var row = new ExportRow();

                row.Cells.Add(member.TransFullName.ToExportCell());
                row.Cells.Add(member.ContractsCount.ToExportCell());

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