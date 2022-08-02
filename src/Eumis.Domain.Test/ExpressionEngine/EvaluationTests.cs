using System.Collections.Generic;
using Eumis.Domain.Procedures.Validation;
using Eumis.Domain.Test.ExpressionEngine.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eumis.Domain.Test.ExpressionEngine
{
    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void ShouldReturnCorrectIsPrivateLeagalValue()
        {
            var expression = "[Is private legal]";

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                expression,
                bigTestProcedure.GoodGovernanceProgramme,
                bigTestProcedure.GoodGovernanceProgrammeCode,
                bigTestProcedure.Project);

            Assert.AreEqual((bool)value, false);
        }

        [TestMethod]
        public void ShouldReturnCorrectCompanySizeTypeValue()
        {
            var expression = "[Company size type]";

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                expression,
                bigTestProcedure.GoodGovernanceProgramme,
                bigTestProcedure.GoodGovernanceProgrammeCode,
                bigTestProcedure.Project);

            Assert.AreEqual((string)value, "Малко");
        }

        [TestMethod]
        public void ShouldMakeCaseInsensitiveStringsValueComparing()
        {
            var expressions = new List<string>
            {
                "'test' = 'TEST'",
                "[Company size type] = 'мАлКо'",
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (string expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectSumFunctionWithoutConditionResult()
        {
            var expressions = new Dictionary<string, decimal>
            {
                { "sum([Бюджет], [Общо], [Всички])", 103700.00M },
                { "sumTotal([Budget])", 103700.00M },
                { "sum([Budget], [Grand], [All])", 70900.00M },
                { "sumGrand([Budget])", 70900.00M },
                { "sum([Бюджет], [СФ], [Всички])", 32800.00M },
                { "sumSelf([Бюджет])", 32800.00M },
                { "sumTotal([I])", 18000.00M },
                { "sumTotal([III])", 1600.00M },
                { "sumGrand([I])", 12500.00M },
                { "sumGrand([III])", 1200.00M },
                { "sumTotal([6])", 400.00M },
                { "sumTotal([17])", 10000.00M },
                { "sumSelf([6])", 100.00M },
                { "sumSelf([17])", 4000.00M },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression.Key,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((decimal)value, expression.Value);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectSumFunctionWitConditionResult()
        {
            var expressions = new Dictionary<string, decimal>
            {
                { "sum([Бюджет], [Общо], [Общо] > 3000.00)", 70600.00M },
                { "sum([Budget], [Self] * 2, [Grand] = 1000.00)", 1000.00M },
                { "sum([I], [Total], [Grand] > [Self])", 18000.00M },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression.Key,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((decimal)value, expression.Value);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectAvgFunctionWithConditionResult()
        {
            var expressions = new List<string>
            {
                { "avg([Бюджет], [БФП], [Да]) > 1865.00 and avg([Бюджет], [БФП], [Да]) < 1866.00" },
                { "avg([Budget], [Grand], [Да]) = 1865.79" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectNuts2Value()
        {
            var expressions = new List<string>
            {
                { "all([Бюджет], [NUTS2] = 'BG41')" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectProgrammePriorityCodeValue()
        {
            var expressions = new List<string>
            {
                { "all([1], [Приоритетна ос] = '2014BG05SFOP001-2')" },
                { "all([16], [Приоритетна ос] = '2014BG05SFOP001-1')" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectFinanceSourceValue()
        {
            var expressions = new List<string>
            {
                { "all([Бюджет], [Финансов източник] = 'ЕСФ')" },
                { "all([Budget], [Finance source] = 'ЕСФ')" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectAidModeValue()
        {
            var expressions = new List<string>
            {
                { "all([1], [Режим на помощта] = 'Неприложимо')" },
                { "any([I], [Aid mode] = 'Държавна помощ')" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectIsEligableCostValue()
        {
            var expressions = new List<string>
            {
                { "all([I], [Допустим разход] = [Да])" },
                { "any([Budget], [Is eligable cost] = [Не])" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectInterventionFieldCodeValue()
        {
            var expressions = new List<string>
            {
                { "all([I], [01] = '002')" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldMakeCorrectCheckForGroupNames()
        {
            var expressions = new List<string>
            {
                { "all([Ниво 1], in([Име на група], 'I', 'II', 'III')) = [Не]" },
                { "any([Ниво 2], in([Име на група], '22'))" },
                { "any([Level 1], in([Group name], '23', '24')) = [False]" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }

        [TestMethod]
        public void ShouldEvalueteCorrectGroupingFunction()
        {
            var expressions = new List<string>
            {
                { "any([Ниво 1], sumGrand([Група]) = 12500.00 and in([Име на група], 'I'))" },
                { "all([Ниво 1], sum([Група], [Total], [All]) >= 1600.00)" },
                { "any([Ниво 2], sum([Група], [Self], [All]) >= 500.00 and in([Име на група], '15'))" },
            };

            BigTestProcedure bigTestProcedure = new BigTestProcedure();

            foreach (var expression in expressions)
            {
                object value = ProcedureValidationEngine.Instance.EvaluateExpression(
                    expression,
                    bigTestProcedure.GoodGovernanceProgramme,
                    bigTestProcedure.GoodGovernanceProgrammeCode,
                    bigTestProcedure.Project);

                Assert.AreEqual((bool)value, true);
            }
        }
    }
}
