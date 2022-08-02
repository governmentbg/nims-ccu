using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation
{
    internal class SumSelfFunction : AliasSumFunction
    {
        public SumSelfFunction()
        {
            this.InitFields(
                "SumSelf",
                "Връща сумата на стойноста [СФ] за всички разходите определени от селектора.",
                new List<Type>
                {
                    typeof(SubTreeParameter),
                },
                Level3DecimalAttrubuteParameter.ParameterType.SelfAmount);
        }
    }
}
