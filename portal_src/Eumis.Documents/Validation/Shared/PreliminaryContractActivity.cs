using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System.Text.RegularExpressions;

namespace Eumis.Documents.Validation.Shared
{
    public class PreliminaryContractActivity : CSValidatorBase<R_10030.PreliminaryContractActivity>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10030.PreliminaryContractActivity complexType, string modelPath, IList<ValidationOption> errors)
        {
            string sectionName = Global.SectionImplementationPlan;

            if (string.IsNullOrWhiteSpace(complexType.Code))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Code",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Activity, sectionName), true, true));

                complexType.IsCodeValid = false;
            }
            else if (complexType.Code.Length > Constants.ContractActivityCodeLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Code",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractActivityCodeLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Activity, sectionName, Constants.ContractActivityCodeLength), true, true));

                complexType.IsCodeValid = false;
            }
            else
                complexType.IsCodeValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Description, sectionName), true, true));

                complexType.IsNameValid = false;
            }
            else if (complexType.Name.Length > Constants.ContractActivityNameLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractActivityNameLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Description, sectionName, Constants.ContractActivityNameLength), true, true));

                complexType.IsNameValid = false;
            }
            else
                complexType.IsNameValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Result))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Result",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Result, sectionName), true, true));

                complexType.IsResultValid = false;
            }
            else if (complexType.Result.Length > Constants.ContractActivityResultLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Result",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractActivityResultLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Result, sectionName, Constants.ContractActivityResultLength), true, true));

                complexType.IsResultValid = false;
            }
            else
                complexType.IsResultValid = true;
        }
    }
}