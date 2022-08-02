using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Mappers;

namespace Eumis.Documents.Validation.Eumis
{
    public class TechnicalReport : CSValidatorBase<R_10044.TechnicalReport>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10044.TechnicalReport complexType, string modelPath, IList<ValidationOption> errors)
        {
            #region TechnicalReportBasicData

            if (complexType.BasicData != null)
            {
                EngineValidate(csValidationEngine, complexType.BasicData, modelPath + ".BasicData", errors);
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BasicData",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionGeneralInformation), true, true));
            }

            #endregion

            #region Activities

            if (complexType.Activities != null && complexType.Activities.ActivityCollection != null && complexType.Activities.ActivityCollection.Count > 0)
            {
                for (int i = 0; i < complexType.Activities.ActivityCollection.Count; i++)
                {
                    EngineValidate(csValidationEngine, complexType.Activities.ActivityCollection[i],
                        modelPath + ".Activities.ActivityCollection[" + i + "]", errors);
                }
            }

            #endregion

            #region Indicators

            if (complexType.Indicators != null && complexType.Indicators.IndicatorCollection != null && complexType.Indicators.IndicatorCollection.Count > 0)
            {
                for (int i = 0; i < complexType.Indicators.IndicatorCollection.Count; i++)
                {
                    EngineValidate(csValidationEngine, complexType.Indicators.IndicatorCollection[i],
                        modelPath + ".Indicators.IndicatorCollection[" + i + "]", errors);
                }
            }

            #endregion

            #region TechnicalReportTeams

            if (complexType.Team != null && complexType.Team.TeamMemberCollection != null && complexType.Team.TeamMemberCollection.Count > 0)
            {
                for (int i = 0; i < complexType.Team.TeamMemberCollection.Count; i++)
                {
                    EngineValidate(csValidationEngine, complexType.Team.TeamMemberCollection[i],
                        modelPath + ".Team.TeamMemberCollection[" + i + "]", errors);
                }
            }

            #endregion

            #region TechnicalReportAttachedDocuments

            if (complexType.AttachedDocuments != null)
            {
                if (complexType.AttachedDocuments.AttachedDocumentCollection != null &&
                    complexType.AttachedDocuments.AttachedDocumentCollection.Count > 0)
                {
                    if (complexType.AttachedDocuments.AttachedDocumentCollection.Count <=
                        Constants.AttachedDocumentsMaxCount)
                    {
                        var hasTechnicalReportDocumentTypes = complexType.Nomenclatures[NomenclatureType.ContractReportDocumentType].Any();

                        for (int i = 0; i < complexType.AttachedDocuments.AttachedDocumentCollection.Count; i++)
                        {
                            var current = complexType.AttachedDocuments.AttachedDocumentCollection[i];

                            var isCurrentVersion = current.VersionNum == complexType.docNumber && current.VersionSubNum == complexType.docSubNumber;

                            if (hasTechnicalReportDocumentTypes && isCurrentVersion &&
                                (current.Type == null || string.IsNullOrWhiteSpace(current.Type.Id) || string.IsNullOrWhiteSpace(current.Type.Name)))
                            {
                                errors.Add(ValidationOption.Create(
                                    modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                    "].Type.id",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.DocumentType,
                                        Global.SectionInventoryDocuments), true, true));

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
                                        Global.SectionInventoryDocuments), true, true));

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
                                        Global.SectionInventoryDocuments,
                                        Constants.AttachedDocumentsDescriptionLength), true, true));

                                current.IsDescriptionValid = false;
                            }
                            else
                            {
                                current.IsDescriptionValid = true;
                            }

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
                                        Global.SectionInventoryDocuments), true, true));

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
                                            current.Type.Name, Global.SectionInventoryDocuments, validExtensions);
                                    }
                                    else
                                    {
                                        viewErr = string.Format(Global.ViewTemplateDocumentExtension,
                                            Global.SectionInventoryDocuments, validExtensions);
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
                                           current.Type.Name, Global.SectionInventoryDocuments);
                                    }
                                    else
                                    {
                                        viewErr = string.Format(Global.ViewTemplateDocumentSizeNoInfo,
                                           Global.SectionInventoryDocuments);
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
                                Constants.AttachedDocumentsMaxCount, Global.SectionInventoryDocuments),
                            true, true));

                        complexType.AttachedDocuments.HasValidCount = false;
                    }
                }

                if (complexType.AttachedDocuments.AttachedDocumentCollection != null && complexType.RequiredDocumentsCodesNames != null && complexType.RequiredDocumentsCodesNames.Count > 0)
                {
                    foreach (var docCodeName in complexType.RequiredDocumentsCodesNames)
                    {
                        bool contains = complexType.AttachedDocuments.AttachedDocumentCollection.Any(d => d != null && d.Type != null && d.Type.Id == docCodeName.Item1);

                        if (!contains)
                        {
                            errors.Add(ValidationOption.Create(
                                                docCodeName.Item1,
                                                string.Empty,
                                                string.Format(Global.ViewTemplateRequiredDocument, docCodeName.Item2, Global.SectionInventoryDocuments), true, true));
                        }

                    }
                }
            }

            #endregion
        }
    }
}
