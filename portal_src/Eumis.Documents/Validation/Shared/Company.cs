using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System.Text.RegularExpressions;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Shared
{
    public class Company : CSValidatorBase<R_10004.Company>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10004.Company complexType, string modelPath, IList<ValidationOption> errors)
        {
            string newPath = Regex.Replace(modelPath, @"\[\d+\]", string.Empty);
            bool isUinTypeEmpty = complexType.UinType == null || string.IsNullOrWhiteSpace(complexType.UinType.Id) || string.IsNullOrWhiteSpace(complexType.UinType.Name);
            
            complexType.IsUinTypeValid = true;
            complexType.IsUinValid = true;
            complexType.IsNameValid = true;
            complexType.IsNameEnValid = true;
            complexType.IsCompanyTypeValid = true;
            complexType.IsCompanyLegalTypeValid = true;
            complexType.IsFinancialContributionValid = true;
            complexType.IsEmailValid = true;
            complexType.IsPhone1Valid = true;
            complexType.IsPhone2Valid = true;
            complexType.IsFaxValid = true;
            complexType.IsCompanyRepresentativePersonValid = true;
            complexType.IsCompanyContactPersonValid = true;
            complexType.IsCompanyContactPersonPhoneValid = true;
            complexType.IsCompanyContactPersonEmailValid = true;

            if (isUinTypeEmpty)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".UinType.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Bulstat, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsUinTypeValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Uin))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".Uin",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Bulstat, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsUinValid = false;
            }
            else if (!isUinTypeEmpty)
            {
                string uinTypeValue = complexType.UinType.Id;

                if ((uinTypeValue == UinTypeNomenclature.Bulstat.Code && !BulstatValidator.ValidateBulstat(complexType.Uin)) ||
                    (uinTypeValue == UinTypeNomenclature.UIC.Code && !BulstatValidator.ValidateBulstat(complexType.Uin)) ||
                     uinTypeValue == UinTypeNomenclature.Freelancers.Code && !BulstatValidator.IsValidEGN(complexType.Uin))
                {
                    errors.Add(ValidationOption.Create(
                            modelPath + ".Uin",
                            Global.ShortTemplateInvalid,
                            string.Format(Global.ViewTemplateInvalid, Global.Bulstat, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                    complexType.IsUinValid = false;
                }
                else if (complexType.Uin.Length > Constants.BulstatFieldMaxLength)
                {
                    errors.Add(ValidationOption.Create(
                                    modelPath + ".Uin",
                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.BulstatFieldMaxLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Bulstat, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.BulstatFieldMaxLength), true, true));

                    complexType.IsUinValid = false;
                }
            }

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyName, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsNameValid = false;
            }
            else if (complexType.Name.Length > Constants.CandidateNameLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyName, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidateNameLength), true, true));

                complexType.IsNameValid = false;
            }

            if (!(complexType.CompanyLegalType != null && Constants.CompanyLegalTypePhysicalGid.Equals(complexType.CompanyLegalType.Id)))
            {
                if (string.IsNullOrWhiteSpace(complexType.NameEN))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".NameEN",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyNameEn, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                    complexType.IsNameEnValid = false;
                }
                else if (complexType.NameEN.Length > Constants.CandidateNameEnLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".NameEN",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameEnLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyNameEn, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidateNameEnLength), true, true));

                    complexType.IsNameEnValid = false;
                }
                else if (!_latinRegex.IsMatch(complexType.NameEN))
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".NameEN",
                        Global.ShortTemplateSymbolsNames2,
                        string.Format(Global.ViewTemplateSymbolsNames2, Global.CompanyNameEn, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                    complexType.IsNameEnValid = false;
                }
            }

            if (complexType.CompanyType == null || string.IsNullOrWhiteSpace(complexType.CompanyType.Id) || string.IsNullOrWhiteSpace(complexType.CompanyType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyType.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyType, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyTypeValid = false;
            }

            if (complexType.CompanyLegalType == null || string.IsNullOrWhiteSpace(complexType.CompanyLegalType.Id) || string.IsNullOrWhiteSpace(complexType.CompanyLegalType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyLegalType.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyLegalType, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyLegalTypeValid = false;
            }

            if (complexType.FinancialContribution < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".FinancialContribution",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.FinancialContribution, GetResourceKey(csValidationEngine.AppRioCode, newPath)),
                                    true, true));

                complexType.IsFinancialContributionValid = false;
            }

            if (complexType.Seat != null)
            {
                EngineValidate(csValidationEngine, complexType.Seat, modelPath + ".Seat", errors);
            }

            if (complexType.Correspondence != null)
            {
                EngineValidate(csValidationEngine, complexType.Correspondence, modelPath + ".Correspondence", errors);
            }

            if (!string.IsNullOrWhiteSpace(complexType.Email))
            {
                if (complexType.Email.Length > Constants.CandidateEmailLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Email",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.Email, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidateEmailLength), true, true));

                    complexType.IsEmailValid = false;
                }
                else if (!_emailRegex.IsMatch(complexType.Email))
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Email",
                        Global.ShortTemplateValidMail,
                        string.Format(Global.ViewTemplateValidMail, Global.Email, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                    complexType.IsEmailValid = false;
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                        modelPath + ".Email",
                        Global.ShortTemplateRequired,
                        string.Format(Global.ViewTemplateRequired, Global.Email, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsEmailValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Phone1))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Phone1",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Phone1, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsPhone1Valid = false;
            }
            else if (complexType.Phone1.Length > Constants.CandidatePhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Phone1",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Phone1, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidatePhoneLength), true, true));

                complexType.IsPhone1Valid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.Phone2) && complexType.Phone2.Length > Constants.CandidatePhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Phone2",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Phone2, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidatePhoneLength), true, true));

                complexType.IsPhone2Valid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.Fax) && complexType.Fax.Length > Constants.CandidatePhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Fax",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Fax, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidatePhoneLength), true, true));

                complexType.IsFaxValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.CompanyRepresentativePerson))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyRepresentativePerson",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyRepresentativePerson, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyRepresentativePersonValid = false;
            }
            else if (complexType.CompanyRepresentativePerson.Length > Constants.CompanyRepresentativePersonLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyRepresentativePerson",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CompanyRepresentativePersonLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyRepresentativePerson, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CompanyRepresentativePersonLength), true, true));

                complexType.IsCompanyRepresentativePersonValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.CompanyContactPerson))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyContactPerson",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyContactPerson, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyContactPersonValid = false;
            }
            else if (complexType.CompanyContactPerson.Length > Constants.ContactPersonLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyContactPerson",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContactPersonLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPerson, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.ContactPersonLength), true, true));

                complexType.IsCompanyContactPersonValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.CompanyContactPersonPhone))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyContactPersonPhone",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonPhone, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyContactPersonPhoneValid = false;
            }
            else if (complexType.CompanyContactPersonPhone.Length > Constants.CandidatePhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyContactPersonPhone",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonPhone, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidatePhoneLength), true, true));

                complexType.IsCompanyContactPersonPhoneValid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.CompanyContactPersonEmail))
            {
                if (complexType.CompanyContactPersonEmail.Length > Constants.CandidateEmailLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".CompanyContactPersonEmail",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonEmail, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidateEmailLength), true, true));

                    complexType.IsCompanyContactPersonEmailValid = false;
                }
                else if (!_emailRegex.IsMatch(complexType.CompanyContactPersonEmail))
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".CompanyContactPersonEmail",
                        Global.ShortTemplateValidMail,
                        string.Format(Global.ViewTemplateValidMail, Global.Email, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                    complexType.IsCompanyContactPersonEmailValid = false;
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".CompanyContactPersonEmail",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonEmail, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCompanyContactPersonEmailValid = false;
            }
        }

        private Regex _latinRegex = new Regex(@"^[^А-Яа-я]*$");
        private Regex _emailRegex = new Regex(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$");
    }
}