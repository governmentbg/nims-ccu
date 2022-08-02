using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Admin;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    [AuthorizeIPAddress]
    public partial class AdminController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public AdminController(IUmisRepository umisRepository, IMapsRepository mapsRepository, IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        public virtual ActionResult Index()
        {
            return this.View();
        }

        public virtual ActionResult UsersCount()
        {
            return this.View(this.umisRepository.GetUsersCount());
        }

        [DecryptParametersAttribute(IdsParamName = new string[] { "page" })]
        public virtual ActionResult UsersStatistics(string page = "")
        {
            int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
            int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

            var statistics = this.umisRepository.GetUsersStatistics(
                offset: offset,
                limit: Configuration.MaxItemsPerPage,
                isEn: SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English);

            var vm = new StaticPagedList<UserStatisticsVO>(statistics.Results, innerPage, Configuration.MaxItemsPerPage, statistics.Count);

            return this.View(vm);
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "programmeId",
            })]
        public virtual ActionResult OPStatistics(
                string programmeId = "",
                bool showRes = false)
        {
            this.ModelState.Clear();

            OPStatisticsSearchVM vm = new OPStatisticsSearchVM()
            {
                ProgrammeId = programmeId,
                ShowRes = showRes,
            };

            this.FillModelValues(ref vm);

            if (vm.ShowRes)
            {
                vm.SearchResult = this.umisRepository.GetOPStatistics(programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId));
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult OPStatistics(OPStatisticsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (!this.ModelState.IsValid)
            {
                this.FillModelValues(ref vm);

                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            OPStatisticsSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.OPStatistics, vm);
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "programmeId",
                    "page",
            })]
        public virtual ActionResult ProgrammeProjectsCount(
                string programmeId = "",
                string page = "")
        {
            int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
            int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

            var vo = this.umisRepository.GetProgrammesProceduresStatistics(
                programmeId: string.IsNullOrWhiteSpace(programmeId) ? (int?)null : int.Parse(programmeId),
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

            ProgrammesProceduresStatisticsVM vm = new ProgrammesProceduresStatisticsVM();
            vm.ProgrammeId = vo.ProgrammeId;
            vm.ProgrammeName = vo.ProgrammeName;
            vm.ProgrammeNameAlt = vo.ProgrammeNameAlt;
            vm.PageProcedures = new StaticPagedList<ProjectProposalVO>(vo.PageProcedures.Results, innerPage, Configuration.MaxItemsPerPage, vo.PageProcedures.Count);

            return this.View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Test()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Test(string test)
        {
            if (string.IsNullOrEmpty(test))
            {
                throw new ArgumentNullException(nameof(test));
            }

            test = test.TrimStart().TrimEnd();

            if (string.IsNullOrWhiteSpace(test))
            {
                return this.RedirectToAction(this.ActionNames.Test);
            }

            var inMC = new InMemoryCache();
            var cachedItem = inMC.GetOrSet<List<string>>(InMemoryCache.DefaultKey, () => new List<string> { test });

            if (!cachedItem.Contains(test))
            {
                cachedItem.Add(test);
                inMC.Update<List<string>>(InMemoryCache.DefaultKey, cachedItem);
            }

            return this.RedirectToAction(this.ActionNames.Index);
        }

        public override ExportTemplate RenderTemplate()
        {
            var users = this.umisRepository
                .GetUsersStatistics(0, null, isEn: SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                .Results.ToList();

            var template = new ExportTemplate("users");
            template.Sheet.Name = "users";

            var table = new ExportTable(Texts.Admin_UsersStatistics);
            var firstRow = new ExportRow();

            for (int i = 0; i < 7; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Admin_UsersStatistics_Number;
            firstRow.Cells[1].Value = Texts.Admin_UsersStatistics_Fullname;
            firstRow.Cells[2].Value = Texts.Admin_UsersStatistics_Username;
            firstRow.Cells[3].Value = Texts.Admin_UsersStatistics_DraftProjectsCount;
            firstRow.Cells[4].Value = Texts.Admin_UsersStatistics_DraftOperationalProgrammes;
            firstRow.Cells[5].Value = Texts.Admin_UsersStatistics_RegisteredProjectsCount;
            firstRow.Cells[6].Value = Texts.Admin_UsersStatistics_RegisteredOperationalProgrammes;

            table.Rows.Add(firstRow);

            foreach (var user in users)
            {
                var row = new ExportRow();

                row.Cells.Add(user.UserId.ToExportCell());
                row.Cells.Add(user.GetFullName().ToExportCell());
                row.Cells.Add(user.Username.ToExportCell());
                row.Cells.Add(user.DraftProjectsCount.ToExportCell());
                row.Cells.Add(user.GetDraftOperationalProgrammes().ToExportCell());
                row.Cells.Add(user.RegisteredProjectsCount.ToExportCell());
                row.Cells.Add(user.GetRegisteredOperationalProgrammes().ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 300 },
                { 3, 300 },
                { 4, 100 },
                { 5, 400 },
                { 6, 100 },
                { 7, 400 },
            };

            return template;
        }

        private void FillModelValues(ref OPStatisticsSearchVM vm)
        {
            if (vm == null)
            {
                vm = new OPStatisticsSearchVM();
            }

            var ops = this.InfrastructureRepository.GetAllOps().OrderBy(e => e.PortalOrderNum).Select(e => new SelectListItem() { Value = e.MapNodeId.ToString(), Text = e.TransName });
            vm.Programs = ops;
        }
    }
}