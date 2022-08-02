using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class AttachedDocument : CSValidatorBase<R_10018.AttachedDocument>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10018.AttachedDocument complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (complexType.Type == null || string.IsNullOrWhiteSpace(complexType.Type.Id) || string.IsNullOrWhiteSpace(complexType.Type.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Type.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Type, Global.SectionAttachedDocuments), true, true));

                complexType.IsTypeValid = false;
            }
            else
                complexType.IsTypeValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionAttachedDocuments), true, true));

                complexType.IsDescriptionValid = false;
            }
            else
                complexType.IsDescriptionValid = true;

            string[] extensions = null;

            //if (!string.IsNullOrWhiteSpace(complexType.Extension))
            //{
            //    extensions = complexType.Extension.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            //}

            if (complexType.AttachedDocumentContent == null)
                complexType.AttachedDocumentContent = new R_09992.AttachedDocumentContent();

            complexType.AttachedDocumentContent.IsDocumentValid = true;
            complexType.IsSignatureValid = true;

            if (complexType.AttachedDocumentContent == null || string.IsNullOrWhiteSpace(complexType.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(complexType.AttachedDocumentContent.FileName))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.SectionAttachedDocuments), true, true));

                complexType.AttachedDocumentContent.IsDocumentValid = false;
            }
            else if (extensions != null && extensions.Length > 0)
            {
                bool hasValidExtension = extensions.Any(ext => complexType.AttachedDocumentContent.FileName.EndsWith(ext));

                if (!hasValidExtension)
                {
                    string validExtensions = string.Join(", ", extensions);

                    errors.Add(ValidationOption.Create(
                                        modelPath + ".AttachedDocumentContent.BlobContentId",
                                        string.Format(Global.ShortTemplateDocumentExtension, validExtensions),
                                        string.Format(Global.ViewTemplateDocumentExtension, Global.SectionAttachedDocuments, validExtensions), true, true));

                    complexType.AttachedDocumentContent.IsDocumentValid = false;
                }
            }

            // if (complexType.SignatureContent == null || string.IsNullOrWhiteSpace(complexType.SignatureContent.BlobContentId) || string.IsNullOrWhiteSpace(complexType.SignatureContent.FileName))
            // {
            //     errors.Add(ValidationOption.Create(
            //                             modelPath + ".SignatureContent.BlobContentId",
            //                             Global.ShortTemplateRequired,
            //                             string.Format(Global.ViewTemplateRequired, Global.Signature, Global.SectionAttachedDocuments), true, true));
            // 
            //     complexType.IsSignatureValid = false;
            // }
        }
    }
}