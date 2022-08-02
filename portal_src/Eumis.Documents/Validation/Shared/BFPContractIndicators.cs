using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class BFPContractIndicators : CSValidatorBase<R_10040.BFPContractIndicators>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10040.BFPContractIndicators complexType, string modelPath, IList<ValidationOption> errors)
        {
            for (int i = 0; i < complexType.BFPContractIndicatorCollection.Count; i++)
            {
                if (complexType.BFPContractIndicatorCollection[i].isActive && !complexType.BFPContractIndicatorCollection[i].IsDeactivated)
                {
                    R_10038.SelectedIndicator original = null;

                    complexType.BFPContractIndicatorCollection[i].IsNameValid = true;

                    if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator == null || string.IsNullOrWhiteSpace(complexType.BFPContractIndicatorCollection[i].SelectedIndicator.Id) || string.IsNullOrWhiteSpace(complexType.BFPContractIndicatorCollection[i].SelectedIndicator.Name))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.Id",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.IndicatorName, Global.SectionIndicators), true, true));

                        complexType.BFPContractIndicatorCollection[i].IsNameValid = false;
                    }
                    else
                    {

                        original = complexType.Items.FirstOrDefault(s => s.Id == complexType.BFPContractIndicatorCollection[i].SelectedIndicator.Id
                                                                      && s.Name == complexType.BFPContractIndicatorCollection[i].SelectedIndicator.Name);
                        if (original == null)
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.Id",
                                            Global.ShortTemplateInvalidIndicator,
                                            string.Format(Global.ViewTemplateInvalidIndicator, Global.SectionIndicators), true, true));

                            complexType.BFPContractIndicatorCollection[i].IsNameValid = false;
                        }
                        else
                        {
                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.TypeName != original.TypeName)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.TypeName",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.TypeName, Global.SectionIndicators), true, true));
                            }

                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.TrendName != original.TrendName)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.TrendName",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.TrendName, Global.SectionIndicators), true, true));
                            }

                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.KindName != original.KindName)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.KindName",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.KindName, Global.SectionIndicators), true, true));
                            }

                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.MeasureName != original.MeasureName)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.MeasureName",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.MeasureName, Global.SectionIndicators), true, true));
                            }

                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.AggregatedReport != original.AggregatedReport)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.AggregatedReport",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.AggregatedReport, Global.SectionIndicators), true, true));
                            }

                            if (complexType.BFPContractIndicatorCollection[i].SelectedIndicator.AggregatedTarget != original.AggregatedTarget)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].SelectedIndicator.AggregatedTarget",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateInvalid, Global.AggregatedTarget, Global.SectionIndicators), true, true));
                            }
                        }
                    }

                    if (original != null)
                    {
                        if (original.HasGenderDivision)
                        {
                            if (complexType.BFPContractIndicatorCollection[i].BaseMen < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].BaseMen",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.BaseMen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsBaseMenValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsBaseMenValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].BaseWomen < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].BaseWomen",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.BaseWomen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsBaseWomenValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsBaseWomenValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].BaseTotal < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].BaseTotal",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Base, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = false;
                            }
                            else if ((complexType.BFPContractIndicatorCollection[i].BaseMen + complexType.BFPContractIndicatorCollection[i].BaseWomen) != complexType.BFPContractIndicatorCollection[i].BaseTotal)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].BaseTotal",
                                                    string.Format(Global.ShortTemplateSumBFP_SF, Global.BaseMen, Global.BaseWomen),
                                                    string.Format(Global.ViewTemplateSumBFP_SF, Global.Base, Global.BaseMen, Global.BaseWomen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].TargetMen < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetMen",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.TargetMen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetMenValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsTargetMenValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].TargetWomen < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetWomen",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.TargetWomen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetWomenValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsTargetWomenValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].TargetTotal < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetTotal",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Target, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = false;
                            }
                            else if ((complexType.BFPContractIndicatorCollection[i].TargetMen + complexType.BFPContractIndicatorCollection[i].TargetWomen) != complexType.BFPContractIndicatorCollection[i].TargetTotal)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetTotal",
                                                    string.Format(Global.ShortTemplateSumBFP_SF, Global.TargetMen, Global.TargetWomen),
                                                    string.Format(Global.ViewTemplateSumBFP_SF, Global.Target, Global.TargetMen, Global.TargetWomen, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = true;
                            }
                        }
                        else
                        {
                            if (complexType.BFPContractIndicatorCollection[i].BaseTotal < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].BaseTotal",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Base, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = true;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].TargetTotal < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetTotal",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Target, Global.SectionIndicators),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = false;
                            }
                            else
                            {
                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = true;
                            }
                        }

                        if (complexType.BFPContractIndicatorCollection[i].TargetTotal >= 0 && complexType.BFPContractIndicatorCollection[i].BaseTotal >= 0)
                        {
                            if (complexType.BFPContractIndicatorCollection[i].Trend == Contracts.IndicatorTrend.Increase && 
                                complexType.BFPContractIndicatorCollection[i].BaseTotal > complexType.BFPContractIndicatorCollection[i].TargetTotal)
                            {

                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetTotal",
                                                    string.Empty,
                                                    string.Format(Global.ViewTemplateSmallerEqualNumberLong, Global.Base, Global.SectionIndicators, Global.Target),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = false;
                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = false;
                            }

                            if (complexType.BFPContractIndicatorCollection[i].Trend == Contracts.IndicatorTrend.Reduction &&
                                complexType.BFPContractIndicatorCollection[i].BaseTotal < complexType.BFPContractIndicatorCollection[i].TargetTotal)
                            {

                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractIndicatorCollection[" + i + "].TargetTotal",
                                                    string.Empty,
                                                    string.Format(Global.ViewTemplateSmallerEqualNumberLong, Global.Target, Global.SectionIndicators, Global.Base),
                                                    true, true));

                                complexType.BFPContractIndicatorCollection[i].IsTargetValid = false;
                                complexType.BFPContractIndicatorCollection[i].IsBaseValid = false;
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(complexType.BFPContractIndicatorCollection[i].Description))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].Description",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.InformationSource, Global.SectionIndicators), true, true));

                        complexType.BFPContractIndicatorCollection[i].IsDescriptionValid = false;
                    }
                    else if (complexType.BFPContractIndicatorCollection[i].Description.Length > Constants.IndicatorDescriptionLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".BFPContractIndicatorCollection[" + i + "].Description",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.IndicatorDescriptionLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.InformationSource, Global.SectionIndicators, Constants.IndicatorDescriptionLength), true, true));

                        complexType.BFPContractIndicatorCollection[i].IsDescriptionValid = false;
                    }
                    else
                    {
                        complexType.BFPContractIndicatorCollection[i].IsDescriptionValid = true;
                    }
                }
            }
        }
    }
}
