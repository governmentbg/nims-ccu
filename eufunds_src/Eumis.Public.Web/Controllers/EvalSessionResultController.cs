using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Model.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.EvalSessionResult;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class EvalSessionResultController : BaseWithExportController
    {
        private IUmisRepository umisRepository;
        private INomenclatureRepository nomenclatureRepository;

        public EvalSessionResultController(
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
                    "procedureId",
                    "resultType",
                    "page",
            })]
        public virtual ActionResult Index(
                string programmeId = "",
                string procedureId = "",
                string resultType = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            EvalSessionResultSearchVM vm = new EvalSessionResultSearchVM()
            {
                ProgrammeId = programmeId,
                ProcedureId = procedureId,
                ResultType = resultType,
                ShowRes = showRes,
                Errors = new List<string>(),
            };

            if (string.IsNullOrEmpty(procedureId) && vm.ShowRes)
            {
                vm.ShowRes = false;
                vm.Errors.Add(Texts.EvalSessionResult_AdminAdmiss_RequiredProcedure);
            }

            if (string.IsNullOrEmpty(resultType) && vm.ShowRes)
            {
                vm.ShowRes = false;
                vm.Errors.Add(Texts.EvalSessionResult_AdminAdmiss_RequiredResultType);
            }

            if (vm.ShowRes)
            {
                vm.SearchResults = this.umisRepository.GetEvalSessionResults(
                    procedureId: int.Parse(vm.ProcedureId),
                    resultType: (EvalSessionResultType)int.Parse(vm.ResultType));
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(EvalSessionResultSearchVM vm)
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

            EvalSessionResultSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
           new string[]
           {
                "resultId",
                "resultType",
                "page",
           })]
        public virtual ActionResult Show(
               string resultId = "",
               string resultType = "",
               string page = "")
        {
            this.ModelState.Clear();

            int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
            int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;
            EvalSessionResultType type = (EvalSessionResultType)int.Parse(resultType);

            int evalSessionResultId = int.Parse(resultId);

            EvalSessionResultVO evalSessionResult = this.umisRepository.GetEvalSessionResult(evalSessionResultId);

            if (type == EvalSessionResultType.AdminAdmiss)
            {
                var projects = this.umisRepository.GetEvaluatedProjectsADS(
                resultId: evalSessionResultId,
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

                AdminAdmissSearchVM vm = new AdminAdmissSearchVM();
                vm.EvalSessionResultId = resultId;
                vm.EvalSessionResultType = resultType;
                vm.EvalSessionResult = evalSessionResult;
                vm.SearchResults = new StaticPagedList<EvalSessionAdminAdmissProjectVO>(projects.Results, innerPage, Configuration.MaxItemsPerPage, projects.Count);
                return this.View(MVC.EvalSessionResult.Views.AdminAdmiss, vm);
            }
            else if (type == EvalSessionResultType.Preliminary)
            {
                var projects = this.umisRepository.GetPreliminaryProjects(
                resultId: evalSessionResultId,
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

                PreliminarySearchVM vm = new PreliminarySearchVM();
                vm.EvalSessionResultId = resultId;
                vm.EvalSessionResultType = resultType;
                vm.EvalSessionResult = evalSessionResult;
                vm.SearchResults = new StaticPagedList<EvalSessionPreliminaryProjectVO>(projects.Results, innerPage, Configuration.MaxItemsPerPage, projects.Count);
                return this.View(MVC.EvalSessionResult.Views.Preliminary, vm);
            }
            else if (type == EvalSessionResultType.Standing)
            {
                var projects = this.umisRepository.GetStandingProjects(
                resultId: evalSessionResultId,
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

                StandingSearchVM vm = new StandingSearchVM();
                vm.EvalSessionResultId = resultId;
                vm.EvalSessionResultType = resultType;
                vm.EvalSessionResult = evalSessionResult;
                vm.SearchResults = new StaticPagedList<EvalSessionStandingProjectVO>(projects.Results, innerPage, Configuration.MaxItemsPerPage, projects.Count);
                return this.View(MVC.EvalSessionResult.Views.Standing, vm);
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

            return this.Json(this.nomenclatureRepository.GetProceduresWithProjectEvaluation(term, int.Parse(parentId)));
        }

        [HttpPost]
        public virtual JsonResult GetResultType(EvalSessionResultType id)
        {
            return this.Json(new Select2VO() { id = ((int)id).ToString(), text = id.GetEnumDescription() });
        }

        [HttpPost]
        public virtual JsonResult GetResultTypes(string term, string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return this.Json(new List<object>());
            }

            return this.Json(this.nomenclatureRepository.GetProcedureResultTypes(term, int.Parse(parentId)));
        }

        public override ExportTemplate RenderTemplate()
        {
            int? resultId = null;
            EvalSessionResultType? resultType = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["resultId"]))
            {
                resultId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["resultId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["resultType"]))
            {
                resultType = (EvalSessionResultType)int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["resultType"]));
            }

            if (!resultId.HasValue || !resultType.HasValue)
            {
                return new ExportTemplate();
            }

            EvalSessionResultVO evalSessionResult = this.umisRepository.GetEvalSessionResult(resultId.Value);

            if (resultType == EvalSessionResultType.AdminAdmiss)
            {
                var projects = this.umisRepository.GetEvaluatedProjectsADS(resultId.Value).Results;
                return this.CreateASDTemplate(projects, evalSessionResult);
            }
            else if (resultType == EvalSessionResultType.Preliminary)
            {
                var projects = this.umisRepository.GetPreliminaryProjects(resultId.Value).Results;
                return this.CreatePreliminaryTemplate(projects, evalSessionResult);
            }
            else if (resultType == EvalSessionResultType.Standing)
            {
                var projects = this.umisRepository.GetStandingProjects(resultId.Value).Results;
                return this.CreateStandingTemplate(projects, evalSessionResult);
            }

            return new ExportTemplate();
        }

        private ExportTemplate CreateASDTemplate(IList<EvalSessionAdminAdmissProjectVO> projects, EvalSessionResultVO evalSessionResult)
        {
            var template = new ExportTemplate("ProjectProposals");
            template.Sheet.Name = "projects";

            if (projects != null && projects.Count > 0)
            {
                var title = string.Format(Texts.EvalSessionResult_Index_Template, evalSessionResult.TransProcedureName, evalSessionResult.Type.GetEnumDescription(), evalSessionResult.EvalSessionNum);
                var table = new ExportTable(title);
                var headerRow = new ExportRow();

                for (int i = 0; i < 7; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.EvalSessionResult_AdminAdmiss_ProjectCode;
                headerRow.Cells[1].Value = Texts.EvalSessionResult_AdminAdmiss_ProjectNamе;
                headerRow.Cells[2].Value = Texts.EvalSessionResult_AdminAdmiss_ProjectRegDate;
                headerRow.Cells[3].Value = Texts.EvalSessionResult_AdminAdmiss_CompanyName;
                headerRow.Cells[4].Value = Texts.EvalSessionResult_AdminAdmiss_CompanyUin;
                headerRow.Cells[5].Value = Texts.EvalSessionResult_AdminAdmiss_AdminAdmissResult;
                headerRow.Cells[6].Value = Texts.EvalSessionResult_AdminAdmiss_NonAdmissionReason;

                table.Rows.Add(headerRow);

                foreach (var project in projects)
                {
                    var row = new ExportRow();
                    var companyId = project.CompanyUinType != UinType.PersonalBulstat ? $"{project.CompanyUinType.GetEnumDescription()}: {project.CompanyUin}" : string.Empty;

                    row.Cells.Add(project.RegNumber.ToExportCell());
                    row.Cells.Add(project.TransName.ToExportCell());
                    row.Cells.Add(project.RegDate.ToExportCell());
                    row.Cells.Add(project.TransCompanyName.ToExportCell());
                    row.Cells.Add(companyId.ToExportCell());
                    row.Cells.Add(project.TransAdminAdmissResult.ToExportCell());
                    row.Cells.Add(project.NonAdmissionReason.ToExportCell(project.AdminAdmissResult == EvalSessionAdminAdmissEvaluation.NotPassed));

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 300 },
                    { 2, 300 },
                    { 3, 100 },
                    { 4, 150 },
                    { 5, 100 },
                    { 6, 200 },
                    { 7, 300 },
                };
            }

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private ExportTemplate CreatePreliminaryTemplate(IList<EvalSessionPreliminaryProjectVO> projects, EvalSessionResultVO evalSessionResult)
        {
            var template = new ExportTemplate("ProjectProposals");
            template.Sheet.Name = "projects";

            if (projects != null && projects.Count > 0)
            {
                var title = string.Format(Texts.EvalSessionResult_Index_Template, evalSessionResult.TransProcedureName, evalSessionResult.Type.GetEnumDescription(), evalSessionResult.EvalSessionNum);
                var table = new ExportTable(title);
                var headerRow = new ExportRow();

                for (int i = 0; i < 11; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.EvalSessionResult_Preliminary_ProjectCode;
                headerRow.Cells[1].Value = Texts.EvalSessionResult_Preliminary_ProjectNamе;
                headerRow.Cells[2].Value = Texts.EvalSessionResult_Preliminary_ProjectRegDate;
                headerRow.Cells[3].Value = Texts.EvalSessionResult_Preliminary_CompanyName;
                headerRow.Cells[4].Value = Texts.EvalSessionResult_Preliminary_CompanyUin;
                headerRow.Cells[5].Value = Texts.EvalSessionResult_Preliminary_GrantAmount;
                headerRow.Cells[6].Value = Texts.EvalSessionResult_Preliminary_SelfAmount;
                headerRow.Cells[7].Value = Texts.EvalSessionResult_Preliminary_Assessment;
                headerRow.Cells[8].Value = Texts.EvalSessionResult_Preliminary_StandingOrder;
                headerRow.Cells[9].Value = Texts.EvalSessionResult_Preliminary_StandingStatus;
                headerRow.Cells[10].Value = Texts.EvalSessionResult_Preliminary_Info;

                table.Rows.Add(headerRow);

                foreach (var project in projects)
                {
                    var companyId = project.CompanyUinType != UinType.PersonalBulstat ? $"{project.CompanyUinType.GetEnumDescription()}: {project.CompanyUin}" : string.Empty;
                    string orderNum = project.OrderNum.HasValue ? project.OrderNum.ToString() : string.Empty;

                    var row = new ExportRow();

                    row.Cells.Add(project.RegNumber.ToExportCell());
                    row.Cells.Add(project.TransName.ToExportCell());
                    row.Cells.Add(project.RegDate.ToExportCell());
                    row.Cells.Add(project.TransCompanyName.ToExportCell());
                    row.Cells.Add(companyId.ToExportCell());
                    row.Cells.Add(project.GrantAmount.Value.ToExportCell());
                    row.Cells.Add(project.SelfAmount.Value.ToExportCell());
                    row.Cells.Add(project.PreliminaryResult.ToExportCell());
                    row.Cells.Add(orderNum.ToExportCell());
                    row.Cells.Add(project.Status.ToExportCell());
                    row.Cells.Add(project.Note.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 300 },
                    { 2, 300 },
                    { 3, 100 },
                    { 4, 150 },
                    { 5, 100 },
                    { 6, 100 },
                    { 7, 100 },
                    { 8, 150 },
                    { 9, 100 },
                    { 10, 150 },
                    { 11, 150 },
                };
            }

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private ExportTemplate CreateStandingTemplate(IList<EvalSessionStandingProjectVO> projects, EvalSessionResultVO evalSessionResult)
        {
            var template = new ExportTemplate("ProjectProposals");
            template.Sheet.Name = "projects";

            if (projects != null && projects.Count > 0)
            {
                var title = string.Format(Texts.EvalSessionResult_Index_Template, evalSessionResult.TransProcedureName, evalSessionResult.Type.GetEnumDescription(), evalSessionResult.EvalSessionNum);
                var table = new ExportTable(title);
                var headerRow = new ExportRow();

                for (int i = 0; i < 20; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.EvalSessionResult_Standing_ProjectCode;
                headerRow.Cells[1].Value = Texts.EvalSessionResult_Standing_ProjectNamе;
                headerRow.Cells[2].Value = Texts.EvalSessionResult_Standing_ProjectRegDate;
                headerRow.Cells[3].Value = Texts.EvalSessionResult_Standing_CompanyName;
                headerRow.Cells[4].Value = Texts.EvalSessionResult_Standing_CompanyUin;
                headerRow.Cells[5].Value = Texts.EvalSessionResult_Standing_GrantAmount;
                headerRow.Cells[6].Value = Texts.EvalSessionResult_Standing_SelfAmount;
                headerRow.Cells[7].Value = Texts.EvalSessionResult_Standing_PreliminaryResult;
                headerRow.Cells[8].Value = Texts.EvalSessionResult_Standing_PreliminaryPoints;
                headerRow.Cells[9].Value = Texts.EvalSessionResult_Standing_ASDResult;
                headerRow.Cells[10].Value = Texts.EvalSessionResult_Standing_ASDResult;
                headerRow.Cells[11].Value = Texts.EvalSessionResult_Standing_TFOResult;
                headerRow.Cells[12].Value = Texts.EvalSessionResult_Standing_TFOPoints;
                headerRow.Cells[13].Value = Texts.EvalSessionResult_Standing_ComplexResult;
                headerRow.Cells[14].Value = Texts.EvalSessionResult_Standing_ComplexPoints;
                headerRow.Cells[15].Value = Texts.EvalSessionResult_Standing_StandingOrder;
                headerRow.Cells[16].Value = Texts.EvalSessionResult_Standing_StandingStatus;
                headerRow.Cells[17].Value = Texts.EvalSessionResult_Standing_CorrectedGrantAmount;
                headerRow.Cells[18].Value = Texts.EvalSessionResult_Standing_CorrectedSelfAmount;
                headerRow.Cells[19].Value = Texts.EvalSessionResult_Standing_Info;

                table.Rows.Add(headerRow);

                foreach (var project in projects)
                {
                    var companyId = project.CompanyUinType != UinType.PersonalBulstat ? $"{project.CompanyUinType.GetEnumDescription()}: {project.CompanyUin}" : string.Empty;
                    string orderNum = project.OrderNum.HasValue ? project.OrderNum.ToString() : string.Empty;

                    var row = new ExportRow();

                    row.Cells.Add(project.RegNumber.ToExportCell());
                    row.Cells.Add(project.TransName.ToExportCell());
                    row.Cells.Add(project.RegDate.ToExportCell());
                    row.Cells.Add(project.TransCompanyName.ToExportCell());
                    row.Cells.Add(companyId.ToExportCell());

                    row.Cells.Add(project.GrantAmount.Value.ToExportCell());
                    row.Cells.Add(project.SelfAmount.Value.ToExportCell());

                    row.Cells.Add(project.IsPassedPreliminaryText.ToExportCell(project.IsPassedPreliminary.HasValue));
                    row.Cells.Add(project.PointsPreliminary.ToExportCell(project.IsPassedPreliminary.HasValue));

                    row.Cells.Add(project.IsPassedASDText.ToExportCell(project.IsPassedASD.HasValue));
                    row.Cells.Add(project.PointsASD.ToExportCell(project.IsPassedASD.HasValue));

                    row.Cells.Add(project.IsPassedTFOText.ToExportCell(project.IsPassedTFO.HasValue));
                    row.Cells.Add(project.PointsTFO.ToExportCell(project.IsPassedTFO.HasValue));

                    row.Cells.Add(project.IsPassedComplexText.ToExportCell(project.IsPassedComplex.HasValue));
                    row.Cells.Add(project.PointsComplex.ToExportCell(project.IsPassedComplex.HasValue));

                    row.Cells.Add(orderNum.ToExportCell());
                    row.Cells.Add(project.Status.ToExportCell());

                    row.Cells.Add(project.GrantAmount.Value.ToExportCell());
                    row.Cells.Add(project.SelfAmount.Value.ToExportCell());

                    row.Cells.Add(project.Note.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 300 },
                    { 2, 300 },
                    { 3, 100 },
                    { 4, 150 },
                    { 5, 100 },
                    { 6, 100 },
                    { 7, 100 },
                    { 8, 150 },
                    { 9, 100 },
                    { 10, 150 },
                    { 11, 150 },
                    { 12, 150 },
                    { 13, 150 },
                    { 14, 150 },
                    { 15, 150 },
                    { 16, 150 },
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
