using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureDiscussionsInfoPVO
    {
        public string Name { get; set; }

        public string NameAlt { get; set; }

        public DateTime? ProcedureDiscussionDeadline { get; set; }

        public Guid? QaBlobKey { get; set; }

        public string QaFileName { get; set; }

        public DateTime? QaModifyDate { get; set; }
    }
}
