using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Eumis
{
    public class SpendingPlan : CSValidatorBase<R_10077.SpendingPlan>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10077.SpendingPlan complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (complexType.SpendingBudget != null)
            {
                for (int i = 0; i < complexType.SpendingBudget.SpendingBudgetLevel1Collection.Count; i++)
                {
                    var spendingBudgetLevel1 = complexType.SpendingBudget.SpendingBudgetLevel1Collection[i];

                    spendingBudgetLevel1.IsTotalCalculatedAmountValid = true;

                    var spendingBudgetLevel1Sum = 0m;

                    for (int j = 0; j < spendingBudgetLevel1.QuarterlyDistributionCollection.Count; j++)
                    {
                        var quarter = spendingBudgetLevel1.QuarterlyDistributionCollection[j];
                        var year = spendingBudgetLevel1.QuarterlyDistributionCollection[j].Year;

                        if (quarter.Q1Amount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].QuarterlyDistributionCollection[" + j + "].Q1Amount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, "I/" + year, Global.SectionSpendingPlan),
                                            true, true));

                            quarter.IsQ1AmountValid = false;
                        }
                        else
                        {
                            quarter.IsQ1AmountValid = true;
                            spendingBudgetLevel1Sum += quarter.Q1Amount;
                        }

                        if (quarter.Q2Amount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].QuarterlyDistributionCollection[" + j + "].Q2Amount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, "II/" + year, Global.SectionSpendingPlan),
                                            true, true));

                            quarter.IsQ2AmountValid = false;
                        }
                        else
                        {
                            quarter.IsQ2AmountValid = true;
                            spendingBudgetLevel1Sum += quarter.Q2Amount;
                        }

                        if (quarter.Q3Amount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].QuarterlyDistributionCollection[" + j + "].Q3Amount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, "III/" + year, Global.SectionSpendingPlan),
                                            true, true));

                            quarter.IsQ3AmountValid = false;
                        }
                        else
                        {
                            quarter.IsQ3AmountValid = true;
                            spendingBudgetLevel1Sum += quarter.Q3Amount;
                        }

                        if (quarter.Q4Amount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].QuarterlyDistributionCollection[" + j + "].Q4Amount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, "IV/" + year, Global.SectionSpendingPlan),
                                            true, true));

                            quarter.IsQ4AmountValid = false;
                        }
                        else
                        {
                            quarter.IsQ4AmountValid = true;
                            spendingBudgetLevel1Sum += quarter.Q4Amount;
                        }
                    }

                    if (spendingBudgetLevel1Sum != spendingBudgetLevel1.TotalCalculatedAmount)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].TotalCalculatedAmount",
                                            Global.ShortTemplateQuaterSum,
                                            string.Format(Global.ViewTemplateQuaterSum, spendingBudgetLevel1.Name, Global.SpendingPlanTotalCurrentDocument, Global.SectionSpendingPlan),
                                            true, true));

                        spendingBudgetLevel1.IsTotalCalculatedAmountValid = false;
                    }

                    if (spendingBudgetLevel1.TotalCalculatedAmount != spendingBudgetLevel1.TotalAmount)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".SpendingBudget.SpendingBudgetLevel1Collection[" + i + "].TotalCalculatedAmount",
                                            string.Format(Global.ShortTemplateEqualValues, Global.SpendingPlanTotalCurrentDocument, Global.SpendingPlanTotalAmount),
                                            string.Format(Global.ViewTemplateEqualValue, spendingBudgetLevel1.Name, Global.SpendingPlanTotalCurrentDocument, Global.SectionSpendingPlan, Global.SpendingPlanTotalAmount),
                                            true, true));

                        spendingBudgetLevel1.IsTotalCalculatedAmountValid = false;
                    }
                }
            }
        }
    }
}
