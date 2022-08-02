using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureSpecFieldMaxLength
    {
        [Description("Малка (1000 символа)")]
        Small = 1000,

        [Description("Средна (3000 символа)")]
        Middle = 3000,

        [Description("Голяма (5000 символа)")]
        Large = 5000,

        [Description("Много голяма (10000 символа)")]
        VeryLarge = 10000
    }
}
