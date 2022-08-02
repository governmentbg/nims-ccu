using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionResultsVO
    {
        public bool CanCreateAdminAdmiss { get; set; }

        public bool CanCreatePreliminary { get; set; }

        public List<EvalSessionResultTablesVO> Tables { get; set; }
    }
}
