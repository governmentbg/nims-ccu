using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.ExecutedContracts.Repositories;
using Eumis.Public.Data.ExecutedContracts.ViewObjects;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.ExecutedContracts;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ExecutedContractsController : BaseWithExportController
    {
        private IExecutedContractsRepository executedContractsRepository;
        private INomenclatureRepository nomenclatureRepository;

        public ExecutedContractsController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IExecutedContractsRepository executedContractsRepository,
            INomenclatureRepository nomenclatureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.executedContractsRepository = executedContractsRepository;
            this.nomenclatureRepository = nomenclatureRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
        new string[]
        {
            "programmeId",
            "procedureId",
            "companyId",
            "page",
        })]
        public virtual ActionResult Index(
                string programmeId = "",
                string procedureId = "",
                string companyId = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ExecutedContractsSearchVM vm = new ExecutedContractsSearchVM()
            {
                ProgrammeId = programmeId,
                ProcedureId = procedureId,
                CompanyId = companyId,
                ShowRes = showRes,
            };

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contracts = this.executedContractsRepository.GetExecutedContracts(
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    companyId: string.IsNullOrWhiteSpace(vm.CompanyId) ? (int?)null : int.Parse(vm.CompanyId),
                    procedureId: string.IsNullOrWhiteSpace(vm.ProcedureId) ? (int?)null : int.Parse(vm.ProcedureId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ExecutedContractVO>(contracts.Results, innerPage, Configuration.MaxItemsPerPage, contracts.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ExecutedContractsSearchVM vm)
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

            ExecutedContractsSearchVM.EncryptProperties(vm);

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

            return this.Json(this.nomenclatureRepository.GetProceduresByProgramme(term, int.Parse(parentId)));
        }

        [HttpPost]
        public virtual JsonResult GetCompany(int id)
        {
            return this.Json(this.nomenclatureRepository.GetCompany(id));
        }

        [HttpPost]
        public virtual JsonResult GetCompanies(string term)
        {
            return this.Json(this.nomenclatureRepository.GetCompanies(term));
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;
            int? procedureId = null;
            int? companyId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["procedureId"]))
            {
                procedureId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["procedureId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyId"]))
            {
                companyId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyId"]));
            }

            var result = this.executedContractsRepository.GetExecutedContracts(programmeId, procedureId, companyId, 0, null);

            var contracts = result.Results;

            var template = new ExportTemplate("contracts");
            template.Sheet.Name = "contracts";

            var table = new ExportTable(Texts.ExecutedContracts_Index_Title);
            var firstRow = new ExportRow();

            for (int i = 0; i < 3; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.ExecutedContracts_Index_Contract;
            firstRow.Cells[0].ColSpan = 4;

            firstRow.Cells[1].Value = Texts.ExecutedContracts_Index_Company;
            firstRow.Cells[1].ColSpan = 5;

            firstRow.Cells[2].Value = Texts.ExecutedContracts_Index_Contract;
            firstRow.Cells[2].ColSpan = 5;

            table.Rows.Add(firstRow);

            var secondRow = new ExportRow();

            for (int i = 0; i < 14; i++)
            {
                secondRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            secondRow.Cells[0].Value = Texts.ExecutedContracts_Index_Programme;
            secondRow.Cells[1].Value = Texts.ExecutedContracts_Index_Procedure;
            secondRow.Cells[2].Value = Texts.ExecutedContracts_Index_ContractRegNumber;
            secondRow.Cells[3].Value = Texts.ExecutedContracts_Index_Contract;
            secondRow.Cells[4].Value = Texts.ExecutedContracts_Index_Uin;
            secondRow.Cells[5].Value = Texts.ExecutedContracts_Index_Company;
            secondRow.Cells[6].Value = Texts.ExecutedContracts_Index_CompanyType;
            secondRow.Cells[7].Value = Texts.ExecutedContracts_Index_CompanyLegalType;
            secondRow.Cells[8].Value = Texts.ExecutedContracts_Index_CompanySizeType;
            secondRow.Cells[9].Value = Texts.ExecutedContracts_Index_ContractDuration;
            secondRow.Cells[10].Value = Texts.ExecutedContracts_Index_InitialContractDate;
            secondRow.Cells[11].Value = Texts.ExecutedContracts_Index_InitialCompletionDate;
            secondRow.Cells[12].Value = Texts.ExecutedContracts_Index_ActualCompletionDate;
            secondRow.Cells[13].Value = Texts.ExecutedContracts_Index_ContractExecutionStatus;

            table.Rows.Add(secondRow);

            foreach (var contract in contracts)
            {
                var row = new ExportRow();

                row.Cells.Add(contract.ProgrammeTransName.ToExportCell());
                row.Cells.Add(contract.ProcedureTransName.ToExportCell());
                row.Cells.Add(contract.ContractRegNumber.ToExportCell());
                row.Cells.Add(contract.ContractTransName.ToExportCell());
                row.Cells.Add(contract.CompanyUin.ToExportCell());
                row.Cells.Add(contract.CompanyTransName.ToExportCell());
                row.Cells.Add(contract.CompanyTypeTransName.ToExportCell());
                row.Cells.Add(contract.CompanyLegalTypeTransName.ToExportCell());
                row.Cells.Add(contract.CompanySizeTypeTransName.ToExportCell());
                row.Cells.Add(contract.ContractDuration == null ? string.Empty.ToExportCell() : DataUtils.IntegerToString(contract.ContractDuration.Value).ToExportCell());
                row.Cells.Add(contract.InitialContractDate.ToExportCell());
                row.Cells.Add(contract.InitialCompletionDate.ToExportCell());
                row.Cells.Add(contract.ActualCompletionDate.ToExportCell());
                row.Cells.Add(contract.ContractExecutionStatus.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);
            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 350 },
                { 2, 350 },
                { 3, 300 },
                { 4, 200 },
                { 5, 200 },
                { 6, 300 },
                { 7, 200 },
                { 8, 200 },
                { 9, 200 },
                { 10, 300 },
                { 11, 300 },
                { 12, 300 },
                { 13, 300 },
                { 14, 300 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}
