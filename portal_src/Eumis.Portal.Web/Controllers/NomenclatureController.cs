using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Eumis.Components.Web;
using System.IO;
using Eumis.Documents.Mappers;
using Eumis.Portal.Model.Repositories;
using Eumis.Common.Linq;
using Eumis.Portal.Model.Entities;
using Eumis.Documents.Enums;
using Eumis.Portal.Web.Helpers;
using Eumis.Common;
using Eumis.Portal.Web.Controllers.Base;
using System.Web.Mvc;
using Eumis.Common.Localization;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    class Select2ListItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string NameEN { get; set; }
        public string parentId { get; set; }
        public bool? isRequired { get; set; }
        public bool? isSignatureRequired { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    class Select2ExtendedItem : Select2ListItem
    {
        public string FullPathName { get; set; }
        public string FullPathNameEN { get; set; }
        public string displayFullPathName { get; set; }
        public string FullPath { get; set; }
    }

    public partial class NomenclatureController : BaseController
    {
        #region AppContext Nomenclatures

        [HttpGet]
        public virtual JsonResult GetInterventionFields(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.InterventionField);
        }

        [HttpGet]
        public virtual JsonResult GetFormOfFinances(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.FormOfFinance);
        }

        [HttpGet]
        public virtual JsonResult GetEconomicDimensions(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.EconomicDimension);
        }

        [HttpGet]
        public virtual JsonResult GetESFSecondaryThemes(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.ESFSecondaryTheme);
        }
        [HttpGet]
        public virtual JsonResult GetTerritorialDeliveryMechanisms(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.TerritorialDeliveryMechanism);
        }

        [HttpGet]
        public virtual JsonResult GetTerritorialDimensions(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.TerritorialDimension);
        }

        [HttpGet]
        public virtual JsonResult GetThematicObjectives(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContextWithValue(term, offset, limit, NomenclatureType.ThematicObjective);
        }

        [HttpGet]
        public virtual JsonResult GetAttachedDocumentTypes(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.AttachedDocumentType);
        }

        [HttpGet]
        public virtual JsonResult GetContractReportDocumentTypes(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.ContractReportDocumentType);
        }

        [HttpGet]
        public virtual JsonResult GetProgrammePriorities(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.ProgrammePriority);
        }

        [HttpGet]
        public virtual JsonResult GetInvestmentPriorities(string parentId = null, string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.InvestmentPriority, parentId);
        }

        [HttpGet]
        public virtual JsonResult GetFinanceSources(string parentId = null, string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.FinanceSource, parentId);
        }

        [HttpGet]
        public virtual JsonResult GetSpecificTargets(string parentId = null, string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.SpecificTarget, parentId);
        }

        [HttpGet]
        public virtual JsonResult GetDummies(string term = null, int offset = 0, int? limit = null)
        {
            return GetNomenclatureFromAppContext(term, offset, limit, NomenclatureType.Dummy);
        }

        private JsonResult GetNomenclatureFromAppContext(
            string term,
            int offset,
            int? limit,
            NomenclatureType nomenclatureType)
        {
            if (AppContext.Current == null || !AppContext.Current.IsNomenclatureLoaded(nomenclatureType))
            {
                return null;
            }

            var nomenclature = AppContext.Current.Nomenclature(nomenclatureType)
                    .Select(e => new Select2ListItem()
                    {
                        id = e.Value,
                        text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                        Code = e.Value,
                        Name = e.Name,
                        Value = e.Value,
                        Description = e.Name,
                        NameEN = e.NameAlt,
                        isRequired = e.IsRequired,
                        isSignatureRequired = e.IsSignatureRequired

                    }).AsQueryable();

            var predicate = PredicateBuilder.True<Select2ListItem>()
                .AndStringContains(i => i.Name.ToUpper(), term == null ? term : term.ToUpper());

            nomenclature = nomenclature.Where(predicate);

            var result = nomenclature
                            .WithOffsetAndLimit(offset, limit)
                            .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private JsonResult GetNomenclatureFromAppContext(
            string term,
            int offset,
            int? limit,
            NomenclatureType nomenclatureType,
            string parentId)
        {
            if (AppContext.Current == null || !AppContext.Current.IsNomenclatureLoaded(nomenclatureType) || String.IsNullOrWhiteSpace(parentId))
            {
                return null;
            }

            var nomenclature = AppContext.Current.Nomenclature(nomenclatureType)
                    .Select(e => new Select2ListItem()
                    {
                        id = e.Value,
                        text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                        Code = e.Value,
                        Name = e.Name,
                        Value = e.Value,
                        Description = e.Name,
                        parentId = e.ParentId,
                        NameEN = e.NameAlt,
                        isRequired = e.IsRequired,
                        isSignatureRequired = e.IsSignatureRequired

                    }).AsQueryable();

            var predicate = PredicateBuilder.True<Select2ListItem>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameEN.ToUpper(), term == null ? term : term.ToUpper());

            predicate = predicate.And(e => e.parentId != null && e.parentId.Equals(parentId));

            nomenclature = nomenclature.Where(predicate);

            var result = nomenclature
                            .WithOffsetAndLimit(offset, limit)
                            .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private JsonResult GetNomenclatureFromAppContextWithValue(
            string term,
            int offset,
            int? limit,
            NomenclatureType nomenclatureType)
        {
            if (AppContext.Current == null || !AppContext.Current.IsNomenclatureLoaded(nomenclatureType))
            {
                return null;
            }

            var nomenclature = AppContext.Current.Nomenclature(nomenclatureType)
                    .Select(e => new Select2ListItem()
                    {
                        id = e.Value,
                        Code = e.Value,
                        Name = String.Format("{0} {1}", e.Value, e.Name),
                        NameEN = String.Format("{0} {1}", e.Value, e.NameAlt),
                        text = SystemLocalization.GetDisplayName(String.Format("{0} {1}", e.Value, e.Name), String.Format("{0} {1}", e.Value, e.NameAlt)),
                        isRequired = e.IsRequired,
                        isSignatureRequired = e.IsSignatureRequired

                    }).AsQueryable();

            var predicate = PredicateBuilder.True<Select2ListItem>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameEN.ToUpper(), term == null ? term : term.ToUpper());

            nomenclature = nomenclature.Where(predicate);

            var result = nomenclature
                            .WithOffsetAndLimit(offset, limit)
                            .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectCommunicationBeneficiarySubjects(string term = null, int offset = 0, int? limit = null)
        {
            ProjectCommunicationSubjectNomenclature container = new ProjectCommunicationSubjectNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetBeneficaryItems());
        }

        [HttpGet]
        public virtual JsonResult GetProjectCommunicationManagingAuthoritySubjects(string term = null, int offset = 0, int? limit = null)
        {
            ProjectCommunicationSubjectNomenclature container = new ProjectCommunicationSubjectNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetManagingAuthorityItems());
        }

        #endregion

        #region Enum Nomenclatures

        [HttpGet]
        public virtual JsonResult GetAmountTypes(string term = null, int offset = 0, int? limit = null)
        {
            AmountTypeNomenclature container = new AmountTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetUinTypes(string term = null, int offset = 0, int? limit = null)
        {
            UinTypeNomenclature container = new UinTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetTechnicalReportTeamUinTypes(string term = null, int offset = 0, int? limit = null)
        {
            UinTypeNomenclature container = new UinTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetTechnicalReportTeamItems());
        }

        [HttpGet]
        public virtual JsonResult GetErrandTypes(string parentId = null, string term = null, int offset = 0, int? limit = null)
        {
            if (String.IsNullOrWhiteSpace(parentId))
            {
                return null;
            }

            ErrandTypeNomenclature container = new ErrandTypeNomenclature();

            var nomenclature = container.GetItems(parentId)
                    .Select(e => new Select2ListItem()
                    {
                        id = e.Code,
                        Code = e.Code,
                        text = SystemLocalization.GetDisplayName(e.Name, e.NameEN),
                        Name = e.Name,
                        NameEN = e.NameEN,
                        parentId = e.ParentCode,
                    }).AsQueryable();

            var predicate = PredicateBuilder.True<Select2ListItem>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameEN.ToUpper(), term == null ? term : term.ToUpper());

            nomenclature = nomenclature.Where(predicate);

            var result = nomenclature
                            .WithOffsetAndLimit(offset, limit)
                            .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetErrandAreas(string term = null, int offset = 0, int? limit = null)
        {
            ErrandAreaNomenclature container = new ErrandAreaNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetErrandLegalActs(string term = null, int offset = 0, int? limit = null)
        {
            ErrandLegalActNomenclature container = new ErrandLegalActNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetCompletionStatuses(string term = null, int offset = 0, int? limit = null)
        {
            CompletionStatusNomenclature container = new CompletionStatusNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetCommitmentTypes(string term = null, int offset = 0, int? limit = null)
        {
            CommitmentTypeNomenclature container = new CommitmentTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetAuditInstitutions(string term = null, int offset = 0, int? limit = null)
        {
            AuditInstitutionNomenclature container = new AuditInstitutionNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetAuditTypes(string term = null, int offset = 0, int? limit = null)
        {
            AuditTypeNomenclature container = new AuditTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetAuditKinds(string term = null, int offset = 0, int? limit = null)
        {
            AuditKindNomenclature container = new AuditKindNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetAuditeeTypes(string term = null, int offset = 0, int? limit = null)
        {
            AuditeeTypeNomenclature container = new AuditeeTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetActivityStatuses(string term = null, int offset = 0, int? limit = null)
        {
            ActivityStatusNomenclature container = new ActivityStatusNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetCostSupprotingDocumentTypes(string term = null, int offset = 0, int? limit = null)
        {
            CostSupportingDocumentTypeNomenclature container = new CostSupportingDocumentTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetIncomeTypes(string term = null, int offset = 0, int? limit = null)
        {
            IncomeTypeNomenclature container = new IncomeTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }


        [HttpGet]
        public virtual JsonResult GetProductKinds(string term = null, int offset = 0, int? limit = null)
        {
            ProductKindNomenclature container = new ProductKindNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetItems());
        }

        [HttpGet]
        public virtual JsonResult GetAdvancePaymentTypes(string term = null, int offset = 0, int? limit = null)
        {
            PaymentRequestTypeNomenclature container = new PaymentRequestTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetAdvanceItems());
        }

        [HttpGet]
        public virtual JsonResult GetFinalPaymentTypes(string term = null, int offset = 0, int? limit = null)
        {
            PaymentRequestTypeNomenclature container = new PaymentRequestTypeNomenclature();

            return GetNomenclatureFromEnums(term, offset, limit, container.GetFinalItems());
        }


        private JsonResult GetNomenclatureFromEnums(string term = null, int offset = 0, int? limit = null, IEnumerable<LocalizedSelectListItem> container = null)
        {
            var nomenclature = container
                    .Select(e => new Select2ListItem()
                    {
                        id = e.Value,
                        Code = e.Value,
                        text = SystemLocalization.GetDisplayName(e.Name, e.NameEN),
                        Name = e.Name,
                        Value = e.Value,
                        Description = e.Name,
                        NameEN = e.NameEN
                    }).AsQueryable();

            var predicate = PredicateBuilder.True<Select2ListItem>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameEN.ToUpper(), term == null ? term : term.ToUpper());

            nomenclature = nomenclature.Where(predicate);

            var result = nomenclature
                            .WithOffsetAndLimit(offset, limit)
                            .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DB Nomenclatures

        [HttpGet]
        public virtual JsonResult GetKidCodes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<KidCode>()
                .AndAnyStringContains(i => string.Format("{0} {1}", i.Code.ToUpper(), i.Name.ToUpper()), i => string.Format("{0} {1}", i.Code.ToUpper(), i.NameAlt.ToUpper()), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllKidCodes().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ListItem
                {
                    id = e.Code,
                    text = string.Format("{0} {1}", e.Code, SystemLocalization.GetDisplayName(e.Name, e.NameAlt)),
                    Code = e.Code,
                    Name = string.Format("{0} {1}", e.Code, e.Name),
                    NameEN = string.Format("{0} {1}", e.Code, e.NameAlt)
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetCompanyTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<CompanyType>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllCompanyTypes().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ListItem
                {
                    id = e.Gid.ToString(),
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Name = e.Name,
                    NameEN = e.NameAlt
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetCompanyLegalTypes(string parentId = null, string term = null, int offset = 0, int? limit = null)
        {
            if (String.IsNullOrWhiteSpace(parentId))
            {
                return null;
            }

            var predicate = PredicateBuilder.True<CompanyLegalType>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            predicate = predicate.And(e => e.CompanyType.Gid.ToString().ToUpper().Equals(parentId.ToUpper()));

            var nomenclature = DataCache.GetAllCompanyLegalTypes().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ListItem
                {
                    id = e.Gid.ToString(),
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    parentId = e.CompanyType.Gid.ToString(),
                    Name = e.Name,
                    NameEN = e.NameAlt
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetCompanySizeTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<CompanySizeType>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllCompanySizeTypes().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ListItem
                {
                    id = e.Gid.ToString(),
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Name = e.Name,
                    NameEN = e.NameAlt
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetCountries(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Country>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllCountries().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.NutsCode,
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Code = e.NutsCode,
                    Name = e.Name,
                    NameEN = e.NameAlt,
                    FullPathName = e.Name,
                    FullPathNameEN = e.NameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    FullPath = e.NutsCode
                })
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProtectedZones(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ProtectedZone>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllProtectedZones().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.NutsCode,
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Code = e.NutsCode,
                    Name = e.Name,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetNuts1s(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Nuts1s>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllNuts1s().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.NutsCode.ToString(),
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Code = e.NutsCode,
                    Name = e.Name,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetNuts2s(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Nuts2s>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllNuts2s().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.NutsCode,
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Code = e.NutsCode,
                    Name = e.Name,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetDistricts(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<District>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllDistricts().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.NutsCode,
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                    Code = e.NutsCode,
                    Name = e.Name,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetSettlements(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Settlement>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllSettlements().AsQueryable().Where(predicate);

            var result = nomenclature
                .OrderBy(e => e.Order)
                .Select(e => new Select2ExtendedItem
                {
                    id = e.LauCode,
                    text = SystemLocalization.GetDisplayName(e.DisplayName, e.NameAlt),
                    Code = e.LauCode,
                    Name = e.DisplayName,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetMunicipalities(string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Municipality>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetAllMunicipalities().AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ExtendedItem
                {
                    id = e.LauCode,
                    text = SystemLocalization.GetDisplayName(e.DisplayName, e.NameAlt),
                    Code = e.LauCode,
                    Name = e.DisplayName,
                    NameEN = e.NameAlt,
                    FullPathName = e.FullPathName,
                    FullPathNameEN = e.FullPathNameAlt,
                    displayFullPathName = SystemLocalization.GetDisplayName(e.FullPathName, e.FullPathNameAlt),
                    FullPath = e.FullPath
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // TODO: Map Code
        [HttpGet]
        public virtual JsonResult GetMunicipalitiesByDistrictId(string term = null, int offset = 0, int? limit = null, int? parentId = null)
        {
            if (!parentId.HasValue)
            {
                return null;
            }

            var predicate = PredicateBuilder.True<Municipality>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetMunicipalitiesByDistrict(parentId.Value).AsQueryable().Where(predicate);

            var result = nomenclature
                .Select(e => new Select2ListItem
                {
                    id = e.DistrictId.ToString(),
                    text = SystemLocalization.GetDisplayName(e.Name, e.NameAlt),
                })
                .OrderBy(e => e.text)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // TODO: Map Code
        [HttpGet]
        public virtual JsonResult GetSettlementsByMunicipalityId(string term = null, int offset = 0, int? limit = null, int? parentId = null)
        {
            if (!parentId.HasValue)
            {
                return null;
            }

            var predicate = PredicateBuilder.True<Settlement>()
                .AndAnyStringContains(i => i.Name.ToUpper(), i => i.NameAlt.ToUpper(), term == null ? term : term.ToUpper());

            var nomenclature = DataCache.GetSettlementsByMunicipality(parentId.Value).AsQueryable().Where(predicate);

            var result = nomenclature
                .OrderBy(e => e.Order)
                .Select(e => new Select2ListItem
                {
                    id = e.MunicipalityId.ToString(),
                    text = e.DisplayName
                })
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}