using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Eumis
{
    public class Message : CSValidatorBase<R_10020.Message>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10020.Message complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (complexType.type == R_09990.MessageTypeNomenclature.Question)
            {
                #region Send

                #region Subject

                if (complexType.IsManagingAuthority)
                {
                    bool isSubjectEmpty = complexType.Subject == null || string.IsNullOrWhiteSpace(complexType.Subject.id) || string.IsNullOrWhiteSpace(complexType.Subject.Description);

                    if (isSubjectEmpty)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Subject",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequiredNoSection, Global.SubjectCommunication), true, true));

                        complexType.IsSubjectValid = false;
                    }
                    else
                    {
                        complexType.IsSubjectValid = true;
                    }
                }

                #endregion

                #region EndingDate

                if (!complexType.IsManagingAuthority)
                {
                    if (!complexType.EndingDate.HasValue)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".EndingDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequiredNoSection, "Краен срок за отговор"), false, true));
                    }
                    else
                    {
                        if (complexType.EndingDate.Value.Date <= DateTime.Now.Date)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".EndingDate",
                                                    Global.ShortTemplateQuestionEndingDateInvalid,
                                                    Global.ViewTemplateQuestionEndingDateInvalid, false, true));
                        }
                    }
                }

                #endregion

                #region Content

                if (string.IsNullOrWhiteSpace(complexType.Content))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Content",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequiredNoSection, Global.Question), false, true));
                }
                else if (complexType.Content.Length > Constants.MessageContentLength)
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Content",
                        string.Format(Global.ShortTemplateSymbolsMax,
                            Constants.MessageContentLength),

                        string.Format(Global.ViewTemplateSymbolsMaxWithoutSection, Global.Question, Constants.MessageContentLength), false, true));
                }

                #endregion

                #region ContentAttachedDocumentCollection

                if (complexType.ContentAttachedDocumentCollection != null && complexType.ContentAttachedDocumentCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.ContentAttachedDocumentCollection.Count; i++)
                    {
                        var current = complexType.ContentAttachedDocumentCollection[i];

                        if (string.IsNullOrWhiteSpace(current.Description))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContentAttachedDocumentCollection[" + i + "].Description",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Description, Global.ContentAttachedDocumentCollection), true, true));

                            complexType.ContentAttachedDocumentCollection[i].IsDescriptionValid = false;
                        }
                        else
                            complexType.ContentAttachedDocumentCollection[i].IsDescriptionValid = true;

                        if (current.AttachedDocumentContent == null)
                            current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();

                        if (current.AttachedDocumentContent == null || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContentAttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.ContentAttachedDocumentCollection), true, true));

                            complexType.ContentAttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = false;
                        }
                        else
                            complexType.ContentAttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = true;
                    }
                }

                #endregion

                #endregion
            }
            else if (complexType.type == R_09990.MessageTypeNomenclature.Answer)
            {
                #region Reply

                #region Reply content

                if (string.IsNullOrWhiteSpace(complexType.Reply))
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Reply",
                                            Global.ShortTemplateRequired,
                                            string.Format(Global.ViewTemplateRequiredNoSection, Global.Reply), false, true));
                }
                else if (complexType.Reply.Length > Constants.MessageReplyLength)
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Reply",
                        string.Format(Global.ShortTemplateSymbolsMax,
                            Constants.MessageReplyLength),
                        string.Format(Global.ViewTemplateSymbolsMaxWithoutSection, Global.Reply, Constants.MessageReplyLength), false, true));
                }

                #endregion

                #region ReplyAttachedDocumentCollection

                if (complexType.ReplyAttachedDocumentCollection != null && complexType.ReplyAttachedDocumentCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.ReplyAttachedDocumentCollection.Count; i++)
                    {
                        var current = complexType.ReplyAttachedDocumentCollection[i];

                        if (string.IsNullOrWhiteSpace(current.Description))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ReplyAttachedDocumentCollection[" + i + "].Description",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Description, Global.ReplyAttachedDocumentCollection), true, true));

                            complexType.ReplyAttachedDocumentCollection[i].IsDescriptionValid = false;
                        }
                        else
                            complexType.ReplyAttachedDocumentCollection[i].IsDescriptionValid = true;

                        if (current.AttachedDocumentContent == null)
                            current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();

                        if (current.AttachedDocumentContent == null || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ReplyAttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.ReplyAttachedDocumentCollection), true, true));

                            complexType.ReplyAttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = false;
                        }
                        else
                            complexType.ReplyAttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = true;
                    }
                }

                #endregion

                #region Project

                if (complexType.Project != null)
                {
                    EngineValidate(csValidationEngine, complexType.Project, modelPath + ".Project", errors);
                }
                else if (complexType.IsManagingAuthority == false)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Project",
                                            string.Empty,
                                            string.Format(Global.ViewTemplateMissingSection, Global.Project), true, true));
                }

                #endregion

                #endregion
            }
        }
    }
}
