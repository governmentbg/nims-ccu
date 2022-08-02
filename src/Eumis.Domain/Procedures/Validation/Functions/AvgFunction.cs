using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class AvgFunction : NumeralConditionalFunction
    {
        public AvgFunction()
        {
            this.InitFields(
                "Avg",
                "Връща средно аритметичната стойност на селектора, от всички елементи на бюджета, отговарящи на условието.",
                new List<Type>
                {
                    typeof(SubTreeParameter),
                    typeof(Level3DecimalAttrubuteParameter),
                    typeof(Level3StringAttrubuteParameter),
                    typeof(Level3BoolAttrubuteParameter),
                    typeof(CurrentGroupParameter),
                },
                this.Evaluate);
        }

        private void Evaluate(FunctionArgs args, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            List<bool> subTreeList = this.EvaluateSubTreeParameter(args.Parameters[0], procedureProgrammeContext, evaluationContext);

            List<decimal> selectorList = this.EvaluateSelectorConditionParameter(args.Parameters[1], procedureProgrammeContext, evaluationContext).Select(e => (decimal)e).ToList();

            List<bool> conditionList = this.EvaluateSelectorConditionParameter(args.Parameters[2], procedureProgrammeContext, evaluationContext).Select(e => (bool)e).ToList();

            decimal sum = 0;
            int count = 0;
            for (int i = 0; i < subTreeList.Count; i++)
            {
                if (subTreeList[i] && conditionList[i])
                {
                    sum += selectorList[i];
                    count++;
                }
            }

            args.Result = Math.Round(sum / count, 2);
        }
    }
}
