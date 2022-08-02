using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Eumis
{
    public class Communication : CSValidatorBase<R_10042.Communication>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10042.Communication complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (string.IsNullOrWhiteSpace(complexType.Subject))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Subject",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequiredNoSection, Global.SubjectCommunication), false, true));
            }

            if (string.IsNullOrWhiteSpace(complexType.Content))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Content",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequiredNoSection, Global.ContentCommunication), false, true));
            }

            if (complexType.AttachedDocumentCollection != null && complexType.AttachedDocumentCollection.Count > 0)
            {
                for (int i = 0; i < complexType.AttachedDocumentCollection.Count; i++)
                {
                    var current = complexType.AttachedDocumentCollection[i];

                    if (string.IsNullOrWhiteSpace(current.Description))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocumentCollection[" + i + "].Description",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionAttachedDocumentsUnsigned), true, true));

                        current.IsDescriptionValid = false;
                    }
                    else
                        current.IsDescriptionValid = true;

                    if (current.AttachedDocumentContent == null)
                        current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();
                    current.AttachedDocumentContent.IsDocumentValid = true;

                    if (string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.SectionAttachedDocumentsUnsigned), true, true));

                        current.AttachedDocumentContent.IsDocumentValid = false;
                    }
                }
            }

        }
    }
}
