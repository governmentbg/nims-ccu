using NCalc;

namespace Eumis.Domain.Procedures.Validation
{
    internal delegate string DatabaseSerializationParameterHandler(string parameterName, ProcedureProgramme procedureProgrammeContext);

    internal delegate string ValidationParameterHandler(string parameterName, ProcedureProgramme procedureProgrammeContext);

    internal delegate void EvaluationParameterHandler(string name, ParameterArgs args, NCalcEvaluationContext evaluationContext);

    internal interface INCalcExpressionParameter
    {
        string Description { get; }

        string ShortDescription { get; }

        NCalcType Type { get; }

        bool IsGlobalParameter { get; }

        EvaluationParameterHandler EvaluationHandler { get; }

        ValidationParameterHandler ValidationHandler { get; }

        DatabaseSerializationParameterHandler SerializeHandler { get; }

        DatabaseSerializationParameterHandler DeserializeHandler { get; }

        bool MatchName(string name);
    }
}
