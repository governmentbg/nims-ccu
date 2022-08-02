using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.ContractContracts.Repositories;
using Eumis.Public.Data.ContractContracts.ViewObjects;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.ContractContractors;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ContractContractsController : BaseWithExportController
    {
        private INomenclatureRepository nomenclatureRepository;
        private IContractContractsRepository contractContractsRepository;

        public ContractContractsController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            INomenclatureRepository nomenclatureRepository,
            IContractContractsRepository contractContractsRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.nomenclatureRepository = nomenclatureRepository;
            this.contractContractsRepository = contractContractsRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
       new string[]
       {
                "programmeId",
                "beneficiary",
                "companyUin",
                "errandLegalActId",
                "page",
       })]
        public virtual ActionResult Index(
               string programmeId = "",
               string beneficiary = "",
               string companyUin = "",
               string errandLegalActId = "",
               bool showRes = false,
               string page = "")
        {
            this.ModelState.Clear();

            ContractContractsSearchVM vm = new ContractContractsSearchVM()
            {
                ProgrammeId = programmeId,
                Beneficiary = beneficiary,
                CompanyUin = companyUin,
                ErrandLegalActId = errandLegalActId,
                ShowRes = showRes,
            };

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contractContracts = this.contractContractsRepository.GetContractContracts(
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    beneficiary: beneficiary,
                    companyUin: companyUin,
                    errandLegalActId: string.IsNullOrWhiteSpace(vm.ErrandLegalActId) ? (int?)null : int.Parse(vm.ErrandLegalActId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractContractVO>(contractContracts.Results, innerPage, Configuration.MaxItemsPerPage, contractContracts.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(ContractContractsSearchVM vm)
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

            ContractContractsSearchVM.EncryptProperties(vm);

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
        public virtual JsonResult GetCompany(int id)
        {
            return this.Json(this.nomenclatureRepository.GetCompany(id));
        }

        [HttpPost]
        public virtual JsonResult GetCompanies(string term)
        {
            return this.Json(this.nomenclatureRepository.GetCompanies(term));
        }

        [HttpPost]
        public virtual JsonResult GetErrandLegalAct(int id)
        {
            return this.Json(this.nomenclatureRepository.GetErrandLegalAct(id));
        }

        [HttpPost]
        public virtual JsonResult GetErrandLegalActs(string term)
        {
            return this.Json(this.nomenclatureRepository.GetErrandLegalActs(term));
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;
            int? errandLegalActId = null;
            string beneficiary = null;
            string companyUin = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["errandLegalActId"]))
            {
                errandLegalActId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["errandLegalActId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["beneficiary"]))
            {
                beneficiary = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["beneficiary"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["companyUin"]))
            {
                companyUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["companyUin"]);
            }

            var result = this.contractContractsRepository.GetContractContracts(programmeId, beneficiary, companyUin, errandLegalActId, 0, null);

            var contractContracts = result.Results;

            var template = new ExportTemplate("contractContracts");
            template.Sheet.Name = "contractContracts";

            var table = new ExportTable(Texts.ContractContracts_Search_ContractContracts);
            var firstRow = new ExportRow();

            for (int i = 0; i < 14; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.ContractContracts_Search_ContractContractNumber;
            firstRow.Cells[1].Value = Texts.ContractContracts_Search_ContractProcurementPlanName;
            firstRow.Cells[2].Value = Texts.ContractContracts_Search_ContractDifferentiatedPositionName;
            firstRow.Cells[3].Value = Texts.ContractContracts_Search_Contract;
            firstRow.Cells[4].Value = Texts.ContractContracts_Search_Company;
            firstRow.Cells[5].Value = Texts.ContractContracts_Search_Uin;
            firstRow.Cells[6].Value = Texts.ContractContracts_Search_CompanyType;
            firstRow.Cells[7].Value = Texts.ContractContracts_Search_ContractContractor;
            firstRow.Cells[8].Value = Texts.ContractContracts_Search_ContractContractorUin;
            firstRow.Cells[9].Value = Texts.ContractContracts_Search_TotalFundedValue;
            firstRow.Cells[10].Value = Texts.ContractContracts_Search_ErrandAreaName;
            firstRow.Cells[11].Value = Texts.ContractContracts_Search_ErrandLegalActName;
            firstRow.Cells[12].Value = Texts.ContractContracts_Search_ErrandTypeName;
            firstRow.Cells[13].Value = Texts.ContractContracts_Search_ContractContractEndDate;

            table.Rows.Add(firstRow);

            foreach (var contractContract in contractContracts)
            {
                var row = new ExportRow();

                row.Cells.Add(contractContract.ContractContractNumber.ToExportCell());
                row.Cells.Add(contractContract.ContractProcurementPlanName.ToExportCell());
                row.Cells.Add(contractContract.ContractDifferentiatedPositions.ToExportCell());
                row.Cells.Add(contractContract.ContractTransName.ToExportCell());
                row.Cells.Add((contractContract.CompanyTransName + (string.IsNullOrWhiteSpace(contractContract.CompanyUin) ? string.Empty : $" ({contractContract.CompanyUin})")).ToExportCell());
                row.Cells.Add(contractContract.CompanyUin.ToExportCell());
                row.Cells.Add(contractContract.CompanyTypeTransName.ToExportCell());
                row.Cells.Add((contractContract.ContractContractorTransName + (string.IsNullOrWhiteSpace(contractContract.ContractContractorUin) ? string.Empty : $" ({contractContract.ContractContractorUin})")).ToExportCell());
                row.Cells.Add(contractContract.ContractContractorUin.ToExportCell());
                row.Cells.Add(contractContract.TotalFundedValue.ToExportCell());
                row.Cells.Add(contractContract.ErrandAreaName.ToExportCell());
                row.Cells.Add(contractContract.ErrandLegalActName.ToExportCell());
                row.Cells.Add(contractContract.ErrandTypeName.ToExportCell());
                row.Cells.Add(contractContract.ContractContractEndDate.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);
            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 150 },
                { 2, 300 },
                { 3, 350 },
                { 4, 200 },
                { 5, 300 },
                { 6, 300 },
                { 7, 200 },
                { 8, 250 },
                { 9, 200 },
                { 10, 200 },
                { 11, 200 },
                { 12, 200 },
                { 13, 200 },
                { 14, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}
