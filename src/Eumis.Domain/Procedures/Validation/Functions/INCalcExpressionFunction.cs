using NCalc;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation
{
    internal delegate void FunctionHandler(FunctionArgs args, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext);

    internal delegate object EspressionHandler(Expression expression, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext);

    internal interface INCalcExpressionFunction
    {
        string Name { get; }

        string Description { get; }

        NCalcType ReturnType { get; }

        List<NCalcFunctionParameter> Parameters { get; }

        List<Type> ExpressionParameterTypes { get; }

        FunctionHandler FunctionHandler { get; }

        EspressionHandler EvaluateExpressionHandler { get; set; }
    }
}
