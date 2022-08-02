using System.Security.Cryptography.X509Certificates;
using System.Web.Http.Results;
using Eumis.Common.Extensions;
using Eumis.Common.Helpers;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.Draft;
using Eumis.Portal.Web.Models.Submit;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Enums;
using Eumis.Common.Crypto;
using Eumis.Portal.Web.Helplers;
using PagedList;
using System.Net;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public partial class SubmitController : WorkflowController<ValidationVM>
    {
        private IDraftCommunicator _draftCommunicator;
        private IProcedureCommunicator _procedureCommunicator;
        private IProjectCommunicator _projectCommunicator;

        public SubmitController(IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator,
            IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator)
            : base(documentSerializer)
        {
            _draftCommunicator = draftCommunicator;
            _procedureCommunicator = procedureCommunicator;
            _projectCommunicator = projectCommunicator;
        }

        #region Steps

        [HttpGet]
        public virtual ActionResult Disclaimer()
        {
            if (SubmissionState.Current == null)
            {
                SubmissionState.Current = new SubmissionState();
            }
            else
            {
                var oldDisclaimer = SubmissionState.Current.IsAcceptedDisclaimer;
                SubmissionState.Current = new SubmissionState();
                SubmissionState.Current.IsAcceptedDisclaimer = oldDisclaimer;
            }

            SubmissionState.Current.CurrentStep = SubmissionStateStep.Disclaimer;

            SubmissionState.Current.RegistrationType = RegistrationTypeNomenclature.DigitalOrPaper;

            return View();
        }

        [HttpPost]
        public virtual ActionResult Disclaimer(bool isAcceptedDisclaimer)
        {
            if (!isAcceptedDisclaimer)
            {
                ModelState.AddModelError("isAcceptedDisclaimer",
                    Eumis.Portal.Web.Views.Shared.App_LocalResources.Disclaimer.AcceptDisclaimer);
            }

            SubmissionState.Current.IsAcceptedDisclaimer = isAcceptedDisclaimer;

            return View();
        }

        [HttpPost]
        [RequiresSubmissionState]
        public virtual ActionResult LoadIsunFile(FormCollection form)
        {
            HttpPostedFileBase hpfb = Request.Files["applied_isun_file"];

            string xml = null;
            R_10019.Project project = null;

            if (hpfb != null && hpfb.ContentLength != 0)
            {
                try
                {
                    xml = ZipManager.UnzipProject(hpfb.InputStream);

                    project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);

                }
                catch { }
            }
            else
            {
                ModelState.AddModelError("project_file", Global.PleaseAttachProject);

                return View(Views.Disclaimer);
            }

            if (project != null)
            {
                var procedureGid = new Guid(project.ProjectBasicData.ProcedureIdentifier);

                ContractProcedure procedure = null;

                try
                {
                    procedure = _procedureCommunicator.GetProcedureAppData(procedureGid);

                    if (!procedure.isActive)
                    {
                        return View(MVC.Shared.Views.Failure, (object)Global.ProcedureEditNotActive);
                    }
                }
                catch
                {
                    string fileName = Path.GetFileName(hpfb.FileName);
                    ModelState.AddModelError("_notValidProcedure", String.Format(Global.NotValidProcedure, fileName));
                    return View(Views.Disclaimer);
                }

                _projectCommunicator.ResurrectFiles(xml);

                project = R_10019.Project.Load(procedure, project);

                project.id = Guid.NewGuid().ToString();
                project.createDate = DateTime.Now;
                project.modificationDate = DateTime.Now;

                if (project.Candidate != null)
                {
                    project.Candidate.Email = CurrentUser.Email;
                }

                SubmissionState.Current.Document = project;
                SubmissionState.Current.IsIsunFileLoaded = true;
                SubmissionState.Current.Xml = _documentSerializer.XmlSerializeToString<R_10019.Project>(project);

                return RedirectToAction(MVC.Submit.ActionNames.Preview, MVC.Submit.Name);
            }

            ModelState.AddModelError("_invalidProject", String.Format(Global.NotSameFormat, hpfb.FileName));

            return View(Views.Disclaimer);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [ChecksSubmissionStep]
        public virtual ActionResult Finalized(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var projects = _draftCommunicator.GetFinalizedProjects(CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);
            var model = new StaticPagedList<RegProjectXmlPVO>(projects.results, page, Constants.PAGE_ITEMS_COUNT, projects.count);

            return View(model);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [ChecksSubmissionStep]
        public virtual ActionResult Preview(Guid? gid)
        {
            if (gid.HasValue)
            {
                var xml = _draftCommunicator.GetDraft(gid.Value, CurrentUser.AccessToken).xml;
                var project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);

                var procedureGid = new Guid(project.ProjectBasicData.ProcedureIdentifier);
                var procedure = _procedureCommunicator.GetProcedureAppData(procedureGid);
                if (!procedure.isActive)
                {
                    return View(MVC.Shared.Views.Failure, (object)Global.ProcedureEditNotActive);
                }

                project = R_10019.Project.Load(procedure, project);

                SubmissionState.Current.ProjectGid = gid.Value;
                SubmissionState.Current.IsIsunFileLoaded = false;
                SubmissionState.Current.Document = project;
                SubmissionState.Current.Xml = xml;
            }

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

            var project = (R_10019.Project)SubmissionState.Current.Document;

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

            if (SubmissionState.Current.IsIsunFileLoaded)
            {
                // create draft
                var newDraft = _draftCommunicator.CreateDraft
                    (
                        SubmissionState.Current.Xml,
                        null,
                        CurrentUser.AccessToken
                    );

                // finalize draft
                _draftCommunicator.FinalizeDraft
                (
                    newDraft.gid.Value,
                    newDraft.version,
                    CurrentUser.AccessToken
                );

                // submit draft from file
                SubmissionState.Current.Hash = _draftCommunicator.Submit
                    (
                        newDraft.gid.Value,
                        CurrentUser.AccessToken
                    );
            }
            else
            {
                // submit from list
                SubmissionState.Current.Hash = _draftCommunicator.Submit
                    (
                        SubmissionState.Current.ProjectGid,
                        CurrentUser.AccessToken
                    );
            }

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
            DeclarationVM vm = DeclarationVM.GetVM((R_10019.Project)SubmissionState.Current.Document);
            vm.Email = CurrentUser.Email;
            vm.ProjectCode = hash;

            if (!string.IsNullOrWhiteSpace(vm.ProcedureCode) && !string.IsNullOrWhiteSpace(vm.ProjectCode))
                vm.FileName = string.Format("{0}-{1}", vm.ProcedureCode, vm.ProjectCode);

            string renderedHtml = RazorRenderHtmlHelper.RenderHtml(vm, MVC.Submit.Views.Declaration);
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
            LabelVM vm = LabelVM.GetVM((R_10019.Project)SubmissionState.Current.Document);
            vm.ProjectCode = hash;

            string renderedHtml = RazorRenderHtmlHelper.RenderHtml(vm, MVC.Submit.Views.Label);
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
                ModelState.AddModelError("_form", "Необходимо е да подпишете Вашето проектно предложение с електронен подпис.");

                return View(this.ExtractCertificates());
            }

            SubmissionState.Current.RegistrationNumber = _draftCommunicator
                        .Register
                        (
                            SubmissionState.Current.isunFile.Content,
                            SubmissionState.Current.signatureFiles.Select(e => e.Value.Value).ToList(),
                            CurrentUser.AccessToken
                        );

            return RedirectToAction(ActionNames.Finish);
        }

        [HttpGet]
        [RequiresSubmissionState]
        [IsSubmissionStateValid]
        public virtual FileResult Download()
        {

            if (SubmissionState.Current.isunFile == null)
            {
                SubmissionState.Current.isunFile = IsunFileManager.Create(SubmissionState.Current.Xml,
                CryptoUtils.GetSha256XMLHash(SubmissionState.Current.Xml).Truncate(10));
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

        #region API

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult QR(string hash, int width, int height)
        {
            ZXing.IBarcodeWriter qrCodeWriter = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                  {
                      Width = width,
                      Height = height,
                      Margin = 0
                  }
            };

            var memStream = new System.IO.MemoryStream();
            qrCodeWriter.Write(hash).Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return this.File(memStream.GetBuffer(), "image/jpg");
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ContentResult DeleteSignature(string key)
        {
            if (SubmissionState.Current != null && SubmissionState.Current.signatureFiles.ContainsKey(key))
            {
                SubmissionState.Current.signatureFiles.Remove(key);

                return Content("true");
            }

            throw new Exception("Excpetion in delete signature method.");
        }

        #endregion

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception is WebException)
            {
                var apiError = ApiErrorHandling.GetError((WebException)filterContext.Exception);

                if (apiError == ApiError.procedureInactive)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = MVC.Shared.Views.Failure,
                        ViewData = new ViewDataDictionary(Global.ProcedureNotActive)
                    };

                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                }
            }

            base.OnException(filterContext);
        }

        #region Private

        private void Validate()
        {
            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
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

        private void ExctractProcedureInfo()
        {
            var project = (R_10019.Project)SubmissionState.Current.Document;
            SubmissionState.Current.PrecedureCode = project.ProjectBasicData.Procedure.Code;
            SubmissionState.Current.PrecedureName = project.ProjectBasicData.Procedure.displayName;
            SubmissionState.Current.ProgrammeName = project.ProjectBasicData.ProgrammeBasicDataCollection.First().Programme.displayName;
            SubmissionState.Current.ProjectName = project.ProjectBasicData.displayName;
        }

        private List<SignatureVM> ExtractCertificates()
        {
            List<SignatureVM> signatures = new List<SignatureVM>();
            foreach (var signature in SubmissionState.Current.signatureFiles)
            {
                var certificate = new X509Certificate2(signature.Value.Value);
                signatures.Add(new SignatureVM()
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

        #endregion
    }
}