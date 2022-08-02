using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Contracts.Repositories;
using Eumis.Public.Data.Contracts.ViewObjects;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;
using Eumis.Public.Web.Models.Charts;
using Eumis.Public.Web.Models.Pies;
using Eumis.Public.Web.Models.Project;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class ProjectController : BaseWithExportController
    {
        private const int FirstYear = 2014;
        private const int LastYear = 2023;

        private IUmisRepository umisRepository;
        private IContractsRepository contractsRepository;
        private INomenclatureRepository nomenclatureRepository;

        private IEnumerable<SelectListItem> years = Enumerable.Range(FirstYear, LastYear - FirstYear + 1).Select(e => new SelectListItem() { Value = e.ToString(), Text = e.ToString() });

        private IDocumentSerializer documentSerializer;

        public ProjectController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IDocumentSerializer documentSerializer,
            IUmisRepository umisRepository,
            IContractsRepository contractsRepository,
            INomenclatureRepository nomenclatureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.documentSerializer = documentSerializer;
            this.umisRepository = umisRepository;
            this.contractsRepository = contractsRepository;
            this.nomenclatureRepository = nomenclatureRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "stFrom",
                    "stTo",
                    "endFrom",
                    "endTo",
                    "prog",
                    "prior",
                    "proc",
                    "ben",
                    "part",
                    "con",
                    "uin",
                    "name",
                    "page",
            })]
        public virtual ActionResult Search(
                string stFrom = "",
                string stTo = "",
                string endFrom = "",
                string endTo = "",
                string prog = "",
                string prior = "",
                string proc = "",
                string ben = "",
                string part = "",
                string con = "",
                string uin = "",
                string name = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            ProjectSearchVM vm = new ProjectSearchVM()
            {
                StFrom = stFrom,
                StTo = stTo,
                EndFrom = endFrom,
                EndTo = endTo,
                Prog = prog,
                Prior = prior,
                Proc = proc,
                Ben = ben,
                Part = part,
                Con = con,
                Uin = uin,
                Name = name,
                ShowRes = showRes,
            };

            this.FillModelValues(ref vm);

            if (vm.ShowRes)
            {
                var emptyCompantVO = new CompanyVO { Uin = null, UinType = null };

                CompanyVO beneficiary = emptyCompantVO,
                    contractor = emptyCompantVO,
                    partner = emptyCompantVO;

                if (!string.IsNullOrWhiteSpace(vm.Ben))
                {
                    if (vm.Ben[0] == 'h')
                    {
                        beneficiary = this.umisRepository.GetContractBeneficiary(int.Parse(vm.Ben.Substring(1)), true);
                    }
                    else
                    {
                        beneficiary = this.umisRepository.GetContractBeneficiary(int.Parse(vm.Ben), false);
                    }
                }

                if (!string.IsNullOrWhiteSpace(vm.Con))
                {
                    if (vm.Con[0] == 'h')
                    {
                        contractor = this.umisRepository.GetContractContractor(int.Parse(vm.Con.Substring(1)), true);
                    }
                    else
                    {
                        contractor = this.umisRepository.GetContractContractor(int.Parse(vm.Con), false);
                    }
                }

                if (!string.IsNullOrWhiteSpace(vm.Part))
                {
                    if (vm.Part[0] == 'h')
                    {
                        partner = this.umisRepository.GetContractPartner(int.Parse(vm.Part.Substring(1)), true);
                    }
                    else
                    {
                        partner = this.umisRepository.GetContractPartner(int.Parse(vm.Part), false);
                    }
                }

                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contracts = this.umisRepository.GetContracts(
                    startDateYearFrom: string.IsNullOrWhiteSpace(vm.StFrom) ? (int?)null : int.Parse(vm.StFrom),
                    startDateYearTo: string.IsNullOrWhiteSpace(vm.StTo) ? (int?)null : int.Parse(vm.StTo),
                    completionDateYearFrom: string.IsNullOrWhiteSpace(vm.EndFrom) ? (int?)null : int.Parse(vm.EndFrom),
                    completionDateYearTo: string.IsNullOrWhiteSpace(vm.EndTo) ? (int?)null : int.Parse(vm.EndTo),
                    programmeId: string.IsNullOrWhiteSpace(vm.Prog) ? (int?)null : int.Parse(vm.Prog),
                    programmePriorityId: string.IsNullOrWhiteSpace(vm.Prior) ? (int?)null : int.Parse(vm.Prior),
                    procedureId: string.IsNullOrWhiteSpace(vm.Proc) ? (int?)null : int.Parse(vm.Proc),
                    companyUin: beneficiary.Uin,
                    companyUinType: beneficiary.UinType,
                    contractorUin: contractor.Uin,
                    contractorUinType: contractor.UinType,
                    partnerUin: partner.Uin,
                    partnerUinType: partner.UinType,
                    searchUin: string.IsNullOrWhiteSpace(vm.Uin) ? null : vm.Uin.Trim(),
                    searchName: string.IsNullOrWhiteSpace(vm.Name) ? null : vm.Name.Trim(),
                    regionNutsLevel: vm.IsRegionSelected ? (NutsLevel?)this.GetPR.NutsLevel : null,
                    regionId: vm.IsRegionSelected ? (int?)this.GetPR.RegionId : null,
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<ContractVO>(contracts.Results, innerPage, Configuration.MaxItemsPerPage, contracts.Count);

                vm.SummarizedSearchResult = contracts.ProjectsSummarizedData;
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Search(ProjectSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (!string.IsNullOrWhiteSpace(vm.StFrom) && !string.IsNullOrWhiteSpace(vm.StTo)
                && int.Parse(vm.StFrom) > int.Parse(vm.StTo))
            {
                this.ModelState.AddModelError("StFrom", string.Format(Texts.Implementation_Index_ValidationDates, Texts.Project_Search_StartDateFrom, Texts.Project_Search_StartDateTo));
            }

            if (!string.IsNullOrWhiteSpace(vm.EndFrom) && !string.IsNullOrWhiteSpace(vm.EndTo)
                && int.Parse(vm.EndFrom) > int.Parse(vm.EndTo))
            {
                this.ModelState.AddModelError("EndFrom", string.Format(Texts.Implementation_Index_ValidationDates, Texts.Project_Search_EndDateFrom, Texts.Project_Search_EndDateTo));
            }

            if (!this.ModelState.IsValid)
            {
                this.FillModelValues(ref vm);

                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            ProjectSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Search, vm);
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

        [HttpPost]
        public virtual JsonResult GetBeneficiary(string id)
        {
            ContractBeneficiaryVO beneficiary = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                if (id[0] == 'h')
                {
                    beneficiary = this.umisRepository.GetContractBeneficiary(int.Parse(id.Substring(1)), true);
                }
                else
                {
                    beneficiary = this.umisRepository.GetContractBeneficiary(int.Parse(id), false);
                }
            }

            if (beneficiary != null)
            {
                return this.Json(new Select2VO { id = beneficiary.Id.ToString(), text = beneficiary.TransName });
            }
            else
            {
                return this.Json(new { id = string.Empty, text = string.Empty });
            }
        }

        [HttpPost]
        public virtual JsonResult GetBeneficiaries(string term)
        {
            var beneficiaries = this.umisRepository.GetSimpleContractBeneficiaries(term).Take(Configuration.MaxNomItems).Select(e => new Select2VO
            {
                id = e.IsHistoric ? $"h{e.Id}" : $"{e.Id}",
                text = e.TransName,
            });

            return this.Json(beneficiaries);
        }

        [HttpPost]
        public virtual JsonResult GetPartner(string id)
        {
            ContractPartnerVO partner = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                if (id[0] == 'h')
                {
                    partner = this.umisRepository.GetContractPartner(int.Parse(id.Substring(1)), true);
                }
                else
                {
                    partner = this.umisRepository.GetContractPartner(int.Parse(id), false);
                }
            }

            if (partner != null)
            {
                return this.Json(new { id = partner.Id, text = partner.TransName });
            }
            else
            {
                return this.Json(new { id = string.Empty, text = string.Empty });
            }
        }

        [HttpPost]
        public virtual JsonResult GetPartners(string term)
        {
            var partners = this.umisRepository.GetSimpleContractPartners(term).Take(Configuration.MaxNomItems).Select(e => new Select2VO
            {
                id = e.IsHistoric ? $"h{e.Id}" : $"{e.Id}",
                text = e.TransName,
            });

            return this.Json(partners);
        }

        [HttpPost]
        public virtual JsonResult GetContractor(string id)
        {
            ContractContractorVO contractor = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                if (id[0] == 'h')
                {
                    contractor = this.umisRepository.GetContractContractor(int.Parse(id.Substring(1)), true);
                }
                else
                {
                    contractor = this.umisRepository.GetContractContractor(int.Parse(id), false);
                }
            }

            if (contractor != null)
            {
                return this.Json(new { id = contractor.Id, text = contractor.TransName });
            }
            else
            {
                return this.Json(new { id = string.Empty, text = string.Empty });
            }
        }

        [HttpPost]
        public virtual JsonResult GetContractors(string term)
        {
            var contractors = this.umisRepository.GetSimpleContractContractors(term).Take(Configuration.MaxNomItems).Select(e => new Select2VO
            {
                id = e.IsHistoric ? $"h{e.Id}" : $"{e.Id}",
                text = e.TransName,
            });

            return this.Json(contractors);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult BasicData(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractBasicData(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult Activities(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractActivities(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult Procurements(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractProcurements(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult Participants(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractParticipants(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult FinancialInformation(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractFinancialInformation(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult Indicators(string contractId, bool isHistoric = false)
        {
            var model = this.contractsRepository.GetContractIndicators(int.Parse(contractId), isHistoric);
            return this.View(model);
        }

        public override ExportTemplate RenderTemplate()
        {
            int? startDateYearFrom = null;
            int? startDateYearTo = null;
            int? completionDateYearFrom = null;
            int? completionDateYearTo = null;
            int? programmeId = null;
            int? programmePriorityId = null;
            int? procedureId = null;
            string searchUin = null;
            string searchName = null;

            bool isHistoric = false;

            var emptyCompanyVO = new CompanyVO
            {
                Uin = null,
                UinType = null,
            };

            CompanyVO beneficiary = emptyCompanyVO;
            CompanyVO contractor = emptyCompanyVO;
            CompanyVO partner = emptyCompanyVO;
            NutsLevel? regionNutsLevel = null;
            int? regionId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["stFrom"]))
            {
                startDateYearFrom = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["stFrom"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["stTo"]))
            {
                startDateYearTo = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["stTo"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["endFrom"]))
            {
                completionDateYearFrom = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["endFrom"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["endTo"]))
            {
                completionDateYearTo = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["endTo"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["prog"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["prog"]));
            }
            else if (this.GetOP != null && this.GetOP.MapNodeId != 0)
            {
                programmeId = this.GetOP.MapNodeId;
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["prior"]))
            {
                programmePriorityId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["prior"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["proc"]))
            {
                procedureId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["proc"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["isHistoric"]))
            {
                isHistoric = Convert.ToBoolean(this.Request.QueryString["isHistoric"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["ben"]))
            {
                beneficiary = this.umisRepository.GetContractBeneficiary(int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["ben"])), isHistoric);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["con"]))
            {
                contractor = this.umisRepository.GetContractContractor(int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["con"])), isHistoric);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["part"]))
            {
                partner = this.umisRepository.GetContractPartner(int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["part"])), isHistoric);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["uin"]))
            {
                searchUin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["uin"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["name"]))
            {
                searchName = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["name"]);
            }

            if (this.GetPR != null)
            {
                regionNutsLevel = (NutsLevel?)this.GetPR.NutsLevel;
                regionId = (int?)this.GetPR.RegionId;
            }

            var contracts = this.umisRepository.GetContracts(
                startDateYearFrom,
                startDateYearTo,
                completionDateYearFrom,
                completionDateYearTo,
                programmeId,
                programmePriorityId,
                procedureId,
                beneficiary.Uin,
                beneficiary.UinType,
                contractor.Uin,
                contractor.UinType,
                null,
                null,
                null,
                null,
                partner.Uin,
                partner.UinType,
                searchUin,
                searchName,
                regionNutsLevel,
                regionId,
                0,
                null);

            var template = new ExportTemplate("projects");
            template.Sheet.Name = "projects";
            if (contracts.Results != null && contracts.Count > 0)
            {
                var table = new ExportTable(Texts.Project_Search_Projects);
                var headerRow = new ExportRow();

                for (int i = 0; i < 10; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Project_Search_Beneficiary;
                headerRow.Cells[1].Value = Texts.Company_Index_Seat;
                headerRow.Cells[2].Value = Texts.Project_Search_Location;
                headerRow.Cells[3].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[4].Value = Texts.Company_Index_TotalAmount;
                headerRow.Cells[5].Value = Texts.Company_Index_BFP;
                headerRow.Cells[6].Value = Texts.Project_Search_FinancinBeneficiary;
                headerRow.Cells[7].Value = Texts.Global_Payed;
                headerRow.Cells[8].Value = Texts.Company_Index_Duration;
                headerRow.Cells[9].Value = Texts.Company_Index_BFPContractStatus;

                table.Rows.Add(headerRow);

                foreach (var contract in contracts.Results)
                {
                    var row = new ExportRow();

                    row.Cells.Add(contract.TransCompanyFullName.ToExportCell());
                    row.Cells.Add(contract.Seat.ToExportCell());
                    row.Cells.Add(contract.TransNutsFullPathNames.ToList().ToExportCell());
                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.ContractedTotalAmount.ToExportCell());
                    row.Cells.Add(contract.ContractedBFPAmount.ToExportCell());
                    row.Cells.Add(contract.ContractedSelfAmount.ToExportCell());
                    row.Cells.Add(contract.PaidTotalAmount.ToExportCell());
                    row.Cells.Add(contract.MonthsDuration.ToExportCell());
                    row.Cells.Add(contract.StatusDescription.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 300 },
                    { 2, 300 },
                    { 3, 300 },
                    { 4, 300 },
                    { 5, 150 },
                    { 6, 150 },
                    { 7, 200 },
                    { 8, 150 },
                    { 9, 200 },
                    { 10, 350 },
                };
            }

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);
            template.Sheet.EndNotes.Add(Texts.Project_Search_ProjectCostNote);
            template.Sheet.EndNotes.Add(Texts.Project_Search_ActuallyPaidAmountsNote);

            return template;
        }

        [DecryptParameters(IdsParamName = new[] { "contractId" })]
        public virtual ActionResult PrintProject(string contractId, bool isHistoric = false)
        {
            var id = int.Parse(contractId);

            var model = new PrintVO();
            model.ContractBasicData = this.contractsRepository.GetContractBasicData(id, isHistoric);
            model.ContractActivities = this.contractsRepository.GetContractActivities(id, isHistoric);
            model.ContractProcurements = this.contractsRepository.GetContractProcurements(id, isHistoric);
            model.ContractParticipants = this.contractsRepository.GetContractParticipants(id, isHistoric);
            model.ContractFinancialInformation = this.contractsRepository.GetContractFinancialInformation(id, isHistoric);
            model.ContractIndicators = this.contractsRepository.GetContractIndicators(id, isHistoric);
            return this.View(model);
        }

        public virtual ActionResult GetContractedFundsPieData(int contractId)
        {
            var pm = new PieModel();
            var data = new List<PieDataModel>();
            var fundsData = this.umisRepository.GetContractedFundsByAidMode(contractId);
            var total = fundsData.TotalAmount;
            var bfpTotal = fundsData.TotalBfpAmount;

            data.Add(new PieDataModel
            {
                Name = $"{Texts.Project_Details_Pie_Deminimis} ({Math.Round(fundsData.DeminimisAmount / bfpTotal * 100, 2)}%)",
                Value = DataUtils.DecimalToStringDecimalPointSpace(fundsData.DeminimisAmount),
                Y = Math.Round(fundsData.DeminimisAmount / total * 100, 2),
            });

            data.Add(new PieDataModel
            {
                Name = $"{Texts.Project_Details_Pie_StateAid} ({Math.Round(fundsData.StateAidAmount / bfpTotal * 100, 2)}%)",
                Value = DataUtils.DecimalToStringDecimalPointSpace(fundsData.StateAidAmount),
                Y = Math.Round(fundsData.StateAidAmount / total * 100, 2),
            });

            data.Add(new PieDataModel
            {
                Name = $"{Texts.Project_Details_Pie_Other} ({Math.Round(fundsData.OtherAmount / bfpTotal * 100, 2)}%)",
                Value = DataUtils.DecimalToStringDecimalPointSpace(fundsData.OtherAmount),
                Y = Math.Round(fundsData.OtherAmount / total * 100, 2),
            });

            data.Add(new PieDataModel
            {
                Name = Texts.Project_Details_Pie_Self,
                Value = DataUtils.DecimalToStringDecimalPointSpace(fundsData.SelfAmount),
                Y = Math.Round(fundsData.SelfAmount / total * 100, 2),
            });

            pm.Data = data;
            pm.ColorByPoint = true;

            return new JsonPieResult(pm);
        }

        public virtual ActionResult GetPaidAmountsChartData(int contractId)
        {
            ChartModel cm = new ChartModel();

            var wrapper = this.umisRepository.GetPaidAmountsByYear(contractId);

            var data = new List<ChartDataModel>();

            data.Add(new ChartDataModel { Name = Texts.Project_Details_Chart_PaidEuAmount, Data = wrapper.Select(f => f.PaidEuAmount) });
            data.Add(new ChartDataModel { Name = Texts.Project_Details_Chart_PaidBgAmount, Data = wrapper.Select(f => f.PaidBgAmount) });
            data.Add(new ChartDataModel { Name = Texts.Project_Details_Chart_ContractedAmount, Data = wrapper.Select(f => f.ContractedAmount), Type = "spline" });

            cm.Categories = wrapper.Select(f => f.Year.ToString());
            cm.Data = data;

            return new JsonChartResult(cm);
        }

        private void FillModelValues(ref ProjectSearchVM vm)
        {
            if (vm == null)
            {
                vm = new ProjectSearchVM();
            }

            vm.IsProgrammeSelected = this.OpId != Common.Configuration.OP_DEFAULT_ID;

            if (vm.IsProgrammeSelected)
            {
                vm.Prog = this.OpId.ToString();
            }

            vm.IsRegionSelected = this.PrId != Common.Configuration.PR_DEFAULT_ID;

            if (vm.IsRegionSelected)
            {
                vm.Reg = this.PrId.ToString();
            }

            vm.Years = this.years;
        }
    }
}
