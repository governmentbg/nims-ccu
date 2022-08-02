using System.Collections.Generic;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class TechnicalReportIndicator : CSValidatorBase<R_10054.TechnicalReportIndicator>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10054.TechnicalReportIndicator complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.BFPContractIndicator.IsDescriptionValid = true;

            complexType.IsPeriodAmountMenValid = true;
            complexType.IsPeriodAmountWomenValid = true;
            complexType.IsPeriodAmountTotalValid = true;

            complexType.IsCumulativeAmountMenValid = true;
            complexType.IsCumulativeAmountWomenValid = true;
            complexType.IsCumulativeAmountTotalValid = true;

            complexType.IsResidueAmountMenValid = true;
            complexType.IsResidueAmountWomenValid = true;
            complexType.IsResidueAmountTotalValid = true;

            complexType.IsCommentValid = true;

            if (string.IsNullOrWhiteSpace(complexType.BFPContractIndicator.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractIndicator.Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.InformationSource, Global.SectionIndicators), true, true));

                complexType.BFPContractIndicator.IsDescriptionValid = false;
            }
            else if (complexType.BFPContractIndicator.Description.Length > Constants.IndicatorDescriptionLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractIndicator.Description",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.IndicatorDescriptionLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.InformationSource, Global.SectionIndicators, Constants.IndicatorDescriptionLength), true, true));

                complexType.BFPContractIndicator.IsDescriptionValid = false;
            }
            else
            {
                complexType.BFPContractIndicator.IsDescriptionValid = true;
            }

            if (complexType.PeriodAmountMen < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PeriodAmountMen",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorPeriodAmount, Global.SectionIndicators), true, true));

                complexType.IsPeriodAmountMenValid = false;
            }
            if (complexType.PeriodAmountWomen < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PeriodAmountWomen",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorPeriodAmount, Global.SectionIndicators), true, true));

                complexType.IsPeriodAmountWomenValid = false;
            }
            if (complexType.PeriodAmountTotal < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PeriodAmountTotal",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorPeriodAmount, Global.SectionIndicators), true, true));

                complexType.IsPeriodAmountTotalValid = false;
            }

            bool leadToLessThanLastApprovedAmountError = complexType.BFPContractIndicator.Trend != Contracts.IndicatorTrend.Inapplicable && complexType.IsAggregatedReport == false;

            if (complexType.CumulativeAmountMen < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountMen",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));

                complexType.IsCumulativeAmountMenValid = false;
            }
            else if (complexType.CumulativeAmountMen < complexType.LastReportCumulativeAmountMen && complexType.HasGenderDivision && leadToLessThanLastApprovedAmountError)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountMen",
                                    Global.ShortTemplateInvalid,
                                    string.Format(Global.ViewTemplateLessThanLastApprovedAmount, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));
                complexType.IsCumulativeAmountMenValid = false;
            }
            if (complexType.CumulativeAmountWomen < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountWomen",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));

                complexType.IsCumulativeAmountWomenValid = false;
            }
            else if (complexType.CumulativeAmountWomen < complexType.LastReportCumulativeAmountWomen && complexType.HasGenderDivision && leadToLessThanLastApprovedAmountError)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountWomen",
                                    Global.ShortTemplateInvalid,
                                    string.Format(Global.ViewTemplateLessThanLastApprovedAmount, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));
                complexType.IsCumulativeAmountWomenValid = false;
            }
            if (complexType.CumulativeAmountTotal < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountTotal",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));

                complexType.IsCumulativeAmountTotalValid = false;
            }
            if (complexType.CumulativeAmountTotal < complexType.LastReportCumulativeAmountTotal && leadToLessThanLastApprovedAmountError)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CumulativeAmountTotal",
                                    Global.ShortTemplateInvalid,
                                    string.Format(Global.ViewTemplateLessThanLastApprovedAmount, Global.IndicatorCumulativeAmount, Global.SectionIndicators), true, true));
                complexType.IsCumulativeAmountTotalValid = false;
            }

            // if (complexType.ResidueAmountMen < 0)
            // {
            //     errors.Add(ValidationOption.Create(
            //                         modelPath + ".ResidueAmountMen",
            //                         Global.ShortTemplateNonNegativeNumber,
            //                         string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorResidueAmount, Global.SectionIndicators), true, true));
            // 
            //     complexType.IsResidueAmountMenValid = false;
            // }
            // if (complexType.ResidueAmountWomen < 0)
            // {
            //     errors.Add(ValidationOption.Create(
            //                         modelPath + ".ResidueAmountWomen",
            //                         Global.ShortTemplateNonNegativeNumber,
            //                         string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorResidueAmount, Global.SectionIndicators), true, true));
            // 
            //     complexType.IsResidueAmountWomenValid = false;
            // }
            // if (complexType.ResidueAmountTotal < 0)
            // {
            //     errors.Add(ValidationOption.Create(
            //                         modelPath + ".ResidueAmountTotal",
            //                         Global.ShortTemplateNonNegativeNumber,
            //                         string.Format(Global.ViewTemplateNonNegativeNumber, Global.IndicatorResidueAmount, Global.SectionIndicators), true, true));
            // 
            //     complexType.IsResidueAmountTotalValid = false;
            // }

            // if (string.IsNullOrWhiteSpace(complexType.Comment))
            // {
            //     errors.Add(ValidationOption.Create(
            //                         modelPath + ".Comment",
            //                         Global.ShortTemplateRequired,
            //                         string.Format(Global.ViewTemplateRequired, Global.IndicatorComment, Global.SectionIndicators), true, true));
            // 
            //     complexType.IsCommentValid = false;
            // }
        }
    }
}
