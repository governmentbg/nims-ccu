using ClosedXML.Excel;
using Eumis.Common.Helpers;
using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.Offers;
using Eumis.Portal.Web.Views.Shared.App_LocalResources;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    [Authenticated]
    public partial class OffersController : BaseController
    {
        private IOffersCommunicator _offersCommunicator;

        public OffersController(IOffersCommunicator offersCommunicator)
        {
            _offersCommunicator = offersCommunicator;
        }

        [AllowAnonymous]
        public virtual ActionResult Index(string dpName = "", string name = "", string companyName = "", DateTime? offersDeadlineDate = null, DateTime? noticeDate = null, int page = 1, string sortBy = "OffersDeadlineDate", SortOrder sortOrder = SortOrder.Descending)
        {
            int offset = (page - 1) * Constants.PAGE_OFFERS_COUNT;
            var positions = _offersCommunicator.GetAnnouncedContractDifferentiatedPositions(
                CurrentUser.AccessToken,
                dpName,
                name,
                companyName,
                offersDeadlineDate,
                noticeDate,
                Constants.PAGE_OFFERS_COUNT,
                offset,
                sortBy,
                sortOrder);

            IndexVM model = new IndexVM()
            {
                DpName = dpName,
                Name = name,
                CompanyName = companyName,
                OffersDeadlineDate = offersDeadlineDate,
                NoticeDate = noticeDate,
                SearchItems = new StaticPagedList<Eumis.Documents.Contracts.ContractDifferentiatedPositionPVO>(positions.results, page, Constants.PAGE_OFFERS_COUNT, positions.count)
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult IndexExport(string dpName = "", string name = "", string companyName = "", DateTime? offersDeadlineDate = null, DateTime? noticeDate = null)
        {
            var positions = _offersCommunicator.GetAnnouncedContractDifferentiatedPositions(
                CurrentUser.AccessToken,
                dpName,
                name,
                companyName,
                offersDeadlineDate,
                noticeDate
                );

            using (var workbook = this.GetAnnouncedWorkbook(positions.results))
            {
                var ms = new MemoryStream();
            
                workbook.SaveAs(ms);

                return DownloadXlsxFile(ms, "Announced");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Details(Guid id)
        {
            var position = _offersCommunicator.GetContractDifferentiatedPosition(id, CurrentUser.AccessToken);

            return View(position);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult DetailsDownload(Guid id, Guid fileKey)
        {
            var contractDifferentiatedPosition = _offersCommunicator.GetContractDifferentiatedPosition(id, CurrentUser.AccessToken);

            if (!contractDifferentiatedPosition.PublicDocuments.Where(d => d.Key == fileKey).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(fileKey);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult AddtitionalDocumentDownload(Guid id, Guid fileKey)
        {
            var contractDifferentiatedPosition = _offersCommunicator.GetContractDifferentiatedPosition(id, CurrentUser.AccessToken);

            if (!contractDifferentiatedPosition.AdditionalDocuments.Where(d => d.Key == fileKey).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(fileKey);
        }

        public virtual ActionResult Submitted(string dpName = "", string name = "", string companyName = "", DateTime? offerSubmitDate = null, int page = 1, string sortBy = null, SortOrder? sortOrder = null)
        {
            int offset = (page - 1) * Constants.PAGE_OFFERS_COUNT;
            var offers = _offersCommunicator.GetRegistrationSubmittedOffers(CurrentUser.AccessToken, dpName, name, companyName, offerSubmitDate, offset, Constants.PAGE_OFFERS_COUNT, sortBy, sortOrder);

            SubmittedVM model = new SubmittedVM()
            {
                DpName = dpName,
                Name = name,
                CompanyName = companyName,
                OfferSubmitDate = offerSubmitDate,
                SearchItems = new StaticPagedList<Eumis.Documents.Contracts.RegOfferXmlPVO>(offers.results, page, Constants.PAGE_OFFERS_COUNT, offers.count)
            };

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult SubmittedExport(string dpName = "", string name = "", string companyName = "", DateTime? offerSubmitDate = null)
        {
            var offers = _offersCommunicator.GetRegistrationSubmittedOffers(CurrentUser.AccessToken, dpName, name, companyName, offerSubmitDate);

            using (var workbook = this.GetSubmittedWorkbook(offers.results))
            {
                var ms = new MemoryStream();

                workbook.SaveAs(ms);

                return DownloadXlsxFile(ms, "Submitted");
            }
        }

        [HttpGet]
        public virtual ActionResult Drafts(string dpName, string name, string companyName, int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_OFFERS_COUNT;
            var offers = _offersCommunicator.GetRegistrationDraftOffers(CurrentUser.AccessToken, dpName, name, companyName, offset, Constants.PAGE_OFFERS_COUNT);

            var model = new DraftsVM()
            {
                DpName = dpName,
                Name = name,
                CompanyName = companyName,
                SearchItems = new StaticPagedList<Eumis.Documents.Contracts.RegOfferXmlPVO>(offers.results, page, Constants.PAGE_OFFERS_COUNT, offers.count)
            };

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult SubmittedDetails(Guid id)
        {
            var offer = _offersCommunicator.GetRegistrationOfferInfo(id, CurrentUser.AccessToken);

            return View(offer);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Archived(string dpName = "", string name = "", string companyName = "", int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_OFFERS_COUNT;
            var positions = _offersCommunicator.GetArchivedContractDifferentiatedPositions(CurrentUser.AccessToken, dpName, name, companyName, Constants.PAGE_OFFERS_COUNT, offset);

            IndexVM model = new IndexVM()
            {
                DpName = dpName,
                Name = name,
                CompanyName = companyName,
                SearchItems = new StaticPagedList<Eumis.Documents.Contracts.ContractDifferentiatedPositionPVO>(positions.results, page, Constants.PAGE_OFFERS_COUNT, positions.count)
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ArchivedExport(string dpName = "", string name = "", string companyName = "")
        {            
            var positions = _offersCommunicator.GetArchivedContractDifferentiatedPositions(CurrentUser.AccessToken, dpName, name, companyName);

            using (var workbook = this.GetArchivedWorkbook(positions.results))
            {
                var ms = new MemoryStream();

                workbook.SaveAs(ms);

                return DownloadXlsxFile(ms, "Archived");
            }
        }

        public virtual ActionResult WithdrawOffer(Guid id, string version)
        {
            var ver = Convert.FromBase64String(version);

            this._offersCommunicator.WithdrawRegistrationOffer(id, ver, CurrentUser.AccessToken);

            return RedirectToAction(MVC.Offers.Submitted());
        }

        private XLWorkbook GetAnnouncedWorkbook(List<ContractDifferentiatedPositionPVO> items)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(Offers.AnnouncementsOffers);

            ws.Cell("A1").Value = Offers.OffersDeadlineDate;
            ws.Cell("B1").Value = Offers.NoticeDate;
            ws.Cell("C1").Value = Offers.DifferentiatedPosition;
            ws.Cell("D1").Value = Offers.ProcurementSubject;
            ws.Cell("E1").Value = Offers.Beneficiary;

            var rngHeaders = ws.Range("A1", "E1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Alignment.WrapText = true;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var rowIndex = 2;
            foreach (var item in items)
            {
                ws.Cell(rowIndex, "A").Value = item.OffersDeadlineDate;
                ws.Cell(rowIndex, "B").Value = item.NoticeDate;
                ws.Cell(rowIndex, "C").Value = item.DpName;
                ws.Cell(rowIndex, "D").Value = item.Name;
                ws.Cell(rowIndex, "E").Value = item.CompanyName;

                rowIndex++;
            }

            ws.Column("A").Width = 20;
            ws.Column("B").Width = 20;
            ws.Column("C").Width = 40;
            ws.Column("D").Width = 40;
            ws.Column("E").Width = 40;

            return wb;
        }

        private XLWorkbook GetSubmittedWorkbook(List<RegOfferXmlPVO> items)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(Offers.SubmittedOffers);

            ws.Cell("A1").Value = Offers.SubmissionDate;
            ws.Cell("B1").Value = Offers.DifferentiatedPosition;
            ws.Cell("C1").Value = Offers.ProcurementSubject;
            ws.Cell("D1").Value = Offers.Beneficiary;

            var rngHeaders = ws.Range("A1", "D1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Alignment.WrapText = true;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var rowIndex = 2;
            foreach (var item in items)
            {
                ws.Cell(rowIndex, "A").Value = item.OfferSubmitDate;
                ws.Cell(rowIndex, "B").Value = item.DpName;
                ws.Cell(rowIndex, "C").Value = item.Name;
                ws.Cell(rowIndex, "D").Value = item.CompanyName;

                rowIndex++;
            }

            ws.Column("A").Width = 20;
            ws.Column("B").Width = 20;
            ws.Column("C").Width = 40;
            ws.Column("D").Width = 40;

            return wb;
        }

        private XLWorkbook GetArchivedWorkbook(List<ContractDifferentiatedPositionPVO> items)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(Offers.Archive);

            ws.Cell("A1").Value = Offers.OffersDeadlineDate;
            ws.Cell("B1").Value = Offers.DifferentiatedPosition;
            ws.Cell("C1").Value = Offers.ProcurementSubject;
            ws.Cell("D1").Value = Offers.Beneficiary;

            var rngHeaders = ws.Range("A1", "D1");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Alignment.WrapText = true;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Border.BottomBorder = XLBorderStyleValues.Double;

            var rowIndex = 2;
            foreach (var item in items)
            {
                ws.Cell(rowIndex, "A").Value = item.OffersDeadlineDate;
                ws.Cell(rowIndex, "B").Value = item.DpName;
                ws.Cell(rowIndex, "C").Value = item.Name;
                ws.Cell(rowIndex, "D").Value = item.CompanyName;

                rowIndex++;
            }

            ws.Column("A").Width = 20;
            ws.Column("B").Width = 40;
            ws.Column("C").Width = 40;
            ws.Column("D").Width = 40;

            return wb;
        }
    }
}
