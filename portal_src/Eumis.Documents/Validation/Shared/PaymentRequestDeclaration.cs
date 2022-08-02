using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class PaymentRequestDeclaration : CSValidatorBase<R_10050.PaymentRequestDeclaration>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10050.PaymentRequestDeclaration complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsRepresentingBeneficiaryEGNValid = true;
            complexType.IsRepresentingFirstNameValid = true;
            complexType.IsRepresentingMiddleNameValid = true;
            complexType.IsRepresentingLastNameValid = true;
            complexType.IsTextPt3Valid = true;
            complexType.IsTextPt25Valid = true;

            if (string.IsNullOrWhiteSpace(complexType.RepresentingBeneficiaryEGN))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".RepresentingBeneficiaryEGN",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PIN, Global.SectionDeclaration), true, true));

                complexType.IsRepresentingBeneficiaryEGNValid = false;
            }
            else if (!BulstatValidator.IsValidEGN(complexType.RepresentingBeneficiaryEGN))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".RepresentingBeneficiaryEGN",
                                    Global.ShortTemplateInvalid,
                                    string.Format(Global.ViewTemplateInvalid, Global.PIN, Global.SectionDeclaration), true, true));

                complexType.IsRepresentingBeneficiaryEGNValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.RepresentingFirstName))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".RepresentingFirstName",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.FirstName, Global.SectionDeclaration), true, true));

                complexType.IsRepresentingFirstNameValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.RepresentingMiddleName))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".RepresentingMiddleName",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.MiddleName, Global.SectionDeclaration), true, true));

                complexType.IsRepresentingMiddleNameValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.RepresentingLastName))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".RepresentingLastName",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.LastName, Global.SectionDeclaration), true, true));

                complexType.IsRepresentingLastNameValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.TextPt3))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".TextPt3",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TextPt3, Global.SectionDeclaration), true, true));

                complexType.IsTextPt3Valid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.TextPt25))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".TextPt25",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TextPt25, Global.SectionDeclaration), true, true));

                complexType.IsTextPt25Valid = false;
            }
        }
    }
}
