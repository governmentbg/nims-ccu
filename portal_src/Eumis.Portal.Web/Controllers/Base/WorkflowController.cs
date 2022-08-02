using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Eumis.Common.Localization;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using System.Security.Claims;
using System;
using Eumis.Components;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Documents.Interfaces;
using Eumis.Documents.Validation;
using Eumis.Common.Validation;
using Eumis.Components.ValidationEngine;
using System.Text.RegularExpressions;
using R_09990;
using Eumis.Portal.Web.Views.Shared.App_LocalResources;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;
using Eumis.Documents.Enums;

namespace Eumis.Portal.Web.Controllers.Base
{
    [Authenticated]
    public abstract partial class WorkflowController<TModel> : BaseController
    {
        public IDocumentSerializer _documentSerializer;

        public WorkflowController() { }
        public WorkflowController(IDocumentSerializer documentSerializer)
            : base()
        {
            _documentSerializer = documentSerializer;
        }

        #region Public

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Prepare()
        {
            TModel vm = (TModel)Activator.CreateInstance(typeof(TModel), AppContext.Current.Document);

            this.Validate(AppStep.Prepare, vm);

            return View(vm);
        }

        [HttpPost]
        [RequiresAppContext]
        public virtual ActionResult Prepare(TModel model)
        {
            UpdateAppContext(model);

            AppContext.Current.ValidatedSteps.Add(AppStep.Prepare);

            switch ((AppStep)typeof(TModel).GetProperty("Step", typeof(AppStep)).GetValue(model))
            {
                case (AppStep.Prepare): { return RedirectToAction("Prepare"); }
                default: { return RedirectToAction("Display"); }
            }
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Display()
        {
            AppContext.Current.ValidatedSteps.Add(AppStep.Display);

            var validationVM = new ValidationVM()
            {
                RemoteValidationErrors = new List<string>(),
                RemoteValidationWarnings = new List<string>()
            };

            this.Validate(AppStep.Display, validationVM);

            int localErrors = 0; int remoteErros = 0;
            if (AppContext.Current.Document is ILocalValidatable)
            {
                ((ILocalValidatable)AppContext.Current.Document).LocalValidationErrors =
                    validationVM.LocalValidationErrors;
                localErrors = validationVM.LocalValidationErrors.Count;
            }

            if (AppContext.Current.Document is IRemoteValidatable)
            {
                ((IRemoteValidatable)AppContext.Current.Document).RemoteValidationErrors =
                    validationVM.RemoteValidationErrors;
                remoteErros = validationVM.RemoteValidationErrors.Count;

                ((IRemoteValidatable)AppContext.Current.Document).RemoteValidationWarnings =
                    validationVM.RemoteValidationWarnings;
            }

            if (localErrors > 0 || remoteErros > 0)
            {
                return RedirectToAction("Prepare");
            }

            return View(AppContext.Current.Document);
        }

        [HttpPost]
        [RequiresAppContext]
        public virtual ContentResult Save(TModel model)
        {
            UpdateAppContext(model);

            return Content("success");
        }

        [HttpPost]
        public virtual void SaveDraft()
        {
            AppContext.Current.LastSaveDate = DateTime.Now;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Print()
        {
            return View("Print", MVC.Shared.Views._PrintLayout, AppContext.Current.Document);
        }

        #endregion

        #region Protected

        [RequiresAppContext]
        protected void Validate(AppStep step, object model)
        {
            if (AppContext.Current.ValidatedSteps.Contains(step))
            {
                if (model is ILocalValidatable)
                {
                    TryLocalValidateModel((ILocalValidatable)model);
                }

                if (model is IRemoteValidatable)
                {
                    TryRemoteValidateModel((IRemoteValidatable)model);
                }
            }
        }

        #endregion

        #region Private

        [RequiresAppContext]
        private void UpdateAppContext(TModel model)
        {
            var document = typeof(TModel).GetMethod("Set")
                .Invoke(model, new object[] { AppContext.Current.Document });

            if (document is IEumisDocument)
            {
                ((IEumisDocument)document).ModificationDate = DateTime.Now;
            }

            AppContext.Current.Document = document;

            AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
        }

        [RequiresAppContext]
        private void TryLocalValidateModel(ILocalValidatable model)
        {
            model.LocalValidationErrors = RunValidationEngine(model);
            foreach (var localError in model.LocalValidationErrors)
            {
                if (!localError.IsAngularValidation)
                {
                    ModelState.AddModelError(localError.MemberName, localError.ErrorSimpleMessage);
                }
            }
        }

        [RequiresAppContext]
        private void TryRemoteValidateModel(IRemoteValidatable model)
        {
            model.RemoteValidationErrors = new List<string>();
            model.RemoteValidationWarnings = new List<string>();

            try
            {
                var xml = string.Empty;

                if (AppContext.Current.Code.Equals(Eumis.Documents.DocumentMetadata.ProjectMetadata.Code))
                {
                    xml = AppContext.Current.Xml;
                    var p = (R_10019.Project)AppContext.Current.Document;
                    if (!p.IsApplicationSectionSelected(ApplicationSectionType.Budget))
                    {
                        return;
                    }
                        
                }
                else if (AppContext.Current.Code.Equals(Eumis.Documents.DocumentMetadata.MessageMetadata.Code))
                {
                    if (((R_10020.Message)AppContext.Current.Document).type == MessageTypeNomenclature.Question)
                        return;

                    if (((R_10020.Message)AppContext.Current.Document).IsManagingAuthority == true)
                        return;

                    var p = ((R_10020.Message)AppContext.Current.Document).Project;
                    if (!p.IsApplicationSectionSelected(ApplicationSectionType.Budget))
                    {
                        return;
                    }

                    xml = _documentSerializer.XmlSerializeToString(((R_10020.Message) AppContext.Current.Document).Project);
                }
                else if (AppContext.Current.Code.Equals(Eumis.Documents.DocumentMetadata.ProcurementsMetadata.Code))
                {
                    var procurements = (R_10041.Procurements)AppContext.Current.Document;
                    model.RemoteValidationWarnings = procurements.GetValidationWarnings();
                    return;
                }
                else
                {
                    return;
                }

                var remoteErrors = DependencyResolver.Current.GetService<IProjectCommunicator>()
                    .ValidateDraft(xml, CurrentUser.AccessToken);

                model.RemoteValidationErrors =
                    remoteErrors.Where(e => e.isRequired).Select(e => e.displayError).ToList();

                model.RemoteValidationWarnings =
                    remoteErrors.Where(e => !e.isRequired).Select(e => e.error).ToList();
            }
            catch (Exception ex)
            {
                model.RemoteValidationErrors.Add(Project.BudgetValidateException);
                NLog.LogManager.GetCurrentClassLogger().Error(ex);
            }
        }

        private List<ModelValidationResultExtended> RunValidationEngine(object model)
        {
            List<ModelValidationResultExtended> result = new List<ModelValidationResultExtended>();

            if (AppContext.Current != null && model is IEngineValidatable)
            {
                IValidationEngine validationEngine = DependencyResolver.Current.GetService<IValidationEngine>();

                IEnumerable<ValidatableObject> validatableObjects = ((IEngineValidatable)model).GetValidatableObjects();

                foreach (ValidatableObject vo in validatableObjects)
                {
                    if (vo.Object == null)
                        continue;

                    IDictionary<string, IEnumerable<ValidationOption>> errors = validationEngine.Validate(AppContext.Current.Code, AppContext.Current.Document, vo.Object, string.Empty);

                    foreach (var error in errors)
                    {
                        string modelPath = error.Key;

                        if (vo.ErrorFilters != null)
                        {
                            string matchedModelPath = null;
                            foreach (var filter in vo.ErrorFilters)
                            {
                                Match match = Regex.Match(modelPath, filter.FilterRegex, RegexOptions.Singleline);

                                if (match.Success)
                                {
                                    if (filter.PathFormat != null)
                                    {
                                        object[] groupValues = match.Groups.Cast<Group>().Skip(1).Select(g => (object)g.Value).ToArray();

                                        if (filter.SplitEnd)
                                        {
                                            for (int i = 0; i < groupValues.Count(); ++i)
                                            {
                                                if (!string.IsNullOrEmpty(groupValues[i].ToString()) && groupValues[0].ToString().Contains("."))
                                                    groupValues[i] = groupValues[i].ToString().Substring(0, groupValues[i].ToString().LastIndexOf("."));
                                            }
                                        }

                                        matchedModelPath = string.Format(filter.PathFormat, groupValues);
                                    }
                                    else
                                    {
                                        matchedModelPath = modelPath;
                                    }

                                    //found the first match, break
                                    break;
                                }
                            }

                            if (matchedModelPath != null)
                            {
                                modelPath = matchedModelPath;
                            }
                            else
                            {
                                //no matches found, skip the error
                                continue;
                            }
                        }

                        foreach (var errorValue in error.Value)
                        {
                            result.Add(new ModelValidationResultExtended()
                                {
                                    MemberName = modelPath,
                                    Message = errorValue.ErrorComplexMessage,
                                    ErrorSimpleMessage = errorValue.ErrorSimpleMessage,
                                    ErrorComplexMessage = errorValue.ErrorComplexMessage,
                                    ErrorRowIdentifier = errorValue.ErrorRowIdentifier,
                                    IsStopError = errorValue.IsStopError,
                                    IsAngularValidation = errorValue.IsAngularValidation
                                });
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}