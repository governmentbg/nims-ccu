using Eumis.Common.Extensions;
using Eumis.Common.Helpers;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.Draft;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public partial class RegisteredController : DraftBaseController
    {
        private IMessageCommunicator _messageCommunicator;
        private PublicSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PublicSignInManager>();
            }
        }

        public RegisteredController(IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator,
            IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator, IMessageCommunicator messageCommunicator)
            : base(documentSerializer, draftCommunicator, procedureCommunicator, projectCommunicator)
        {
            _messageCommunicator = messageCommunicator;
        }

        public virtual ActionResult Switch()
        {
            if (this.CheckForNewMessages())
            {
                return RedirectToAction(MVC.Message.ActionNames.Index, MVC.Message.Name);
            }
            else
            {
                return RedirectToAction(ActionNames.Index);
            }
        }


        public virtual ActionResult Index(int page = 1)
        {
            this.CheckForNewMessages();

            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var projects = _draftCommunicator.GetRegisteredProjects(CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);
            var model = new StaticPagedList<RegProjectXmlPVO>(projects.results, page, Constants.PAGE_ITEMS_COUNT, projects.count);

            return View(model);
        }

        public virtual ActionResult Submitted(int page = 1)
        {
            this.CheckForNewMessages();

            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var projects = _draftCommunicator.GetSubmittedProjects(CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);
            var model = new StaticPagedList<RegProjectXmlPVO>(projects.results, page, Constants.PAGE_ITEMS_COUNT, projects.count);

            return View(model);
        }

        public virtual ActionResult Communication(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var projects = _draftCommunicator.GetRegisteredProjectsCommunications(CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);
            var model = new StaticPagedList<RegProjectXmlPVO>(projects.results, page, Constants.PAGE_ITEMS_COUNT, projects.count);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult PreviewVersion(Guid gid)
        {
            var xml = _draftCommunicator.GetProjectVersion(gid, CurrentUser.AccessToken).xml;

            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);
            AppContext.Current.Xml = xml;

            return RedirectToAction(MVC.Project.ActionNames.Preview, MVC.Project.Name);
        }

        [HttpGet]
        public virtual FileResult SaveDeclaration(Guid gid, string hash)
        {
            var draft = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken);
            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(draft.xml);

            DeclarationVM vm = DeclarationVM.GetVM(project);
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
        public virtual FileResult SaveLabel(Guid gid, string hash)
        {
            var draft = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken);
            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(draft.xml);

            LabelVM vm = LabelVM.GetVM(project);
            vm.ProjectCode = hash;

            string renderedHtml = RazorRenderHtmlHelper.RenderHtml(vm, MVC.Submit.Views.Label);
            string serverPath = Server.MapPath("~");

            byte[] renderedHtmlArr = System.Text.Encoding.UTF8.GetBytes(renderedHtml);

            byte[] pdfDoc = PdfManager.ConvertHtmlToPdf(renderedHtmlArr, serverPath, new PdfManagerSettings() { MarginLeft = 20, MarginRight = 20, MarginTop = 8, MarginBottom = 0, IsLandscape = true });

            return File(pdfDoc, "application/pdf", "label.pdf");
        }

        private bool CheckForNewMessages()
        {
            try
            {
                var messages = _messageCommunicator.GetMessagesCount(CurrentUser.AccessToken);

                if (!CurrentUser.HasNewMessages && messages.NewMessages > 0)
                {
                    if (SplashContext.Current != null && SplashContext.Current.State != null)
                    {
                        SplashContext.Current.State[SplashType.MessageNotification] = false;
                    }
                }

                CurrentUser.HasNewMessages = messages.NewMessages > 0;
                CurrentUser.HasMessages = messages.AllMessages > 0;

                return CurrentUser.HasNewMessages;
            }
            catch(Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(Helper.GetDetailedExceptionInfo(ex));
            }

            return false;
        }
    }
}