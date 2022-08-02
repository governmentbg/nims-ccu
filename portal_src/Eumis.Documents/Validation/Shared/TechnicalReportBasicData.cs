using System.Collections.Generic;
using System.Text.RegularExpressions;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class TechnicalReportBasicData : CSValidatorBase<R_10052.TechnicalReportBasicData>
    {
        private Regex _emailRegex = new Regex(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$");

        protected override void Validate(ICSValidationEngine csValidationEngine, R_10052.TechnicalReportBasicData complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsStartDateValid = true;
            complexType.IsEndDateValid = true;
            complexType.IsReportTypeValid = true;

            complexType.IsPreparerNameValid = true;
            complexType.IsPreparerPositionValid = true;
            complexType.IsPreparerPhoneValid = true;
            complexType.IsPreparerEmailValid = true;

            if (!complexType.StartDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".StartDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TechReportStartDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
            }

            if (!complexType.EndDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TechReportEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsEndDateValid = false;
            }

            if (complexType.StartDate.HasValue && complexType.EndDate.HasValue && complexType.EndDate.Value < complexType.StartDate.Value)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateStartEndDates, Global.TechReportStartDate, Global.TechReportEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
                complexType.IsEndDateValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.PreparerName))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PreparerName",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PreparerName, Global.SectionGeneralInformation), true, true));

                complexType.IsPreparerNameValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.PreparerPosition))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PreparerPosition",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PreparerPosition, Global.SectionGeneralInformation), true, true));

                complexType.IsPreparerPositionValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.PreparerPhone))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PreparerPhone",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PreparerPhone, Global.SectionGeneralInformation), true, true));

                complexType.IsPreparerPhoneValid = false;
            }
            else if (complexType.PreparerPhone.Length > Constants.PreparerPhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".PreparerPhone",
                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.PreparerPhoneLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.PreparerPhone, Global.SectionGeneralInformation, Constants.PreparerPhoneLength), true, true));

                complexType.IsPreparerPhoneValid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.PreparerEmail))
            {
                if (complexType.PreparerEmail.Length > Constants.PreparerEmailLength)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".PreparerEmail",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.PreparerEmailLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.PreparerEmail, Global.SectionGeneralInformation, Constants.PreparerEmailLength), true, true));

                    complexType.IsPreparerEmailValid = false;
                }
                else if (!_emailRegex.IsMatch(complexType.PreparerEmail))
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".PreparerEmail",
                                        Global.ShortTemplateValidMail,
                                        string.Format(Global.ViewTemplateValidMail, Global.PreparerEmail, Global.SectionGeneralInformation), true, true));

                    complexType.IsPreparerEmailValid = false;
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                            modelPath + ".PreparerEmail",
                            Global.ShortTemplateRequired,
                            string.Format(Global.ViewTemplateRequired, Global.PreparerEmail, Global.SectionGeneralInformation), true, true));

                complexType.IsPreparerEmailValid = false;
            }
        }
    }
}
