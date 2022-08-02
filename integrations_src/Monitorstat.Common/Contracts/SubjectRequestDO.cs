using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.Contracts
{
    public class SubjectRequestDO
    {
        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public Guid ProcedureInquiryGid { get; set; }

        public FileDO File { get; set; }

        public SubjectRequest[] ToSubjectRequestArray()
        {
            List<SubjectRequest> requests = new List<SubjectRequest>();
            var request = new SubjectRequest(this);
            requests.Add(request);

            return requests.ToArray();
        }
    }
}
