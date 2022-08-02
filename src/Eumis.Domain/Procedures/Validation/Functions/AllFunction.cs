using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class AllFunction : BooleanConditionalFunction
    {
        public AllFunction()
        {
            this.InitFields(
                "All",
                "Връща истина, ако правилото е изпълено за всеки елемент на бюджета, отговарящ на условието.",
                new List<Type>
                {
                    typeof(SubTreeParameter),
                    typeof(Level3DecimalAttrubuteParameter),
                    typeof(Level3StringAttrubuteParameter),
                    typeof(Level3BoolAttrubuteParameter),
                    typeof(GroupNameParameter),
                    typeof(CurrentGroupParameter),
                    typeof(GroupLevelParameter),
                },
                this.Evaluate);
        }

        private void Evaluate(FunctionArgs args, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            string firstParameterName = this.ExtractParameterName(args.Parameters[0].ParsedExpression.ToString());

            if (GroupLevelParameter.MatchParameterAlias(firstParameterName))
            {
                List<string> groupList = GroupLevelParameter.GetContextValue(firstParameterName, evaluationContext);

                List<bool> conditionList = this.EvaluateConditionParameterInGroupMode(groupList, args.Parameters[1], procedureProgrammeContext, evaluationContext);

                bool returnValue = true;
                for (int i = 0; i < groupList.Count; i++)
                {
                    if (!conditionList[i])
                    {
                        returnValue = false;
                        break;
                    }
                }

                args.Result = returnValue;
            }
            else
            {
                List<bool> subTreeList = this.EvaluateSubTreeParameter(args.Parameters[0], procedureProgrammeContext, evaluationContext);

                List<bool> conditionList = this.EvaluateSelectorConditionParameter(args.Parameters[1], procedureProgrammeContext, evaluationContext).Select(e => (bool)e).ToList();

                bool returnValue = true;
                for (int i = 0; i < subTreeList.Count; i++)
                {
                    if (subTreeList[i] && !conditionList[i])
                    {
                        returnValue = false;
                        break;
                    }
                }

                args.Result = returnValue;
            }
        }
    }
}
