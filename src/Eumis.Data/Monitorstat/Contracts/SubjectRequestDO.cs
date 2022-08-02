using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.Contracts
{
    public class SubjectRequestDO
    {
        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public Guid ProcedureInquiryGid { get; set; }

        public FileDO File { get; set; }
    }
}
