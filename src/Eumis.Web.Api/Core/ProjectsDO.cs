using System.Collections.Generic;

namespace Eumis.Web.Api.Core
{
    public class ProjectsDO
    {
        public ProjectsDO()
        {
        }

        public ProjectsDO(IList<int> projectIds, IList<string> errors)
        {
            this.ProjectIds = projectIds;
            this.Errors = errors;
        }

        public IList<int> ProjectIds { get; set; }

        public IList<string> Errors { get; set; }
    }
}
