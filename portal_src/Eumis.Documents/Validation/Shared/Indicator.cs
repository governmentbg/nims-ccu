using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class Indicator : CSValidatorBase<R_10013.Indicator>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10013.Indicator complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (string.IsNullOrWhiteSpace(complexType.Id) || string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.IndicatorName, Global.SectionIndicators), true, true));
            }

            if (complexType.BaseTotal < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".BaseTotal",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Base, Global.SectionIndicators),
                                    true, true));
            }

            if (complexType.TargetTotal < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".TargetTotal",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Target, Global.SectionIndicators),
                                    true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.InformationSource, Global.SectionIndicators), true, true));
            }
        }
    }
}