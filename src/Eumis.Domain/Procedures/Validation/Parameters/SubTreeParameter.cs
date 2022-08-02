using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eumis.Domain.Procedures.Validation
{
    internal class SubTreeParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, SubTreeParameterType> RegexTypeDictionary = new Dictionary<string, SubTreeParameterType>()
        {
            { @"^Бюджет$",  SubTreeParameterType.FullTree },
            { @"^Budget$",  SubTreeParameterType.FullTree },
            { @"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$", SubTreeParameterType.Level1Tree },
            { @"^[1-9]\d*$",  SubTreeParameterType.Level2Tree },
        };

        public SubTreeParameter()
        {
            this.InitFields(
                "Селектор на дърво от беджета. Селектира дърво, върху детайлите на което да се изпълнят проверките (Формат: '[Бюджет]', '[I]', '[II]'... '[1]', '[2]'... )",
                "Селектор на дърво от беджета",
                NCalcType.NCalcExpressionParameter,
                false,
                null,
                this.Validate,
                this.SerializeToDatabase,
                this.DeserializeFromDatabase);
        }

        private enum SubTreeParameterType
        {
            FullTree,
            Level1Tree,
            Level2Tree,
        }

        public static bool MatchParameterAlias(string parameterName)
        {
            return RegexTypeDictionary.Any(e => Regex.IsMatch(parameterName.Trim(), e.Key, RegexOptions.IgnoreCase));
        }

        public static List<bool> GetContextValue(string levelName, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            SubTreeParameterType parameterType = GetSubTreeParameterType(levelName);

            Guid? level1Guid = null;
            Guid? level2Guid = null;

            if (parameterType == SubTreeParameterType.Level1Tree)
            {
                int level1Index = RomanNumeralsConverter.ConvertRomanToArabic(levelName.Trim());

                level1Guid = GetLevel1GuidByIndex(procedureProgrammeContext, level1Index);
            }
            else if (parameterType == SubTreeParameterType.Level2Tree)
            {
                int level2Index = int.Parse(levelName.Trim());

                level2Guid = GetLevel2GuidByIndex(procedureProgrammeContext, level2Index);
            }

            List<bool> resultList = new List<bool>();

            bool isSubTreeItem = false;
            if (parameterType == SubTreeParameterType.FullTree)
            {
                isSubTreeItem = true;
            }

            foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
            {
                if (parameterType == SubTreeParameterType.Level1Tree)
                {
                    if (Guid.Parse(programmeBudget.gid) == level1Guid)
                    {
                        isSubTreeItem = true;
                    }
                    else
                    {
                        isSubTreeItem = false;
                    }
                }

                foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                {
                    if (parameterType == SubTreeParameterType.Level2Tree)
                    {
                        if (Guid.Parse(programmeExpenseBudget.gid) == level2Guid)
                        {
                            isSubTreeItem = true;
                        }
                        else
                        {
                            isSubTreeItem = false;
                        }
                    }

                    foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        resultList.Add(isSubTreeItem);
                    }
                }
            }

            return resultList;
        }

        private static string GetLevelAliasByGuid(ProcedureProgramme procedureProgrammeContext, Guid g)
        {
            var level1Index = 0;
            var level2Index = 0;
            foreach (var lev1Item in procedureProgrammeContext.ProcedureBudgetLevel1.OrderBy(e => e.OrderNum).ToList())
            {
                level1Index++;
                if (lev1Item.Gid == g)
                {
                    return RomanNumeralsConverter.ConvertArabicToRoman(level1Index);
                }

                foreach (var lev2Item in lev1Item.ProcedureBudgetLevel2.OrderBy(e => e.OrderNum).ToList())
                {
                    level2Index++;

                    if (lev2Item.Gid == g)
                    {
                        return level2Index.ToString();
                    }
                }
            }

            return null;
        }

        private static SubTreeParameterType GetSubTreeParameterType(string parameterName)
        {
            return RegexTypeDictionary.Single(e => Regex.IsMatch(parameterName.Trim(), e.Key, RegexOptions.IgnoreCase)).Value;
        }

        private static Guid GetLevel1GuidByIndex(ProcedureProgramme procedureProgrammeContext, int level1Index)
        {
            var index = 0;
            foreach (var lev1Item in procedureProgrammeContext.ProcedureBudgetLevel1.OrderBy(e => e.OrderNum).ToList())
            {
                index++;

                if (index == level1Index)
                {
                    return lev1Item.Gid;
                }
            }

            throw new Exception("Level1 item not found.");
        }

        private static Guid GetLevel2GuidByIndex(ProcedureProgramme procedureProgrammeContext, int level2Index)
        {
            var index = 0;
            foreach (var lev1Item in procedureProgrammeContext.ProcedureBudgetLevel1.OrderBy(e => e.OrderNum).ToList())
            {
                foreach (var lev2Item in lev1Item.ProcedureBudgetLevel2.OrderBy(e => e.OrderNum).ToList())
                {
                    index++;

                    if (index == level2Index)
                    {
                        return lev2Item.Gid;
                    }
                }
            }

            throw new Exception("Level2 item not found.");
        }

        public override bool MatchName(string parameterName)
        {
            return MatchParameterAlias(parameterName);
        }

        private string Validate(string parameterName, ProcedureProgramme procedureProgrammeContext)
        {
            string error = null;

            SubTreeParameterType parameterType = GetSubTreeParameterType(parameterName);

            switch (parameterType)
            {
                case SubTreeParameterType.FullTree:
                    break;
                case SubTreeParameterType.Level1Tree:
                    {
                        int level1Index = RomanNumeralsConverter.ConvertRomanToArabic(parameterName.Trim());

                        if (level1Index > procedureProgrammeContext.ProcedureBudgetLevel1.Count)
                        {
                            error = "Невалидна стойност за номер на бюджет от ниво 1.";
                        }

                        break;
                    }

                case SubTreeParameterType.Level2Tree:
                    {
                        int level2Index = int.Parse(parameterName.Trim());

                        int level2Number = 0;
                        foreach (var lev1 in procedureProgrammeContext.ProcedureBudgetLevel1)
                        {
                            level2Number += lev1.ProcedureBudgetLevel2.Count;
                        }

                        if (level2Index > level2Number)
                        {
                            error = "Невалидна стойност за номер на бюджет от ниво 2.";
                        }

                        break;
                    }

                default:
                    throw new Exception("Invalid SubTreeParameter.");
            }

            return error;
        }

        private string SerializeToDatabase(string parameterName, ProcedureProgramme procedureProgrammeContext)
        {
            if (!this.MatchName(parameterName))
            {
                return parameterName;
            }

            SubTreeParameterType parameterType = GetSubTreeParameterType(parameterName);

            if (parameterType == SubTreeParameterType.FullTree)
            {
                return parameterName;
            }
            else
            {
                Guid? guid = null;
                if (parameterType == SubTreeParameterType.Level1Tree)
                {
                    int level1Index = RomanNumeralsConverter.ConvertRomanToArabic(parameterName.Trim());

                    guid = GetLevel1GuidByIndex(procedureProgrammeContext, level1Index);
                }
                else if (parameterType == SubTreeParameterType.Level2Tree)
                {
                    int level2Index = int.Parse(parameterName.Trim());

                    guid = GetLevel2GuidByIndex(procedureProgrammeContext, level2Index);
                }

                if (guid.HasValue)
                {
                    return guid.ToString();
                }
                else
                {
                    throw new Exception("Invalid level alias.");
                }
            }
        }

        private string DeserializeFromDatabase(string parameterName, ProcedureProgramme procedureProgrammeContext)
        {
            Guid g;
            bool isGuid = Guid.TryParse(parameterName, out g);

            if (!isGuid)
            {
                return parameterName;
            }
            else
            {
                string levelAlias = GetLevelAliasByGuid(procedureProgrammeContext, g);

                if (levelAlias != null)
                {
                    return levelAlias;
                }
                else
                {
                    throw new Exception("Invalid level guid.");
                }
            }
        }
    }
}
