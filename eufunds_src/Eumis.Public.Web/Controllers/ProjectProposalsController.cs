using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Indicators;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ProjectProposalsController : BaseWithExportController
    {
        private IUmisRepository umisRepository;
        private INomenclatureRepository nomenclatureRepository;

        public ProjectProposalsController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IUmisRepository umisRepository,
            INomenclatureRepository nomenclatureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
            this.nomenclatureRepository = nomenclatureRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "programmeId",
                    "priorityAxisId",
                    "procedureId",
                    "page",
            })]
        public virtual ActionResult Index(
                string programmeId = "",
                string priorityAxisId = "",
                string procedureId = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ProjectProposalsSearchVM vm = new ProjectProposalsSearchVM()
            {
                ProgrammeId = programmeId,
                PriorityAxisId = priorityAxisId,
                ProcedureId = procedureId,
                ShowRes = showRes,
            };

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var projects = this.umisRepository.GetStatisticProjects(
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    programmePriorityId: string.IsNullOrWhiteSpace(vm.PriorityAxisId) ? (int?)null : int.Parse(vm.PriorityAxisId),
                    procedureId: string.IsNullOrWhiteSpace(vm.ProcedureId) ? (int?)null : int.Parse(vm.ProcedureId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResultsTotals = projects.Totals;
                vm.SearchResults = new StaticPagedList<ProjectProposalVO>(projects.PageResults.Results, innerPage, Configuration.MaxItemsPerPage, projects.PageResults.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ProjectProposalsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (!this.ModelState.IsValid)
            {
                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            ProjectProposalsSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        [HttpPost]
        public virtual JsonResult GetProgramme(int id)
        {
            return this.Json(this.nomenclatureRepository.GetProgramme(id));
        }

        [HttpPost]
        public virtual JsonResult GetProgrammes(string term)
        {
            return this.Json(this.nomenclatureRepository.GetProgrammes(term));
        }

        [HttpPost]
        public virtual JsonResult GetPriority(int id)
        {
            return this.Json(this.nomenclatureRepository.GetPriorityLine(id));
        }

        [HttpPost]
        public virtual JsonResult GetPriorities(string term, string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return this.Json(new List<object>());
            }

            return this.Json(this.nomenclatureRepository.GetPriorityLines(term, int.Parse(parentId)));
        }

        [HttpPost]
        public virtual JsonResult GetProcedure(int id)
        {
            return this.Json(this.nomenclatureRepository.GetProcedure(id));
        }

        [HttpPost]
        public virtual JsonResult GetProcedures(string term, string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return this.Json(new List<object>());
            }

            return this.Json(this.nomenclatureRepository.GetProcedures(term, int.Parse(parentId)));
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;
            int? priorityAxisId = null;
            int? procedureId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["priorityAxisId"]))
            {
                priorityAxisId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["priorityAxisId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["procedureId"]))
            {
                procedureId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["procedureId"]));
            }

            var result = this.umisRepository.GetStatisticProjects(programmeId, priorityAxisId, procedureId, 0, null);

            var totals = result.Totals;
            var projects = result.PageResults.Results.ToList();

            var template = new ExportTemplate("projectProposals");
            template.Sheet.Name = "projectProposals";
            var totalsTable = new ExportTable(Texts.Global_Total);
            var firstTotalRow = new ExportRow();

            for (int i = 0; i < 6; i++)
            {
                firstTotalRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstTotalRow.Cells[0].Value = Texts.ProjectProposals_Index_TotalSubmittedCount;
            firstTotalRow.Cells[1].Value = Texts.ProjectProposals_Index_TotalTotalAmount;
            firstTotalRow.Cells[2].Value = Texts.ProjectProposals_Index_TotalBFPAmount;
            firstTotalRow.Cells[3].Value = Texts.ProjectProposals_Index_TotalApprovedCount;
            firstTotalRow.Cells[4].Value = Texts.ProjectProposals_Index_TotalReservedCount;
            firstTotalRow.Cells[5].Value = Texts.ProjectProposals_Index_TotalRejectedCount;

            totalsTable.Rows.Add(firstTotalRow);

            var newRow = new ExportRow();

            newRow.Cells.Add(totals.ProjectCount.ToExportCell());
            newRow.Cells.Add(totals.TotalAmout.ToExportCell());
            newRow.Cells.Add(totals.BfpAmount.ToExportCell());
            newRow.Cells.Add(totals.ApprovedCount.ToExportCell());
            newRow.Cells.Add(totals.ReserveCount.ToExportCell());
            newRow.Cells.Add(totals.RejectedCount.ToExportCell());

            totalsTable.Rows.Add(newRow);

            template.Sheet.Tables.Add(totalsTable);
            var table = new ExportTable(Texts.ProjectProposals_Index_ProjectProposals);
            var firstRow = new ExportRow();

            for (int i = 0; i < 8; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.ProjectProposals_Index_ProcedureNumber;
            firstRow.Cells[1].Value = Texts.ProjectProposals_Index_ProcedureNamе;
            firstRow.Cells[2].Value = Texts.ProjectProposals_Index_SubmittedCount;
            firstRow.Cells[3].Value = Texts.ProjectProposals_Index_TotalAmount;
            firstRow.Cells[4].Value = Texts.ProjectProposals_Index_BFPAmount;
            firstRow.Cells[5].Value = Texts.ProjectProposals_Index_ApprovedCount;
            firstRow.Cells[6].Value = Texts.ProjectProposals_Index_ReservedCount;
            firstRow.Cells[7].Value = Texts.ProjectProposals_Index_RejectedCount;

            table.Rows.Add(firstRow);

            foreach (var project in projects)
            {
                var row = new ExportRow();

                row.Cells.Add(project.Code.ToExportCell());
                row.Cells.Add(project.TransName.ToExportCell());
                row.Cells.Add(project.ProjectCount.ToExportCell());
                row.Cells.Add(project.TotalAmout.ToExportCell());
                row.Cells.Add(project.BfpAmount.ToExportCell());
                row.Cells.Add(project.ApprovedCount.ToExportCell());
                row.Cells.Add(project.ReserveCount.ToExportCell());
                row.Cells.Add(project.RejectedCount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);
            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 500 },
                { 3, 200 },
                { 4, 200 },
                { 5, 200 },
                { 6, 200 },
                { 7, 200 },
                { 8, 200 },
            };
            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);
            return template;
        }
    }
}