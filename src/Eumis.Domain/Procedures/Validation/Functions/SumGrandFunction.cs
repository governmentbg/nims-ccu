using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Validation
{
    internal class SumGrandFunction : AliasSumFunction
    {
        public SumGrandFunction()
        {
            this.InitFields(
                "SumGrand",
                "Връща сумата на стойноста [БФП] за всички разходите определени от селектора.",
                new List<Type>
                {
                    typeof(SubTreeParameter),
                },
                Level3DecimalAttrubuteParameter.ParameterType.GrandAmount);
        }
    }
}
