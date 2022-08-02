using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation
{
    internal class SumTotalFunction : AliasSumFunction
    {
        public SumTotalFunction()
        {
            this.InitFields(
                "SumTotal",
                "Връща сумата на стойноста [Общо] за всички разходите определени от селектора.",
                new List<Type>
                {
                    typeof(SubTreeParameter),
                },
                Level3DecimalAttrubuteParameter.ParameterType.TotalAmount);
        }
    }
}
