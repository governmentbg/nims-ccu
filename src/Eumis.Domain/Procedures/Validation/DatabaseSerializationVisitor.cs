using NCalc.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal enum DatabaseSerializationMode
    {
        Serializing,
        Deserializing,
    }

    internal class DatabaseSerializationVisitor : SerializationVisitor
    {
        public DatabaseSerializationVisitor(DatabaseSerializationMode serializationMode, List<INCalcExpressionFunction> customFunctions, List<INCalcExpressionParameter> parameters, ProcedureProgramme procedureProgrammeContext)
        {
            this.SerializationMode = serializationMode;
            this.CustomFunctions = customFunctions;
            this.Parameters = parameters;
            this.ProcedureProgrammeContext = procedureProgrammeContext;
        }

        private DatabaseSerializationMode SerializationMode { get; set; }

        private List<INCalcExpressionFunction> CustomFunctions { get; set; }

        private List<INCalcExpressionParameter> Parameters { get; set; }

        private ProcedureProgramme ProcedureProgrammeContext { get; set; }

        public override void Visit(ValueExpression valueExpression)
        {
            switch (valueExpression.Type)
            {
                case NCalc.Domain.ValueType.Boolean:
                    this.Result.Append(((bool)valueExpression.Value).ToString().ToLower());
                    break;
                case NCalc.Domain.ValueType.Float:
                    this.Result.Append(((decimal)valueExpression.Value).ToString("0.00", CultureInfo.InvariantCulture));
                    break;
                default:
                    base.Visit(valueExpression);
                    break;
            }
        }

        public override void Visit(Function function)
        {
            var nCalcFunction = this.GetCustomFunctionByName(function.Identifier.Name);

            if (nCalcFunction != null)
            {
                this.Result.Append(function.Identifier.Name);

                this.Result.Append("(");

                for (int i = 0; i < function.Expressions.Length; i++)
                {
                    bool isDatabaseSerializable = false;

                    if (nCalcFunction.Parameters[i].NCalcType == NCalcType.NCalcExpressionParameter)
                    {
                        var nCalcParameter = this.GetParameterByType(nCalcFunction.Parameters[i].Type);

                        if (nCalcParameter.SerializeHandler != null && nCalcParameter.DeserializeHandler != null)
                        {
                            isDatabaseSerializable = true;

                            var identifierExpression = (NCalc.Domain.Identifier)function.Expressions[i];

                            string newParameterValue = this.SerializationMode == DatabaseSerializationMode.Serializing ?
                                nCalcParameter.SerializeHandler(identifierExpression.Name, this.ProcedureProgrammeContext) :
                                nCalcParameter.DeserializeHandler(identifierExpression.Name, this.ProcedureProgrammeContext);

                            this.Result.Append(string.Format("[{0}] ", newParameterValue));
                        }
                    }

                    if (!isDatabaseSerializable)
                    {
                        function.Expressions[i].Accept(this);
                    }

                    if (i < function.Expressions.Length - 1)
                    {
                        this.Result.Remove(this.Result.Length - 1, 1);
                        this.Result.Append(", ");
                    }
                }

                // trim spaces before adding a closing paren
                while (this.Result[this.Result.Length - 1] == ' ')
                {
                    this.Result.Remove(this.Result.Length - 1, 1);
                }

                this.Result.Append(") ");
            }
            else if (function.Identifier.Name.ToLower().Trim() == "in")
            {
                this.Result.Append(function.Identifier.Name);

                this.Result.Append("(");

                for (int i = 0; i < function.Expressions.Length; i++)
                {
                    bool isDatabaseSerializable = false;

                    if (i != 0 && function.Expressions[i].GetType() == typeof(NCalc.Domain.Identifier))
                    {
                        var identifierExpression = (NCalc.Domain.Identifier)function.Expressions[i];

                        if ((this.SerializationMode == DatabaseSerializationMode.Serializing && SubTreeParameter.MatchParameterAlias(identifierExpression.Name)) ||
                            (this.SerializationMode == DatabaseSerializationMode.Deserializing && Guid.TryParse(identifierExpression.Name, out Guid guid)))
                        {
                            var nCalcParameter = this.GetParameterByType(typeof(SubTreeParameter));

                            if (nCalcParameter.SerializeHandler != null && nCalcParameter.DeserializeHandler != null)
                            {
                                isDatabaseSerializable = true;

                                string newParameterValue = this.SerializationMode == DatabaseSerializationMode.Serializing ?
                                    nCalcParameter.SerializeHandler(identifierExpression.Name, this.ProcedureProgrammeContext) :
                                    nCalcParameter.DeserializeHandler(identifierExpression.Name, this.ProcedureProgrammeContext);

                                this.Result.Append(string.Format("[{0}] ", newParameterValue));
                            }
                        }
                    }

                    if (!isDatabaseSerializable)
                    {
                        function.Expressions[i].Accept(this);
                    }

                    if (i < function.Expressions.Length - 1)
                    {
                        this.Result.Remove(this.Result.Length - 1, 1);
                        this.Result.Append(", ");
                    }
                }

                // trim spaces before adding a closing paren
                while (this.Result[this.Result.Length - 1] == ' ')
                {
                    this.Result.Remove(this.Result.Length - 1, 1);
                }

                this.Result.Append(") ");
            }
            else
            {
                base.Visit(function);
            }
        }

        private INCalcExpressionParameter GetParameterByType(Type type)
        {
            return this.Parameters.Where(e => e.Type == NCalcType.NCalcExpressionParameter && e.GetType() == type).SingleOrDefault();
        }

        private INCalcExpressionFunction GetCustomFunctionByName(string name)
        {
            return this.CustomFunctions.SingleOrDefault(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
