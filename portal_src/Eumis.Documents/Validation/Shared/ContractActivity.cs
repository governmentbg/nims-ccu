using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class ContractActivity : CSValidatorBase<R_10011.ContractActivity>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10011.ContractActivity complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (complexType.CompanyCollection == null || complexType.CompanyCollection.Count == 0)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Company.Id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Company, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Code))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Code",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Activity, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ActivityDescription, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.ExecutionMethod))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".ExecutionMethod",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ExecutionMethod, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Result))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Result",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Result, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.StartMonth))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".StartMonth",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.StartMonth, Global.SectionImplementationPlan), true, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Duration))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Duration",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Duration2, Global.SectionImplementationPlan), true, true));
            }
        }
    }
}