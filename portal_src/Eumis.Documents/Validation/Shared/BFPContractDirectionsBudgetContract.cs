using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class BFPContractDirectionsBudgetContract : CSValidatorBase<R_10040.BFPContractDirectionsBudgetContract>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10040.BFPContractDirectionsBudgetContract complexType, string modelPath, IList<ValidationOption> errors)
        {
            #region BFPContractBudget

            bool hasFilledRow = false;
            decimal eligible = 0.00m;
            decimal ineligible = 0.00m;
            decimal grandTotal = 0.00m;
            decimal selfTotal = 0.00m;
            int level3Count = 0;

            if (complexType.BFPContractBudget != null && complexType.BFPContractBudget.BFPContractProgrammeBudgetCollection != null && complexType.BFPContractBudget.BFPContractProgrammeBudgetCollection.Count > 0)
            {
                for (int i = 0; i < complexType.BFPContractBudget.BFPContractProgrammeBudgetCollection.Count; i++)
                {
                    var level1 = complexType.BFPContractBudget.BFPContractProgrammeBudgetCollection[i];
                    if (level1.BFPContractProgrammeExpenseBudgetCollection != null && level1.BFPContractProgrammeExpenseBudgetCollection.Count > 0)
                    {
                        for (int j = 0; j < level1.BFPContractProgrammeExpenseBudgetCollection.Count; j++)
                        {
                            var level2 = level1.BFPContractProgrammeExpenseBudgetCollection[j];

                            if (level2.BFPContractProgrammeDetailsExpenseBudgetCollection != null && level2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count > 0)
                            {
                                if (!hasFilledRow)
                                {
                                    hasFilledRow = true;
                                }

                                level3Count += level2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count;

                                if (level2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count > Constants.BudgetMaxLevel3Items)
                                {
                                    errors.Add(ValidationOption.Create(
                                                                modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateBudgetLevel3Current, Constants.BudgetMaxLevel3Items, Global.SectionBudget), true, true));

                                    continue;
                                }

                                for (int k = 0; k < level2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count; k++)
                                {
                                    var level3 = complexType.BFPContractBudget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k];

                                    level3.IsNameValid = true;

                                    level3.IsNutsValid = true;

                                    if (string.IsNullOrWhiteSpace(level3.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].Name",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.ExpenseDescription, Global.SectionBudget), true, true));

                                        level3.IsNameValid = false;
                                    }
                                    else if (level3.Name.Length > Constants.BudgetExpenseLength)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].Name",
                                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.BudgetExpenseLength),
                                                                string.Format(Global.ViewTemplateSymbolsMax, Global.ExpenseDescription, Global.SectionBudget, Constants.BudgetExpenseLength), true, true));

                                        level3.IsNameValid = false;
                                    }

                                    #region CurrentState

                                    //decimal grandEUCurrentStateAmountRounded = 0;
                                    //decimal grandNationalCurrentStateAmountRounded = 0;
                                    decimal grandTotalCurrentStateAmountRounded = 0;
                                    decimal selfCurrentStateAmountRounded = 0;


                                    if (level3.GrandAmount < 0)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].GrandAmounts.TotalAmounts.CurrentState",
                                                            Global.ShortTemplateNonNegativeNumber,
                                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.Total_AS, Global.SectionBudget),
                                                            true, true));

                                        level3.IsGrandAmountValid = false;
                                    }
                                    else
                                    {
                                        if (!level1.IsDeactivated && !level2.IsDeactivated && level3.isActive)
                                        {
                                            grandTotalCurrentStateAmountRounded = Math.Round(level3.GrandAmount, 2);
                                            grandTotal += grandTotalCurrentStateAmountRounded;

                                            eligible += grandTotalCurrentStateAmountRounded;
                                        }
                                    }

                                    if (level3.SelfAmount >= 0)
                                    {
                                        if (!level1.IsDeactivated && !level2.IsDeactivated && level3.isActive)
                                        {
                                            selfCurrentStateAmountRounded = Math.Round(level3.SelfAmount, 2);
                                            selfTotal += selfCurrentStateAmountRounded;
                                            eligible += selfCurrentStateAmountRounded;
                                        }
                                    }

                                    if (level3.TotalAmount < 0)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].TotalAmountsDisplay.CurrentState",
                                                            Global.ShortTemplateNonNegativeNumber,
                                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalAmountAS, Global.SectionBudget),
                                                            true, true));

                                        level3.IsGrandAmountValid = false;
                                    }

                                    #endregion

                                    #region Nuts

                                    if (level3.Nuts == null || string.IsNullOrWhiteSpace(level3.Nuts.Code) || string.IsNullOrWhiteSpace(level3.Nuts.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].Nuts.Code",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.Location, Global.SectionBudget), true, true));

                                        level3.IsNutsValid = false;
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
                                                                modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].DirectionItem.Id",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.DirectionName, Global.SectionBudget), true, true));

                                                currentDirectionNomenclature.IsDirectionValid = false;
                                            }
                                        }

                                        // Direction in budget is mandatory
                                        if (level3.Direction == null || level3.Direction.id == null || level3.Direction.DirectionItem == null)
                                        {
                                            errors.Add(ValidationOption.Create(
                                                                    modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].Direction.Id",
                                                                    Global.ShortTemplateRequired,
                                                                    string.Format(Global.ViewTemplateRequired, Global.DirectionName, Global.SectionBudget), true, true));

                                            level3.IsDirectionValid = false;
                                        }
                                        else
                                        {
                                            if (!allDirections.Any(x => x.DirectionItem.Id == level3.Direction.DirectionItem.Id && x.SubDirection?.Id == level3.Direction?.SubDirection?.Id))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".BFPContractBudget.BFPContractProgrammeBudgetCollection[" + i + "].BFPContractProgrammeExpenseBudgetCollection[" + j + "].BFPContractProgrammeDetailsExpenseBudgetCollection[" + k + "].Direction.Id",
                                                                    string.Empty,
                                                                    string.Format(Global.ViewTemplateDirectionsMissmatch), true, true));

                                                level3.IsDirectionValid = false;
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
                                    modelPath + ".BFPContractBudget",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateBudgetLevel3Total, Constants.BudgetMaxLevel3ItemsTotal, Global.SectionBudget), true, true));
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractBudget",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionBudget), true, true));
            }

            if (!hasFilledRow)
            {
                errors.Add(ValidationOption.Create(
                       modelPath + ".BFPContractProgrammeDetailsExpenseBudgetCollection",
                       string.Empty,
                       string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionBudget),
                       true, true));
            }

            #endregion

            #region Contract

            if (complexType.Contract != null)
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
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.BFPContractContractRequestedFundingAmount, Global.SectionFinancialInformation),
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
                                        string.Format(Global.ShortTemplateSectionSumsMatch, Global.BFP, Global.SectionBudget),
                                        string.Format(Global.ViewTemplateSectionSumsMatch, Global.BFPContractContractRequestedFundingAmount, Global.SectionFinancialInformation, Global.BFP, Global.SectionBudget),
                                        true, true));

                        complexType.Contract.IsRequestedFundingAmountValid = false;
                    }
                }

                if (complexType.Contract.RequestedFundingPartOfCrossFinancingAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.RequestedFundingPartOfCrossFinancingAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsRequestedFundingPartOfCrossFinancingAmountValid = false;
                }

                if (complexType.Contract.CoFinancingBudgetAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.CoFinancingBudgetAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsCoFinancingBudgetAmountValid = false;
                }

                if (complexType.Contract.BudgetEIBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsBudgetEIBAmountValid = false;
                }

                if (complexType.Contract.BudgetEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsBudgetEBRDAmountValid = false;
                }

                if (complexType.Contract.BudgetWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsBudgetWBAmountValid = false;
                }

                if (complexType.Contract.BudgetOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsBudgetOtherMFIAmountValid = false;
                }

                if (complexType.Contract.BudgetOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.BudgetOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsBudgetOtherAmountValid = false;
                }

                if (complexType.Contract.CoFinancingNonBudgetAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.CoFinancingNonBudgetAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsCoFinancingNonBudgetAmountValid = false;
                }

                if (complexType.Contract.NonBudgetEIBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsNonBudgetEIBAmountValid = false;
                }

                if (complexType.Contract.NonBudgetEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsNonBudgetEBRDAmountValid = false;
                }

                if (complexType.Contract.NonBudgetWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsNonBudgetWBAmountValid = false;
                }

                if (complexType.Contract.NonBudgetOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsNonBudgetOtherMFIAmountValid = false;
                }

                if (complexType.Contract.NonBudgetOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.NonBudgetOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsNonBudgetOtherAmountValid = false;
                }

                if (complexType.Contract.TotalCoFinancingAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalCoFinancingAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalCoFinancingAmount, Global.SectionFinancialInformation),
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
                                        string.Format(Global.ShortTemplateSectionSumsMatch, Global.SF, Global.SectionBudget),
                                        string.Format(Global.ViewTemplateSectionSumsMatch, Global.TotalCoFinancingAmount, Global.SectionFinancialInformation, Global.SF, Global.SectionBudget),
                                        true, true));

                        complexType.Contract.IsTotalCoFinancingAmountValid = false;
                    }
                }

                if (complexType.Contract.ExpectedLeverageLoanAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.ExpectedLeverageLoanAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsExpectedLeverageLoanAmountValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESIPublic < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESIPublic",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESIPublicValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESIPrivate < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESIPrivate",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESIPrivateValid = false;
                }

                if (complexType.Contract.OtherContributionsOutsideESITotal < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.OtherContributionsOutsideESITotal",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsOtherContributionsOutsideESITotalValid = false;
                }

                if (complexType.Contract.TotalEligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalEligibleCosts",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalEligibleCosts, Global.SectionFinancialInformation),
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
                                        string.Format(Global.ShortTemplateMatchEligibleCosts, Global.SectionBudget),
                                        string.Format(Global.ViewTemplateMatchEligibleCosts, Global.TotalEligibleCosts, Global.SectionFinancialInformation, Global.SectionBudget),
                                        true, true));

                        complexType.Contract.IsTotalEligibleCostsValid = false;
                    }
                }

                if (complexType.Contract.TotalEligibleCostsPublicFunding < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalEligibleCostsPublicFunding",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsTotalEligibleCostsPublicFundingValid = false;
                }

                if (complexType.Contract.ExpectedRevenue < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.ExpectedRevenue",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsExpectedRevenueValid = false;
                }

                if (complexType.Contract.IneligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleCosts",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.BFPIneligibleCosts, Global.SectionFinancialInformation),
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
                                        string.Format(Global.ShortTemplateMatchIneligibleCosts, Global.SectionBudget),
                                        string.Format(Global.ViewTemplateMatchIneligibleCosts, Global.BFPIneligibleCosts, Global.SectionFinancialInformation, Global.SectionBudget),
                                        true, true));

                        complexType.Contract.IsIneligibleCostsValid = false;
                    }
                }

                if (complexType.Contract.IneligibleCosts < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleEIBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsIneligibleEIBAmountValid = false;
                }

                if (complexType.Contract.IneligibleEBRDAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleEBRDAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsIneligibleEBRDAmountValid = false;
                }

                if (complexType.Contract.IneligibleWBAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleWBAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsIneligibleWBAmountValid = false;
                }

                if (complexType.Contract.IneligibleOtherMFIAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleOtherMFIAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsIneligibleOtherMFIAmountValid = false;
                }

                if (complexType.Contract.IneligibleOtherAmount < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.IneligibleOtherAmount",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionFinancialInformation),
                                        true, true));

                    complexType.Contract.IsIneligibleOtherAmountValid = false;
                }

                if (complexType.Contract.TotalProjectCost < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract.TotalProjectCost",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.BFPContractContractTotalProjectCost, Global.SectionFinancialInformation),
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
                                        string.Format(Global.ViewTemplateTotalProjectCost, Global.BFPContractContractTotalProjectCost, Global.SectionFinancialInformation),
                                        true, true));

                        complexType.Contract.IsTotalProjectCostValid = false;
                    }
                }

                if (complexType.Contract.CoFinancingBudgetAmount + complexType.Contract.CoFinancingNonBudgetAmount != complexType.Contract.TotalCoFinancingAmount)
                {
                    errors.Add(ValidationOption.Create(
                                    modelPath + ".Contract.TotalCoFinancingAmount",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateTotalCoFinancingAmount, Global.CoFinancingBudgetAmountField, Global.CoFinancingNonBudgetAmountField, Global.SectionFinancialInformation, Global.TotalCoFinancingAmount),
                                    true, true));
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Contract",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionFinancialInformation),
                                        true, true));
            }

            #endregion
        }
    }
}
