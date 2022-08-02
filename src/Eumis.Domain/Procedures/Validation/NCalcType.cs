using System.Diagnostics.CodeAnalysis;

namespace Eumis.Domain.Procedures.Validation
{
    [SuppressMessage("", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "Enum values represent types")]
    internal enum NCalcType
    {
        Integer,
        String,
        DateTime,
        Float,
        Boolean,
        NCalcExpressionParameter,
        SystemFunctionUnknown,
    }
}
