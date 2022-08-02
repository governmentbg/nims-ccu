using System.Collections.Generic;
using Eumis.Domain.Procedures.Validation;
using Eumis.Domain.Test.ExpressionEngine.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eumis.Domain.Test.ExpressionEngine
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void ShouldValidateAllCustomParameters()
        {
            var expressions = new List<string>
            {
                "true",
                "[Да]",
                "[Всички]",
                "[Винаги]",
                "false",
                "[Не]",
                "[Категория/статус на предприятието] = 'test'",
                "[Частно правна]",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                Assert.AreEqual(error, null);
            }
        }

        [TestMethod]
        public void ShouldAcceptCaseAndWhiteSpaceInsensitiveParametersAndFunctions()
        {
            var expressions = new List<string>
            {
                "[Да]",
                "[True  ]",
                "[да]",
                "[     да]",
                "[да   ]",
                "[    да   ]",
                "[    Частно   правна]",
                "[  частно    правна]",
                "[isPrivateLegal]",
                "[is private legal]",
                "[частно правна    ]",
                "Sum([бюджет], [бФП], [   Винаги]) > 0",
                "Sum([Budget], [Grand], [   Винаги]) > 0",
                "sum([   бюджет ], [сф], [   Винаги]) > 0",
                "AVg([i], [сф], [   Винаги]) > 0",
                "Sum([  iv], [сф], [   Винаги]) > 0",
                "Sum([ 2  ], [сф], [   Винаги]) > 0",
                "any([ 2  ], [БФП] > 0)",
                "ALL([ 2  ], [сф] > 0)",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                Assert.AreEqual(error, null);
            }
        }

        [TestMethod]
        public void ShouldDisplayExpressionMustBeConditionalError()
        {
            var expressions = new List<string>
            {
                "10",
                "'test'",
                "1.45",
                "-4.26",
                "[Категория/статус на предприятието]",
                "sum([Бюджет], [БФП], [Винаги])",
                "avg([Бюджет], [БФП], [Винаги])",
                "SumTotal([I])",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                Assert.AreEqual(error, "Изразът трябва да връща булева стойност.");
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidSelectorParameterError()
        {
            var invalidExpressions = new List<string>
            {
                "sumTotal([Бюджеттт])",
                "sumTotal([asd])",
                "sumTotal([IX])",
                "sumTotal([XII])",
                "sumTotal([48])",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in invalidExpressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "Невалиден системен параметър:");
            }

            var wrongParameterTypeExpressions = new List<string>
            {
                "sumTotal(1)",
                "sumTotal(2.54)",
                "sumTotal(-2.54)",
                "sumTotal(true)",
            };

            foreach (string expression in wrongParameterTypeExpressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "1-ят параметър на функцията 'sumTotal' трябва да бъде от системен тип.");
            }
        }

        [TestMethod]
        public void ShouldDisplayIncorrectNumberOfParametersError()
        {
            var expressions = new List<string>
            {
                "sum([Бюджет], [БФП])",
                "sum([Бюджет], [БФП], [Да], 2)",
                "sumTotal()",
                "any([I])",
                "all([Бюджет])",
                "all([Бюджет], [БФП] > 0, [Да])",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "Функцията '");
                StringAssert.Contains(error, "' трябва да съдържа ");
                StringAssert.EndsWith(error, "параметри.");
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidFunctionError()
        {
            var expressions = new List<string>
            {
                "adasd()",
                "summ([Бюджет])",
                "function(1, 2)",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "Невалидна функция: '");
            }
        }

        [TestMethod]
        public void ShouldDisplayParsingFailedError()
        {
            var expressions = new List<string>
            {
                "1 + (2+",
                "10 < (2 + 1",
                "sumTotal([Бюджет]",
                "sumTotal(Бюджет]",
                "sumTotal(шБюджет",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "Грешка при парсването на израза ");
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidOperandTypeError()
        {
            var expressions = new List<string>
            {
                "1 + true",
                "true and 2.5",
                "5 > any([I], [БФП] > 0)",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                Assert.AreNotEqual(error, null);
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidFunctionParameterTypeError()
        {
            var expressions = new List<string>
            {
                "sum([Бюджет], [Nuts2], [Всички])",
                "sum([Бюджет], [Категория/статус на предприятието] = 'тест', [Всички])",
                "sum([Бюджет], [СФ] = 2, [Всички])",
                "any([Бюджет], [Общо])",
                "any([Бюджет], [БФП] + 102)",
                "any([Бюджет], [СФ] * 10)",
                "sum([Бюджет], true, true)",
                "sum([Бюджет], 'asdsad', true)",
                "sum([Бюджет], 2, 'asdad')",
                "sum([Бюджет], 2, 1.54)",
                "any([Бюджет], 24.6)",
                "any([Бюджет], 'test')",
                "all([Бюджет], 12)",
                "all([Бюджет], 'test')",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.Contains(error, "-ят параметър на функцията '");
                StringAssert.Contains(error, "' трябва да бъде един от тип ");
            }
        }

        [TestMethod]
        public void ShouldValidateInccorectGroupLevelParameter()
        {
            var expressions = new List<string>
            {
                "any([Ниво 3], sumTotal([Група]) > 0)",
                "any(Ниво2, sumTotal([Група]) > 0)",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                Assert.AreNotEqual(error, null);
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidSubTreeParameterInFunctionIn()
        {
            var expressions = new List<string>
            {
                "any([Ниво 1], in([Име на група], 'ad', 'II', 'III'))",
                "any([Ниво 1], in([Име на група], 'II', 5, 'III'))",
                "any([Ниво 2], in([Име на група], '1', '2', true))",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.EndsWith(error, "-ят параметър на функцията 'in' трябва бъде идентификатор на ниво и да бъде указан в квадратни скоби '[<идентификатор>]'.");
            }
        }

        [TestMethod]
        public void ShouldDisplayInvalidSystemParameter()
        {
            var expressions = new List<string>
            {
                "[Бюджет]",
                "[I]",
                "[II]",
                "[1]",
                "[2]",
                "[БФП]",
                "[СФ]",
                "[02]",
                "[04]",
                "[NUTS2]",
                "[Приоритетна ос]",
                "[Финансов източник]",
                "[Режим на помощта]",
                "[Допустим разход]",
                "[Име на група]",
                "[Група]",
                "[Ниво 1]",
                "[Ниво 2]",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                string error = ProcedureValidationEngine.Instance.ValidateExpression(expression, bigTestProcedure.GoodGovernanceProgramme);

                StringAssert.StartsWith(error, "Невалиден системен параметър");
            }
        }
    }
}
