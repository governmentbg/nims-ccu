using System;

namespace Eumis.Domain.Procedures.Validation
{
    internal class NCalcFunctionParameter
    {
        public NCalcFunctionParameter(string description, NCalcType nCalcType)
        {
            this.Description = description;
            this.NCalcType = nCalcType;
            this.Type = null;
        }

        public NCalcFunctionParameter(string description, Type type)
        {
            this.Description = description;
            this.NCalcType = NCalcType.NCalcExpressionParameter;
            this.Type = type;
        }

        public string Description { get; private set; }

        public NCalcType NCalcType { get; private set; }

        public Type Type { get; private set; }
    }
}
