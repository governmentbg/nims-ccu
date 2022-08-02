using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Areas.Report.Models.Communication;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.Communication;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class CommunicationController : WorkflowController<EditVM>
    {
        private ICommunicationCommunicator _communicationCommunicator;

        public CommunicationController(ICommunicationCommunicator communicationCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _communicationCommunicator = communicationCommunicator;
        }

        public virtual ActionResult Index(R_09987.CommunicationTypeNomenclature type, int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var comms = _communicationCommunicator.GetCommunications(
                    new Guid(RouteData.Values["cgid"].ToString()), type.ToString(), CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);

            IndexVM model = new IndexVM();
            model.type = type;
            model.Communications = new StaticPagedList<ContractCommunicationInfo>(comms.results, page, Constants.PAGE_ITEMS_COUNT, comms.count);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult New(R_09987.CommunicationTypeNomenclature type)
        {
            AppContext.Current = new AppContext(DocumentMetadata.CommunicationMetadata.Code);

            R_10042.Communication communication = null;

            communication = R_10042.Communication.Init(communication, type);

            AppContext.Current.Document = communication;

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid, R_09987.CommunicationTypeNomenclature type)
        {
            var communication = _communicationCommunicator.GetCommunication(new Guid(this.RouteData.Values["cgid"].ToString()), gid, CurrentUser.AccessToken);

            _communicationCommunicator.DeleteCommunication(
                new Guid(this.RouteData.Values["cgid"].ToString()),
                gid,
                CurrentUser.AccessToken,
                string.Empty,
                communication.version);

            TempData["SuccessAction"] = "Кореспонденцията беше изтрита успешно.";

            return RedirectToAction(ActionNames.Index, new { type = type });
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutCommunication();

            return base.Prepare(vm);
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid gid)
        {
            this.InitAppContext(gid, CurrentUser.AccessToken);

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid)
        {
            return View(this.InitAppContext(gid, CurrentUser.AccessToken));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            var type = ((R_10042.Communication)AppContext.Current.Document).type;

            PutCommunication();
            _communicationCommunicator.SubmitCommunication
                (
                    new Guid(this.RouteData.Values["cgid"].ToString()),
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            TempData["SuccessAction"] = "Кореспонденцията беше изпратена успешно.";

            return RedirectToAction(ActionNames.Index, new { type = type });
        }

        [HttpPost]
        public override void SaveDraft()
        {
            var modifyDate = PutCommunication();

            if (!modifyDate.HasValue)
            {
                base.SaveDraft();
            }
            else
            {
                AppContext.Current.LastSaveDate = modifyDate;
            }
        }

        private DateTime? PutCommunication()
        {
            byte[] version;
            DateTime? modifyDate = null;

            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            if (AppContext.Current.WorkingDocument.gid == null
                || AppContext.Current.WorkingDocument.gid == Guid.Empty)
            {
                var newDraft = _communicationCommunicator.CreateCommunication
                (
                    new Guid(this.RouteData.Values["cgid"].ToString()),
                    ((R_10042.Communication)AppContext.Current.Document).type.ToString(),
                    CurrentUser.AccessToken,
                    AppContext.Current.Xml,
                    null
                );

                version = newDraft.version;
                modifyDate = newDraft.modifyDate;
                AppContext.Current.WorkingDocument.gid = newDraft.gid.Value;
            }
            else
            {
                var updateDraft = _communicationCommunicator.PutCommunication
                (
                    new Guid(this.RouteData.Values["cgid"].ToString()),
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                );

                version = updateDraft.version;
                modifyDate = updateDraft.modifyDate;
            }

            AppContext.Current.WorkingDocument.version = version;
            return modifyDate;
        }

        private R_10042.Communication InitAppContext(Guid gid, string access_token)
        {
            AppContext.Current = new AppContext(DocumentMetadata.CommunicationMetadata.Code);

            var contractCommunication = _communicationCommunicator.GetCommunication(new Guid(this.RouteData.Values["cgid"].ToString()), gid, access_token);

            var communication = _documentSerializer.XmlDeserializeFromString<R_10042.Communication>(contractCommunication.xml);

            AppContext.Current.Document = communication;
            AppContext.Current.Xml = contractCommunication.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractCommunication.version
            };

            return communication;
        }
    }
}