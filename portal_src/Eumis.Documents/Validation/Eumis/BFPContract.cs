using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Eumis
{
    public class BFPContract : CSValidatorBase<R_10040.BFPContract>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10040.BFPContract complexType,
            string modelPath, IList<ValidationOption> errors)
        {
            bool isDurationValid = true;

            #region BFPContractBasicData

            if (complexType.ProcedureKind.Value == ProcedureKind.Schema.Value)
            {
                if (complexType.BFPContractBasicData != null)
                {
                    if (!string.IsNullOrWhiteSpace(complexType.BFPContractBasicData.Duration))
                    {
                        int t;
                        int minMonths = 1;
                        //int maxMonths = 24;

                        if (!int.TryParse(complexType.BFPContractBasicData.Duration, out t) || t < minMonths || t > complexType.BFPContractBasicData.MaxDuration)
                        {
                            isDurationValid = false;
                        }
                    }
                    else
                    {
                        isDurationValid = false;
                    }

                    EngineValidate(csValidationEngine, complexType.BFPContractBasicData, modelPath + ".BFPContractBasicData", errors);
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".BFPContractBasicData",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateMissingSection, Global.SectionBasicData), true, true));
                }
            }

            #endregion

            #region Beneficiary

            if (complexType.Beneficiary != null)
            {
                bool isUinTypeEmpty = complexType.Beneficiary.UinType == null || string.IsNullOrWhiteSpace(complexType.Beneficiary.UinType.Id) || string.IsNullOrWhiteSpace(complexType.Beneficiary.UinType.Name);

                complexType.Beneficiary.IsUinTypeValid = true;
                complexType.Beneficiary.IsUinValid = true;
                complexType.Beneficiary.IsNameValid = true;
                complexType.Beneficiary.IsNameEnValid = true;
                complexType.Beneficiary.IsCompanyTypeValid = true;
                complexType.Beneficiary.IsCompanyLegalTypeValid = true;
                complexType.Beneficiary.IsEmailValid = true;
                complexType.Beneficiary.IsPhone1Valid = true;
                complexType.Beneficiary.IsPhone2Valid = true;
                complexType.Beneficiary.IsFaxValid = true;
                complexType.Beneficiary.IsCompanyRepresentativePersonValid = true;
                complexType.Beneficiary.IsCompanyContactPersonValid = true;
                complexType.Beneficiary.IsCompanyContactPersonPhoneValid = true;
                complexType.Beneficiary.IsCompanyContactPersonEmailValid = true;

                if (isUinTypeEmpty)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.UinType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionBeneficiary), true, true));

                    complexType.Beneficiary.IsUinTypeValid = false;
                }

                if (string.IsNullOrWhiteSpace(complexType.Beneficiary.Uin))
                {
                    if (isUinTypeEmpty || complexType.Beneficiary.UinType.Id != UinTypeNomenclature.Foreign.Code)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.Uin",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsUinValid = false;
                    }
                }
                else if (!isUinTypeEmpty)
                {
                    string uinTypeValue = complexType.Beneficiary.UinType.Id;

                    if ((uinTypeValue == UinTypeNomenclature.Bulstat.Code && !BulstatValidator.ValidateBulstat(complexType.Beneficiary.Uin)) ||
                        (uinTypeValue == UinTypeNomenclature.UIC.Code && !BulstatValidator.ValidateBulstat(complexType.Beneficiary.Uin)) ||
                         uinTypeValue == UinTypeNomenclature.Freelancers.Code && !BulstatValidator.IsValidEGN(complexType.Beneficiary.Uin))
                    {
                        errors.Add(ValidationOption.Create(
                                modelPath + ".Beneficiary.Uin",
                                Global.ShortTemplateInvalid,
                                string.Format(Global.ViewTemplateInvalid, Global.Bulstat, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsUinValid = false;
                    }
                    else if (complexType.Beneficiary.Uin.Length > Constants.BulstatFieldMaxLength)
                    {
                        errors.Add(ValidationOption.Create(
                                        modelPath + ".Beneficiary.Uin",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.BulstatFieldMaxLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Bulstat, Global.SectionBeneficiary, Constants.BulstatFieldMaxLength), true, true));

                        complexType.Beneficiary.IsUinValid = false;
                    }
                }

                if (string.IsNullOrWhiteSpace(complexType.Beneficiary.Name))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.Name",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyName, Global.SectionBeneficiary), true, true));

                    complexType.Beneficiary.IsNameValid = false;
                }
                else if (complexType.Beneficiary.Name.Length > Constants.CandidateNameLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.Name",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyName, Global.SectionBeneficiary, Constants.CandidateNameLength), true, true));

                    complexType.Beneficiary.IsNameValid = false;
                }

                if (!(complexType.Beneficiary.CompanyLegalType != null && Constants.CompanyLegalTypePhysicalGid.Equals(complexType.Beneficiary.CompanyLegalType.Id)))
                {
                    if (string.IsNullOrWhiteSpace(complexType.Beneficiary.NameEN))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.NameEN",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CompanyNameEn, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsNameEnValid = false;
                    }
                    else if (complexType.Beneficiary.NameEN.Length > Constants.CandidateNameEnLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.NameEN",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameEnLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyNameEn, Global.SectionBeneficiary, Constants.CandidateNameEnLength), true, true));

                        complexType.Beneficiary.IsNameEnValid = false;
                    }
                    else if (!_latinRegex.IsMatch(complexType.Beneficiary.NameEN))
                    {
                        errors.Add(ValidationOption.Create(
                            modelPath + ".Beneficiary.NameEN",
                            Global.ShortTemplateSymbolsNames2,
                            string.Format(Global.ViewTemplateSymbolsNames2, Global.CompanyNameEn, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsNameEnValid = false;
                    }
                }

                if (complexType.Beneficiary.CompanyType == null || string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyType.Id) || string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyType.Name))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.CompanyType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyType, Global.SectionBeneficiary), true, true));

                    complexType.Beneficiary.IsCompanyTypeValid = false;
                }

                if (complexType.Beneficiary.CompanyLegalType == null || string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyLegalType.Id) || string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyLegalType.Name))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Beneficiary.CompanyLegalType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyLegalType, Global.SectionBeneficiary), true, true));

                    complexType.Beneficiary.IsCompanyLegalTypeValid = false;
                }

                if (complexType.Beneficiary.Seat != null)
                {
                    EngineValidate(csValidationEngine, complexType.Beneficiary.Seat, modelPath + ".Beneficiary.Seat", errors);
                }

                if (complexType.ProcedureKind == ProcedureKind.Schema)
                {
                    if (complexType.Beneficiary.Correspondence != null)
                    {
                        EngineValidate(csValidationEngine, complexType.Beneficiary.Correspondence, modelPath + ".Beneficiary.Correspondence", errors);
                    }

                    if (!string.IsNullOrWhiteSpace(complexType.Beneficiary.Email))
                    {
                        if (complexType.Beneficiary.Email.Length > Constants.CandidateEmailLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Beneficiary.Email",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Email, Global.SectionBeneficiary, Constants.CandidateEmailLength), true, true));

                            complexType.Beneficiary.IsEmailValid = false;
                        }
                        else if (!_emailRegex.IsMatch(complexType.Beneficiary.Email))
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".Beneficiary.Email",
                                Global.ShortTemplateValidMail,
                                string.Format(Global.ViewTemplateValidMail, Global.Email, Global.SectionBeneficiary), true, true));

                            complexType.Beneficiary.IsEmailValid = false;
                        }
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                                modelPath + ".Beneficiary.Email",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.Email, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsEmailValid = false;
                    }

                    if (string.IsNullOrWhiteSpace(complexType.Beneficiary.Phone1))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.Phone1",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Phone1, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsPhone1Valid = false;
                    }
                    else if (complexType.Beneficiary.Phone1.Length > Constants.CandidatePhoneLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.Phone1",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Phone1, Global.SectionBeneficiary, Constants.CandidatePhoneLength), true, true));

                        complexType.Beneficiary.IsPhone1Valid = false;
                    }

                    if (!string.IsNullOrWhiteSpace(complexType.Beneficiary.Phone2) && complexType.Beneficiary.Phone2.Length > Constants.CandidatePhoneLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.Phone2",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Phone2, Global.SectionBeneficiary, Constants.CandidatePhoneLength), true, true));

                        complexType.Beneficiary.IsPhone2Valid = false;
                    }

                    if (!string.IsNullOrWhiteSpace(complexType.Beneficiary.Fax) && complexType.Beneficiary.Fax.Length > Constants.CandidatePhoneLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.Fax",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Fax, Global.SectionBeneficiary, Constants.CandidatePhoneLength), true, true));

                        complexType.Beneficiary.IsFaxValid = false;
                    }

                    if (string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyRepresentativePerson))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyRepresentativePerson",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CompanyRepresentativePerson, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsCompanyRepresentativePersonValid = false;
                    }
                    else if (complexType.Beneficiary.CompanyRepresentativePerson.Length > Constants.CompanyRepresentativePersonLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyRepresentativePerson",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CompanyRepresentativePersonLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyRepresentativePerson, Global.SectionBeneficiary, Constants.CompanyRepresentativePersonLength), true, true));

                        complexType.Beneficiary.IsCompanyRepresentativePersonValid = false;
                    }

                    if (string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyContactPerson))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyContactPerson",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CompanyContactPerson, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsCompanyContactPersonValid = false;
                    }
                    else if (complexType.Beneficiary.CompanyContactPerson.Length > Constants.ContactPersonLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyContactPerson",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.ContactPersonLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPerson, Global.SectionBeneficiary, Constants.ContactPersonLength), true, true));

                        complexType.Beneficiary.IsCompanyContactPersonValid = false;
                    }

                    if (string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyContactPersonPhone))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyContactPersonPhone",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonPhone, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsCompanyContactPersonPhoneValid = false;
                    }
                    else if (complexType.Beneficiary.CompanyContactPersonPhone.Length > Constants.CandidatePhoneLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyContactPersonPhone",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonPhone, Global.SectionBeneficiary, Constants.CandidatePhoneLength), true, true));

                        complexType.Beneficiary.IsCompanyContactPersonPhoneValid = false;
                    }

                    if (!string.IsNullOrWhiteSpace(complexType.Beneficiary.CompanyContactPersonEmail))
                    {
                        if (complexType.Beneficiary.CompanyContactPersonEmail.Length > Constants.CandidateEmailLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Beneficiary.CompanyContactPersonEmail",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonEmail, Global.SectionBeneficiary, Constants.CandidateEmailLength), true, true));

                            complexType.Beneficiary.IsCompanyContactPersonEmailValid = false;
                        }
                        else if (!_emailRegex.IsMatch(complexType.Beneficiary.CompanyContactPersonEmail))
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".Beneficiary.CompanyContactPersonEmail",
                                Global.ShortTemplateValidMail,
                                string.Format(Global.ViewTemplateValidMail, Global.Email, Global.SectionBeneficiary), true, true));

                            complexType.Beneficiary.IsCompanyContactPersonEmailValid = false;
                        }
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Beneficiary.CompanyContactPersonEmail",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonEmail, Global.SectionBeneficiary), true, true));

                        complexType.Beneficiary.IsCompanyContactPersonEmailValid = false;
                    }
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Beneficiary",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionBeneficiary), true, true));
            }

            #endregion

            #region Partners

            if (complexType.Partners != null && complexType.Partners.PartnerCollection != null && complexType.Partners.PartnerCollection.Count > 0)
            {
                if (complexType.Partners.PartnerCollection.Count <= Constants.PartnersMaxCount)
                {
                    for (int i = 0; i < complexType.Partners.PartnerCollection.Count; i++)
                    {
                        EngineValidate(csValidationEngine, complexType.Partners.PartnerCollection[i],
                            modelPath + ".Partners.PartnerCollection[" + i + "]", errors);
                    }
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Partners.PartnerCollection",
                        string.Format(Global.ShortTemplateMaxPartners, Constants.PartnersMaxCount),
                        string.Format(Global.ViewTemplateMaxPartners, Constants.PartnersMaxCount, Global.SectionPartners),
                        true, true));

                    complexType.Partners.PartnerCollection.HasValidCount = false;
                }
            }

            #endregion

            #region Directions

            #region Directions

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Directions))
            {
                if (complexType.BFPContractDirectionsBudgetContract != null)
                {
                        var directions = complexType.BFPContractDirectionsBudgetContract?.Directions?.DirectionCollection;
                        if (directions == null)
                        {
                            complexType.BFPContractDirectionsBudgetContract.Directions = new R_10093.DirectionSection();
                            complexType.BFPContractDirectionsBudgetContract.Directions.IsValid = false;
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".BFPContractDirectionsBudgetContract.Directions.DirectionSection",
                                            string.Empty,
                                            Global.ViewTemplateDirections, true, true));
                        }
                        else
                        {
                            if (directions.Count == 0)
                            {
                                complexType.BFPContractDirectionsBudgetContract.Directions.IsValid = false;
                                errors.Add(ValidationOption.Create(
                                            modelPath + ".BFPContractDirectionsBudgetContract.Directions.DirectionSection",
                                            string.Empty,
                                            Global.ViewTemplateDirections, true, true));
                            }
                        }
                    
                }
            }

            #endregion

            #endregion

            #region BFPContractDimensionBudgetContract (triple)

            if (complexType.BFPContractDirectionsBudgetContract != null)
            {
                complexType.BFPContractDirectionsBudgetContract.Nomenclatures = complexType.Nomenclatures;

                EngineValidate(csValidationEngine, complexType.BFPContractDirectionsBudgetContract, modelPath + ".BFPContractDimensionBudgetContract", errors);
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractDimensionBudgetContract",
                                        string.Empty,
                                        Global.ViewTemplateDimensionsBudgetContract, true, true));
            }

            #endregion

            #region BFPContractContractActivities

            if (complexType.BFPContractContractActivities != null)
            {
                if (complexType.BFPContractContractActivities.BFPContractContractActivityCollection != null &&
                    complexType.BFPContractContractActivities.BFPContractContractActivityCollection.Count > 0)
                {
                    if (complexType.BFPContractContractActivities.BFPContractContractActivityCollection.Count <= Constants.ContractActivitiesMaxCount)
                    {
                        for (int i = 0; i < complexType.BFPContractContractActivities.BFPContractContractActivityCollection.Count; i++)
                        {
                            int minMonth = int.MaxValue;
                            int maxDuration = 0;

                            var current = complexType.BFPContractContractActivities.BFPContractContractActivityCollection[i];

                            current.IsCompanyValid = true;
                            current.IsCodeValid = true;
                            current.IsNameValid = true;
                            current.IsExecutionMethodValid = true;
                            current.IsResultValid = true;
                            current.IsStartMonthValid = true;
                            current.IsDurationValid = true;
                            current.IsAmountValid = true;
                            current.IsPeriodValid = true;

                            if (current.CompanyCollection == null || current.CompanyCollection.Count == 0)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].CompanyCollection",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Company, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsCompanyValid = false;
                            }

                            if (string.IsNullOrWhiteSpace(current.Code))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Code",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Activity, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsCodeValid = false;
                            }
                            else if (current.Code.Length > Constants.ContractActivityCodeLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Code",
                                    string.Format(Global.ShortTemplateSymbolsMax,
                                        Constants.ContractActivityCodeLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Activity,
                                        Global.SectionImplementationPlanBFP, Constants.ContractActivityCodeLength), true, true));

                                current.IsCodeValid = false;
                            }

                            if (string.IsNullOrWhiteSpace(current.Name))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Name",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsNameValid = false;
                            }
                            else if (current.Name.Length > Constants.ContractActivityNameLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Name",
                                    string.Format(Global.ShortTemplateSymbolsMax,
                                        Constants.ContractActivityNameLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Description,
                                        Global.SectionImplementationPlanBFP, Constants.ContractActivityNameLength), true, true));

                                current.IsNameValid = false;
                            }

                            if (string.IsNullOrWhiteSpace(current.ExecutionMethod))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].ExecutionMethod",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.ExecutionMethod, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsExecutionMethodValid = false;
                            }
                            else if (current.ExecutionMethod.Length > Constants.ContractActivityExecutionMethodLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].ExecutionMethod",
                                    string.Format(Global.ShortTemplateSymbolsMax,
                                        Constants.ContractActivityExecutionMethodLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ExecutionMethod,
                                        Global.SectionImplementationPlanBFP, Constants.ContractActivityExecutionMethodLength),
                                    true, true));

                                current.IsExecutionMethodValid = false;
                            }

                            if (string.IsNullOrWhiteSpace(current.Result))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Result",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Result, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsResultValid = false;
                            }
                            else if (current.Result.Length > Constants.ContractActivityResultLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Result",
                                    string.Format(Global.ShortTemplateSymbolsMax,
                                        Constants.ContractActivityResultLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Result, Global.SectionImplementationPlanBFP,
                                        Constants.ContractActivityResultLength), true, true));

                                current.IsResultValid = false;
                            }

                            bool hasMonth = false;
                            bool hasDuration = false;
                            int month = 0, duration = 0;

                            if (!string.IsNullOrWhiteSpace(current.StartMonth))
                            {
                                if (!int.TryParse(current.StartMonth, out month))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].StartMonth",
                                        Global.ShortTemplateInteger,
                                        string.Format(Global.ViewTemplateInteger, Global.StartMonth, Global.SectionImplementationPlanBFP),
                                        true, true));

                                    current.IsStartMonthValid = false;
                                }
                                else if (month < 1)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].StartMonth",
                                        Global.ShortTemplatePositiveNumber,
                                        string.Format(Global.ViewTemplatePositiveNumber, Global.StartMonth, Global.SectionImplementationPlanBFP),
                                        true, true));

                                    current.IsStartMonthValid = false;
                                }
                                else
                                {
                                    hasMonth = true;
                                }
                            }
                            else
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].StartMonth",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.StartMonth, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsStartMonthValid = false;
                            }

                            if (!string.IsNullOrWhiteSpace(current.Duration))
                            {
                                if (!int.TryParse(current.Duration, out duration))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Duration",
                                        Global.ShortTemplateInteger,
                                        string.Format(Global.ViewTemplateInteger, Global.Duration2, Global.SectionImplementationPlanBFP),
                                        true, true));

                                    current.IsDurationValid = false;
                                }
                                else if (duration < 1)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Duration",
                                        Global.ShortTemplatePositiveNumber,
                                        string.Format(Global.ViewTemplatePositiveNumber, Global.Duration2, Global.SectionImplementationPlanBFP),
                                        true, true));

                                    current.IsDurationValid = false;
                                }
                                else
                                {
                                    hasDuration = true;
                                }
                            }
                            else
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Duration",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Duration2, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsDurationValid = false;
                            }

                            if (hasMonth && hasDuration)
                            {
                                if (month < minMonth)
                                {
                                    minMonth = month;
                                }

                                if (month + duration > maxDuration)
                                {
                                    maxDuration = month + duration;
                                }
                            }

                            if (current.Amount < 0)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].Amount",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount, Global.SectionImplementationPlanBFP),
                                    true, true));

                                current.IsAmountValid = false;
                            }

                            if (isDurationValid)
                            {
                                int basiDataDuration;
                                int.TryParse(complexType.BFPContractBasicData.Duration, out basiDataDuration);

                                //if (minMonth > 0 && maxDuration < int.MaxValue &&
                                //    (maxDuration - minMonth) > duration)
                                //       ||
                                //       ||
                                //       \/
                                // В максималния срок на изпълнение на дейностите се включват и непосочените първи месеци.
                                if (maxDuration < int.MaxValue && (maxDuration - 1) > basiDataDuration)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection[" + i + "].IsPeriodValid",
                                        string.Format(Global.ShortTemplateDuration, Global.Duration,
                                            Global.SectionBasicData),
                                        string.Format(Global.ViewTemplateDuration, Global.SectionImplementationPlanBFP, Global.Duration,
                                            Global.SectionBasicData), true, true));

                                    complexType.BFPContractContractActivities.BFPContractContractActivityCollection[i].IsPeriodValid = false;
                                }
                            }
                        }

                        complexType.BFPContractContractActivities.IsValid = true;
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                            modelPath + ".BFPContractContractActivities.BFPContractContractActivityCollection",
                            string.Format(Global.ShortTemplateMaxContractActivities,
                                Constants.ContractActivitiesMaxCount),
                            string.Format(Global.ViewTemplateMaxContractActivities,
                                Constants.ContractActivitiesMaxCount, Global.SectionImplementationPlanBFP),
                            true, true));

                        complexType.BFPContractContractActivities.IsValid = false;
                    }
                }
                else
                {
                    // errors.Add(ValidationOption.Create(
                    //     modelPath + ".ProgrammeContractActivitiesCollection[" + i + "]",
                    //     Global.ShortTemplateAtLeastOneRow,
                    //     string.Format(Global.ViewTemplateAtLeastOneRow, sectionName),
                    //     true, true));
                    // 
                    // complexType.ProgrammeContractActivitiesCollection[i].IsValid = false;
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                    modelPath + ".BFPContractContractActivities",
                    string.Empty,
                    Global.ViewTemplateContractActivities, true, true));
            }

            #endregion

            #region BFPContractIndicators

            if (complexType.AreIndicatorsUsed)
            {
                if (complexType.BFPContractIndicators != null)
                {
                    if (complexType.BFPContractIndicators.BFPContractIndicatorCollection != null && complexType.BFPContractIndicators.BFPContractIndicatorCollection.Count > 0)
                    {
                        var notNullList = complexType.BFPContractIndicators.BFPContractIndicatorCollection.Where(b => b != null && b.SelectedIndicator != null && !string.IsNullOrWhiteSpace(b.SelectedIndicator.Id)).ToList();
                        var distinctCount = notNullList.Select(x => x.SelectedIndicator.Id).Distinct().Count();

                        if (distinctCount < notNullList.Count)
                        {
                            errors.Add(ValidationOption.Create(
                                                                modelPath + ".BFPContractIndicators.BFPContractIndicatorCollection",
                                                                Global.ShortTemplateIndicatorsUnique,
                                                                string.Format(Global.ViewTemplateIndicatorsUnique, Global.SectionIndicators), true, true));

                            complexType.BFPContractIndicators.HasUniqueIds = false;
                        }
                        else
                        {
                            complexType.BFPContractIndicators.HasUniqueIds = true;
                        }

                        EngineValidate(csValidationEngine, complexType.BFPContractIndicators, modelPath + ".BFPContractIndicators", errors);

                        complexType.BFPContractIndicators.IsValid = true;
                    }
                    else
                    {
                        if (!complexType.IsStandardSimplifiedApplicationFormType)
                        {
                            errors.Add(
                                ValidationOption.Create(
                                    modelPath + ".BFPContractIndicators.BFPContractIndicatorCollection",
                                    Global.ShortTemplateAtLeastOneRow,
                                    string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionIndicators),
                                    true, true));

                            complexType.BFPContractIndicators.IsValid = false;
                        }
                    }
                }
                else
                {
                    if (!complexType.IsStandardSimplifiedApplicationFormType)
                    {
                        errors.Add(
                            ValidationOption.Create(
                                modelPath + ".BFPContractIndicators",
                                string.Empty,
                                string.Format(Global.ViewTemplateMissingSection, Global.SectionIndicators), true, true));
                    }
                }
            }

            #endregion

            #region BFPContractContractTeams

            if (complexType.BFPContractContractTeams != null && complexType.BFPContractContractTeams.BFPContractContractTeamCollection != null
                && complexType.BFPContractContractTeams.BFPContractContractTeamCollection.Count > 0)
            {
                if (complexType.BFPContractContractTeams.BFPContractContractTeamCollection.Count <= Constants.ContractTeamsMaxCount)
                {
                    for (int i = 0; i < complexType.BFPContractContractTeams.BFPContractContractTeamCollection.Count; i++)
                    {
                        EngineValidate(csValidationEngine, complexType.BFPContractContractTeams.BFPContractContractTeamCollection[i],
                            modelPath + ".BFPContractContractTeams.BFPContractContractTeamCollection[" + i + "]", errors);
                    }

                    complexType.BFPContractContractTeams.IsValid = true;
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".BFPContractContractTeams.BFPContractContractTeamCollection",
                        string.Format(Global.ShortTemplateMaxContractTeams, Constants.ContractTeamsMaxCount),
                        string.Format(Global.ViewTemplateMaxContractTeams, Constants.ContractTeamsMaxCount,
                            Global.SectionTeam),
                        true, true));

                    complexType.BFPContractContractTeams.IsValid = false;
                }
            }

            #endregion

            #region BFPContractPlans

            if (complexType.BFPContractPlans != null && complexType.BFPContractPlans.BFPContractPlanCollection != null && complexType.BFPContractPlans.BFPContractPlanCollection.Count > 0)
            {
                if (complexType.BFPContractPlans.BFPContractPlanCollection.Count <= Constants.ProcurementPlansMaxCount)
                {
                    for (int i = 0; i < complexType.BFPContractPlans.BFPContractPlanCollection.Count; i++)
                    {
                        var current = complexType.BFPContractPlans.BFPContractPlanCollection[i];

                        current.IsNameValid = true;
                        current.IsErrandAreaValid = true;
                        current.IsErrandLegalActValid = true;
                        current.IsErrandTypeValid = true;
                        current.IsAmountValid = true;
                        current.IsPlanDateValid = true;
                        current.IsDescriptionValid = true;

                        if (string.IsNullOrWhiteSpace(current.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].Name",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureSubject, Global.SectionOutsourcingPlan), true, true));

                            current.IsNameValid = false;
                        }
                        else if (current.Name.Length > Constants.ProjectErrandNameLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].Name",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandNameLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ProcedureSubject, Global.SectionOutsourcingPlan, Constants.ProjectErrandNameLength), true, true));

                            current.IsNameValid = false;
                        }

                        if (current.ErrandArea == null || string.IsNullOrWhiteSpace(current.ErrandArea.Code) || string.IsNullOrWhiteSpace(current.ErrandArea.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].ErrandArea.Code",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureObject, Global.SectionOutsourcingPlan), true, true));

                            current.IsErrandAreaValid = false;
                        }

                        if (current.ErrandLegalAct == null || string.IsNullOrWhiteSpace(current.ErrandLegalAct.Id) || string.IsNullOrWhiteSpace(current.ErrandLegalAct.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].ErrandLegalAct.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ApplicableLegalAct, Global.SectionOutsourcingPlan), true, true));

                            current.IsErrandLegalActValid = false;
                        }

                        if (current.ErrandType == null || string.IsNullOrWhiteSpace(current.ErrandType.Code) || string.IsNullOrWhiteSpace(current.ErrandType.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].ErrandType.Code",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureType, Global.SectionOutsourcingPlan), true, true));

                            current.IsErrandTypeValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(current.Description))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].Description",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProjectErrandDescription, Global.SectionOutsourcingPlan), true, true));

                            current.IsDescriptionValid = false;
                        }
                        else if (current.Description.Length > Constants.ProjectErrandDescriptionLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".BFPContractPlans.BFPContractPlanCollection[" + i + "].Description",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandDescriptionLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ProjectErrandDescription, Global.SectionOutsourcingPlan, Constants.ProjectErrandDescriptionLength), true, true));

                            current.IsDescriptionValid = false;
                        }
                    }
                }
            }

            #endregion

            #region AttachedDocuments

            if (complexType.AttachedDocuments != null)
            {
                if (complexType.AttachedDocuments.AttachedDocumentCollection != null &&
                    complexType.AttachedDocuments.AttachedDocumentCollection.Count > 0)
                {
                    if (complexType.AttachedDocuments.AttachedDocumentCollection.Count <=
                        Constants.AttachedDocumentsMaxCount)
                    {
                        for (int i = 0; i < complexType.AttachedDocuments.AttachedDocumentCollection.Count; i++)
                        {
                            var current = complexType.AttachedDocuments.AttachedDocumentCollection[i];

                            if ((current.Type == null) || string.IsNullOrWhiteSpace(current.Type.Name))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].Type.Name",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Type,
                                        Global.AttachedDocuments), true, true));

                                current.IsTypeValid = false;
                            }
                            else
                            {
                                current.IsTypeValid = true;
                            }

                            if (string.IsNullOrWhiteSpace(current.Description))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                    "].Description",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Description,
                                        Global.AttachedDocuments), true, true));

                                current.IsDescriptionValid = false;
                            }
                            else if (current.Description.Length > Constants.AttachedDocumentsDescriptionLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                    "].Description",
                                    string.Format(Global.ShortTemplateSymbolsMax,
                                        Constants.AttachedDocumentsDescriptionLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Description,
                                        Global.AttachedDocuments,
                                        Constants.AttachedDocumentsDescriptionLength), true, true));

                                current.IsDescriptionValid = false;
                            }
                            else
                            {
                                current.IsDescriptionValid = true;
                            }

                            string[] extensions = null;

                            bool hasDocumentContent = false;
                            //bool hasSignatureContent = false;
                            bool hasValidExtension = false;
                            int documentTotalSize = 0;

                            if (current.AttachedDocumentContent == null)
                                current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();
                            current.AttachedDocumentContent.IsDocumentValid = true;

                            if (current.AttachedDocumentContent == null ||
                                string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) ||
                                string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName) ||
                                string.IsNullOrWhiteSpace(current.AttachedDocumentContent.Size))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                    "].AttachedDocumentContent.BlobContentId",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.SystemName,
                                        Global.AttachedDocuments), true, true));

                                if (current.AttachedDocumentContent != null)
                                {
                                    current.AttachedDocumentContent.IsDocumentValid = false;
                                }
                            }
                            else if (extensions != null && extensions.Length > 0)
                            {
                                hasDocumentContent = true;
                                hasValidExtension =
                                    extensions.Any(ext => current.AttachedDocumentContent.FileName.EndsWith(ext));

                                if (!hasValidExtension)
                                {
                                    string validExtensions = string.Join(", ", extensions);
                                    string viewErr;

                                    if (current.IsTypeValid)
                                    {
                                        viewErr = string.Format(Global.ViewTemplateNamedDocumentExtension,
                                            current.Type.Name, Global.AttachedDocuments, validExtensions);
                                    }
                                    else
                                    {
                                        viewErr = string.Format(Global.ViewTemplateDocumentExtension,
                                            Global.AttachedDocuments, validExtensions);
                                    }

                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                        "].AttachedDocumentContent.BlobContentId",
                                        string.Format(Global.ShortTemplateDocumentExtension, validExtensions),
                                        viewErr, true, true));

                                    current.AttachedDocumentContent.IsDocumentValid = false;
                                }
                            }
                            else
                            {
                                hasDocumentContent = true;
                                hasValidExtension = true;
                            }

                            if (hasValidExtension && hasDocumentContent)
                            {
                                int size;

                                if (!int.TryParse(current.AttachedDocumentContent.Size, out size))
                                {
                                    string viewErr;

                                    if (current.IsTypeValid)
                                    {
                                        viewErr = string.Format(Global.ViewTemplateNamedDocumentSizeNoInfo,
                                           current.Type.Name, Global.AttachedDocuments);
                                    }
                                    else
                                    {
                                        viewErr = string.Format(Global.ViewTemplateDocumentSizeNoInfo,
                                           Global.AttachedDocuments);
                                    }

                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                        "].AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateDocumentSizeNoInfo,
                                        viewErr, true, true));
                                }
                                else
                                {
                                    documentTotalSize += size;
                                }
                            }
                        }

                        complexType.AttachedDocuments.HasValidCount = true;
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                            modelPath + ".AttachedDocuments.AttachedDocumentCollection",
                            string.Format(Global.ShortTemplateMaxAttachedDocuments,
                                Constants.AttachedDocumentsMaxCount),
                            string.Format(Global.ViewTemplateMaxAttachedDocuments,
                                Constants.AttachedDocumentsMaxCount, Global.AttachedDocuments),
                            true, true));

                        complexType.AttachedDocuments.HasValidCount = false;
                    }
                }
            }

            #endregion
        }

        #region Private

        private Regex _latinRegex = new Regex(@"^[^А-Яа-я]*$");
        private Regex _emailRegex = new Regex(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$");

        #endregion
    }
}
