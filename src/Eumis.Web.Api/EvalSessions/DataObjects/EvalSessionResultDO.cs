using Eumis.Common.Json;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionResultDO
    {
        public EvalSessionResultDO()
        {
        }

        public EvalSessionResultDO(EvalSessionResult result, byte[] evalSessionVersion)
        {
            this.CreateDate = result.CreateDate;
            this.EvalSessionResultId = result.EvalSessionResultId;
            this.EvalSessionId = result.EvalSessionId;
            this.OrderNum = result.OrderNum;
            this.PublicationDate = result.PublicationDate;
            this.Status = result.Status;
            this.Type = result.Type;
            this.StatusNote = result.StatusNote;
            this.UserId = result.PublicationUserId;
            this.Version = evalSessionVersion;
        }

        public EvalSessionResultDO(EvalSessionResult result, byte[] evalSessionVersion, IList<ProcedureEvalTable> procedureTables)
            : this(result, evalSessionVersion)
        {
            var adminAdmissTable = procedureTables.Where(x => x.Type == ProcedureEvalTableType.AdminAdmiss).FirstOrDefault();
            var techFinanceTable = procedureTables.Where(x => x.Type == ProcedureEvalTableType.TechFinance).FirstOrDefault();
            var complexTable = procedureTables.Where(x => x.Type == ProcedureEvalTableType.Complex).FirstOrDefault();

            this.PreliminaryEvalType = null;

            this.AdminAdmissEvalType = adminAdmissTable?.EvalType;

            this.TechFinanceEvalType = techFinanceTable?.EvalType;

            this.ComplexEvalType = complexTable?.EvalType;
        }

        public int EvalSessionResultId { get; set; }

        public int EvalSessionId { get; set; }

        public int OrderNum { get; set; }

        public EvalSessionResultStatus Status { get; set; }

        public EvalSessionResultType Type { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UserId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public byte[] Version { get; set; }

        public ProcedureEvalType? PreliminaryEvalType { get; private set; }

        public ProcedureEvalType? AdminAdmissEvalType { get; private set; }

        public ProcedureEvalType? TechFinanceEvalType { get; private set; }

        public ProcedureEvalType? ComplexEvalType { get; private set; }
    }
}
