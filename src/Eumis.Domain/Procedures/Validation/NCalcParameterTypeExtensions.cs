using System;

namespace Eumis.Domain.Procedures.Validation
{
    internal static class NCalcParameterTypeExtensions
    {
        public static string Description(this NCalcType type)
        {
            switch (type)
            {
                case NCalcType.Integer:
                    return "Цяло число";
                case NCalcType.Float:
                    return "Дробно число";
                case NCalcType.Boolean:
                    return "Булева стойност";
                case NCalcType.String:
                    return "Текстова стойност";
                case NCalcType.DateTime:
                    return "Дата";
                case NCalcType.NCalcExpressionParameter:
                    return "Системен параметър";
                case NCalcType.SystemFunctionUnknown:
                    return "Неразпознат тип";
                default:
                    throw new ArgumentException();
            }
        }

        public static bool CompromiseEquals(this NCalcType instanceType, NCalcType type)
        {
            if (instanceType == NCalcType.NCalcExpressionParameter ||
                instanceType == NCalcType.SystemFunctionUnknown ||
                type == NCalcType.NCalcExpressionParameter ||
                type == NCalcType.SystemFunctionUnknown)
            {
                return true;
            }
            else
            {
                return instanceType == type;
            }
        }
    }
}
