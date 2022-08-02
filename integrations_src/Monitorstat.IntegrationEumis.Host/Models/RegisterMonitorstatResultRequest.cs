using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Models
{
    [DataContract]
    public partial class RegisterMonitorstatResultRequest
    {
        [DataMember]
        public string ProcedureIdentifier { get; set; }

        [DataMember]
        public string SubjectIdentifier { get; set; }

        [DataMember]
        public IdentifierType SubjectIdentifierType { get; set; }

        [DataMember]
        public File File { get; set; }
    }
}
