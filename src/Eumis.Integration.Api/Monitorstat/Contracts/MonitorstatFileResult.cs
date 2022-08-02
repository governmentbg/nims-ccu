using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Integration.Api.Monitorstat.Contracts
{
    public class MonitorstatFileResult
    {
        public string ProcedureIdentifier { get; set; }

        public string SubjectIdentifier { get; set; }

        public UinType SubjectIdentifierType { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public Guid? SubjectRequestGuid { get; set; }
    }
}
