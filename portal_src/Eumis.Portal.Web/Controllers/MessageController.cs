using System.Net;
using Eumis.Components;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Enums;
using Eumis.Documents;
using Eumis.Components.Communicators;
using Microsoft.AspNet.Identity.Owin;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Documents.Validation;
using Eumis.Common.Extensions;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public partial class MessageController : WorkflowController<EditVM>
    {
        private IMessageCommunicator _messageCommunicator;
        private IProcedureCommunicator _procedureCommunicator;

        private PublicSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PublicSignInManager>();
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.Exception is WebException)
            {
                var apiError = ApiErrorHandling.GetError((WebException) filterContext.Exception);
                if (apiError == ApiError.messageCanceled)
                {
                    ViewResult viewResult = new ViewResult()
                    {
                        ViewName = MVC.Shared.Views.Failure,
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                        {
                            Model = (object)"Въпросът е анулиран."
                        }
                    };

                    filterContext.Result = viewResult;
                    filterContext.ExceptionHandled = true;
                }
                else if (apiError == ApiError.messageTimedOut)
                {
                    ViewResult viewResult = new ViewResult()
                    {
                        ViewName = MVC.Shared.Views.Failure,
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                        {
                            Model = (object)"Срокът за отговор на въпроса е изтекъл."
                        }
                    };

                    filterContext.Result = viewResult;
                    filterContext.ExceptionHandled = true;
                }
            }
        }

        public MessageController(IMessageCommunicator messageCommunicator, IProcedureCommunicator procedureCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _messageCommunicator = messageCommunicator;
            _procedureCommunicator = procedureCommunicator;
        }

        public virtual ActionResult Index(bool? hasFinishedAnswer)
        {
            return View(new IndexVM()
            {
                Messages = _messageCommunicator.GetMessages(CurrentUser.AccessToken),
                HasFinishedAnswer = hasFinishedAnswer
            });
        }

        [HttpGet]
        public virtual ActionResult New()
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            // TODO: Real data
            Guid procedureGid = new Guid("8cf3e381-428c-435d-9d44-f9daffe25751");

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(procedureGid);

            var initMessage = new R_10020.Message();
            initMessage.Content = "Иницииране на съдържанието на съобщението. Това е примерен текст";
            initMessage.ContentAttachedDocumentCollection = new R_10020.AttachedDocumentCollection()
            {
                new R_10018.AttachedDocument()
                {
                    Description = "123"
                }
            };

            R_10020.Message message = R_10020.Message.LoadReply(procedure, initMessage);
            // END TODO

            AppContext.Current.Document = message;

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid)
        {
            var xml = _messageCommunicator.GetMessage(gid, CurrentUser.AccessToken).xml;

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(xml);

            var procedureData = _procedureCommunicator.GetProcedureAppData(Guid.Parse(message.Project.ProjectBasicData.ProcedureIdentifier));
            message.Project.LoadNomenclature(procedureData.applicationSections);

            message.Project.SetDeclarationsAttributes(procedureData.declarations);

            AppContext.Current.Document = message;
            AppContext.Current.Xml = xml;

            return View(AppContext.Current.Document);
        }

        [HttpGet]
        public virtual ActionResult PreviewAnswer(Guid communicationGid, Guid answerGid)
        {
            var xml = _messageCommunicator.GetAnswer(communicationGid, answerGid, CurrentUser.AccessToken).xml;

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(xml);

            var procedureData = _procedureCommunicator.GetProcedureAppData(Guid.Parse(message.Project.ProjectBasicData.ProcedureIdentifier));
            message.Project.LoadNomenclature(procedureData.applicationSections);

            message.Project.SetDeclarationsAttributes(procedureData.declarations);

            AppContext.Current.Document = message;
            AppContext.Current.Xml = xml;

            return View(Views.Preview, AppContext.Current.Document);
        }

        [HttpGet]
        public virtual ActionResult NewAnswer(Guid communicationGid, string version)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _messageCommunicator.GetNewAnswer(
                communicationGid,
                Convert.FromBase64String(version),
                CurrentUser.AccessToken);

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            AppContext.Current.Xml = contractMessage.xml;

            return RedirectToAction(ActionNames.Edit,
                new
                {
                    communicationGid = contractMessage.gid,
                    answerGid = contractMessage.answerGid,
                    status = RegMessageAnswerType.Draft,
                });
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutDraft();

            return base.Prepare(vm);
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid communicationGid, Guid answerGid, RegMessageAnswerType status, string version)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            if (status == RegMessageAnswerType.AnswerFinalized)
            {
                _messageCommunicator.DefinalizeAnswer(
                    communicationGid,
                    answerGid,
                    Convert.FromBase64String(version),
                    CurrentUser.AccessToken);
            }

            var contractMessage = _messageCommunicator.GetAnswer(communicationGid, answerGid, CurrentUser.AccessToken);

            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            var procedure = _procedureCommunicator.GetProcedureAppData(new Guid(message.Project.ProjectBasicData.ProcedureIdentifier));
            //if (!procedure.isActive)
            //{
            //    return View(MVC.Shared.Views.Failure, (object)Global.ProcedureEditNotActive);
            //}

            message = R_10020.Message.LoadReply(procedure, message);
            message.EndingDate = contractMessage.messageEndingDate;
            message.RegistrationNumber = contractMessage.registrationNumber;
            message.MessageDate = contractMessage.messageDate;
            message.LastSendingDate = contractMessage.lastSendingDate;

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                parentGid = communicationGid,
                gid = answerGid,
                version = contractMessage.version
            };

            return RedirectToAction(MVC.Message.ActionNames.Prepare, MVC.Message.Name);
        }

        [HttpPost]
        [RequiresAppContext]
        public virtual ActionResult Finalize()
        {
            this.PutDraft();
            this.FinalizeDraft();

            AppContext.Current.Clear();

            return RedirectToAction(MVC.Message.ActionNames.Index, MVC.Message.Name, new { hasFinishedAnswer = true });
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid communicationGid, Guid answerGid, string version)
        {
            _messageCommunicator.DeleteAnswer(
                    communicationGid,
                    answerGid,
                    Convert.FromBase64String(version),
                    CurrentUser.AccessToken);

            TempData["SuccessAction"] = "Отговорът беше изтрит успешно.";

            return RedirectToAction(MVC.Message.ActionNames.Index, MVC.Message.Name, new { hasFinishedAnswer = false });
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutDraft();

            base.SaveDraft();
        }

        [HttpGet]
        public virtual FileResult GetDeclaration(Guid gid, string hash)
        {
            var messageXml = _messageCommunicator.GetMessage(gid, CurrentUser.AccessToken);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(messageXml.xml);

            Eumis.Portal.Web.Models.Draft.DeclarationVM vm =
                Eumis.Portal.Web.Models.Draft.DeclarationVM.GetVM(message.Project);

            vm.Email = CurrentUser.Email;
            vm.ProjectCode = hash;

            vm.RegistrationNumber = messageXml.registrationNumber;
            vm.RegistrationDate = messageXml.messageDate.Value;

            if (!string.IsNullOrWhiteSpace(vm.ProcedureCode) && !string.IsNullOrWhiteSpace(vm.ProjectCode))
                vm.FileName = string.Format("{0}-{1}", vm.ProcedureCode, vm.ProjectCode);

            string renderedHtml = Eumis.Common.Helpers.RazorRenderHtmlHelper.RenderHtml(vm, MVC.Message.Views.Declaration);
            string serverPath = Server.MapPath("~");

            byte[] renderedHtmlArr = System.Text.Encoding.UTF8.GetBytes(renderedHtml);

            byte[] pdfDoc = PdfManager.ConvertHtmlToPdf(renderedHtmlArr, serverPath, new PdfManagerSettings() { MarginLeft = 25, MarginRight = 25, MarginTop = 8, MarginBottom = 8, IsLandscape = false });

            return File(pdfDoc, "application/pdf", "declaration.pdf");
        }

        [HttpGet]
        public virtual FileResult GetLabel(Guid communicationGid, Guid answerGid, string hash)
        {
            var messageXml = _messageCommunicator.GetAnswer(communicationGid, answerGid, CurrentUser.AccessToken);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(messageXml.xml);

            Eumis.Portal.Web.Models.Draft.LabelVM vm = 
                Eumis.Portal.Web.Models.Draft.LabelVM.GetVM(message.Project);
            vm.ProjectCode = hash;

            vm.RegistrationNumber = messageXml.registrationNumber;
            vm.RegistrationDate = messageXml.messageDate.Value;

            string renderedHtml = Eumis.Common.Helpers.RazorRenderHtmlHelper.RenderHtml(vm, MVC.Message.Views.Label);
            string serverPath = Server.MapPath("~");

            byte[] renderedHtmlArr = System.Text.Encoding.UTF8.GetBytes(renderedHtml);

            byte[] pdfDoc = PdfManager.ConvertHtmlToPdf(renderedHtmlArr, serverPath, new PdfManagerSettings() { MarginLeft = 20, MarginRight = 20, MarginTop = 8, MarginBottom = 0, IsLandscape = true });

            return File(pdfDoc, "application/pdf", "label.pdf");
        }


        #region Submission

        [HttpGet]
        public virtual ActionResult Submit(Guid communicationGid, Guid answerGid)
        {
            var contractMessage = _messageCommunicator.GetAnswer(communicationGid, answerGid, CurrentUser.AccessToken);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            var procedure = _procedureCommunicator.GetProcedureAppData(new Guid(message.Project.ProjectBasicData.ProcedureIdentifier));

            message = R_10020.Message.LoadReply(procedure, message);
            message.RegistrationNumber = contractMessage.registrationNumber;
            message.MessageDate = contractMessage.messageDate;

            SubmissionState.Current = new SubmissionState()
            {
                IsMessageSubmission = true,
                CurrentStep = SubmissionStateStep.Preview,
                CommunicationGid = communicationGid,
                AnswerGid = answerGid,
                Version = contractMessage.version,
                IsAcceptedDisclaimer = true,
                IsIsunFileLoaded = false,
                Document = message,
                Xml = contractMessage.xml,
                RegistrationNumber = message.RegistrationNumber
            };

            this.Validate();

            return View(SubmissionState.Current.Document);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        [ChecksSubmissionStep]
        public virtual ActionResult HowToSubmit()
        {
            this.ExctractProcedureInfo();

            var project = ((R_10020.Message)SubmissionState.Current.Document).Project;

            if (project != null)
            {
                if (project.RegistrationType == RegistrationTypeNomenclature.Digital)
                {
                    SubmissionState.Current.RegistrationType = RegistrationTypeNomenclature.Digital;
                    return RedirectToAction(ActionNames.Signature);
                }

                if (project.RegistrationType == RegistrationTypeNomenclature.Paper)
                {
                    SubmissionState.Current.RegistrationType = RegistrationTypeNomenclature.Paper;
                    return RedirectToAction(ActionNames.SaveForPaper);
                }
            }

            SubmissionState.Current.RegistrationType = RegistrationTypeNomenclature.DigitalOrPaper;

            return View();
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        [ChecksSubmissionStep]
        public virtual ActionResult SaveForPaper()
        {
            SubmissionState.Current.IsElectronicSubmission = false;

            // submit from list
            var submittedMessage = _messageCommunicator.SubmitAnswer
                (
                    SubmissionState.Current.CommunicationGid,
                    SubmissionState.Current.AnswerGid,
                    SubmissionState.Current.Version,
                    CurrentUser.AccessToken
                );

            SubmissionState.Current.Hash = submittedMessage.hash;
            SubmissionState.Current.RegistrationNumber = submittedMessage.registrationNumber;
            SubmissionState.Current.RegistrationDate = submittedMessage.messageDate.Value;

            return RedirectToAction(ActionNames.Paper);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        [ChecksSubmissionStep]
        public virtual ActionResult Paper()
        {
            return View();
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        public virtual FileResult SaveDeclaration(string hash)
        {
            Eumis.Portal.Web.Models.Draft.DeclarationVM vm =
                Eumis.Portal.Web.Models.Draft.DeclarationVM.GetVM(((R_10020.Message)SubmissionState.Current.Document).Project);
            vm.Email = CurrentUser.Email;
            vm.ProjectCode = hash;

            vm.RegistrationNumber = SubmissionState.Current.RegistrationNumber;
            vm.RegistrationDate = SubmissionState.Current.RegistrationDate;

            if (!string.IsNullOrWhiteSpace(vm.ProcedureCode) && !string.IsNullOrWhiteSpace(vm.ProjectCode))
                vm.FileName = string.Format("{0}-{1}", vm.ProcedureCode, vm.ProjectCode);

            string renderedHtml = Eumis.Common.Helpers.RazorRenderHtmlHelper.RenderHtml(vm, MVC.Message.Views.Declaration);
            string serverPath = Server.MapPath("~");

            byte[] renderedHtmlArr = System.Text.Encoding.UTF8.GetBytes(renderedHtml);

            byte[] pdfDoc = PdfManager.ConvertHtmlToPdf(renderedHtmlArr, serverPath, new PdfManagerSettings() { MarginLeft = 25, MarginRight = 25, MarginTop = 8, MarginBottom = 8, IsLandscape = false });

            return File(pdfDoc, "application/pdf", "declaration.pdf");
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        public virtual FileResult SaveLabel(string hash)
        {
            Eumis.Portal.Web.Models.Draft.LabelVM vm =
                Eumis.Portal.Web.Models.Draft.LabelVM.GetVM(((R_10020.Message)SubmissionState.Current.Document).Project);
            vm.ProjectCode = hash;

            vm.RegistrationNumber = SubmissionState.Current.RegistrationNumber;
            vm.RegistrationDate = SubmissionState.Current.RegistrationDate;

            string renderedHtml = Eumis.Common.Helpers.RazorRenderHtmlHelper.RenderHtml(vm, MVC.Message.Views.Label);
            string serverPath = Server.MapPath("~");

            byte[] renderedHtmlArr = System.Text.Encoding.UTF8.GetBytes(renderedHtml);

            byte[] pdfDoc = PdfManager.ConvertHtmlToPdf(renderedHtmlArr, serverPath, new PdfManagerSettings() { MarginLeft = 20, MarginRight = 20, MarginTop = 8, MarginBottom = 0, IsLandscape = true });

            return File(pdfDoc, "application/pdf", "label.pdf");
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        [ChecksSubmissionStep]
        public virtual ActionResult Signature()
        {
            SubmissionState.Current.IsElectronicSubmission = true;

            return View(this.ExtractCertificates());
        }

        [HttpPost]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        public virtual ActionResult Signature(FormCollection form)
        {
            if (SubmissionState.Current.signatureFiles.Count == 0)
            {
                ModelState.AddModelError("_form", "Необходимо е да подпишете Вашия отговор с електронен подпис.");

                return View(this.ExtractCertificates());
            }

            var sendResult = _messageCommunicator
                                 .SendAnswer
                                 (
                                     SubmissionState.Current.isunFile.Content,
                                     SubmissionState.Current.signatureFiles.Select(e => e.Value.Value).ToList(),
                                     SubmissionState.Current.Version,
                                     CurrentUser.AccessToken
                                 );

            SubmissionState.Current.RegistrationNumber = sendResult.registrationNumber;
            SubmissionState.Current.RegistrationDate = sendResult.replyDate;

            this.CheckForNewMessages();

            return RedirectToAction(ActionNames.Finish);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        public virtual FileResult Download()
        {

            if (SubmissionState.Current.isunFile == null)
            {
                SubmissionState.Current.isunFile = Eumis.Portal.Web.Helplers.IsunFileManager.CreateMessageFile(SubmissionState.Current.Xml,
                                                    Eumis.Common.Crypto.CryptoUtils.GetSha256XMLHash(SubmissionState.Current.Xml).Truncate(10),
                                                    SubmissionState.Current.RegistrationNumber);
            }

            return File(SubmissionState.Current.isunFile.Content, SubmissionState.Current.isunFile.MimeType, SubmissionState.Current.isunFile.Filename);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        [ChecksSubmissionStep]
        public virtual ActionResult Finish()
        {
            return View();
        }

        #endregion

        #region Private

        private void PutDraft()
        {
            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            AppContext.Current.WorkingDocument.version =
                _messageCommunicator.UpdateAnswer
                   (
                       AppContext.Current.WorkingDocument.parentGid,
                       AppContext.Current.WorkingDocument.gid,
                       AppContext.Current.Xml,
                       AppContext.Current.WorkingDocument.version,
                       CurrentUser.AccessToken
                   ).version;
        }

        private void FinalizeDraft()
        {
            _messageCommunicator.FinalizeAnswer
            (
                AppContext.Current.WorkingDocument.parentGid,
                AppContext.Current.WorkingDocument.gid,
                AppContext.Current.WorkingDocument.version,
                CurrentUser.AccessToken
            );
        }

        private void ExctractProcedureInfo()
        {
            var project = ((R_10020.Message)SubmissionState.Current.Document).Project;
            SubmissionState.Current.PrecedureCode = project.ProjectBasicData.Procedure.Code;
            SubmissionState.Current.PrecedureName = project.ProjectBasicData.Procedure.displayName;
            SubmissionState.Current.ProgrammeName = project.ProjectBasicData.ProgrammeBasicDataCollection.First().Programme.displayName;
            SubmissionState.Current.ProjectName = project.ProjectBasicData.displayName;
        }

        private void Validate()
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);
            AppContext.Current.Document = SubmissionState.Current.Document;
            AppContext.Current.Xml = SubmissionState.Current.Xml;
            AppContext.Current.ValidatedSteps.Add(AppStep.Display);

            var validationVM = new ValidationVM()
            {
                RemoteValidationErrors = new List<string>(),
                RemoteValidationWarnings = new List<string>()
            };

            base.Validate(AppStep.Display, validationVM);

            if (SubmissionState.Current.Document is ILocalValidatable)
            {
                ((ILocalValidatable)SubmissionState.Current.Document).LocalValidationErrors =
                    validationVM.LocalValidationErrors;
            }

            if (SubmissionState.Current.Document is IRemoteValidatable)
            {
                ((IRemoteValidatable)SubmissionState.Current.Document).RemoteValidationErrors =
                    validationVM.RemoteValidationErrors;

                ((IRemoteValidatable)SubmissionState.Current.Document).RemoteValidationWarnings =
                    validationVM.RemoteValidationWarnings;
            }

            SubmissionState.Current.IsProjectValid =
                validationVM.LocalValidationErrors.Count == 0
                    && validationVM.RemoteValidationErrors.Count == 0;
        }

        private List<Eumis.Portal.Web.Models.Submit.SignatureVM> ExtractCertificates()
        {
            List<Eumis.Portal.Web.Models.Submit.SignatureVM> signatures = new List<Eumis.Portal.Web.Models.Submit.SignatureVM>();
            foreach (var signature in SubmissionState.Current.signatureFiles)
            {
                var certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(signature.Value.Value);
                signatures.Add(new Eumis.Portal.Web.Models.Submit.SignatureVM()
                {
                    fileKey = signature.Key,
                    fileName = signature.Value.Key,
                    serialNumber = certificate.SerialNumber,
                    effectiveDate = certificate.GetEffectiveDateString(),
                    expirationDate = certificate.GetExpirationDateString(),
                    issuer = certificate.Issuer,
                    subject = certificate.Subject
                });
            }
            return signatures;
        }

        private void CheckForNewMessages()
        {
            try
            {
                var messages = _messageCommunicator.GetMessagesCount(CurrentUser.AccessToken);
                CurrentUser.HasNewMessages = messages.NewMessages > 0;
                CurrentUser.HasMessages = messages.AllMessages > 0;
            }
            catch
            {
                NLog.LogManager.GetCurrentClassLogger().Error("No message communication, api/registration/messages/count");
            }
        }

        #endregion
    }
}
