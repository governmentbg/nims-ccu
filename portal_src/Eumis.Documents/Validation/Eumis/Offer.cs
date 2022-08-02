using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Eumis
{
    public class Offer : CSValidatorBase<R_10080.Offer>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10080.Offer complexType,
            string modelPath, IList<ValidationOption> errors)
        {
            #region Candidate

            if (complexType.Candidate != null)
            {
                bool isUinTypeEmpty = complexType.Candidate.UinType == null || string.IsNullOrWhiteSpace(complexType.Candidate.UinType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.UinType.Name);

                complexType.Candidate.IsUinTypeValid = true;
                complexType.Candidate.IsUinValid = true;
                complexType.Candidate.IsNameValid = true;
                complexType.Candidate.IsNameEnValid = true;
                complexType.Candidate.IsCompanyTypeValid = true;
                complexType.Candidate.IsCompanyLegalTypeValid = true;
                complexType.Candidate.IsEmailValid = true;
                complexType.Candidate.IsPhone1Valid = true;
                complexType.Candidate.IsPhone2Valid = true;
                complexType.Candidate.IsFaxValid = true;
                complexType.Candidate.IsCompanyRepresentativePersonValid = true;
                complexType.Candidate.IsCompanyContactPersonValid = true;
                complexType.Candidate.IsCompanyContactPersonPhoneValid = true;
                complexType.Candidate.IsCompanyContactPersonEmailValid = true;

                if (isUinTypeEmpty)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.UinType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionCandidate), true, true));

                    complexType.Candidate.IsUinTypeValid = false;
                }

                if (string.IsNullOrWhiteSpace(complexType.Candidate.Uin))
                {
                    if (isUinTypeEmpty || complexType.Candidate.UinType.Id != UinTypeNomenclature.Foreign.Code)
                    {
                        errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.Uin",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionCandidate), true, true));

                        complexType.Candidate.IsUinValid = false;
                    }
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
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Bulstat, Global.SectionCandidate, Constants.BulstatFieldMaxLength), true, true));

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

                if (!(complexType.Candidate.CompanyLegalType != null && Constants.CompanyLegalTypePhysicalGid.Equals(complexType.Candidate.CompanyLegalType.Id)))
                {
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
                }

                if (complexType.Candidate.CompanyType == null || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyType.Name))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.CompanyType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyType, Global.SectionCandidate), true, true));

                    complexType.Candidate.IsCompanyTypeValid = false;
                }

                if (complexType.Candidate.CompanyLegalType == null || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyLegalType.Id) || string.IsNullOrWhiteSpace(complexType.Candidate.CompanyLegalType.Name))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.CompanyLegalType.id",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequired, Global.CompanyLegalType, Global.SectionCandidate), true, true));

                    complexType.Candidate.IsCompanyLegalTypeValid = false;
                }

                if (complexType.Candidate.Seat != null)
                {
                    EngineValidate(csValidationEngine, complexType.Candidate.Seat, modelPath + ".Candidate.Seat", errors);
                }

                if (complexType.Candidate.Correspondence != null)
                {
                    EngineValidate(csValidationEngine, complexType.Candidate.Correspondence, modelPath + ".Candidate.Correspondence", errors);
                }

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

                if (!string.IsNullOrWhiteSpace(complexType.Candidate.Phone2) && complexType.Candidate.Phone2.Length > Constants.CandidatePhoneLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.Phone2",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.Phone2, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                    complexType.Candidate.IsPhone2Valid = false;
                }

                if (!string.IsNullOrWhiteSpace(complexType.Candidate.Fax) && complexType.Candidate.Fax.Length > Constants.CandidatePhoneLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Candidate.Fax",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidatePhoneLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.Fax, Global.SectionCandidate, Constants.CandidatePhoneLength), true, true));

                    complexType.Candidate.IsFaxValid = false;
                }

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
                            string.Format(Global.ViewTemplateValidMail, Global.Email, Global.SectionCandidate), true, true));

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
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Candidate",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionCandidate), true, true));
            }

            #endregion

            #region AttachedDocuments

            if (!(complexType.AttachedDocuments != null && complexType.AttachedDocuments.isLocked))
            {
                if (complexType.AttachedDocuments != null)
                {
                    if (complexType.AttachedDocuments.AttachedDocumentCollection != null && complexType.AttachedDocuments.AttachedDocumentCollection.Count > 0)
                    {
                        if (complexType.AttachedDocuments.AttachedDocumentCollection.Count <= Constants.AttachedDocumentsMaxCount)
                        {
                            for (int i = 0; i < complexType.AttachedDocuments.AttachedDocumentCollection.Count; i++)
                            {
                                var current = complexType.AttachedDocuments.AttachedDocumentCollection[i];

                                current.IsDescriptionValid = true;
                                if (string.IsNullOrWhiteSpace(current.Description))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionPackageDocuments),
                                        true,
                                        true));

                                    current.IsDescriptionValid = false;
                                }
                                else if (current.Description.Length > Constants.AttachedDocumentsDescriptionLength)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].Description",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.AttachedDocumentsDescriptionLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Description, Global.SectionPackageDocuments, Constants.AttachedDocumentsDescriptionLength),
                                        true,
                                        true));

                                    current.IsDescriptionValid = false;
                                }

                                int documentTotalSize = 0;

                                if (current.AttachedDocumentContent == null)
                                {
                                    current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();
                                }
                                current.AttachedDocumentContent.IsDocumentValid = true;

                                if (string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) ||
                                    string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName) ||
                                    string.IsNullOrWhiteSpace(current.AttachedDocumentContent.Size))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.SectionPackageDocuments),
                                        true,
                                        true));

                                    current.AttachedDocumentContent.IsDocumentValid = false;
                                }
                                else
                                {
                                    if (!int.TryParse(current.AttachedDocumentContent.Size, out int size))
                                    {
                                        errors.Add(ValidationOption.Create(
                                           modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                           Global.ShortTemplateDocumentSizeNoInfo,
                                           string.Format(Global.ViewTemplateDocumentSizeNoInfo, Global.SectionPackageDocuments),
                                           true,
                                           true));
                                    }
                                    else
                                    {
                                        documentTotalSize += size;
                                    }
                                }

                                int signatureTotalSize = 0;

                                if (current.SignatureContentCollection != null && current.SignatureContentCollection.Count > 0)
                                {
                                    for (int j = 0; j < current.SignatureContentCollection.Count; j++)
                                    {
                                        current.SignatureContentCollection[j].IsDocumentValid = true;

                                        if (!string.IsNullOrWhiteSpace(current.SignatureContentCollection[j].BlobContentId) &&
                                            !string.IsNullOrWhiteSpace(current.SignatureContentCollection[j].FileName) &&
                                            !string.IsNullOrWhiteSpace(current.SignatureContentCollection[j].Size))
                                        {
                                            if (!int.TryParse(current.SignatureContentCollection[j].Size, out int size))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                                    "].SignatureContentCollection[" + j + "].BlobContentId",
                                                    Global.ShortTemplateSignatureSizeNoInfo,
                                                    string.Format(Global.ViewTemplateSignatureSizeNoInfo,
                                                        Global.SectionPackageDocuments), true, true));
                                            }
                                            else
                                            {
                                                signatureTotalSize += size;
                                            }
                                        }
                                    }
                                }

                                documentTotalSize += signatureTotalSize;

                                current.IsDocumentValid = true;
                                if (documentTotalSize > Constants.AttachedDocumentMaxSize)
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                        "].AttachedDocumentContent.BlobContentId",
                                        string.Format(Global.ShortTemplateDocumentTotalSize,
                                            Constants.AttachedDocumentMaxSize / (1024 * 1024)),
                                        string.Format(Global.ViewTemplateDocumentTotalSize,
                                            Constants.AttachedDocumentMaxSize / (1024 * 1024),
                                            Global.SectionPackageDocuments), true, true));

                                    current.IsDocumentValid = false;
                                }

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
                                    Constants.AttachedDocumentsMaxCount, Global.SectionPackageDocuments),
                                true, true));

                            complexType.AttachedDocuments.HasValidCount = false;
                        }
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection",
                                    Global.ShortTemplateAtLeastOneRow,
                                    string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionPackageDocuments),
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
