using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.IndicativeAnnualWorkingProgrammes;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class IndicativeAnnualWorkingProgrammesController : BaseWithExportController
    {
        private IUmisRepository umisRepository;
        private INomenclatureRepository nomenclatureRepository;

        public IndicativeAnnualWorkingProgrammesController(
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
                    "year",
                    "iawpType",
                    "page",
            })]
        public virtual ActionResult Index(
                string programmeId = "",
                string year = "",
                string iawpType = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            IndicativeAnnualWorkingProgrammesSearchVM vm = new IndicativeAnnualWorkingProgrammesSearchVM()
            {
                ProgrammeId = programmeId,
                Year = year,
                IawpType = iawpType,
                ShowRes = showRes,
            };

            if (vm.ShowRes)
            {
                vm.SearchResults = this.umisRepository.GetIndicativeAnnualWorkingProgrammes(
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    year: string.IsNullOrWhiteSpace(vm.Year) ? (IndicativeAnnualWorkingProgrammeYear?)null : (IndicativeAnnualWorkingProgrammeYear)int.Parse(vm.Year),
                    type: string.IsNullOrWhiteSpace(vm.IawpType) ? (IndicativeAnnualWorkingProgrammeType?)null : (IndicativeAnnualWorkingProgrammeType)int.Parse(vm.IawpType));
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(IndicativeAnnualWorkingProgrammesSearchVM vm)
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

            IndicativeAnnualWorkingProgrammesSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
           new string[]
           {
                "iawpId",
                "iawpType",
                "page",
           })]
        public virtual ActionResult Show(
               string iawpId = "",
               string iawpType = "",
               string page = "")
        {
            this.ModelState.Clear();

            int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
            int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;
            IndicativeAnnualWorkingProgrammeType type = (IndicativeAnnualWorkingProgrammeType)int.Parse(iawpType);

            int indicativeAnnualWorkingProgrammeId = int.Parse(iawpId);

            IndicativeAnnualWorkingProgrammeVO iawp = this.umisRepository.GetIndicativeAnnualWorkingProgramme(indicativeAnnualWorkingProgrammeId);

            var table = this.umisRepository.GetIndicativeAnnualWorkingProgrammeTable(
                iawpId: indicativeAnnualWorkingProgrammeId,
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

            IndicativeAnnualWorkingProgrammeTableSearchVM vm = new IndicativeAnnualWorkingProgrammeTableSearchVM();
            vm.IawpId = iawpId;
            vm.IawpType = iawpType;
            vm.IndicativeAnnualWorkingProgramme = iawp;
            vm.SearchResults = new StaticPagedList<IndicativeAnnualWorkingProgrammeTableVO>(table.Results, innerPage, Configuration.MaxItemsPerPage, table.Count);

            if (type == IndicativeAnnualWorkingProgrammeType.IawpTable)
            {
                return this.View(MVC.IndicativeAnnualWorkingProgrammes.Views.IawpTable, vm);
            }
            else if (type == IndicativeAnnualWorkingProgrammeType.IawpTableForIntegratedProcedures)
            {
                return this.View(MVC.IndicativeAnnualWorkingProgrammes.Views.IawpTableForIntegratedProcedures, vm);
            }

            return this.View();
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
        public virtual JsonResult GetYear(IndicativeAnnualWorkingProgrammeYear id)
        {
            return this.Json(new Select2VO() { id = ((int)id).ToString(), text = id.GetEnumDescription() });
        }

        [HttpPost]
        public virtual JsonResult GetYears()
        {
            var result = new List<Select2VO>();

            foreach (IndicativeAnnualWorkingProgrammeYear type in Enum.GetValues(typeof(IndicativeAnnualWorkingProgrammeYear)))
            {
                result.Add(new Select2VO { id = ((int)type).ToString(), text = type.GetEnumDescription() });
            }

            return this.Json(result);
        }

        [HttpPost]
        public virtual JsonResult GetIawpType(IndicativeAnnualWorkingProgrammeType id)
        {
            return this.Json(new Select2VO() { id = ((int)id).ToString(), text = id.GetEnumDescription() });
        }

        [HttpPost]
        public virtual JsonResult GetIawpTypes()
        {
            var result = new List<Select2VO>();

            foreach (IndicativeAnnualWorkingProgrammeType type in Enum.GetValues(typeof(IndicativeAnnualWorkingProgrammeType)))
            {
                result.Add(new Select2VO { id = ((int)type).ToString(), text = type.GetEnumDescription() });
            }

            return this.Json(result);
        }

        public override ExportTemplate RenderTemplate()
        {
            int? iawpId = null;
            IndicativeAnnualWorkingProgrammeType? iawpType = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["iawpId"]))
            {
                iawpId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["iawpId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["iawpType"]))
            {
                iawpType = (IndicativeAnnualWorkingProgrammeType)int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["iawpType"]));
            }

            if (!iawpId.HasValue || !iawpType.HasValue)
            {
                return new ExportTemplate();
            }

            IndicativeAnnualWorkingProgrammeVO iawp = this.umisRepository.GetIndicativeAnnualWorkingProgramme(iawpId.Value);
            var table = this.umisRepository.GetIndicativeAnnualWorkingProgrammeTable(iawpId.Value);

            if (iawpType == IndicativeAnnualWorkingProgrammeType.IawpTable)
            {
                return this.CreateIawpTableTemplate(table.Results, iawp);
            }
            else if (iawpType == IndicativeAnnualWorkingProgrammeType.IawpTableForIntegratedProcedures)
            {
                return this.CreateIawpTableForIntegratedProceduresTemplate(table.Results, iawp);
            }

            return new ExportTemplate();
        }

        private ExportTemplate CreateIawpTableTemplate(IList<IndicativeAnnualWorkingProgrammeTableVO> table, IndicativeAnnualWorkingProgrammeVO iawp)
        {
            var template = new ExportTemplate("IawpTables");
            template.Sheet.Name = "iawpTable";

            if (table != null && table.Count > 0)
            {
                var title = string.Format(Texts.IndicativeAnnualWorkingProgrammes_Index_Template, iawp.TransProgrammeName, iawp.Type.GetEnumDescription());
                var exportTable = new ExportTable(title);
                var firstRow = new ExportRow();

                for (int i = 0; i < 16; i++)
                {
                    firstRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                firstRow.Cells[0].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_OrderNum;
                firstRow.Cells[0].RowSpan = 2;

                firstRow.Cells[1].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProgrammePriorityName;
                firstRow.Cells[1].RowSpan = 2;

                firstRow.Cells[2].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureCode;
                firstRow.Cells[2].RowSpan = 2;

                firstRow.Cells[3].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureName;
                firstRow.Cells[3].RowSpan = 2;

                firstRow.Cells[4].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureDescription;
                firstRow.Cells[4].RowSpan = 2;

                firstRow.Cells[5].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_TypeConducting;
                firstRow.Cells[5].RowSpan = 2;

                firstRow.Cells[6].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_WithPreSelection;
                firstRow.Cells[6].RowSpan = 2;

                firstRow.Cells[7].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureTotalAmount;
                firstRow.Cells[7].RowSpan = 2;

                firstRow.Cells[8].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_Candidates;
                firstRow.Cells[8].RowSpan = 2;

                firstRow.Cells[9].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_EligibleActivities;
                firstRow.Cells[9].RowSpan = 2;

                firstRow.Cells[10].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_EligibleCosts;
                firstRow.Cells[10].RowSpan = 2;

                firstRow.Cells[11].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_MaxPercentCoFinancing;
                firstRow.Cells[11].RowSpan = 2;

                firstRow.Cells[12].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ListingDate;
                firstRow.Cells[12].RowSpan = 2;

                firstRow.Cells[13].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_TimeLimits;
                firstRow.Cells[13].RowSpan = 2;

                firstRow.Cells[14].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_Assistance;
                firstRow.Cells[14].ColSpan = 2;

                firstRow.Cells[15].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectAmount;
                firstRow.Cells[15].ColSpan = 2;

                exportTable.Rows.Add(firstRow);

                var secondRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    secondRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                secondRow.Cells[0].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_IsStateAssistance;
                secondRow.Cells[1].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_IsMinimalAssistance;
                secondRow.Cells[2].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectMinAmount;
                secondRow.Cells[3].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectMaxAmount;

                exportTable.Rows.Add(secondRow);

                foreach (var row in table)
                {
                    var exportRow = new ExportRow();

                    exportRow.Cells.Add(row.OrderNum.ToExportCell());
                    exportRow.Cells.Add(row.TransProgrammePriorityName.ToExportCell());
                    exportRow.Cells.Add(row.ProcedureCode.ToExportCell());
                    exportRow.Cells.Add(row.TransProcedureName.ToExportCell());
                    exportRow.Cells.Add(row.TransProcedureDescription.ToExportCell());
                    exportRow.Cells.Add(row.IndicativeAnnualWorkingProgrammeTypeConducting.ToExportCell());
                    exportRow.Cells.Add(row.WithPreSelectionText.ToExportCell());
                    exportRow.Cells.Add(row.ProcedureTotalAmount.ToExportCell());
                    exportRow.Cells.Add(row.TransIndicativeAnnualWorkingProgrammeTableCandidates.ToExportCell());
                    exportRow.Cells.Add(row.TransEligibleActivities.ToExportCell());
                    exportRow.Cells.Add(row.TransEligibleCosts.ToExportCell());
                    exportRow.Cells.Add(string.Format("{0} {1}", $"{DataUtils.DecimalToStringDecimalPointSpace(row.MaxPercentCoFinancing)}%", string.IsNullOrEmpty(row.TransMaxPercentCoFinancingInfo) ? string.Empty : $"({row.TransMaxPercentCoFinancingInfo})").ToExportCell());
                    exportRow.Cells.Add(row.ListingDate.ToExportCell());
                    exportRow.Cells.Add(row.IndicativeAnnualWorkingProgrammeTableTimeLimits.ToExportCell());
                    exportRow.Cells.Add(row.IsStateAssistance.ToExportCell());
                    exportRow.Cells.Add(row.IsMinimalAssistance.ToExportCell());
                    exportRow.Cells.Add((row.ProjectMinAmount + " " + row.TransProjectMinAmountInfo).ToExportCell());
                    exportRow.Cells.Add((row.ProjectMaxAmount + " " + row.TransProjectMaxAmountInfo).ToExportCell());

                    exportTable.Rows.Add(exportRow);
                }

                template.Sheet.Tables.Add(exportTable);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 100 },
                    { 2, 400 },
                    { 3, 150 },
                    { 4, 300 },
                    { 5, 400 },
                    { 6, 400 },
                    { 7, 400 },
                    { 8, 400 },
                    { 9, 400 },
                    { 10, 400 },
                    { 11, 400 },
                    { 12, 400 },
                    { 13, 400 },
                    { 14, 400 },
                    { 15, 150 },
                    { 16, 150 },
                    { 17, 150 },
                    { 18, 150 },
                };
            }

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private ExportTemplate CreateIawpTableForIntegratedProceduresTemplate(IList<IndicativeAnnualWorkingProgrammeTableVO> table, IndicativeAnnualWorkingProgrammeVO iawp)
        {
            var template = new ExportTemplate("IawpTables");
            template.Sheet.Name = "iawpTable";

            if (table != null && table.Count > 0)
            {
                var title = string.Format(Texts.IndicativeAnnualWorkingProgrammes_Index_Template, iawp.TransProgrammeName, iawp.Type.GetEnumDescription());
                var exportTable = new ExportTable(title);
                var firstRow = new ExportRow();

                for (int i = 0; i < 18; i++)
                {
                    firstRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                firstRow.Cells[0].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_OrderNum;
                firstRow.Cells[0].RowSpan = 2;

                firstRow.Cells[1].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProgrammePriorityName;
                firstRow.Cells[1].RowSpan = 2;

                firstRow.Cells[2].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureCode;
                firstRow.Cells[2].RowSpan = 2;

                firstRow.Cells[3].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureName;
                firstRow.Cells[3].RowSpan = 2;

                firstRow.Cells[4].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureDescription;
                firstRow.Cells[4].RowSpan = 2;

                firstRow.Cells[5].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_TypeConducting;
                firstRow.Cells[5].RowSpan = 2;

                firstRow.Cells[6].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_WithPreSelection;
                firstRow.Cells[6].RowSpan = 2;

                firstRow.Cells[7].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_Programmes;
                firstRow.Cells[7].RowSpan = 2;

                firstRow.Cells[8].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_LeadingProgram;
                firstRow.Cells[8].RowSpan = 2;

                firstRow.Cells[9].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProcedureTotalAmount;
                firstRow.Cells[9].RowSpan = 2;

                firstRow.Cells[10].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_Candidates;
                firstRow.Cells[10].RowSpan = 2;

                firstRow.Cells[11].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_EligibleActivities;
                firstRow.Cells[11].RowSpan = 2;

                firstRow.Cells[12].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_EligibleCosts;
                firstRow.Cells[12].RowSpan = 2;

                firstRow.Cells[13].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_MaxPercentCoFinancing;
                firstRow.Cells[13].RowSpan = 2;

                firstRow.Cells[14].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ListingDate;
                firstRow.Cells[14].RowSpan = 2;

                firstRow.Cells[15].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_TimeLimits;
                firstRow.Cells[15].RowSpan = 2;

                firstRow.Cells[16].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_Assistance;
                firstRow.Cells[16].ColSpan = 2;

                firstRow.Cells[17].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectAmount;
                firstRow.Cells[17].ColSpan = 2;

                exportTable.Rows.Add(firstRow);

                var secondRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    secondRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                secondRow.Cells[0].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_IsStateAssistance;
                secondRow.Cells[1].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_IsMinimalAssistance;
                secondRow.Cells[2].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectMinAmount;
                secondRow.Cells[3].Value = Texts.IndicativeAnnualWorkingProgrammes_Table_ProjectMaxAmount;

                exportTable.Rows.Add(secondRow);

                foreach (var row in table)
                {
                    var exportRow = new ExportRow();

                    exportRow.Cells.Add(row.OrderNum.ToExportCell());
                    exportRow.Cells.Add(row.TransProgrammePriorityName.ToExportCell());
                    exportRow.Cells.Add(row.ProcedureCode.ToExportCell());
                    exportRow.Cells.Add(row.TransProcedureName.ToExportCell());
                    exportRow.Cells.Add(row.TransProcedureDescription.ToExportCell());
                    exportRow.Cells.Add(row.IndicativeAnnualWorkingProgrammeTypeConducting.ToExportCell());
                    exportRow.Cells.Add(row.WithPreSelectionText.ToExportCell());
                    exportRow.Cells.Add(row.TransIndicativeAnnualWorkingProgrammeTableProgrammes.ToExportCell());
                    exportRow.Cells.Add(row.TransLeadingProgram.ToExportCell());
                    exportRow.Cells.Add(row.ProcedureTotalAmount.ToExportCell());
                    exportRow.Cells.Add(row.TransIndicativeAnnualWorkingProgrammeTableCandidates.ToExportCell());
                    exportRow.Cells.Add(row.TransEligibleActivities.ToExportCell());
                    exportRow.Cells.Add(row.TransEligibleCosts.ToExportCell());
                    exportRow.Cells.Add(string.Format("{0} {1}", $"{DataUtils.DecimalToStringDecimalPointSpace(row.MaxPercentCoFinancing)}%", string.IsNullOrEmpty(row.TransMaxPercentCoFinancingInfo) ? string.Empty : $"({row.TransMaxPercentCoFinancingInfo})").ToExportCell());
                    exportRow.Cells.Add(row.ListingDate.ToExportCell());
                    exportRow.Cells.Add(row.IndicativeAnnualWorkingProgrammeTableTimeLimits.ToExportCell());
                    exportRow.Cells.Add(row.IsStateAssistance.ToExportCell());
                    exportRow.Cells.Add(row.IsMinimalAssistance.ToExportCell());
                    exportRow.Cells.Add((row.ProjectMinAmount + " " + row.TransProjectMinAmountInfo).ToExportCell());
                    exportRow.Cells.Add((row.ProjectMaxAmount + " " + row.TransProjectMaxAmountInfo).ToExportCell());

                    exportTable.Rows.Add(exportRow);
                }

                template.Sheet.Tables.Add(exportTable);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 100 },
                    { 2, 400 },
                    { 3, 150 },
                    { 4, 300 },
                    { 5, 400 },
                    { 6, 400 },
                    { 7, 400 },
                    { 8, 400 },
                    { 9, 400 },
                    { 10, 400 },
                    { 11, 400 },
                    { 12, 400 },
                    { 13, 400 },
                    { 14, 400 },
                    { 15, 400 },
                    { 16, 400 },
                    { 17, 150 },
                    { 18, 150 },
                    { 19, 150 },
                    { 20, 150 },
                };
            }

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}
