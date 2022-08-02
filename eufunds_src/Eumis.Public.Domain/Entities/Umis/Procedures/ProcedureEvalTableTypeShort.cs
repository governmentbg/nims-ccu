using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureEvalTableTypeShort
    {
        [Description("ОАСД")]
        AdminAdmiss = 1,

        [Description("ТФО")]
        TechFinance = 2,

        [Description("КО")]
        Complex = 3
    }
}
