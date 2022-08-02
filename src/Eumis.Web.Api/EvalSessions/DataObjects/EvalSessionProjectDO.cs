using Eumis.Domain.EvalSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionProjectDO
    {
        public EvalSessionProjectDO()
        {
        }

        public EvalSessionProjectDO(EvalSessionProject evalSessionProject)
        {
            this.EvalSessionId = evalSessionProject.EvalSessionId;
            this.ProjectId = evalSessionProject.ProjectId;
            this.IsDeleted = evalSessionProject.IsDeleted;
            this.IsDeletedNote = evalSessionProject.IsDeletedNote;
        }

        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }
    }
}
