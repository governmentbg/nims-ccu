using NCalc;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation.Abstract
{
    internal abstract class NumeralConditionalFunction : INCalcExpressionFunction
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public NCalcType ReturnType { get; protected set; }

        public List<Type> ExpressionParameterTypes { get; protected set; }

        public List<NCalcFunctionParameter> Parameters { get; protected set; }

        public FunctionHandler FunctionHandler { get; protected set; }

        public EspressionHandler EvaluateExpressionHandler { get; set; }

        protected void InitFields(string name, string description, List<Type> expressionParameterTypes, FunctionHandler evaluate)
        {
            this.Name = name;
            this.Description = description;
            this.FunctionHandler = evaluate;
            this.ExpressionParameterTypes = expressionParameterTypes;
            this.ReturnType = NCalcType.Float;
            this.Parameters = new List<NCalcFunctionParameter>
            {
                new NCalcFunctionParameter("Клон на бюджетното дърво", typeof(SubTreeParameter)),
                new NCalcFunctionParameter("Селектор за елемент от бюджетното дърво", NCalcType.Float),
                new NCalcFunctionParameter("Израз условие за елемент от бюджетното дърво", NCalcType.Boolean),
            };
        }

        protected List<object> EvaluateSelectorConditionParameter(Expression expression, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            var level3DecimalAttrubutePairs = Level3DecimalAttrubuteParameter.GetContextValue(evaluationContext);
            var level3StringAttrubutePairs = Level3StringAttrubuteParameter.GetContextValue(evaluationContext);
            var level3BoolAttrubutePairs = Level3BoolAttrubuteParameter.GetContextValue(evaluationContext);

            Expression exp = new Expression(expression.ParsedExpression, EvaluateOptions.IterateParameters)
            {
                Parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase),
            };

            foreach (var pair in level3DecimalAttrubutePairs)
            {
                exp.Parameters.Add(pair.Key, pair.Value);
            }

            foreach (var pair in level3StringAttrubutePairs)
            {
                exp.Parameters.Add(pair.Key, pair.Value);
            }

            foreach (var pair in level3BoolAttrubutePairs)
            {
                exp.Parameters.Add(pair.Key, pair.Value);
            }

            return (List<object>)this.EvaluateExpressionHandler(exp, procedureProgrammeContext, evaluationContext);
        }

        protected List<bool> EvaluateSubTreeParameter(Expression expression, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            string firstParameterName = this.ExtractParameterName(expression.ParsedExpression.ToString());

            List<bool> subTreeList;
            if (CurrentGroupParameter.MatchParameterAlias(firstParameterName))
            {
                subTreeList = (List<bool>)expression.Evaluate();
            }
            else
            {
                subTreeList = SubTreeParameter.GetContextValue(firstParameterName, procedureProgrammeContext, evaluationContext);
            }

            return subTreeList;
        }

        protected string ExtractParameterName(string parameterName)
        {
            string paramName = parameterName.Trim();
            if (paramName.StartsWith("[") && paramName.EndsWith("]"))
            {
                paramName = paramName.Substring(1, paramName.Length - 2);
            }

            return paramName;
        }
    }
}
