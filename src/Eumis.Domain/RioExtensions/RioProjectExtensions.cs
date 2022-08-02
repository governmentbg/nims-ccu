using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Validation;
using Eumis.Rio;

namespace Eumis.Domain.RioExtensions
{
    public static class RioProjectExtensions
    {
        public static IList<ProgrammeDetailsExpenseBudget> GetBudget(this Eumis.Rio.Project projectXml)
        {
            var result = new List<ProgrammeDetailsExpenseBudget>();

            foreach (var budget in projectXml.DirectionsBudgetContractCollection)
            {
                foreach (var programmeBudget in budget.Budget.ProgrammeBudgetCollection)
                {
                    foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        result = result.Concat(programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection).ToList();
                    }
                }
            }

            return result;
        }

        public static Tuple<string, string> GetProjectPlace(this Rio.Project projectXml)
        {
            Func<Rio.NutsAddressContent, string> fullPathGetter = null;
            Func<Rio.NutsAddressContent, string> fullPathNameGetter = null;
            var nutsLevel = projectXml.ProjectBasicData.NutsAddress.NutsLevel.GetEnum<PrivateNomenclature, NutsLevel>(na => na.Id);
            switch (nutsLevel)
            {
                case NutsLevel.Country:
                    fullPathGetter = (na) => na.Country.FullPath;
                    fullPathNameGetter = (na) => na.Country.FullPathName;
                    break;
                case NutsLevel.RegionNUTS1:
                    fullPathGetter = (na) => na.Nuts1.FullPath;
                    fullPathNameGetter = (na) => na.Nuts1.FullPathName;
                    break;
                case NutsLevel.RegionNUTS2:
                    fullPathGetter = (na) => na.Nuts2.FullPath;
                    fullPathNameGetter = (na) => na.Nuts2.FullPathName;
                    break;
                case NutsLevel.District:
                    fullPathGetter = (na) => na.District.FullPath;
                    fullPathNameGetter = (na) => na.District.FullPathName;
                    break;
                case NutsLevel.Municipality:
                    fullPathGetter = (na) => na.Municipality.FullPath;
                    fullPathNameGetter = (na) => na.Municipality.FullPathName;
                    break;
                case NutsLevel.Settlement:
                    fullPathGetter = (na) => na.Settlement.FullPath;
                    fullPathNameGetter = (na) => na.Settlement.FullPathName;
                    break;
                case NutsLevel.ProtectedZone:
                    fullPathGetter = (na) => na.ProtectedZone.FullPath;
                    fullPathNameGetter = (na) => na.ProtectedZone.FullPathName;
                    break;
            }

            return new Tuple<string, string>(
                string.Join("; ", projectXml.ProjectBasicData.NutsAddress.NutsAddressContentCollection.Select(fullPathGetter)),
                string.Join("; ", projectXml.ProjectBasicData.NutsAddress.NutsAddressContentCollection.Select(fullPathNameGetter)));
        }

        public static IList<Tuple<string, string, bool>> Validate(
            this Eumis.Rio.Project projectXml,
            Procedure procedure,
            Dictionary<int, Tuple<string, string>> programmeAttributesDictionary)
        {
            List<Tuple<string, string, bool>> validationErrors = null;

            Func<string, string> errorBg = s => DomainEnumTexts.ResourceManager.GetString(s, new CultureInfo(SystemLocalization.Bg_BG));
            Func<string, string> errorEn = s => DomainEnumTexts.ResourceManager.GetString(s, new CultureInfo(SystemLocalization.En_GB));

            try
            {
                validationErrors = new List<Tuple<string, string, bool>>();

                if (!Procedure.EvalSessionOrProjectCreationStatuses.Contains(procedure.ProcedureStatus))
                {
                    validationErrors.Add(new Tuple<string, string, bool>(
                        errorBg(nameof(DomainEnumTexts.Projects_Validate_CantCreate)),
                        errorEn(nameof(DomainEnumTexts.Projects_Validate_CantCreate)),
                        true));
                }

                if (procedure.ApplicationFormType != ApplicationFormType.PreliminarySelection)
                {
                    decimal budgetGrandSum = 0;

                    foreach (var procedureProgramme in procedure.ProcedureProgrammes)
                    {
                        string programmeCode = programmeAttributesDictionary.Single(e => e.Key == procedureProgramme.ProgrammeId).Value.Item1;
                        string programmeShortName = programmeAttributesDictionary.Single(e => e.Key == procedureProgramme.ProgrammeId).Value.Item2;

                        ProcedureValidationEngine validationEngine = ProcedureValidationEngine.Instance;

                        foreach (var validationRule in procedureProgramme.ProcedureBudgetValidationRules)
                        {
                            if (string.IsNullOrWhiteSpace(validationRule.Condition) || (bool)validationEngine.EvaluateExpression(validationRule.Condition, procedureProgramme, programmeCode, projectXml))
                            {
                                if (!(bool)validationEngine.EvaluateExpression(validationRule.Rule, procedureProgramme, programmeCode, projectXml))
                                {
                                    var errorMessage = string.Format("Бюджет към {0}: {1}", programmeShortName, validationRule.Message);

                                    validationErrors.Add(new Tuple<string, string, bool>(errorMessage, errorMessage, false));
                                }
                            }
                        }

                        budgetGrandSum += (decimal)validationEngine.EvaluateExpression("SumGrand([Budget])", procedureProgramme, programmeCode, projectXml);
                    }

                    NumberFormatInfo decimalFormatInfo = new CultureInfo("en-US", false).NumberFormat;
                    decimalFormatInfo.NumberDecimalSeparator = ".";
                    decimalFormatInfo.NumberGroupSeparator = " ";
                    decimalFormatInfo.NumberDecimalDigits = 2;

                    if (budgetGrandSum < procedure.ProjectMinAmount && procedure.ProjectMinAmount.HasValue)
                    {
                        validationErrors.Add(new Tuple<string, string, bool>(
                            string.Format(errorBg(nameof(DomainEnumTexts.Projects_Validate_ProjectMinAmount)), procedure.ProjectMinAmount.Value.ToString("N", decimalFormatInfo)),
                            string.Format(errorEn(nameof(DomainEnumTexts.Projects_Validate_ProjectMinAmount)), procedure.ProjectMinAmount.Value.ToString("N", decimalFormatInfo)),
                            true));
                    }

                    if (budgetGrandSum > procedure.ProjectMaxAmount && procedure.ProjectMaxAmount.HasValue)
                    {
                        validationErrors.Add(new Tuple<string, string, bool>(
                            string.Format(errorBg(nameof(DomainEnumTexts.Projects_Validate_ProjectMaxAmount)), procedure.ProjectMaxAmount.Value.ToString("N", decimalFormatInfo)),
                            string.Format(errorEn(nameof(DomainEnumTexts.Projects_Validate_ProjectMaxAmount)), procedure.ProjectMaxAmount.Value.ToString("N", decimalFormatInfo)),
                            true));
                    }
                }

                string projectExecutionDuration = projectXml.ProjectBasicData.Duration;
                if (!string.IsNullOrWhiteSpace(projectExecutionDuration))
                {
                    if (int.Parse(projectExecutionDuration) > procedure.ProjectDuration)
                    {
                        validationErrors.Add(new Tuple<string, string, bool>(
                            errorBg(nameof(DomainEnumTexts.Projects_Validate_ProjectDuration)),
                            errorEn(nameof(DomainEnumTexts.Projects_Validate_ProjectDuration)),
                            true));
                    }
                }

                return validationErrors;
            }
            catch
            {
                return new List<Tuple<string, string, bool>>()
                {
                    new Tuple<string, string, bool>(
                        errorBg(nameof(DomainEnumTexts.Projects_Validate_BudgetException)),
                        errorEn(nameof(DomainEnumTexts.Projects_Validate_BudgetException)),
                        true),
                };
            }
        }
    }
}
