using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammeTableSearchVM
    {
        public string IawpId { get; set; }

        public string IawpType { get; set; }

        public bool ShowRes
        {
            get
            {
                return true;
            }
        }

        public IPagedList<IndicativeAnnualWorkingProgrammeTableVO> SearchResults { get; set; }

        public IndicativeAnnualWorkingProgrammeVO IndicativeAnnualWorkingProgramme { get; set; }
    }
}
