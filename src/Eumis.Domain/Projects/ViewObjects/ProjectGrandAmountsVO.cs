using Eumis.Domain.Core;

namespace Eumis.Domain.Projects.ViewObjects
{
    public class ProjectGrandAmountsVO : ProcedureGrandAmountsVO
    {
        public int ProjectId { get; set; }

        public int ProjectVersionXmlId { get; set; }
    }
}
