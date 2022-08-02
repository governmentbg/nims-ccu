using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class NewEvalSessionResultDO
    {
        public NewEvalSessionResultDO(int evalSessionId, byte[] version, int orderNum, EvalSessionResultType type)
        {
            this.EvalSessionId = evalSessionId;
            this.Version = version;
            this.OrderNum = orderNum;
            this.Type = type;
            this.CreateDate = DateTime.Now;
            this.Status = EvalSessionResultStatus.Draft;
        }

        public int EvalSessionId { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public EvalSessionResultType Type { get; set; }

        public int OrderNum { get; set; }

        public EvalSessionResultStatus Status { get; set; }
    }
}
