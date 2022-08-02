using Eumis.Documents.Contracts;
using PagedList;
using System;
using System.Collections.Generic;

namespace Eumis.Portal.Web.Models.ProcedureDiscussion
{
    public class ProcedureDiscussionsInfoVM
    {
        public Guid? Id { get; set; }

        public List<ContractProcedureDiscussionsInfo> Questions { get; set; }


    }
}
