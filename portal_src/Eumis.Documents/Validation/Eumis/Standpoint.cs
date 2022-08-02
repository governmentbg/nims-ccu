using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Eumis
{
    public class Standpoint : CSValidatorBase<R_10027.Standpoint>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10027.Standpoint complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsSubjectValid = true;
            complexType.IsContentValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Subject))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Subject",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequiredNoSection, Global.Subject), true, true));

                complexType.IsSubjectValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Content))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Content",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequiredNoSection, Global.Content), true, true));

                complexType.IsContentValid = false;
            }

            if (complexType.AttachedDocumentCollection != null && complexType.AttachedDocumentCollection.Count > 0)
            {
                for (int i = 0; i < complexType.AttachedDocumentCollection.Count; i++)
                {
                    var current = complexType.AttachedDocumentCollection[i];
                    current.IsDescriptionValid = true;
                    current.AttachedDocumentContent.IsDocumentValid = true;

                    if (string.IsNullOrWhiteSpace(current.Description))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocumentCollection[" + i + "].Description",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionAttachedDocumentsUnsigned), true, true));

                        current.IsDescriptionValid = false;
                    }

                    if (current.AttachedDocumentContent == null || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName))
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
