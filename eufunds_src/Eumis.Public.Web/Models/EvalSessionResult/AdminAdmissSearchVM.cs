using System.Collections.Generic;
using Eumis.Public.Data.UmisVOs;
using PagedList;

namespace Eumis.Public.Web.Models.EvalSessionResult
{
    public class AdminAdmissSearchVM
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

        public IPagedList<EvalSessionAdminAdmissProjectVO> SearchResults { get; set; }

        public EvalSessionResultVO EvalSessionResult { get; set; }
    }
}
