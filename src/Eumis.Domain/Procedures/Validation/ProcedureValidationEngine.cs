using NCalc;
using NCalc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    public class ProcedureValidationEngine
    {
        private static object syncRoot = new object();
        private static ProcedureValidationEngine instance;

        private Stack<INCalcExpressionFunction> customFunctionsStack;

        private ProcedureValidationEngine()
        {
            this.CustomFunctions = new List<INCalcExpressionFunction>
            {
                new SumFunction(),
                new SumTotalFunction(),
                new SumGrandFunction(),
                new SumSelfFunction(),
                new AvgFunction(),
                new AnyFunction(),
                new AllFunction(),
            };

            this.Parameters = new List<INCalcExpressionParameter>
            {
                // Add parameter types which can be used outside of the functions
                new BooleanValueParameter(),
                new IsPrivateLegalParameter(),
                new CompanySizeTypeParameter(),
                new CandidateUinParameter(),
                new CompanyTypeParameter(),
                new CompanyLegalTypeParameter(),
                new KidCodeOrganizationParameter(),
                new KidCodeProjectParameter(),
            };

            foreach (var function in this.CustomFunctions)
            {
                function.EvaluateExpressionHandler = this.EvaluateExpressionViaExtendedEngine;

                foreach (var expressionParameterType in function.ExpressionParameterTypes)
                {
                    if (!this.Parameters.Any(e => e.GetType() == expressionParameterType))
                    {
                        this.Parameters.Add((INCalcExpressionParameter)Activator.CreateInstance(expressionParameterType));
                    }
                }
            }

            this.SystemFunctions = new List<string>
            {
                "Min",
                "Max",
                "Pow",
                "Abs",
                "Truncate",
                "In",
                "If",
            };
        }

        public static ProcedureValidationEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ProcedureValidationEngine();
                        }
                    }
                }

                return instance;
            }
        }

        private List<string> SystemFunctions { get; set; }

        private List<INCalcExpressionFunction> CustomFunctions { get; set; }

        private List<INCalcExpressionParameter> Parameters { get; set; }

        public string ValidateExpression(string expression, ProcedureProgramme procedureProgramme)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            Expression exp = new Expression(expression, EvaluateOptions.IgnoreParametersAndFunctionsCase | EvaluateOptions.IgnoreCaseWhenCompareStrings);

            if (exp.HasErrors())
            {
                return this.GetErrorParsingFailed(exp.Error);
            }

            NCalcType expressionReturnType;
            try
            {
                this.customFunctionsStack = new Stack<INCalcExpressionFunction>();
                expressionReturnType = this.Validate(exp.ParsedExpression, procedureProgramme);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            if (!expressionReturnType.CompromiseEquals(NCalcType.Boolean))
            {
                return this.GetErrorExpressionMustBeConditional();
            }

            return null;
        }

        public object EvaluateExpression(string expression, ProcedureProgramme procedureProgramme, string programmeCode, Rio.Project project)
        {
            Expression exp = new Expression(expression, EvaluateOptions.IgnoreParametersAndFunctionsCase | EvaluateOptions.IgnoreCaseWhenCompareStrings);

            var candidate = project.Candidate;
            var budget = project.DirectionsBudgetContractCollection.Single(e => e.programmeCode == programmeCode).Budget;
            var projectBasicData = project.ProjectBasicData;

            return this.EvaluateExpressionViaExtendedEngine(exp, procedureProgramme, new NCalcEvaluationContext
            {
                Candidate = candidate,
                Budget = budget,
            });
        }

        public string SerializeToDatabase(string expression, ProcedureProgramme procedureProgrammeContext)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return expression;
            }

            Expression exp = new Expression(expression, EvaluateOptions.IgnoreParametersAndFunctionsCase | EvaluateOptions.IgnoreCaseWhenCompareStrings);

            if (exp.HasErrors())
            {
                throw new DomainException("Parsing failed: " + exp.Error);
            }

            DatabaseSerializationVisitor serializationVisitor = new DatabaseSerializationVisitor(DatabaseSerializationMode.Serializing, this.CustomFunctions, this.Parameters, procedureProgrammeContext);

            return this.VisitLogicalExpression(serializationVisitor, exp.ParsedExpression);
        }

        public string DeserializeFromDatabase(string expression, ProcedureProgramme procedureProgrammeContext)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return expression;
            }

            Expression exp = new Expression(expression, EvaluateOptions.IgnoreParametersAndFunctionsCase | EvaluateOptions.IgnoreCaseWhenCompareStrings);

            if (exp.HasErrors())
            {
                throw new DomainException("Parsing failed: " + exp.Error);
            }

            DatabaseSerializationVisitor deserializationVisitor = new DatabaseSerializationVisitor(DatabaseSerializationMode.Deserializing, this.CustomFunctions, this.Parameters, procedureProgrammeContext);

            return this.VisitLogicalExpression(deserializationVisitor, exp.ParsedExpression);
        }

        public bool HasExpressionSerializableGuid(string expression, Guid gid)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return false;
            }

            return expression.ToLower().Contains(string.Format("[{0}]", gid.ToString().ToLower()));
        }

        #region Private Memebers

        private object EvaluateExpressionViaExtendedEngine(Expression expression, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            if (this.CustomFunctions.Count > 0)
            {
                expression.EvaluateFunction += (functionName, args) =>
                {
                    var function = this.GetCustomFunctionByName(functionName);

                    if (function != null)
                    {
                        function.FunctionHandler(args, procedureProgrammeContext, evaluationContext);
                    }
                };
            }

            if (this.Parameters.Any(e => e.EvaluationHandler != null))
            {
                expression.EvaluateParameter += (parameterName, args) =>
                {
                    var parameter = this.GetParameterByName(parameterName);

                    if (parameter != null && parameter.EvaluationHandler != null)
                    {
                        parameter.EvaluationHandler(parameterName, args, evaluationContext);
                    }
                };
            }

            return expression.Evaluate();
        }

        private INCalcExpressionFunction GetCustomFunctionByName(string name)
        {
            return this.CustomFunctions.SingleOrDefault(e => e.Name.ToLower().Trim() == name.ToLower());
        }

        private INCalcExpressionParameter GetParameterByName(string name)
        {
            return this.Parameters.Where(e => e.MatchName(name)).SingleOrDefault();
        }

        private INCalcExpressionParameter GetParameterByType(Type type)
        {
            return this.Parameters.Where(e => e.Type == NCalcType.NCalcExpressionParameter && e.GetType() == type).SingleOrDefault();
        }

        private NCalcType Validate(LogicalExpression logicalExpression, ProcedureProgramme procedureProgrammeContext)
        {
            if (logicalExpression.GetType() == typeof(NCalc.Domain.ValueExpression))
            {
                var valueExpression = (NCalc.Domain.ValueExpression)logicalExpression;

                switch (valueExpression.Type)
                {
                    case NCalc.Domain.ValueType.Integer:
                        return NCalcType.Integer;
                    case NCalc.Domain.ValueType.Float:
                        return NCalcType.Float;
                    case NCalc.Domain.ValueType.Boolean:
                        return NCalcType.Boolean;
                    case NCalc.Domain.ValueType.String:
                        return NCalcType.String;
                    case NCalc.Domain.ValueType.DateTime:
                        return NCalcType.DateTime;
                    default: // unreachable
                        throw new Exception("SystemFunctionUnknown value expression.");
                }
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.BinaryExpression))
            {
                var binaryExpression = (NCalc.Domain.BinaryExpression)logicalExpression;

                if (binaryExpression.Type == NCalc.Domain.BinaryExpressionType.And ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Or ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.NotEqual ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.LesserOrEqual ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.GreaterOrEqual ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Lesser ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Greater ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Equal ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Minus ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Plus ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Modulo ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Div ||
                    binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Times)
                {
                    NCalcType leftExpressionType = this.Validate(binaryExpression.LeftExpression, procedureProgrammeContext);
                    NCalcType rightExpressionType = this.Validate(binaryExpression.RightExpression, procedureProgrammeContext);

                    if (binaryExpression.Type == NCalc.Domain.BinaryExpressionType.And ||
                        binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Or)
                    {
                        if (!leftExpressionType.CompromiseEquals(NCalcType.Boolean))
                        {
                            throw new Exception(this.GetErrorInvalidOperandType(1, binaryExpression.Type.ToString(), NCalcType.Boolean.Description()));
                        }

                        if (!rightExpressionType.CompromiseEquals(NCalcType.Boolean))
                        {
                            throw new Exception(this.GetErrorInvalidOperandType(2, binaryExpression.Type.ToString(), NCalcType.Boolean.Description()));
                        }

                        return NCalcType.Boolean;
                    }
                    else if (binaryExpression.Type == NCalc.Domain.BinaryExpressionType.NotEqual ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.LesserOrEqual ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.GreaterOrEqual ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Lesser ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Greater ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Equal)
                    {
                        if ((leftExpressionType != NCalcType.Integer && leftExpressionType != NCalcType.Float) ||
                            (rightExpressionType != NCalcType.Integer && rightExpressionType != NCalcType.Float))
                        {
                            if (!leftExpressionType.CompromiseEquals(rightExpressionType))
                            {
                                throw new Exception(this.GetErrorDifferentOperandTypes(binaryExpression.Type.ToString()));
                            }
                        }

                        return NCalcType.Boolean;
                    }
                    else if (binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Minus ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Plus ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Modulo ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Div ||
                             binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Times)
                    {
                        if (!leftExpressionType.CompromiseEquals(NCalcType.Integer) && !leftExpressionType.CompromiseEquals(NCalcType.Float))
                        {
                            throw new Exception(
                                this.GetErrorInvalidOperandType(
                                    1,
                                    binaryExpression.Type.ToString(),
                                    new List<string> { NCalcType.Integer.Description(), NCalcType.Float.Description() }));
                        }

                        if (!rightExpressionType.CompromiseEquals(NCalcType.Integer) && !rightExpressionType.CompromiseEquals(NCalcType.Float))
                        {
                            throw new Exception(
                                this.GetErrorInvalidOperandType(
                                    2,
                                    binaryExpression.Type.ToString(),
                                    new List<string> { NCalcType.Integer.Description(), NCalcType.Float.Description() }));
                        }

                        if (binaryExpression.Type == NCalc.Domain.BinaryExpressionType.Div)
                        {
                            return NCalcType.Float;
                        }
                        else
                        {
                            if (leftExpressionType == NCalcType.SystemFunctionUnknown || rightExpressionType == NCalcType.SystemFunctionUnknown)
                            {
                                return NCalcType.SystemFunctionUnknown;
                            }

                            if (leftExpressionType == NCalcType.Float || rightExpressionType == NCalcType.Float)
                            {
                                return NCalcType.Float;
                            }
                            else
                            {
                                return NCalcType.Integer;
                            }
                        }
                    }

                    // unreachable
                    else
                    {
                        throw new Exception("SystemFunctionUnknown binary expression.");
                    }
                }
                else
                {
                    throw new Exception(this.GetErrorInvalidOperator(binaryExpression.Type.ToString()));
                }
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.UnaryExpression))
            {
                var unaryExpression = (NCalc.Domain.UnaryExpression)logicalExpression;

                if (unaryExpression.Type == UnaryExpressionType.Not ||
                    unaryExpression.Type == UnaryExpressionType.Negate)
                {
                    NCalcType expressionType = this.Validate(unaryExpression.Expression, procedureProgrammeContext);

                    if (unaryExpression.Type == UnaryExpressionType.Not)
                    {
                        if (!expressionType.CompromiseEquals(NCalcType.Boolean))
                        {
                            throw new Exception(this.GetErrorInvalidOperandType(null, unaryExpression.Type.ToString(), NCalcType.Boolean.Description()));
                        }

                        return NCalcType.Boolean;
                    }
                    else if (unaryExpression.Type == UnaryExpressionType.Negate)
                    {
                        if (expressionType == NCalcType.NCalcExpressionParameter)
                        {
                            throw new Exception(this.GetErrorInvalidNCalcOperandType(null, unaryExpression.Type.ToString()));
                        }

                        return expressionType;
                    }

                    // unreachable
                    else
                    {
                        throw new Exception("SystemFunctionUnknown unary expression.");
                    }
                }
                else
                {
                    throw new Exception(this.GetErrorInvalidOperator(unaryExpression.Type.ToString()));
                }
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.TernaryExpression))
            {
                var ternaryExpression = (NCalc.Domain.TernaryExpression)logicalExpression;

                NCalcType leftExpressionType = this.Validate(ternaryExpression.LeftExpression, procedureProgrammeContext);
                NCalcType middleExpressionType = this.Validate(ternaryExpression.MiddleExpression, procedureProgrammeContext);
                NCalcType rightExpressionType = this.Validate(ternaryExpression.RightExpression, procedureProgrammeContext);

                if (!leftExpressionType.CompromiseEquals(NCalcType.Boolean))
                {
                    throw new Exception(this.GetErrorInvalidOperandType(1, "?:", NCalcType.Boolean.Description()));
                }

                if (!middleExpressionType.CompromiseEquals(rightExpressionType))
                {
                    throw new Exception(this.GetErrorDifferentMiddleAndRightTernaryOperandTypes());
                }

                return middleExpressionType == NCalcType.SystemFunctionUnknown || rightExpressionType == NCalcType.SystemFunctionUnknown ?
                    NCalcType.SystemFunctionUnknown :
                    middleExpressionType;
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.Identifier))
            {
                var identifierExpression = (NCalc.Domain.Identifier)logicalExpression;

                INCalcExpressionParameter parameter = this.GetParameterByName(identifierExpression.Name);

                if (parameter == null)
                {
                    throw new Exception(this.GetErrorInvalidSystemParameter(identifierExpression.Name));
                }

                if (!parameter.IsGlobalParameter && !this.customFunctionsStack.Any(f => f.ExpressionParameterTypes.Any(p => p == parameter.GetType())))
                {
                    throw new Exception(this.GetErrorInvalidSystemParameter(identifierExpression.Name));
                }

                if (parameter.ValidationHandler != null)
                {
                    string error = parameter.ValidationHandler(identifierExpression.Name, procedureProgrammeContext);
                    if (error != null)
                    {
                        throw new Exception(this.GetErrorInvalidSystemParameter(identifierExpression.Name, error));
                    }
                }

                return parameter.Type;
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.Function))
            {
                var functionExpression = (NCalc.Domain.Function)logicalExpression;

                var customFunction = this.GetCustomFunctionByName(functionExpression.Identifier.Name);
                bool systemFunctionExist = this.SystemFunctions.Any(e => e.ToLower().Trim() == functionExpression.Identifier.Name.ToLower().Trim());

                if (customFunction == null && !systemFunctionExist)
                {
                    throw new Exception(this.GetErrorInvalidFunction(functionExpression.Identifier.Name));
                }

                if (customFunction != null)
                {
                    if (customFunction.Parameters.Count != functionExpression.Expressions.Count())
                    {
                        throw new Exception(this.GetErrorFunctionParametersNumberNotMatch(functionExpression.Identifier.Name, customFunction.Parameters.Count));
                    }

                    for (int i = 0; i < functionExpression.Expressions.Count(); i++)
                    {
                        this.customFunctionsStack.Push(customFunction);

                        var expressionType = this.Validate(functionExpression.Expressions[i], procedureProgrammeContext);

                        this.customFunctionsStack.Pop();

                        if (customFunction.Parameters[i].NCalcType == NCalcType.NCalcExpressionParameter && expressionType != NCalcType.NCalcExpressionParameter)
                        {
                            throw new Exception(this.GetErrorParameterMustBeNCalcExpressionParameter(i + 1, functionExpression.Identifier.Name));
                        }

                        if (!expressionType.CompromiseEquals(customFunction.Parameters[i].NCalcType))
                        {
                            throw new Exception(this.GetErrorInvalidFunctionParameterType(i + 1, functionExpression.Identifier.Name, customFunction.Parameters[i]));
                        }
                    }

                    return customFunction.ReturnType;
                }

                // Function is system
                else
                {
                    if (functionExpression.Identifier.Name.ToLower().Trim() == "in")
                    {
                        if (functionExpression.Expressions[0].GetType() == typeof(NCalc.Domain.Identifier))
                        {
                            var firstInParameter = (NCalc.Domain.Identifier)functionExpression.Expressions[0];
                            if (GroupNameParameter.MatchParameterAlias(firstInParameter.Name))
                            {
                                for (int i = 1; i < functionExpression.Expressions.Length; i++)
                                {
                                    if (functionExpression.Expressions[i].GetType() != typeof(NCalc.Domain.Identifier))
                                    {
                                        throw new Exception(this.GetErrorParameterInFunctionInMustBeSubTree(i + 1));
                                    }
                                    else
                                    {
                                        var identifierExpression = (NCalc.Domain.Identifier)functionExpression.Expressions[i];

                                        if (!SubTreeParameter.MatchParameterAlias(identifierExpression.Name))
                                        {
                                            throw new Exception(this.GetErrorInvalidSubTreeParameterInFunctionIn(i + 1, identifierExpression.Name));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 0; i < functionExpression.Expressions.Count(); i++)
                    {
                        this.Validate(functionExpression.Expressions[i], procedureProgrammeContext);
                    }

                    return NCalcType.SystemFunctionUnknown;
                }
            }
            else
            {
                throw new Exception("Неразпознат оператор, функция или израз.");
            }
        }

        private string VisitLogicalExpression(DatabaseSerializationVisitor visitor, LogicalExpression logicalExpression)
        {
            if (logicalExpression.GetType() == typeof(NCalc.Domain.BinaryExpression))
            {
                var binaryExpression = (NCalc.Domain.BinaryExpression)logicalExpression;
                visitor.Visit(binaryExpression);
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.UnaryExpression))
            {
                var unaryExpression = (NCalc.Domain.UnaryExpression)logicalExpression;
                visitor.Visit(unaryExpression);
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.TernaryExpression))
            {
                var ternaryExpression = (NCalc.Domain.TernaryExpression)logicalExpression;
                visitor.Visit(ternaryExpression);
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.ValueExpression))
            {
                var valueExpression = (NCalc.Domain.ValueExpression)logicalExpression;
                visitor.Visit(valueExpression);
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.Function))
            {
                var functionExpression = (NCalc.Domain.Function)logicalExpression;
                visitor.Visit(functionExpression);
            }
            else if (logicalExpression.GetType() == typeof(NCalc.Domain.Identifier))
            {
                var identifierExpression = (NCalc.Domain.Identifier)logicalExpression;
                visitor.Visit(identifierExpression);
            }

            return visitor.Result.ToString().Trim();
        }

        #region Error message methods

        private string GetErrorInvalidOperator(string operatorName)
        {
            return string.Format("Невалиден оператор: '{0}'", this.GetOperatorAlias(operatorName));
        }

        private string GetErrorInvalidSystemParameter(string parameterName, string errorDescription = null)
        {
            string error = string.Format("Невалиден системен параметър: '[{0}]'.", parameterName);

            if (!string.IsNullOrWhiteSpace(errorDescription))
            {
                error += string.Format(" ({0})", errorDescription);
            }

            return error;
        }

        private string GetErrorInvalidFunction(string functionName)
        {
            return string.Format("Невалидна функция: '{0}'", functionName);
        }

        private string GetErrorInvalidOperandType(int? operandIndex, string operatorName, List<string> expectedTypes)
        {
            string error = null;
            if (operandIndex.HasValue)
            {
                error = string.Format("{0}-ят операнд на оператора '{1}' трябва да бъде един от следните типове: ", operandIndex, this.GetOperatorAlias(operatorName));
            }
            else
            {
                error = string.Format("Операнда на оператора '{0}' трябва да бъде един от следните типове: ", this.GetOperatorAlias(operatorName));
            }

            for (int i = 0; i < expectedTypes.Count; i++)
            {
                error += "'" + expectedTypes[i] + "'";
                if (i != expectedTypes.Count - 1)
                {
                    error += ", ";
                }
                else
                {
                    error += ".";
                }
            }

            return error;
        }

        private string GetErrorInvalidOperandType(int? operandIndex, string operatorName, string expectedType)
        {
            if (operandIndex.HasValue)
            {
                return string.Format("{0}-ят операнд на оператора '{1}' трябва да бъде от тип '{2}'.", operandIndex, this.GetOperatorAlias(operatorName), expectedType);
            }
            else
            {
                return string.Format("Операнда на оператора '{0}' трябва да бъде от тип '{2}'.", this.GetOperatorAlias(operatorName), expectedType);
            }
        }

        private string GetErrorInvalidNCalcOperandType(int? operandIndex, string operatorName)
        {
            if (operandIndex.HasValue)
            {
                return string.Format("{0}-ят операнд на оператора '{1}' не може да бъде от тип системен параметър.", operandIndex, this.GetOperatorAlias(operatorName));
            }
            else
            {
                return string.Format("Операнда на оператора '{0}' не може да бъде от тип системен параметър.", this.GetOperatorAlias(operatorName));
            }
        }

        private string GetErrorDifferentOperandTypes(string operatorName)
        {
            return string.Format("Операндите на оператора '{0}' трябва да бъдат от един и същ тип.", this.GetOperatorAlias(operatorName));
        }

        private string GetErrorDifferentMiddleAndRightTernaryOperandTypes()
        {
            return "Вторият и третият операнд на  оператора '?:' трябва да бъдат от един и същ тип.";
        }

        private string GetErrorFunctionParametersNumberNotMatch(string functionName, int expectedParameterNumber)
        {
            return string.Format("Функцията '{0}' трябва да съдържа {1} параметри.", functionName, expectedParameterNumber);
        }

        private string GetErrorInvalidFunctionParameterType(int parameterIndex, string functionName, List<string> expectedTypes)
        {
            string error = string.Format("{0}-ят параметър на функцията '{1}' трябва да бъде един от следните типове: ", parameterIndex, functionName);

            for (int i = 0; i < expectedTypes.Count; i++)
            {
                error += "'" + expectedTypes[i] + "'";
                if (i != expectedTypes.Count - 1)
                {
                    error += ", ";
                }
                else
                {
                    error += ".";
                }
            }

            return error;
        }

        private string GetErrorInvalidFunctionParameterType(int parameterIndex, string functionName, NCalcFunctionParameter expectedParameter)
        {
            if (expectedParameter.NCalcType != NCalcType.NCalcExpressionParameter)
            {
                return string.Format("{0}-ят параметър на функцията '{1}' трябва да бъде един от тип '{2}' ", parameterIndex, functionName, expectedParameter.NCalcType.Description());
            }
            else
            {
                INCalcExpressionParameter nCalcExpressionParameter = this.GetParameterByType(expectedParameter.Type);

                if (nCalcExpressionParameter != null)
                {
                    return string.Format("{0}-ят параметър на функцията '{1}' трябва да бъде един от тип '{2}' ", parameterIndex, functionName, nCalcExpressionParameter.ShortDescription);
                }
                else
                {
                    return string.Format("{0}-ят параметър на функцията '{1}' трябва да бъде един от тип '{2}' ", parameterIndex, functionName, expectedParameter.NCalcType.Description());
                }
            }
        }

        private string GetErrorExpressionMustBeConditional()
        {
            return "Изразът трябва да връща булева стойност.";
        }

        private string GetErrorParameterInFunctionInMustBeSubTree(int parameterIndex)
        {
            return string.Format("{0}-ят параметър на функцията 'in' трябва бъде идентификатор на ниво и да бъде указан в квадратни скоби '[<идентификатор>]'.", parameterIndex);
        }

        private string GetErrorInvalidSubTreeParameterInFunctionIn(int parameterIndex, string parameterName)
        {
            return string.Format("{0}-ят параметър [{1}] на функцията 'in' е невалиден.", parameterIndex, parameterName);
        }

        private string GetErrorParameterMustBeNCalcExpressionParameter(int parameterIndex, string functionName)
        {
            return string.Format("{0}-ят параметър на функцията '{1}' трябва да бъде от системен тип.", parameterIndex, functionName);
        }

        private string GetErrorParsingFailed(string description)
        {
            return string.Format("Грешка при парсването на израза ({0}).", description);
        }

        private string GetOperatorAlias(string operatorName)
        {
            switch (operatorName.ToLower().Trim())
            {
                // Binary operators
                case "and":
                    return "and / &&";
                case "or":
                    return "or / ||";
                case "notequal":
                    return "!= / <>";
                case "lesserorequal":
                    return "<=";
                case "greaterorequal":
                    return ">=";
                case "lesser":
                    return "<";
                case "greater":
                    return ">";
                case "equal":
                    return "=";
                case "minus":
                    return "-";
                case "plus":
                    return "+";
                case "modulo":
                    return "%";
                case "div":
                    return "/";
                case "times":
                    return "*";

                // Unary operators
                case "not":
                    return "not / !";
                case "negate":
                    return "-";
                default:
                    return operatorName;
            }
        }

        #endregion

        #endregion
    }
}
