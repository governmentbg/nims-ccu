using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureEvalType
    {
        [Description("Да/Не")]
        Rejection = 1,

        [Description("Точки")]
        Weight = 2
    }
}
