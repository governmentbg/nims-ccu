using System.Collections.Generic;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.EvalSessionResult
{
    public class PreliminarySearchVM
    {
        public string EvalSessionResultId { get; set; }

        public string EvalSessionResultType { get; set; }

        public bool ShowRes
        {
            get
            {
                return true;
            }
        }

        public List<string> Errors { get; set; }

        public IPagedList<EvalSessionPreliminaryProjectVO> SearchResults { get; set; }

        public EvalSessionResultVO EvalSessionResult { get; set; }
    }
}
