using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;
using System.Text.RegularExpressions;
using System.Linq;
using Eumis.Documents.Mappers;
using Eumis.Common.Helpers;
using Eumis.Documents.Validation.Shared;
using System.Globalization;

namespace Eumis.Documents.Validation.Eumis
{
    public class Project : CSValidatorBase<R_10019.Project>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10019.Project complexType, string modelPath, IList<ValidationOption> errors)
        {
            bool areFinLabels = complexType.IsFinalRecipients || complexType.IsFinancialIntermediaries;

            bool isDurationValid = true;

            #region ProjectBasicData

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.BasicData))
            {
                if (complexType.ProjectBasicData != null)
                {
                    if (!complexType.ProjectBasicData.isLocked)
                    {
                        if (!string.IsNullOrWhiteSpace(complexType.ProjectBasicData.Duration))
                        {
                            int t;
                            int minMonths = 1;
                            //int maxMonths = 24;

                            if (!int.TryParse(complexType.ProjectBasicData.Duration, out t) || t < minMonths || t > complexType.ProjectBasicData.MaxDuration)
                            {
                                isDurationValid = false;
                            }
                        }
                        else
                            isDurationValid = false;

                        EngineValidate(csValidationEngine, complexType.ProjectBasicData, modelPath + ".ProjectBasicData", errors);
                    }
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".ProjectBasicData",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateMissingSection, Global.SectionBasicData), true, true));
                }
            }
            #endregion

            #region Candidate

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Beneficary))
            {
                if (complexType.Candidate != null)
                {
                    if (!complexType.Candidate.isLocked)
                    {
                        #region Данни за кандидата
                        bool isUinTypeEmpty = complexType.Candidate.UinType == null || string.IsNullOrWhiteSpace(complexType.Candidate.UinType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.UinType.Name);

                        complexType.Candidate.IsUinValid = true;

                        if (isUinTypeEmpty)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.UinType.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsUinTypeValid = false;
                        }
                        else
                            complexType.Candidate.IsUinTypeValid = true;

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.Uin))
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".Candidate.Uin",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsUinValid = false;
                        }
                        else if (!isUinTypeEmpty)
                        {
                            string uinTypeValue = complexType.Candidate.UinType.Id;

                            if ((uinTypeValue == UinTypeNomenclature.Bulstat.Code && !BulstatValidator.ValidateBulstat(complexType.Candidate.Uin)) ||
                                (uinTypeValue == UinTypeNomenclature.UIC.Code && !BulstatValidator.ValidateBulstat(complexType.Candidate.Uin)) ||
                                 uinTypeValue == UinTypeNomenclature.Freelancers.Code && !BulstatValidator.IsValidEGN(complexType.Candidate.Uin))
                            {
                                errors.Add(ValidationOption.Create(
                                        modelPath + ".Candidate.Uin",
                                        Global.ShortTemplateInvalid,
                                        string.Format(Global.ViewTemplateInvalid, Global.Bulstat, Global.SectionCandidate), true, true));

                                complexType.Candidate.IsUinValid = false;
                            }
                            else if (complexType.Candidate.Uin.Length > Constants.BulstatFieldMaxLength)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".Candidate.Uin",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.BulstatFieldMaxLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Bulstat, Global.SectionBasicData, Constants.BulstatFieldMaxLength), true, true));

                                complexType.Candidate.IsUinValid = false;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Name",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyName, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsNameValid = false;
                        }
                        else if (complexType.Candidate.Name.Length > Constants.CandidateNameLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Name",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyName, Global.SectionCandidate, Constants.CandidateNameLength), true, true));

                            complexType.Candidate.IsNameValid = false;
                        }
                        else
                            complexType.Candidate.IsNameValid = true;

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.NameEN))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.NameEN",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyNameEn, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsNameEnValid = false;
                        }
                        else if (complexType.Candidate.NameEN.Length > Constants.CandidateNameEnLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.NameEN",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateNameEnLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyNameEn, Global.SectionCandidate, Constants.CandidateNameEnLength), true, true));

                            complexType.Candidate.IsNameEnValid = false;
                        }
                        else if (!_latinRegex.IsMatch(complexType.Candidate.NameEN))
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".Candidate.NameEN",
                                Global.ShortTemplateSymbolsNames2,
                                string.Format(Global.ViewTemplateSymbolsNames2, Global.CompanyNameEn, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsNameEnValid = false;
                        }
                        else
                        {
                            complexType.Candidate.IsNameEnValid = true;
                        }

                        if (complexType.Candidate.CompanyType == null || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyType.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyType.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyType, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyTypeValid = false;
                        }
                        else
                            complexType.Candidate.IsCompanyTypeValid = true;

                        if (complexType.Candidate.CompanyLegalType == null || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyLegalType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyLegalType.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyLegalType.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyLegalType, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyLegalTypeValid = false;
                        }
                        else
                            complexType.Candidate.IsCompanyLegalTypeValid = true;

                        if (complexType.Candidate.Seat != null)
                        {
                            EngineValidate(csValidationEngine, complexType.Candidate.Seat, modelPath + ".Candidate.Seat", errors);
                        }

                        if (complexType.Candidate.Correspondence != null)
                        {
                            EngineValidate(csValidationEngine, complexType.Candidate.Correspondence, modelPath + ".Candidate.Correspondence", errors);
                        }

                        complexType.Candidate.IsEmailValid = true;

                        if (!string.IsNullOrWhiteSpace(complexType.Candidate.Email))
                        {
                            if (complexType.Candidate.Email.Length > Constants.CandidateEmailLength)
                            {
                                errors.Add(ValidationOption.Create(
                                                        modelPath + ".Candidate.Email",
                                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Email, Global.SectionCandidate, Constants.CandidateEmailLength), true, true));

                                complexType.Candidate.IsEmailValid = false;
                            }
                            else if (!_emailRegex.IsMatch(complexType.Candidate.Email))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".Candidate.Email",
                                    Global.ShortTemplateValidMail,
                                    string.Format(Global.ViewTemplateValidMail, Global.Email, Global.SectionCandidate), true, true));

                                complexType.Candidate.IsEmailValid = false;
                            }
                        }
                        else
                        {
                            errors.Add(ValidationOption.Create(
                                    modelPath + ".Candidate.Email",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.Email, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsEmailValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.Phone1))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Phone1",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Phone1, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsPhone1Valid = false;
                        }
                        else if (complexType.Candidate.Phone1.Length > Constants.CandidatePhoneLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Phone1",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Phone1, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                            complexType.Candidate.IsPhone1Valid = false;
                        }
                        else
                            complexType.Candidate.IsPhone1Valid = true;

                        if (!string.IsNullOrWhiteSpace(complexType.Candidate.Phone2) && complexType.Candidate.Phone2.Length > Constants.CandidatePhoneLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Phone2",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Phone2, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                            complexType.Candidate.IsPhone2Valid = false;
                        }
                        else
                            complexType.Candidate.IsPhone2Valid = true;

                        if (!string.IsNullOrWhiteSpace(complexType.Candidate.Fax) && complexType.Candidate.Fax.Length > Constants.CandidatePhoneLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.Fax",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Fax, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                            complexType.Candidate.IsFaxValid = false;
                        }
                        else
                            complexType.Candidate.IsFaxValid = true;

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.CompanyRepresentativePerson))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyRepresentativePerson",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyRepresentativePerson, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyRepresentativePersonValid = false;
                        }
                        else if (complexType.Candidate.CompanyRepresentativePerson.Length > Constants.CompanyRepresentativePersonLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyRepresentativePerson",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CompanyRepresentativePersonLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyRepresentativePerson, Global.SectionCandidate, Constants.CompanyRepresentativePersonLength), true, true));

                            complexType.Candidate.IsCompanyRepresentativePersonValid = false;
                        }
                        else
                            complexType.Candidate.IsCompanyRepresentativePersonValid = true;

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.CompanyContactPerson))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyContactPerson",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyContactPerson, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyContactPersonValid = false;
                        }
                        else if (complexType.Candidate.CompanyContactPerson.Length > Constants.ContactPersonLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyContactPerson",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ContactPersonLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPerson, Global.SectionCandidate, Constants.ContactPersonLength), true, true));

                            complexType.Candidate.IsCompanyContactPersonValid = false;
                        }
                        else
                            complexType.Candidate.IsCompanyContactPersonValid = true;

                        if (string.IsNullOrWhiteSpace(complexType.Candidate.CompanyContactPersonPhone))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyContactPersonPhone",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonPhone, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyContactPersonPhoneValid = false;
                        }
                        else if (complexType.Candidate.CompanyContactPersonPhone.Length > Constants.CandidatePhoneLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyContactPersonPhone",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonPhone, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                            complexType.Candidate.IsCompanyContactPersonPhoneValid = false;
                        }
                        else
                            complexType.Candidate.IsCompanyContactPersonPhoneValid = true;

                        complexType.Candidate.IsCompanyContactPersonEmailValid = true;

                        if (!string.IsNullOrWhiteSpace(complexType.Candidate.CompanyContactPersonEmail))
                        {
                            if (complexType.Candidate.CompanyContactPersonEmail.Length > Constants.CandidateEmailLength)
                            {
                                errors.Add(ValidationOption.Create(
                                                        modelPath + ".Candidate.CompanyContactPersonEmail",
                                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateEmailLength),
                                                        string.Format(Global.ViewTemplateSymbolsMax, Global.CompanyContactPersonEmail, Global.SectionCandidate, Constants.CandidateEmailLength), true, true));

                                complexType.Candidate.IsCompanyContactPersonEmailValid = false;
                            }
                            else if (!_emailRegex.IsMatch(complexType.Candidate.CompanyContactPersonEmail))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".Candidate.CompanyContactPersonEmail",
                                    Global.ShortTemplateValidMail,
                                    string.Format(Global.ViewTemplateValidMail, Global.CompanyContactPersonEmail, Global.SectionCandidate), true, true));

                                complexType.Candidate.IsCompanyContactPersonEmailValid = false;
                            }
                        }
                        else
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Candidate.CompanyContactPersonEmail",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.CompanyContactPersonEmail, Global.SectionCandidate), true, true));

                            complexType.Candidate.IsCompanyContactPersonEmailValid = false;
                        }
                        #endregion
                    }
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateMissingSection, Global.SectionCandidate), true, true));
                }
            }
            #endregion

            #region ProjectBasicData AdditionalDescription

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.BasicData))
            {
                if (complexType.ProjectBasicData != null && !string.IsNullOrWhiteSpace(complexType.ProjectBasicData.AdditionalDescription) &&
                complexType.ProjectBasicData.AdditionalDescription.Length > Constants.ProjectBasicDataAdditionalDescriptionLength && !complexType.ProjectBasicData.isLocked)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".ProjectBasicData.AdditionalDescription",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataAdditionalDescriptionLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.AdditionalDescription, Global.SectionCandidate, Constants.ProjectBasicDataAdditionalDescriptionLength), true, true));

                    complexType.ProjectBasicData.IsAdditionalDescriptionValid = false;
                }
                else
                    complexType.ProjectBasicData.IsAdditionalDescriptionValid = true;
            }
            #endregion

            #region Standard

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Partners))
            {
                if (!complexType.IsStandardForBudgetLine)
                {
                    if (complexType.Partners != null && complexType.Partners.PartnerCollection != null &&
                        complexType.Partners.PartnerCollection.Count > 0 && !complexType.Partners.isLocked)
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
                                String.Format(Global.ShortTemplateMaxPartners, Constants.PartnersMaxCount),
                                string.Format(Global.ViewTemplateMaxPartners, Constants.PartnersMaxCount,
                                    Global.SectionPartners),
                                true, true));

                            complexType.Partners.PartnerCollection.HasValidCount = false;
                        }
                    }
                }
            }

            #endregion

            #region Directions

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Directions))
            {
                if (complexType.DirectionsBudgetContractCollection != null && complexType.DirectionsBudgetContractCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.DirectionsBudgetContractCollection.Count; i++)
                    {
                        var directions = complexType.DirectionsBudgetContractCollection[i]?.Directions?.DirectionCollection;
                        if (directions == null)
                        {
                            complexType.DirectionsBudgetContractCollection[i].Directions = new R_10093.DirectionSection();
                            complexType.DirectionsBudgetContractCollection[i].Directions.IsValid = false;
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".DirectionsBudgetContractCollection[" + i + "].DirectionSection",
                                            string.Empty,
                                            Global.ViewTemplateDirections, true, true));
                        }
                        else 
                        {
                            if (directions.Count == 0)
                            {
                                complexType.DirectionsBudgetContractCollection[i].Directions.IsValid = false;
                                errors.Add(ValidationOption.Create(
                                            modelPath + ".DirectionsBudgetContractCollection[" + i + "].DirectionSection",
                                            string.Empty,
                                            Global.ViewTemplateDirections, true, true));
                            }
                        }
                    }
                }
            }

            #endregion

            #region DimensionsBudgetContract (triple)

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Budget))
            {
                if (complexType.DirectionsBudgetContractCollection != null && complexType.DirectionsBudgetContractCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.DirectionsBudgetContractCollection.Count; i++)
                    {
                        complexType.DirectionsBudgetContractCollection[i].Nomenclatures = complexType.Nomenclatures;

                        EngineValidate(csValidationEngine, complexType.DirectionsBudgetContractCollection[i], modelPath + ".DirectionsBudgetContractCollection[" + i + "]", errors);
                    }
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".DirectionsBudgetContractCollection",
                                            string.Empty,
                                            Global.ViewTemplateDimensionsBudgetContract, true, true));
                }
            }

            #endregion

            #region ContractActivities

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Activities))
            {
                if (!complexType.IsStandardForBudgetLine && !complexType.IsFinancialIntermediaries && !complexType.IsFinalRecipients)
                {
                    if (complexType.ProgrammeContractActivitiesCollection != null &&
                        complexType.ProgrammeContractActivitiesCollection.Count > 0)
                    {
                        for (int i = 0; i < complexType.ProgrammeContractActivitiesCollection.Count; i++)
                        {
                            if (!complexType.ProgrammeContractActivitiesCollection[i].isLocked)
                            {
                                int minMonth = int.MaxValue;
                                int maxDuration = 0;
                                string programmeName = complexType.ProgrammeContractActivitiesCollection[i].ProgrammeNameFormatted;
                                string sectionName =
                                    string.Format("{0} {1}", Global.SectionImplementationPlan, programmeName).TrimEnd();

                                if (complexType.ProgrammeContractActivitiesCollection[i].ContractActivityCollection != null &&
                                    complexType.ProgrammeContractActivitiesCollection[i].ContractActivityCollection.Count >
                                    0)
                                {
                                    if (
                                        complexType.ProgrammeContractActivitiesCollection[i].ContractActivityCollection
                                            .Count <= Constants.ContractActivitiesMaxCount)
                                    {
                                        for (int j = 0;
                                            j <
                                            complexType.ProgrammeContractActivitiesCollection[i].ContractActivityCollection
                                                .Count;
                                            j++)
                                        {
                                            //EngineValidate(csValidationEngine, complexType.ProgrammeContractActivitiesCollection[i].ContractActivityCollection[j],
                                            //   modelPath + ".ProgrammeContractActivitiesCollection[" + i + "].ContractActivityCollection[" + j + "]", errors);

                                            var current =
                                                complexType.ProgrammeContractActivitiesCollection[i]
                                                    .ContractActivityCollection[j];

                                            if (current.CompanyCollection == null || current.CompanyCollection.Count == 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].CompanyCollection",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Company, sectionName),
                                                    true, true));

                                                current.IsCompanyValid = false;
                                            }
                                            else
                                                current.IsCompanyValid = true;

                                            if (string.IsNullOrWhiteSpace(current.Code))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Code",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Activity, sectionName),
                                                    true, true));

                                                current.IsCodeValid = false;
                                            }
                                            else if (current.Code.Length > Constants.ContractActivityCodeLength)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Code",
                                                    string.Format(Global.ShortTemplateSymbolsMax,
                                                        Constants.ContractActivityCodeLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Activity,
                                                        sectionName, Constants.ContractActivityCodeLength), true, true));

                                                current.IsCodeValid = false;
                                            }
                                            else
                                                current.IsCodeValid = true;

                                            if (string.IsNullOrWhiteSpace(current.Name))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Name",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Description,
                                                        sectionName), true, true));

                                                current.IsNameValid = false;
                                            }
                                            else if (current.Name.Length > Constants.ContractActivityNameLength)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Name",
                                                    string.Format(Global.ShortTemplateSymbolsMax,
                                                        Constants.ContractActivityNameLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Description,
                                                        sectionName, Constants.ContractActivityNameLength), true, true));

                                                current.IsNameValid = false;
                                            }
                                            else
                                                current.IsNameValid = true;

                                            if (string.IsNullOrWhiteSpace(current.ExecutionMethod))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].ExecutionMethod",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ExecutionMethod,
                                                        sectionName), true, true));

                                                current.IsExecutionMethodValid = false;
                                            }
                                            else if (current.ExecutionMethod.Length >
                                                     Constants.ContractActivityExecutionMethodLength)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].ExecutionMethod",
                                                    string.Format(Global.ShortTemplateSymbolsMax,
                                                        Constants.ContractActivityExecutionMethodLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ExecutionMethod,
                                                        sectionName, Constants.ContractActivityExecutionMethodLength),
                                                    true, true));

                                                current.IsExecutionMethodValid = false;
                                            }
                                            else
                                                current.IsExecutionMethodValid = true;

                                            if (string.IsNullOrWhiteSpace(current.Result))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Result",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Result, sectionName),
                                                    true, true));

                                                current.IsResultValid = false;
                                            }
                                            else if (current.Result.Length > Constants.ContractActivityResultLength)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Result",
                                                    string.Format(Global.ShortTemplateSymbolsMax,
                                                        Constants.ContractActivityResultLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.Result, sectionName,
                                                        Constants.ContractActivityResultLength), true, true));

                                                current.IsResultValid = false;
                                            }
                                            else
                                                current.IsResultValid = true;

                                            bool hasMonth = false;
                                            bool hasDuration = false;
                                            int month = 0, duration = 0;

                                            current.IsStartMonthValid = true;
                                            if (!string.IsNullOrWhiteSpace(current.StartMonth))
                                            {
                                                if (!int.TryParse(current.StartMonth, out month))
                                                {
                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                        "].ContractActivityCollection[" + j + "].StartMonth",
                                                        Global.ShortTemplateInteger,
                                                        string.Format(Global.ViewTemplateInteger, Global.StartMonth,
                                                            sectionName), true, true));

                                                    current.IsStartMonthValid = false;
                                                }
                                                else if (month < 1)
                                                {
                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                        "].ContractActivityCollection[" + j + "].StartMonth",
                                                        Global.ShortTemplatePositiveNumber,
                                                        string.Format(Global.ViewTemplatePositiveNumber, Global.StartMonth,
                                                            sectionName), true, true));

                                                    current.IsStartMonthValid = false;
                                                }
                                                else
                                                    hasMonth = true;
                                            }
                                            else
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].StartMonth",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.StartMonth,
                                                        sectionName), true, true));

                                                current.IsStartMonthValid = false;
                                            }

                                            current.IsDurationValid = true;
                                            if (!string.IsNullOrWhiteSpace(current.Duration))
                                            {
                                                if (!int.TryParse(current.Duration, out duration))
                                                {
                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                        "].ContractActivityCollection[" + j + "].Duration",
                                                        Global.ShortTemplateInteger,
                                                        string.Format(Global.ViewTemplateInteger, Global.Duration2,
                                                            sectionName), true, true));

                                                    current.IsDurationValid = false;
                                                }
                                                else if (duration < 1)
                                                {
                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                        "].ContractActivityCollection[" + j + "].Duration",
                                                        Global.ShortTemplatePositiveNumber,
                                                        string.Format(Global.ViewTemplatePositiveNumber, Global.Duration2,
                                                            sectionName), true, true));

                                                    current.IsDurationValid = false;
                                                }
                                                else
                                                    hasDuration = true;
                                            }
                                            else
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Duration",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Duration2, sectionName),
                                                    true, true));

                                                current.IsDurationValid = false;
                                            }

                                            if (hasMonth && hasDuration)
                                            {
                                                if (month < minMonth)
                                                    minMonth = month;
                                                if (month + duration > maxDuration)
                                                    maxDuration = month + duration;
                                            }

                                            if (current.Amount < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                    "].ContractActivityCollection[" + j + "].Amount",
                                                    Global.ShortTemplateNonNegativeNumber,
                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Amount,
                                                        sectionName),
                                                    true, true));

                                                current.IsAmountValid = false;
                                            }
                                            else
                                                current.IsAmountValid = true;
                                        }
                                        complexType.ProgrammeContractActivitiesCollection[i].IsValid = true;
                                    }
                                    else
                                    {
                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".ProgrammeContractActivitiesCollection[" + i + "]",
                                            string.Format(Global.ShortTemplateMaxContractActivities,
                                                Constants.ContractActivitiesMaxCount),
                                            string.Format(Global.ViewTemplateMaxContractActivities,
                                                Constants.ContractActivitiesMaxCount, sectionName),
                                            true, true));

                                        complexType.ProgrammeContractActivitiesCollection[i].IsValid = false;
                                    }

                                    if (isDurationValid)
                                    {
                                        int duration;
                                        int.TryParse(complexType.ProjectBasicData.Duration, out duration);

                                        //if (minMonth > 0 && maxDuration < int.MaxValue &&
                                        //    (maxDuration - minMonth) > duration)
                                        //       ||
                                        //       ||
                                        //       \/
                                        // В максималния срок на изпълнение на дейностите се включват и непосочените първи месеци.
                                        if (maxDuration < int.MaxValue && (maxDuration - 1) > duration)
                                        {
                                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ProgrammeContractActivitiesCollection[" + i +
                                                "].IsPeriodValid",
                                                string.Format(Global.ShortTemplateDuration, Global.Duration,
                                                    Global.SectionBasicData),
                                                string.Format(Global.ViewTemplateDuration, sectionName, Global.Duration,
                                                    Global.SectionBasicData), true, true));

                                            complexType.ProgrammeContractActivitiesCollection[i].IsPeriodValid = false;
                                        }
                                        else
                                            complexType.ProgrammeContractActivitiesCollection[i].IsPeriodValid = true;
                                    }
                                }
                                else
                                {
                                    if (!complexType.IsStandardSimplified)
                                    {
                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".ProgrammeContractActivitiesCollection[" + i + "]",
                                            Global.ShortTemplateAtLeastOneRow,
                                            string.Format(Global.ViewTemplateAtLeastOneRow, sectionName),
                                            true, true));

                                        complexType.ProgrammeContractActivitiesCollection[i].IsValid = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!complexType.IsStandardSimplified)
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".ProgrammeContractActivitiesCollection",
                                string.Empty,
                                Global.ViewTemplateContractActivities, true, true));
                        }
                    }
                }

            }

            #endregion

            #region Indicators

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Indicators))
            {
                if (complexType.ProgrammeIndicatorsCollection != null && complexType.ProgrammeIndicatorsCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.ProgrammeIndicatorsCollection.Count; i++)
                    {

                        if (!complexType.ProgrammeIndicatorsCollection[i].isLocked)
                        {
                            string programmeName = complexType.ProgrammeIndicatorsCollection[i].ProgrammeNameFormatted;
                            string sectionName = string.Format("{0} {1}", Global.SectionIndicators, programmeName).TrimEnd();

                            if (complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection != null && complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection.Count > 0)
                            {
                                var notNullList = complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection.Where(pi => pi != null && !string.IsNullOrWhiteSpace(pi.Id)).ToList();
                                var distinctCount = notNullList.Select(x => x.Id).Distinct().Count();

                                if (distinctCount < notNullList.Count)
                                {
                                    errors.Add(ValidationOption.Create(
                                                                        modelPath + ".ProgrammeIndicatorsCollection[" + i + "]",
                                                                        Global.ShortTemplateIndicatorsUnique,
                                                                        string.Format(Global.ViewTemplateIndicatorsUnique, sectionName), true, true));

                                    complexType.ProgrammeIndicatorsCollection[i].HasUniqueIds = false;
                                }
                                else
                                    complexType.ProgrammeIndicatorsCollection[i].HasUniqueIds = true;

                                for (int j = 0; j < complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection.Count; j++)
                                {
                                    //EngineValidate(csValidationEngine, complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection[j],
                                    //   modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "]", errors);

                                    var current = complexType.ProgrammeIndicatorsCollection[i].IndicatorCollection[j];
                                    R_10013.Indicator original = null;

                                    current.IsNameValid = true;

                                    if (string.IsNullOrWhiteSpace(current.Id) || string.IsNullOrWhiteSpace(current.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].Id",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.IndicatorName, sectionName), true, true));

                                        current.IsNameValid = false;
                                    }
                                    else
                                    {
                                        original = complexType.ProgrammeIndicatorsCollection[i].Items.FirstOrDefault(ind => ind.Id == current.Id && ind.Name.RemoveNewLines() == current.Name.RemoveNewLines());

                                        if (original == null)
                                        {
                                            errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].Id",
                                                                Global.ShortTemplateInvalidIndicator,
                                                                string.Format(Global.ViewTemplateInvalidIndicator, sectionName), true, true));

                                            current.IsNameValid = false;
                                        }
                                        else
                                        {
                                            if (current.TypeName != original.TypeName)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TypeName",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.TypeName, sectionName), true, true));
                                            }

                                            if (current.TrendName != original.TrendName)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TrendName",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.TrendName, sectionName), true, true));
                                            }

                                            if (current.KindName != original.KindName)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].KindName",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.KindName, sectionName), true, true));
                                            }

                                            if (current.MeasureName != original.MeasureName)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].MeasureName",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.MeasureName, sectionName), true, true));
                                            }

                                            if (current.AggregatedReport != original.AggregatedReport)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].AggregatedReport",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.AggregatedReport, sectionName), true, true));
                                            }

                                            if (current.AggregatedTarget != original.AggregatedTarget)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].AggregatedTarget",
                                                                string.Empty,
                                                                string.Format(Global.ViewTemplateInvalid, Global.AggregatedTarget, sectionName), true, true));
                                            }
                                        }
                                    }

                                    if (original != null)
                                    {
                                        if (original.HasGenderDivision)
                                        {
                                            if (current.BaseMen < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].BaseMen",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.BaseMen, sectionName),
                                                                    true, true));

                                                current.IsBaseMenValid = false;
                                            }
                                            else
                                                current.IsBaseMenValid = true;

                                            if (current.BaseWomen < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].BaseWomen",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.BaseWomen, sectionName),
                                                                    true, true));

                                                current.IsBaseWomenValid = false;
                                            }
                                            else
                                                current.IsBaseWomenValid = true;

                                            if (current.BaseTotal < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].BaseTotal",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Base, sectionName),
                                                                    true, true));

                                                current.IsBaseValid = false;
                                            }
                                            else if ((current.BaseMen + current.BaseWomen) != current.BaseTotal)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].BaseTotal",
                                                                    string.Format(Global.ShortTemplateSumBFP_SF, Global.BaseMen, Global.BaseWomen),
                                                                    string.Format(Global.ViewTemplateSumBFP_SF, Global.Base, Global.BaseMen, Global.BaseWomen, sectionName),
                                                                    true, true));

                                                current.IsBaseValid = false;
                                            }
                                            else
                                                current.IsBaseValid = true;

                                            if (current.TargetMen < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TargetMen",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.TargetMen, sectionName),
                                                                    true, true));

                                                current.IsTargetMenValid = false;
                                            }
                                            else
                                                current.IsTargetMenValid = true;

                                            if (current.TargetWomen < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TargetWomen",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.TargetWomen, sectionName),
                                                                    true, true));

                                                current.IsTargetWomenValid = false;
                                            }
                                            else
                                                current.IsTargetWomenValid = true;

                                            if (current.TargetTotal < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TargetTotal",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Target, sectionName),
                                                                    true, true));

                                                current.IsTargetValid = false;
                                            }
                                            else if ((current.TargetMen + current.TargetWomen) != current.TargetTotal)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TargetTotal",
                                                                    string.Format(Global.ShortTemplateSumBFP_SF, Global.TargetMen, Global.TargetWomen),
                                                                    string.Format(Global.ViewTemplateSumBFP_SF, Global.Target, Global.TargetMen, Global.TargetWomen, sectionName),
                                                                    true, true));

                                                current.IsTargetValid = false;
                                            }
                                            else
                                                current.IsTargetValid = true;
                                        }
                                        else
                                        {
                                            if (current.BaseTotal < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].BaseTotal",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Base, sectionName),
                                                                    true, true));

                                                current.IsBaseValid = false;
                                            }
                                            else
                                                current.IsBaseValid = true;

                                            if (current.TargetTotal < 0)
                                            {
                                                errors.Add(ValidationOption.Create(
                                                                    modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].TargetTotal",
                                                                    Global.ShortTemplateNonNegativeNumber,
                                                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.Target, sectionName),
                                                                    true, true));

                                                current.IsTargetValid = false;
                                            }
                                            else
                                                current.IsTargetValid = true;
                                        }
                                    }

                                    if (string.IsNullOrWhiteSpace(current.Description))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].Description",
                                                                Global.ShortTemplateRequired,
                                                                string.Format(Global.ViewTemplateRequired, Global.InformationSource, sectionName), true, true));

                                        current.IsDescriptionValid = false;
                                    }
                                    else if (current.Description.Length > Constants.IndicatorDescriptionLength)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".ProgrammeIndicatorsCollection[" + i + "].IndicatorCollection[" + j + "].Description",
                                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.IndicatorDescriptionLength),
                                                                string.Format(Global.ViewTemplateSymbolsMax, Global.InformationSource, sectionName, Constants.IndicatorDescriptionLength), true, true));

                                        current.IsDescriptionValid = false;
                                    }
                                    else
                                        current.IsDescriptionValid = true;
                                }

                                complexType.ProgrammeIndicatorsCollection[i].IsValid = true;
                            }
                            else
                            {
                                if (!complexType.IsStandardSimplified)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".ProgrammeIndicatorsCollection[" + i + "]",
                                        Global.ShortTemplateAtLeastOneRow,
                                        string.Format(Global.ViewTemplateAtLeastOneRow, sectionName),
                                        true, true));

                                    complexType.ProgrammeIndicatorsCollection[i].IsValid = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!complexType.IsStandardSimplified)
                    {
                        errors.Add(ValidationOption.Create(
                            modelPath + ".ProgrammeIndicatorsCollection",
                            string.Empty,
                            Global.ViewTemplateIndicators, true, true));
                    }
                }
            }

            #endregion

            #region ContractTeams

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.Team))
            {
                if (!complexType.IsStandardForBudgetLine && !complexType.IsFinancialIntermediaries && !complexType.IsFinalRecipients)
                {
                    if (complexType.ContractTeams?.isLocked != true)
                    {
                        if (complexType.ContractTeams == null ||
                            complexType.ContractTeams.ContractTeamCollection == null ||
                            complexType.ContractTeams.ContractTeamCollection.Count == 0)
                        {
                            if (!complexType.IsStandardSimplified)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ContractTeams.ContractTeamCollection",
                                    Global.ShortTemplateAtLeastOneRow,
                                    string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionTeam),
                                    true, true));

                                complexType.ContractTeams.ContractTeamCollection = new R_10019.ContractTeamCollection()
                                {
                                    IsValid = false
                                };
                            }
                        }
                        else
                        {
                            if (complexType.ContractTeams.ContractTeamCollection.Count <= Constants.ContractTeamsMaxCount)
                            {
                                for (int i = 0; i < complexType.ContractTeams.ContractTeamCollection.Count; i++)
                                {
                                    EngineValidate(csValidationEngine, complexType.ContractTeams.ContractTeamCollection[i],
                                        modelPath + ".ContractTeams.ContractTeamCollection[" + i + "]", errors);
                                }
                                complexType.ContractTeams.ContractTeamCollection.IsValid = true;
                            }
                            else
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ContractTeams.ContractTeamCollection",
                                    string.Format(Global.ShortTemplateMaxContractTeams, Constants.ContractTeamsMaxCount),
                                    string.Format(Global.ViewTemplateMaxContractTeams, Constants.ContractTeamsMaxCount,
                                        Global.SectionTeam),
                                    true, true));

                                complexType.ContractTeams.ContractTeamCollection.IsValid = false;
                            }
                        }
                    }
                }
            }

            #endregion

            #region ProjectErrands (skip if IsStandardForBudgetLine or IsFinancialIntermediaries or IsFinalRecipients)

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.ProcurementPlan))
            {
                if (!complexType.IsStandardForBudgetLine && !complexType.IsFinancialIntermediaries && !complexType.IsFinalRecipients)
                {
                    if (complexType.ProjectErrands != null && complexType.ProjectErrands.ProjectErrandCollection != null &&
                        complexType.ProjectErrands.ProjectErrandCollection.Count > 0 && !complexType.ProjectErrands.isLocked)
                    {
                        if (complexType.ProjectErrands.ProjectErrandCollection.Count <= Constants.ProjectErrandsMaxCount)
                        {
                            for (int i = 0; i < complexType.ProjectErrands.ProjectErrandCollection.Count; i++)
                            {
                                EngineValidate(csValidationEngine, complexType.ProjectErrands.ProjectErrandCollection[i],
                                    modelPath + ".ProjectErrands.ProjectErrandCollection[" + i + "]", errors);
                            }
                            complexType.ProjectErrands.ProjectErrandCollection.IsValid = true;
                        }
                        else
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".ProjectErrands.ProjectErrandCollection",
                                string.Format(Global.ShortTemplateMaxProjectErrands, Constants.ProjectErrandsMaxCount),
                                string.Format(Global.ViewTemplateMaxProjectErrands, Constants.ProjectErrandsMaxCount,
                                    Global.SectionOutsourcingPlan),
                                true, true));

                            complexType.ProjectErrands.ProjectErrandCollection.IsValid = false;
                        }
                    }
                }
            }

            #endregion


            #region ProjectSpecFields (skip if IsStandardForBudgetLine)

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.AdditionalInformation))
            {
                if (!complexType.IsStandardForBudgetLine)
                {
                    if (complexType.ProjectSpecFields != null &&
                        complexType.ProjectSpecFields.ProjectSpecFieldCollection != null &&
                        complexType.ProjectSpecFields.ProjectSpecFieldCollection.Count > 0
                        && !complexType.ProjectSpecFields.isLocked)
                    {
                        for (int i = 0; i < complexType.ProjectSpecFields.ProjectSpecFieldCollection.Count; i++)
                        {
                            var current = complexType.ProjectSpecFields.ProjectSpecFieldCollection[i];

                            if (current.IsRequired.HasValue && current.IsRequired.Value)
                            {
                                if (string.IsNullOrWhiteSpace(current.Value))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".ProjectSpecFields.ProjectSpecFieldCollection[" + i + "].Value",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, current.DisplayTitle,
                                            areFinLabels ? Global.FinancialIntermediariesSectionAdditionalInformation : Global.SectionAdditionalInformation),
                                        true, true));

                                    current.IsValueValid = false;
                                }
                                else
                                {
                                    current.IsValueValid = true;
                                }
                            }

                            if (current.Value != null && current.MaxLength > 0 && current.Value.Length > current.MaxLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ProjectSpecFields.ProjectSpecFieldCollection[" + i + "].Value",
                                    string.Format(Global.ShortTemplateSymbolsMax, current.MaxLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, current.Title,
                                        areFinLabels ? Global.FinancialIntermediariesSectionAdditionalInformation : Global.SectionAdditionalInformation, current.MaxLength),
                                    true, true));

                                current.IsValueValid = false;
                            }
                        }
                    }
                }

                if (complexType.ProjectSpecFields != null &&
                    complexType.ProjectSpecFields.ProjectSpecFieldCollection != null &&
                    complexType.ProjectSpecFields.ProjectSpecFieldCollection.Count > 0 &&
                    !complexType.ProjectSpecFields.isLocked)
                {
                    for (int i = 0; i < complexType.ProjectSpecFields.ProjectSpecFieldCollection.Count; i++)
                    {
                        var current = complexType.ProjectSpecFields.ProjectSpecFieldCollection[i];

                        if (current.MaxLength == 0 && !string.IsNullOrEmpty(current.Value))
                        {
                            var ibanValidationResult = IbanValidator.Validate(current.Value);

                            if (ibanValidationResult != IbanValidator.ValidationResult.IsValid)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ProjectSpecFields.ProjectSpecFieldCollection[" + i + "].Value",
                                    Global.ShortTemplateInvalid,
                                    string.Format(Global.ViewTemplateInvalid, current.DisplayTitle, Global.SectionAdditionalInformation),
                                    true, true));

                                current.IsValueValid = false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Init flags

            List<string> codes = null;
            bool hasDigital = complexType.AttachedDocuments != null && complexType.AttachedDocuments.AttachedDocumentCollection != null;

            if (complexType.RequiredDocumentsCodesNames != null)
            {
                codes = complexType.RequiredDocumentsCodesNames.Select(d => d.Item1).ToList();
            }

            #endregion

            #region ElectronicDeclarations

            if (!(complexType.ElectronicDeclarations != null && complexType.ElectronicDeclarations.isLocked))
            {
                if (complexType.ElectronicDeclarations != null &&
                    complexType.ElectronicDeclarations.ElectronicDeclarationCollection != null &&
                    complexType.ElectronicDeclarations.ElectronicDeclarationCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.ElectronicDeclarations.ElectronicDeclarationCollection.Count; i++)
                    {
                        var current = complexType.ElectronicDeclarations.ElectronicDeclarationCollection[i];

                        if (current.FieldType == R_10098.FieldType.CheckBox && current.IsRequired && current.IsActive)
                        {
                            bool fieldValueBool;
                            var canParseToBool = bool.TryParse(current.FieldValue, out fieldValueBool);

                            if (!canParseToBool)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueCheckBox",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else if (fieldValueBool != true)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueCheckBox",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else if (current.FieldType == R_10098.FieldType.Numeric)
                        {
                            if (string.IsNullOrWhiteSpace(current.FieldValue) && current.IsRequired && current.IsActive)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueNumeric",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else if (!string.IsNullOrWhiteSpace(current.FieldValue) && !decimal.TryParse(current.FieldValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fieldValueDecimal))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueNumeric",
                                    Global.ShortTemplateDigits,
                                    string.Format(Global.ViewTemplateDigits, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }

                        }
                        else if (current.FieldType == R_10098.FieldType.Text)
                        {
                            if (string.IsNullOrWhiteSpace(current.FieldValue) && current.IsRequired && current.IsActive)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueText",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else if (!string.IsNullOrWhiteSpace(current.FieldValue) && current.FieldValue.Length > Constants.ProjectDeclarationApprovalLength)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueText",
                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectDeclarationApprovalLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.DeclarationApproval, Global.SectionProjectDeclarations, Constants.ProjectDeclarationApprovalLength),
                                    true, true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else if (current.FieldType == R_10098.FieldType.Currency)
                        {
                            if (string.IsNullOrWhiteSpace(current.FieldValue) && current.IsRequired && current.IsActive)
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueCurrency",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else if (!string.IsNullOrWhiteSpace(current.FieldValue) && !decimal.TryParse(current.FieldValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fieldValueDecimal))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueCurrency",
                                    Global.ShortTemplateDigits,
                                    string.Format(Global.ViewTemplateDigits, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else if (current.FieldType == R_10098.FieldType.Nomenclature && current.IsRequired && current.IsActive)
                        {
                            if (string.IsNullOrWhiteSpace(current.FieldValue) || string.IsNullOrWhiteSpace(current.FieldValueId))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueNomenclature.gid",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else if (current.FieldType == R_10098.FieldType.Date && current.IsRequired && current.IsActive)
                        {
                            if (string.IsNullOrWhiteSpace(current.FieldValue))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValueDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApproval, Global.SectionProjectDeclarations),
                                    true,
                                    true));

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else if (current.FieldType == R_10098.FieldType.Period)
                        {
                            var dateFromError = ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValuePeriod.DateFrom",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApprovalDateFrom, Global.SectionProjectDeclarations),
                                    true,
                                    true);

                            var dateToError = ValidationOption.Create(
                                    modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValuePeriod.DateTo",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DeclarationApprovalDateTo, Global.SectionProjectDeclarations),
                                    true,
                                    true);

                            var dates = current.FieldValue?.Split('$');

                            if (dates?.Length == 2)
                            {
                                var isDateFromFilled = DateTime.TryParse(dates[0], out DateTime dateFrom);
                                var isDateToFilled = DateTime.TryParse(dates[1], out DateTime dateTo);

                                if (current.IsRequired && current.IsActive)
                                {
                                    if (!isDateFromFilled)
                                    {
                                        errors.Add(dateFromError);
                                        current.IsFieldValueValid = false;
                                    }

                                    if (!isDateToFilled)
                                    {
                                        errors.Add(dateToError);
                                        current.IsFieldValueValid = false;
                                    }
                                }

                                if (isDateFromFilled && isDateToFilled)
                                {
                                    if (dateFrom > dateTo)
                                    {
                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValuePeriod.DateFrom",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateStartEndDates, Global.DeclarationApprovalDateFrom, Global.DeclarationApprovalDateTo, Global.SectionProjectDeclarations),
                                            true,
                                            true));

                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".ElectronicDeclarations.ElectronicDeclarationCollection[" + i + "].FieldValuePeriod.DateTo",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateStartEndDates, Global.DeclarationApprovalDateFrom, Global.DeclarationApprovalDateTo, Global.SectionProjectDeclarations),
                                            true,
                                            true));

                                        current.IsFieldValueValid = false;
                                    }
                                    else
                                    {
                                        current.IsFieldValueValid = true;
                                    }
                                }
                                else if (!current.IsRequired || !current.IsActive)
                                {
                                    current.IsFieldValueValid = true;
                                }
                            }
                            else if (current.IsRequired && current.IsActive)
                            {
                                errors.Add(dateFromError);
                                errors.Add(dateToError);

                                current.IsFieldValueValid = false;
                            }
                            else
                            {
                                current.IsFieldValueValid = true;
                            }
                        }
                        else
                        {
                            current.IsFieldValueValid = true;
                        }
                    }
                }
            }

            #endregion ProjectDeclarations

            #region AttachedDocuments

            if (complexType.IsApplicationSectionSelected(ApplicationSectionType.AttachedDocuments))
            {
                if (!(complexType.AttachedDocuments != null && complexType.AttachedDocuments.isLocked))
                {
                    // Validate documents only if has certificate
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

                                    if (current.Type == null || string.IsNullOrWhiteSpace(current.Type.Id) ||
                                        string.IsNullOrWhiteSpace(current.Type.Name))
                                    {
                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].Type.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Type,
                                                Global.SectionAttachedDocuments), true, true));

                                        current.IsTypeValid = false;
                                    }
                                    else
                                        current.IsTypeValid = true;

                                    if (string.IsNullOrWhiteSpace(current.Description))
                                    {
                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                            "].Description",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Description,
                                                Global.SectionAttachedDocuments), true, true));

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
                                                Global.SectionAttachedDocuments,
                                                Constants.AttachedDocumentsDescriptionLength), true, true));

                                        current.IsDescriptionValid = false;
                                    }
                                    else
                                        current.IsDescriptionValid = true;

                                    string[] extensions = null;

                                    if (current.Type != null && !string.IsNullOrWhiteSpace(current.Type.Id) &&
                                        complexType.DocumentsExtensions != null &&
                                        complexType.DocumentsExtensions.ContainsKey(current.Type.Id))
                                    {
                                        extensions =
                                            complexType.DocumentsExtensions[current.Type.Id].Split(
                                                new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                                        extensions = extensions.Select(x => x.ToLower()).ToArray();
                                    }

                                    bool hasDocumentContent = false;
                                    bool hasSignatureContent = false;
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
                                                Global.SectionAttachedDocuments), true, true));

                                        if (current.AttachedDocumentContent != null)
                                            current.AttachedDocumentContent.IsDocumentValid = false;
                                    }
                                    else if (extensions != null && extensions.Length > 0)
                                    {
                                        hasDocumentContent = true;
                                        hasValidExtension =
                                            extensions.Any(ext => current.AttachedDocumentContent.FileName.ToLower().EndsWith(ext));

                                        if (!hasValidExtension)
                                        {
                                            string validExtensions = string.Join(", ", extensions);
                                            string viewErr;

                                            if (current.IsTypeValid)
                                                viewErr = string.Format(Global.ViewTemplateNamedDocumentExtension,
                                                    current.Type.Name, Global.SectionAttachedDocuments, validExtensions);
                                            else
                                                viewErr = string.Format(Global.ViewTemplateDocumentExtension,
                                                    Global.SectionAttachedDocuments, validExtensions);

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
                                                viewErr = string.Format(Global.ViewTemplateNamedDocumentSizeNoInfo,
                                                    current.Type.Name, Global.SectionAttachedDocuments);
                                            else
                                                viewErr = string.Format(Global.ViewTemplateDocumentSizeNoInfo,
                                                    Global.SectionAttachedDocuments);

                                            errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                                "].AttachedDocumentContent.BlobContentId",
                                                Global.ShortTemplateDocumentSizeNoInfo,
                                                viewErr, true, true));
                                        }
                                        else
                                            documentTotalSize += size;
                                    }

                                    int signatureTotalSize = 0;

                                    if (current.Type != null && current.Type.isSignatureRequired)
                                    {
                                        if (current.SignatureContentCollection != null &&
                                            current.SignatureContentCollection.Count > 0)
                                        {
                                            for (int j = 0; j < current.SignatureContentCollection.Count; j++)
                                            {
                                                current.SignatureContentCollection[j].IsDocumentValid = true;

                                                if (current.SignatureContentCollection[j] == null ||
                                                    string.IsNullOrWhiteSpace(
                                                        current.SignatureContentCollection[j].BlobContentId) ||
                                                    string.IsNullOrWhiteSpace(current.SignatureContentCollection[j].FileName)
                                                    || string.IsNullOrWhiteSpace(current.SignatureContentCollection[j].Size))
                                                {
                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                                        "].SignatureContentCollection[" + j + "].BlobContentId",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequired, Global.Signature,
                                                            Global.SectionAttachedDocuments), true, true));

                                                    if (current.SignatureContentCollection[j] != null)
                                                        current.SignatureContentCollection[j].IsDocumentValid = false;

                                                    continue;
                                                }

                                                int size;

                                                if (!int.TryParse(current.SignatureContentCollection[j].Size, out size))
                                                {
                                                    string viewErr;

                                                    if (current.IsTypeValid)
                                                        viewErr = string.Format(
                                                            Global.ViewTemplateNamedSignatureSizeNoInfo, current.Type.Name,
                                                            Global.SectionAttachedDocuments);
                                                    else
                                                        viewErr = string.Format(Global.ViewTemplateSignatureSizeNoInfo,
                                                            Global.SectionAttachedDocuments);

                                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                                        "].SignatureContentCollection[" + j + "].BlobContentId",
                                                        Global.ShortTemplateSignatureSizeNoInfo,
                                                        viewErr, true, true));
                                                }
                                                else
                                                {
                                                    if (!hasSignatureContent)
                                                        hasSignatureContent = true;

                                                    signatureTotalSize += size;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                                "].SignatureContent",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Signature,
                                                    Global.SectionAttachedDocuments), true, true));
                                        }
                                    }

                                    if (hasSignatureContent)
                                        documentTotalSize += signatureTotalSize;

                                    if (((hasDocumentContent && hasValidExtension) || hasSignatureContent) &&
                                        documentTotalSize > Constants.AttachedDocumentMaxSize)
                                    {
                                        string viewErr;

                                        if (current.IsTypeValid)
                                            viewErr = string.Format(Global.ViewTemplateNamedDocumentTotalSize,
                                                current.Type.Name, Constants.AttachedDocumentMaxSize / (1024 * 1024),
                                                Global.SectionAttachedDocuments);
                                        else
                                            viewErr = string.Format(Global.ViewTemplateDocumentTotalSize,
                                                Constants.AttachedDocumentMaxSize / (1024 * 1024),
                                                Global.SectionAttachedDocuments);

                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                            "].AttachedDocumentContent.BlobContentId",
                                            string.Format(Global.ShortTemplateDocumentTotalSize,
                                                Constants.AttachedDocumentMaxSize / (1024 * 1024)),
                                            viewErr, true, true));

                                        current.IsDocumentValid = false;
                                    }
                                    else
                                        current.IsDocumentValid = true;

                                    //EngineValidate(csValidationEngine, complexType.AttachedDocuments.AttachedDocumentCollection[i], modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "]", errors);
                                }
                            }
                            else
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection",
                                    string.Format(Global.ShortTemplateMaxAttachedDocuments,
                                        Constants.AttachedDocumentsMaxCount),
                                    string.Format(Global.ViewTemplateMaxAttachedDocuments,
                                        Constants.AttachedDocumentsMaxCount, Global.SectionAttachedDocuments),
                                    true, true));

                                complexType.AttachedDocuments.AttachedDocumentCollection.HasValidCount = false;
                            }
                        }
                    }

                    if (complexType.RequiredDocumentsCodesNames != null && complexType.RequiredDocumentsCodesNames.Count > 0)
                    {
                        foreach (var docCodeName in complexType.RequiredDocumentsCodesNames)
                        {
                            if (hasDigital)
                            {
                                bool contains = false;

                                if (hasDigital)
                                    contains = complexType.AttachedDocuments.AttachedDocumentCollection.Any(d => d != null && d.Type != null && d.Type.Id == docCodeName.Item1);

                                if (!contains)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        docCodeName.Item1,
                                                        string.Empty,
                                                        string.Format(Global.ViewTemplateRequiredDocuments, docCodeName.Item2, Global.SectionPaperAttachedDocuments, Global.SectionAttachedDocuments), true, true));
                                }
                            }
                        }
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
