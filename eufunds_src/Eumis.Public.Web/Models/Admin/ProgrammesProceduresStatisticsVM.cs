using Eumis.Public.Common.Localization;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.Admin
{
    public class ProgrammesProceduresStatisticsVM
    {
        public int? ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public string TransProgrammeName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProgrammeNameAlt;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public bool IsProgrammeChosen
        {
            get
            {
                return this.ProgrammeId.HasValue;
            }
        }

        public IPagedList<ProjectProposalVO> PageProcedures { get; set; }
    }
}