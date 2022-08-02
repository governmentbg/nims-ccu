using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionLoadedProjectsFromFileVO
    {
        public EvalSessionLoadedProjectsFromFileVO()
        {
        }

        public EvalSessionLoadedProjectsFromFileVO(IList<int> projectIds, IList<string> errors)
        {
            this.ProjectIds = projectIds;
            this.Errors = errors;
        }

        public IList<int> ProjectIds { get; set; }

        public IList<string> Errors { get; set; }
    }
}
