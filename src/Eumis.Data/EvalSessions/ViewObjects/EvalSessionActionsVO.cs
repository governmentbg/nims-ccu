using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionActionsVO
    {
        public EvalSessionActionsVO()
        {
        }

        public EvalSessionActionsVO(List<ProcedureEvalTableType> procedureTableTypes)
            : this()
        {
            procedureTableTypes.ForEach(x =>
                {
                    switch (x)
                    {
                        case ProcedureEvalTableType.AdminAdmiss:
                            this.IsAdminAdmiss = true;
                            break;
                        case ProcedureEvalTableType.TechFinance:
                            this.IsTechFinance = true;
                            break;
                        case ProcedureEvalTableType.Complex:
                            this.IsComplex = true;
                            break;
                        default:
                            break;
                    }
                });
        }

        public bool IsPreliminary { get; set; }

        public bool IsAdminAdmiss { get; set; }

        public bool IsTechFinance { get; set; }

        public bool IsComplex { get; set; }
    }
}
