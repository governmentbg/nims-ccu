using Eumis.Public.Common.Localization;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Admin
{
    public class ProcedureProjectsStatisticsVM
    {
        public int? ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string TransProcedureName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProcedureNameAlt;
                }
                else
                {
                    return this.ProcedureName;
                }
            }
        }

        public IPagedList<ProjectStatisticsVO> PageProjects { get; set; }
    }
}