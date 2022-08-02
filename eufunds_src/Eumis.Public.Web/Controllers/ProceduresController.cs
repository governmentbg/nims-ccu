using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Procedures.Repositories;
using Eumis.Public.Data.Procedures.ViewObjects;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Procedures;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.Controllers
{
    public partial class ProceduresController : BaseWithExportController
    {
        private INomenclatureRepository nomenclatureRepository;
        private IProceduresRepository proceduresRepository;

        public ProceduresController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            INomenclatureRepository nomenclatureRepository,
            IProceduresRepository proceduresRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.nomenclatureRepository = nomenclatureRepository;
            this.proceduresRepository = proceduresRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
        new string[]
        {
                "settlementId",
                "companyTypeId",
                "companyLegalTypeId",
                "page",
        })]
        public virtual ActionResult Index(
                string settlementId = "",
                string companyTypeId = "",
                string companyLegalTypeId = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ProceduresSearchVM vm = new ProceduresSearchVM()
            {
                SettlementId = settlementId,
                CompanyTypeId = companyTypeId,
                CompanyLegalTypeId = companyLegalTypeId,
                ShowRes = showRes,
            };

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var procedures = this.proceduresRepository.GetProcedures(
                    settlementId: string.IsNullOrWhiteSpace(vm.SettlementId) ? (int?)null : int.Parse(vm.SettlementId),
                    companyTypeId: string.IsNullOrWhiteSpace(vm.CompanyTypeId) ? (int?)null : int.Parse(vm.CompanyTypeId),
                    companyLegalTypeId: string.IsNullOrWhiteSpace(vm.CompanyLegalTypeId) ? (int?)null : int.Parse(vm.CompanyLegalTypeId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ProcedureVO>(procedures.Results, innerPage, Configuration.MaxItemsPerPage, procedures.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ProceduresSearchVM vm)
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

            ProceduresSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        [HttpPost]
        public virtual JsonResult GetSettlement(int id)
        {
            return this.Json(this.nomenclatureRepository.GetSettlement(id));
        }

        [HttpPost]
        public virtual JsonResult GetSettlements(string term)
        {
            return this.Json(this.nomenclatureRepository.GetSettlements(term));
        }

        public override ExportTemplate RenderTemplate()
        {
            int? settlementId = null;
            int? companyTypeId = null;
            int? companyLegalTypeId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["settlementId"]))
            {
                settlementId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["settlementId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyTypeId"]))
            {
                companyTypeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyTypeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyLegalTypeId"]))
            {
                companyLegalTypeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyLegalTypeId"]));
            }

            var result = this.proceduresRepository.GetProcedures(settlementId, companyTypeId, companyLegalTypeId, 0, null);

            var procedures = result.Results;

            var template = new ExportTemplate("procedures");
            template.Sheet.Name = "procedures";

            var table = new ExportTable(Texts.Procedures_Index_Procedures);
            var firstRow = new ExportRow();

            for (int i = 0; i < 9; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Procedures_Index_Status;
            firstRow.Cells[1].Value = Texts.Procedures_Index_ProgrammeName;
            firstRow.Cells[2].Value = Texts.Procedures_Index_ProcedureName;
            firstRow.Cells[3].Value = Texts.Procedures_Index_ProcedureEndDate;
            firstRow.Cells[4].Value = Texts.Procedures_Index_ProcedureBudgetTotal;
            firstRow.Cells[5].Value = Texts.Procedures_Index_EligibleCandidates;
            firstRow.Cells[6].Value = Texts.Procedures_Index_EligibleActivities;
            firstRow.Cells[7].Value = Texts.Procedures_Index_EligibleCosts;
            firstRow.Cells[8].Value = Texts.Procedures_Index_MaxPercentCoFinancing;

            table.Rows.Add(firstRow);

            foreach (var procedure in procedures)
            {
                var row = new ExportRow();

                row.Cells.Add(procedure.Status == ProcedureStatus.Active ? Texts.Procedures_Index_Status_Opened.ToExportCell() : Texts.Procedures_Index_Status_NotAnnounced.ToExportCell());
                row.Cells.Add(procedure.ProgrammeTransName.ToExportCell());
                row.Cells.Add(procedure.ProcedureTransName.ToExportCell());
                row.Cells.Add(procedure.EndingDate.ToExportCell());
                row.Cells.Add(procedure.BudgetTotal.ToExportCell());
                row.Cells.Add(procedure.Candidates.ToExportCell());
                row.Cells.Add(procedure.EligibleActivities.ToExportCell());
                row.Cells.Add(procedure.EligibleCosts.ToExportCell());
                row.Cells.Add(procedure.MaxPercentCoFinancing.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);
            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 300 },
                { 3, 350 },
                { 4, 200 },
                { 5, 200 },
                { 6, 300 },
                { 7, 200 },
                { 8, 200 },
                { 9, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Procedures_Index_FooterNote);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}
