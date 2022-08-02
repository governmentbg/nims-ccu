using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System.Linq;

namespace Eumis.Documents.Validation.Shared
{
    public class DirectionsBudgetContract : CSValidatorBase<R_09998.DirectionsBudgetContract>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_09998.DirectionsBudgetContract complexType, string modelPath, IList<ValidationOption> errors)
        {
            bool areFinLabels = complexType.IsFinalRecipients || complexType.IsFinancialIntermediaries;

            string programmeName = complexType.ProgrammeNameFormatted;
            string budgetSectionName = string.Format("{0} {1}", Global.SectionBudget, programmeName).TrimEnd();
            string contractSectionName = string.Format("{0} {1}", Global.SectionFinancialInformation, programmeName).TrimEnd();

            bool hasFilledRow = false;
            decimal eligible = 0.00m;
            decimal grandTotal = 0.00m;
            decimal selfTotal = 0.00m;
            int level3Count = 0;

            if (complexType.Budget != null && complexType.Budget.ProgrammeBudgetCollection != null && complexType.Budget.ProgrammeBudgetCollection.Count > 0)
            {
                for (int i = 0; i < complexType.Budget.ProgrammeBudgetCollection.Count; i++)
                {
                    if (complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection != null && complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection.Count > 0)
                    {
                        for (int j = 0; j < complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection.Count; j++)
                        {
                            if (complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection != null &&
                                complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection.Count > 0)
                            {
                                if (!hasFilledRow)
                                    hasFilledRow = true;

                                level3Count += complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection.Count;

                                if (complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection.Count > Constants.BudgetMaxLevel3Items)
                                {
                                    errors.Add(ValidationOption.Create(
                                                                modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateBudgetLevel3Current, Constants.BudgetMaxLevel3Items, budgetSectionName), true, true));

                                    continue;
                                }

                                for (int k = 0; k < complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection.Count; k++)
                                {
                                    var current = complexType.Budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection[k];
                                    bool hasGrandAmount = false;
                                    bool hasSelfAmount = false;
                                    decimal grandAmountRounded = 0, selfAmountRounded = 0;

                                    current.IsNameValid = true;
                                    current.IsGrandAmountValid = true;
                                    current.IsSelfAmountValid = true;
                                    current.IsTotalAmountValid = true;
                                    current.IsDirectionValid = true;

                                    current.IsNutsValid = true;

                                    if (string.IsNullOrWhiteSpace(current.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].Name",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.ExpenseDescription, budgetSectionName), true, true));

                                        current.IsNameValid = false;
                                    }
                                    else if (current.Name.Length > Constants.BudgetExpenseLength)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].Name",
                                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.BudgetExpenseLength),
                                                                string.Format(Global.ViewTemplateSymbolsMax, Global.ExpenseDescription, budgetSectionName, Constants.BudgetExpenseLength), true, true));

                                        current.IsNameValid = false;
                                    }

                                    if (current.GrandAmount < 0)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].GrandAmount",
                                                            Global.ShortTemplateNonNegativeNumber,
                                                            string.Format(Global.ViewTemplateNonNegativeNumber, areFinLabels ? Global.FinancialIntermediariesBFP : Global.BFP, budgetSectionName),
                                                            true, true));

                                        current.IsGrandAmountValid = false;
                                    }
                                    else
                                    {
                                        hasGrandAmount = true;
                                        grandAmountRounded = Math.Round(current.GrandAmount, 2);
                                        grandTotal += grandAmountRounded;
                                        eligible += grandAmountRounded;
                                        
                                    }

                                    if (current.SelfAmount < 0)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].SelfAmount",
                                                            Global.ShortTemplateNonNegativeNumber,
                                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.SF, budgetSectionName),
                                                            true, true));

                                        current.IsSelfAmountValid = false;
                                    }
                                    else
                                    {
                                        hasSelfAmount = true;
                                        selfAmountRounded = Math.Round(current.SelfAmount, 2);
                                        selfTotal += selfAmountRounded;
                                        
                                        eligible += selfAmountRounded;
                                    }

                                    if (current.TotalAmount < 0)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].TotalAmount",
                                                            Global.ShortTemplateNonNegativeNumber,
                                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalAmount, budgetSectionName),
                                                            true, true));

                                        current.IsTotalAmountValid = false;
                                    }
                                    else if (hasGrandAmount && hasSelfAmount)
                                    {
                                        decimal sum = Math.Round(grandAmountRounded + selfAmountRounded, 2);
                                        decimal totalRounded = Math.Round(current.TotalAmount, 2);

                                        if (sum != totalRounded)
                                        {
                                            errors.Add(ValidationOption.Create(
                                                        modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].TotalAmount",
                                                        string.Format(Global.ShortTemplateSumBFP_SF, areFinLabels ? Global.FinancialIntermediariesBFP : Global.BFP, Global.SF),
                                                        string.Format(Global.ViewTemplateSumBFP_SF, Global.TotalAmount, areFinLabels ? Global.FinancialIntermediariesBFP : Global.BFP, Global.SF, budgetSectionName),
                                                        true, true));

                                            current.IsTotalAmountValid = false;
                                        }
                                    }

                                    #region Nuts

                                    if (current.Nuts == null || string.IsNullOrWhiteSpace(current.Nuts.Code) || string.IsNullOrWhiteSpace(current.Nuts.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].Nuts.Code",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.Location, budgetSectionName), true, true));

                                        current.IsNutsValid = false;
                                    }

                                    #endregion

                                    #region Directions
                                    var allDirections = complexType.Directions?.DirectionCollection;

                                    if (allDirections != null && allDirections.Count > 0)
                                    {
                                        for (int z = 0; z < allDirections.Count; z++)
                                        {
                                            var currentDirectionNomenclature = allDirections[z];
                                            if (string.IsNullOrEmpty(currentDirectionNomenclature.DirectionItem?.Id))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".DirectionsBudgetContractCollection[" + i + "].DirectionSection.DirectionCollection[" + z + "].DirectionItem.Id",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.DirectionName, Global.SectionDirection), true, true));

                                                currentDirectionNomenclature.IsDirectionValid = false;
                                            }
                                        }

                                        // Direction in budget is mandatory
                                        if (current.Direction == null || current.Direction.id == null || current.Direction.DirectionItem == null)
                                        {
                                            errors.Add(ValidationOption.Create(
                                                                    modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].Direction.Id",
                                                                    Global.ShortTemplateRequired,
                                                                    string.Format(Global.ViewTemplateRequired, Global.DirectionName, budgetSectionName), true, true));

                                            current.IsDirectionValid = false;
                                        }
                                        else
                                        {
                                            if (!allDirections.Any(x => x.DirectionItem.Id == current.Direction.DirectionItem.Id && x.SubDirection?.Id == current.Direction?.SubDirection?.Id)) 
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".Budget.ProgrammeBudgetCollection[" + i + "].ProgrammeExpenseBudgetCollection[" + j + "].ProgrammeDetailsExpenseBudgetCollection[" + k + "].Direction.Id",
                                                                    string.Empty,
                                                                    string.Format(Global.ViewTemplateDirectionsMissmatch), true, true));

                                                current.IsDirectionValid = false;
                                            }
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                }

                if (level3Count > Constants.BudgetMaxLevel3ItemsTotal)
                {
                    errors.Add(ValidationOption.Create(
                                    modelPath + ".Budget",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateBudgetLevel3Total, Constants.BudgetMaxLevel3ItemsTotal, budgetSectionName), true, true));
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Budget",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, budgetSectionName), true, true));
            }

            if (!hasFilledRow)
            {
                errors.Add(ValidationOption.Create(
                       modelPath + ".ProgrammeDetailsExpenseBudgetCollection",
                       string.Empty,
                       string.Format(Global.ViewTemplateAtLeastOneRow, budgetSectionName),
                       true, true));
            }

            //if (complexType.Contract != null)
            //{
            //    EngineValidate(csValidationEngine, complexType.Contract, modelPath + ".Contract", errors);
            //}

            /*if (complexType.Contract != null)
            {
                complexType.Contract.IsRequestedFundingAmountValid = true;
                complexType.Contract.IsRequestedFundingPartOfCrossFinancingAmountValid = true;
                complexType.Contract.IsCoFinancingBudgetAmountValid = true;
                complexType.Contract.IsBudgetEIBAmountValid = true;
                complexType.Contract.IsBudgetEBRDAmountValid = true;
                complexType.Contract.IsBudgetWBAmountValid = true;
                complexType.Contract.IsBudgetOtherMFIAmountValid = true;
                complexType.Contract.IsBudgetOtherAmountValid = true;
                complexType.Contract.IsCoFinancingNonBudgetAmountValid = true;
                complexType.Contract.IsNonBudgetEIBAmountValid = true;
                complexType.Contract.IsNonBudgetEBRDAmountValid = true;
                complexType.Contract.IsNonBudgetWBAmountValid = true;
                complexType.Contract.IsNonBudgetOtherMFIAmountValid = true;
                complexType.Contract.IsNonBudgetOtherAmountValid = true;
                complexType.Contract.IsTotalCoFinancingAmountValid = true;
                complexType.Contract.IsExpectedLeverageLoanAmountValid = true;
                complexType.Contract.IsOtherContributionsOutsideESIPublicValid = true;
                complexType.Contract.IsOtherContributionsOutsideESIPrivateValid = true;
                complexType.Contract.IsOtherContributionsOutsideESITotalValid = true;
                complexType.Contract.IsTotalEligibleCostsValid = true;
                complexType.Contract.IsTotalEligibleCostsPublicFundingValid = true;
                complexType.Contract.IsRatioRequestedFundingTotalEligibleCostsValid = true;
                complexType.Contract.IsExpectedRevenueValid = true;
                complexType.Contract.IsIneligibleCostsValid = true;
                complexType.Contract.IsIneligibleEIBAmountValid = true;
                complexType.Contract.IsIneligibleEBRDAmountValid = true;
                complexType.Contract.IsIneligibleWBAmountValid = true;
                complexType.Contract.IsIneligibleOtherMFIAmountValid = true;
                complexType.Contract.IsIneligibleOtherAmountValid = true;
                complexType.Contract.IsTotalProjectCostValid = true;

                if (complexType.Contract.RequestedFundingAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.RequestedFundingAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, areFinLabels ? Global.FinancialIntermediariesRequestedFundingAmount : Global.RequestedFundingAmount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsRequestedFundingAmountValid = false;
                }
                else
                {
                    decimal requestedFundingRounded = Math.Round(complexType.Contract.RequestedFundingAmount, 2);

                    if (requestedFundingRounded != grandTotal)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.RequestedFundingAmount",
                                        string.Format(Global.ShortTemplateSectionSumsMatch, areFinLabels ? Global.FinancialIntermediariesBFP : Global.BFP, budgetSectionName),
                                        string.Format(Global.ViewTemplateSectionSumsMatch, areFinLabels ? Global.FinancialIntermediariesRequestedFundingAmount : Global.RequestedFundingAmount, contractSectionName, areFinLabels ? Global.FinancialIntermediariesBFP : Global.BFP, budgetSectionName),
                                        true, true));

                        complexType.Contract.IsRequestedFundingAmountValid = false;
                    }
                }

                if (complexType.Contract.RequestedFundingPartOfCrossFinancingAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.RequestedFundingPartOfCrossFinancingAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsRequestedFundingPartOfCrossFinancingAmountValid = false;
                }

                if (complexType.Contract.CoFinancingBudgetAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.CoFinancingBudgetAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsCoFinancingBudgetAmountValid = false;
                }

                if (complexType.Contract.BudgetEIBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsBudgetEIBAmountValid = false;
                }

                if (complexType.Contract.BudgetEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsBudgetEBRDAmountValid = false;
                }

                if (complexType.Contract.BudgetWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsBudgetWBAmountValid = false;
                }

                if (complexType.Contract.BudgetOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsBudgetOtherMFIAmountValid = false;
                }

                if (complexType.Contract.BudgetOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsBudgetOtherAmountValid = false;
                }

                if (complexType.Contract.CoFinancingNonBudgetAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.CoFinancingNonBudgetAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsCoFinancingNonBudgetAmountValid = false;
                }

                if (complexType.Contract.NonBudgetEIBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsNonBudgetEIBAmountValid = false;
                }

                if (complexType.Contract.NonBudgetEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsNonBudgetEBRDAmountValid = false;
                }

                if (complexType.Contract.NonBudgetWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsNonBudgetWBAmountValid = false;
                }

                if (complexType.Contract.NonBudgetOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsNonBudgetOtherMFIAmountValid = false;
                }

                if (complexType.Contract.NonBudgetOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsNonBudgetOtherAmountValid = false;
                }

                if (complexType.Contract.TotalCoFinancingAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalCoFinancingAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalCoFinancingAmount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsTotalCoFinancingAmountValid = false;
                }
                else
                {
                    decimal totalCoFinancingRounded = Math.Round(complexType.Contract.TotalCoFinancingAmount, 2);

                    if (totalCoFinancingRounded != selfTotal)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalCoFinancingAmount",
                                        string.Format(Global.ShortTemplateSectionSumsMatch, Global.SF, budgetSectionName),
                                        string.Format(Global.ViewTemplateSectionSumsMatch, Global.TotalCoFinancingAmount, contractSectionName, Global.SF, budgetSectionName),
                                        true, true));

                        complexType.Contract.IsTotalCoFinancingAmountValid = false;
                    }
                }

                if (complexType.Contract.ExpectedLeverageLoanAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.ExpectedLeverageLoanAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsExpectedLeverageLoanAmountValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESIPublic < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESIPublic",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESIPublicValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESIPrivate < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESIPrivate",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESIPrivateValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESITotal < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESITotal",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESITotalValid = false;
                }

                if (complexType.Contract.TotalEligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalEligibleCosts",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalEligibleCosts, contractSectionName),
                                        true, true));

                    complexType.Contract.IsTotalEligibleCostsValid = false;
                }
                else
                {
                    decimal totalEligibleCostsRounded = Math.Round(complexType.Contract.TotalEligibleCosts, 2);

                    if (totalEligibleCostsRounded != eligible)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalEligibleCosts",
                                        string.Format(Global.ShortTemplateMatchEligibleCosts, budgetSectionName),
                                        string.Format(Global.ViewTemplateMatchEligibleCosts, Global.TotalEligibleCosts, contractSectionName, budgetSectionName),
                                        true, true));

                        complexType.Contract.IsTotalEligibleCostsValid = false;
                    }
                }

                if (complexType.Contract.TotalEligibleCostsPublicFunding < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalEligibleCostsPublicFunding",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsTotalEligibleCostsPublicFundingValid = false;
                }

                if (complexType.Contract.ExpectedRevenue < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.ExpectedRevenue",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsExpectedRevenueValid = false;
                }

                if (complexType.Contract.IneligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleCosts",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, areFinLabels ? Global.FinancialIntermediariesIneligibleCosts : Global.IneligibleCosts, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleCostsValid = false;
                }
                else
                {
                    decimal totalIneligibleCostsRounded = Math.Round(complexType.Contract.IneligibleCosts, 2);

                    if (totalIneligibleCostsRounded != ineligible)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleCosts",
                                        string.Format(Global.ShortTemplateMatchIneligibleCosts, budgetSectionName),
                                        string.Format(Global.ViewTemplateMatchIneligibleCosts, areFinLabels ? Global.FinancialIntermediariesIneligibleCosts : Global.IneligibleCosts, contractSectionName, budgetSectionName),
                                        true, true));

                        complexType.Contract.IsIneligibleCostsValid = false;
                    }
                }

                if (complexType.Contract.IneligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleEIBAmountValid = false;
                }

                if (complexType.Contract.IneligibleEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleEBRDAmountValid = false;
                }

                if (complexType.Contract.IneligibleWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleWBAmountValid = false;
                }

                if (complexType.Contract.IneligibleOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleOtherMFIAmountValid = false;
                }

                if (complexType.Contract.IneligibleOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, contractSectionName),
                                        true, true));

                    complexType.Contract.IsIneligibleOtherAmountValid = false;
                }

                if (complexType.Contract.TotalProjectCost < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalProjectCost",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, areFinLabels ? Global.FinancialIntermediariesTotalProjectCost : Global.TotalProjectCost, contractSectionName),
                                        true, true));

                    complexType.Contract.IsTotalProjectCostValid = false;
                }
                else
                {
                    decimal totalProjectCostRounded = Math.Round(complexType.Contract.TotalProjectCost, 2);

                    if ((eligible + ineligible) != totalProjectCostRounded)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalProjectCost",
                                        Global.ShortTemplateTotalProjectCost,
                                        string.Format(Global.ViewTemplateTotalProjectCost, areFinLabels ? Global.FinancialIntermediariesTotalProjectCost : Global.TotalProjectCost, contractSectionName),
                                        true, true));

                        complexType.Contract.IsTotalProjectCostValid = false;
                    }
                }

                if (complexType.Contract.CoFinancingBudgetAmount + complexType.Contract.CoFinancingNonBudgetAmount != complexType.Contract.TotalCoFinancingAmount)
                {
                    errors.Add(ValidationOption.Create(
                                    modelPath + ".Contract.TotalCoFinancingAmount",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateTotalCoFinancingAmount, Global.CoFinancingBudgetAmountField, Global.CoFinancingNonBudgetAmountField, contractSectionName, Global.TotalCoFinancingAmount),
                                    true, true));
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, contractSectionName), true, true));
            }*/
        }
    }
}