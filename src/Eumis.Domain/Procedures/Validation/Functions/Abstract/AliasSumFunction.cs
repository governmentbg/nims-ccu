using NCalc;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation.Abstract
{
    internal abstract class AliasSumFunction : INCalcExpressionFunction
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public NCalcType ReturnType { get; protected set; }

        public List<Type> ExpressionParameterTypes { get; protected set; }

        public Level3DecimalAttrubuteParameter.ParameterType Level3ParameterType { get; protected set; }

        public List<NCalcFunctionParameter> Parameters { get; protected set; }

        public FunctionHandler FunctionHandler { get; protected set; }

        public EspressionHandler EvaluateExpressionHandler { get; set; }

        protected void InitFields(string name, string description, List<Type> expressionParameterTypes, Level3DecimalAttrubuteParameter.ParameterType level3ParameterType)
        {
            this.Name = name;
            this.Description = description;
            this.FunctionHandler = this.Evaluate;
            this.ExpressionParameterTypes = expressionParameterTypes;
            this.ReturnType = NCalcType.Float;
            this.Parameters = new List<NCalcFunctionParameter>
            {
                new NCalcFunctionParameter("Клон на бюджетното дърво", typeof(SubTreeParameter)),
            };
            this.Level3ParameterType = level3ParameterType;
        }

        private void Evaluate(FunctionArgs args, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            List<bool> subTreeList = this.EvaluateSubTreeParameter(args.Parameters[0], procedureProgrammeContext, evaluationContext);

            List<decimal> selectorList = Level3DecimalAttrubuteParameter.GetExactParameterValue(this.Level3ParameterType, evaluationContext);

            decimal sum = 0;
            for (int i = 0; i < subTreeList.Count; i++)
            {
                if (subTreeList[i])
                {
                    sum += selectorList[i];
                }
            }

            args.Result = sum;
        }

        protected List<bool> EvaluateSubTreeParameter(Expression expression, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            string parameter = this.ExtractParameterName(expression.ParsedExpression.ToString());

            List<bool> subTreeList;
            if (CurrentGroupParameter.MatchParameterAlias(parameter))
            {
                subTreeList = (List<bool>)expression.Evaluate();
            }
            else
            {
                subTreeList = SubTreeParameter.GetContextValue(parameter, procedureProgrammeContext, evaluationContext);
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
