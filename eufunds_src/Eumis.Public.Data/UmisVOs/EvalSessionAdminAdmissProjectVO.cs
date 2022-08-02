using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Resources;

namespace Eumis.Public.Data.UmisVOs
{
    public class EvalSessionAdminAdmissProjectVO : EvalSessionResultProjectVO
    {
        public string NonAdmissionReason { get; set; }

        public EvalSessionAdminAdmissEvaluation AdminAdmissResult { get; set; }

        public string TransAdminAdmissResult
        {
            get
            {
                return this.AdminAdmissResult == EvalSessionAdminAdmissEvaluation.Passed ? Texts.EvalSessionResult_AdminAdmiss_PassedASD : Texts.EvalSessionResult_AdminAdmiss_NotPassedASD;
            }
        }
    }
}
