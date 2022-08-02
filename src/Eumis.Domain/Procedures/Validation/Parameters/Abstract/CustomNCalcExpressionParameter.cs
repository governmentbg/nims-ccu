namespace Eumis.Domain.Procedures.Validation.Abstract
{
    internal abstract class CustomNCalcExpressionParameter : INCalcExpressionParameter
    {
        public string Description { get; private set; }

        public string ShortDescription { get; private set; }

        public NCalcType Type { get; private set; }

        public bool IsGlobalParameter { get; private set; }

        public EvaluationParameterHandler EvaluationHandler { get; private set; }

        public ValidationParameterHandler ValidationHandler { get; private set; }

        public DatabaseSerializationParameterHandler SerializeHandler { get; private set; }

        public DatabaseSerializationParameterHandler DeserializeHandler { get; private set; }

        public abstract bool MatchName(string name);

        protected void InitFields(
            string description,
            string shortDescription,
            NCalcType type,
            bool isGlobalParameter,
            EvaluationParameterHandler evaluationHandler,
            ValidationParameterHandler validationHandler,
            DatabaseSerializationParameterHandler serializeHandler,
            DatabaseSerializationParameterHandler deserializeHandler)
        {
            this.Description = description;
            this.ShortDescription = shortDescription;
            this.Type = type;
            this.IsGlobalParameter = isGlobalParameter;
            this.EvaluationHandler = evaluationHandler;
            this.ValidationHandler = validationHandler;
            this.SerializeHandler = serializeHandler;
            this.DeserializeHandler = deserializeHandler;
        }
    }
}
