using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.ActuallyPaidAmounts;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ActuallyPaidAmountsController : BaseWithExportController
    {
        private IUmisRepository umisRepository;
        private INomenclatureRepository nomenclatureRepository;

        public ActuallyPaidAmountsController(
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
                    "programmePriorityId",
                    "procedureId",
                    "groupingLevel",
                    "dateTo",
                    "page",
            })]
        public virtual ActionResult Index(
                string programmeId = "",
                string programmePriorityId = "",
                string procedureId = "",
                string groupingLevel = "",
                string dateTo = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ActuallyPaidAmountsSearchVM vm = new ActuallyPaidAmountsSearchVM()
            {
                ProgrammeId = programmeId,
                ProgrammePriorityId = programmePriorityId,
                ProcedureId = procedureId,
                DateTo = dateTo,
                GroupingLevel = groupingLevel,
                ShowRes = showRes,
            };

            DateTime? to = null;

            DateTime temp;

            if (DateTime.TryParse(dateTo, out temp))
            {
                to = temp;
            }

            GroupingLevel grouping = GroupingLevel.Contract;

            if (!string.IsNullOrEmpty(this.Request.QueryString["groupingLevel"]))
            {
                grouping = (GroupingLevel)Enum.Parse(typeof(GroupingLevel), ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["groupingLevel"]));
            }

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var actuallyPaidAmounts = this.umisRepository.GetActuallyPaidAmounts(
                    groupingLevel: grouping,
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    programmePriorityId: string.IsNullOrWhiteSpace(vm.ProgrammePriorityId) ? (int?)null : int.Parse(vm.ProgrammePriorityId),
                    procedureId: string.IsNullOrWhiteSpace(vm.ProcedureId) ? (int?)null : int.Parse(vm.ProcedureId),
                    dateTo: to,
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ActuallyPaidAmountsVO>(actuallyPaidAmounts.Results, innerPage, Configuration.MaxItemsPerPage, actuallyPaidAmounts.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ActuallyPaidAmountsSearchVM vm)
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

            ActuallyPaidAmountsSearchVM.EncryptProperties(vm);

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
        public virtual JsonResult GetGroupingLevels(string parentId, string parent2Id)
        {
            var result = new List<Select2VO>();

            if (!string.IsNullOrEmpty(parentId) && !string.IsNullOrEmpty(parent2Id))
            {
                result.Add(new Select2VO { id = ((int)GroupingLevel.Procedure).ToString(), text = GroupingLevel.Procedure.GetEnumDescription() });
                result.Add(new Select2VO { id = ((int)GroupingLevel.Contract).ToString(), text = GroupingLevel.Contract.GetEnumDescription() });
            }
            else if (!string.IsNullOrEmpty(parentId) && string.IsNullOrEmpty(parent2Id))
            {
                result.Add(new Select2VO { id = ((int)GroupingLevel.ProgrammePriority).ToString(), text = GroupingLevel.ProgrammePriority.GetEnumDescription() });
                result.Add(new Select2VO { id = ((int)GroupingLevel.Procedure).ToString(), text = GroupingLevel.Procedure.GetEnumDescription() });
                result.Add(new Select2VO { id = ((int)GroupingLevel.Contract).ToString(), text = GroupingLevel.Contract.GetEnumDescription() });
            }
            else
            {
                foreach (GroupingLevel foo in Enum.GetValues(typeof(GroupingLevel)))
                {
                    result.Add(new Select2VO { id = ((int)foo).ToString(), text = foo.GetEnumDescription() });
                }
            }

            return this.Json(result);
        }

        [HttpPost]
        public virtual JsonResult GetGroupingLevel(GroupingLevel id)
        {
            var result = new Select2VO { id = ((int)id).ToString(), text = id.GetEnumDescription() };

            return this.Json(result);
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
            int? programmePriorityId = null;
            int? procedureId = null;
            DateTime? to = null;
            GroupingLevel groupingLevel = GroupingLevel.Contract;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmePriorityId"]))
            {
                programmePriorityId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmePriorityId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["procedureId"]))
            {
                procedureId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["procedureId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["groupingLevel"]))
            {
                groupingLevel = (GroupingLevel)Enum.Parse(typeof(GroupingLevel), ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["groupingLevel"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["dateTo"]))
            {
                to = DateTime.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["dateTo"]));
            }

            var result = this.umisRepository.GetActuallyPaidAmounts(groupingLevel, programmeId, programmePriorityId, procedureId, to, 0, null).Results.ToList();

            var template = new ExportTemplate("actuallyPaidAmounts");
            template.PageOrientation = PageOrientation.Landscape;
            template.Sheet.Name = "actuallyPaidAmounts";

            var table = new ExportTable(Texts.ActuallyPaidAmounts_Index_Title);
            var firstRow = new ExportRow();

            for (int i = 0; i < 12; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.ActuallyPaidAmounts_Index_RegNumber;
            firstRow.Cells[1].Value = Texts.ActuallyPaidAmounts_Index_Programme;
            firstRow.Cells[2].Value = Texts.ActuallyPaidAmounts_Index_PriorityAxis;
            firstRow.Cells[3].Value = Texts.ActuallyPaidAmounts_Index_Procedure;
            firstRow.Cells[4].Value = Texts.ActuallyPaidAmounts_Index_ProcedureNumber;
            firstRow.Cells[5].Value = Texts.ActuallyPaidAmounts_Index_ContractedTotalAmount;
            firstRow.Cells[6].Value = Texts.ActuallyPaidAmounts_Index_ContractedEuAmount;
            firstRow.Cells[7].Value = Texts.ActuallyPaidAmounts_Index_ContractedBgAmount;
            firstRow.Cells[8].Value = Texts.ActuallyPaidAmounts_Index_ContractedSelfAmount;
            firstRow.Cells[9].Value = Texts.ActuallyPaidAmounts_Index_ActuallyPaidTotalAmount;
            firstRow.Cells[10].Value = Texts.ActuallyPaidAmounts_Index_ActuallyPaidEuAmount;
            firstRow.Cells[11].Value = Texts.ActuallyPaidAmounts_Index_ActuallyPaidBgAmount;

            table.Rows.Add(firstRow);

            foreach (var actuallyPaidAmount in result)
            {
                var row = new ExportRow();

                row.Cells.Add(actuallyPaidAmount.ContractRegNumber.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ProgrammeName.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ProgrammePriorityName.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ProcedureName.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ProcedureNumber.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ContractedTotalAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ContractedEuAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ContractedBgAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ContractedSelfAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ActuallyPaidTotalAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ActuallyPaidEuAmount.ToExportCell());
                row.Cells.Add(actuallyPaidAmount.ActuallyPaidBgAmount.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 300 },
                { 3, 200 },
                { 4, 200 },
                { 5, 200 },
                { 6, 200 },
                { 7, 200 },
                { 8, 200 },
                { 9, 200 },
                { 10, 200 },
                { 11, 200 },
                { 12, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}