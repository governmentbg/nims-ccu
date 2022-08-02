using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class ProjectErrand : CSValidatorBase<R_10016.ProjectErrand>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10016.ProjectErrand complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsNameValid = true;
            complexType.IsErrandAreaValid = true;
            complexType.IsErrandLegalActValid = true;
            complexType.IsErrandTypeValid = true;
            complexType.IsAmountValid = true;
            complexType.IsPlanDateValid = true;
            complexType.IsDescriptionValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ProcedureSubject, Global.SectionOutsourcingPlan), true, true));

                complexType.IsNameValid = false;
            }
            else if (complexType.Name.Length > Constants.ProjectErrandNameLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandNameLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.ProcedureSubject, Global.SectionOutsourcingPlan, Constants.ProjectErrandNameLength), true, true));

                complexType.IsNameValid = false;
            }

            if (complexType.ErrandArea == null || string.IsNullOrWhiteSpace(complexType.ErrandArea.Id) || string.IsNullOrWhiteSpace(complexType.ErrandArea.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".ErrandArea.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ContractObject, Global.SectionOutsourcingPlan), true, true));

                complexType.IsErrandAreaValid = false;
            }

            if (complexType.ErrandLegalAct == null || string.IsNullOrWhiteSpace(complexType.ErrandLegalAct.Id) || string.IsNullOrWhiteSpace(complexType.ErrandLegalAct.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".ErrandLegalAct.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ApplicableLegalAct, Global.SectionOutsourcingPlan), true, true));

                complexType.IsErrandLegalActValid = false;
            }

            if (complexType.ErrandType == null || string.IsNullOrWhiteSpace(complexType.ErrandType.Id) || string.IsNullOrWhiteSpace(complexType.ErrandType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".ErrandType.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ProcedureType, Global.SectionOutsourcingPlan), true, true));

                complexType.IsErrandTypeValid = false;
            }

            if (complexType.Amount < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".Amount",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionOutsourcingPlan),
                                    true, true));

                complexType.IsAmountValid = false;
            }

            //if (!complexType.PlanDate.HasValue)
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".PlanDate",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ScheduledDate, Global.SectionOutsourcingPlan),
            //                        true, true));

            //    complexType.IsPlanDateValid = false;
            //}

            if (string.IsNullOrWhiteSpace(complexType.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ProjectErrandDescription, Global.SectionOutsourcingPlan), true, true));

                complexType.IsDescriptionValid = false;
            }
            else if (complexType.Description.Length > Constants.ProjectErrandDescriptionLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandDescriptionLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.ProjectErrandDescription, Global.SectionOutsourcingPlan, Constants.ProjectErrandDescriptionLength), true, true));

                complexType.IsDescriptionValid = false;
            }
        }
    }
}