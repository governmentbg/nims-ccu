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
    public partial class AdminProcedureController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public AdminProcedureController(IUmisRepository umisRepository, IMapsRepository mapsRepository, IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        public virtual ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "procedureId",
                    "page",
            })]
        public virtual ActionResult ProcedureProjects(
                string procedureId,
                string page = "")
        {
            int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
            int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

            var vo = this.umisRepository.GetProcedureProjectsStatistics(
                procedureId: int.Parse(procedureId),
                offset: offset,
                limit: Configuration.MaxItemsPerPage);

            ProcedureProjectsStatisticsVM vm = new ProcedureProjectsStatisticsVM();
            vm.ProcedureId = vo.ProcedureId;
            vm.ProcedureName = vo.ProcedureName;
            vm.ProcedureNameAlt = vo.ProcedureNameAlt;
            vm.PageProjects = new StaticPagedList<ProjectStatisticsVO>(vo.PageProjects.Results, innerPage, Configuration.MaxItemsPerPage, vo.PageProjects.Count);

            return this.View(vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            int procedureId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["procedureId"]));

            var vo = this.umisRepository.GetProcedureProjectsStatistics(procedureId, 0, null);

            var projects = vo.PageProjects.Results.ToList();

            var template = new ExportTemplate("projects");
            template.Sheet.Name = "projects";

            var table = new ExportTable(Texts.Admin_ProcedureProjects + " "
                + (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English ? vo.ProcedureNameAlt : vo.ProcedureName));
            var firstRow = new ExportRow();

            for (int i = 0; i < 3; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Admin_ProcedureProjects_Number;
            firstRow.Cells[1].Value = Texts.Admin_ProcedureProjects_Beneficiary;
            firstRow.Cells[2].Value = Texts.Admin_ProcedureProjects_Project;

            table.Rows.Add(firstRow);

            foreach (var project in projects)
            {
                var row = new ExportRow();

                row.Cells.Add(project.UserId.ToExportCell());
                row.Cells.Add(project.BeneficiaryName.ToExportCell());
                row.Cells.Add(project.Name.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 600 },
                { 3, 800 },
            };

            return template;
        }
    }
}