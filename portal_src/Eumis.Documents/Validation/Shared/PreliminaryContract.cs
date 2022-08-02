using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class PreliminaryContract : CSValidatorBase<R_10029.PreliminaryContract>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10029.PreliminaryContract complexType, string modelPath, IList<ValidationOption> errors)
        {
            string contractSectionName = Global.SectionFinancialInformation;

            if (complexType != null)
            {

                if (!complexType.isLocked)
                {
                    complexType.IsRequestedFundingAmountValid = true;
                    complexType.IsCoFinancingBudgetAmountValid = true;

                    if (complexType.RequestedFundingAmount < 0)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".RequestedFundingAmount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.RequestedFundingAmount, contractSectionName),
                                            true, true));

                        complexType.IsRequestedFundingAmountValid = false;
                    }

                    if (complexType.CoFinancingBudgetAmount < 0)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".CoFinancingBudgetAmount",
                                            Global.ShortTemplateNonNegativeNumber,
                                            string.Format(Global.ViewTemplateNonNegativeNumber, Global.CoFinancingBudgetAmount, contractSectionName),
                                            true, true));

                        complexType.IsCoFinancingBudgetAmountValid = false;
                    }
                }

            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + "",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, contractSectionName), true, true));
            }
        }
    }
}