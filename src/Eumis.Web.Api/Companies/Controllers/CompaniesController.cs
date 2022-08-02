using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Regix;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Companies.DataObjects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.Companies.Controllers
{
    [RoutePrefix("api/companies")]
    public class CompaniesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICompaniesRepository companiesRepository;
        private IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository;
        private IEntityGidNomsRepository<CompanyLegalType, CompanyLegalTypeGidNomVO> companyLegalTypesRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IAuthorizer authorizer;
        private IRegixService regixService;

        public CompaniesController(
            IUnitOfWork unitOfWork,
            ICompaniesRepository companiesRepository,
            IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository,
            ICompanyLegalTypeNomsRepository companyLegalTypesRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            IAuthorizer authorizer,
            IRegixService regixService)
        {
            this.unitOfWork = unitOfWork;
            this.companiesRepository = companiesRepository;
            this.companyTypesRepository = companyTypesRepository;
            this.companyLegalTypesRepository = companyLegalTypesRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.authorizer = authorizer;
            this.regixService = regixService;
        }

        [Route("")]
        public IList<CompaniesVO> GetCompanies(
            string name = null,
            UinType? uinTypeId = null,
            string uin = null)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Search);

            return this.companiesRepository.GetCompanies(name, uinTypeId, uin);
        }

        [Route("{companyId:int}")]
        public CompanyDO GetCompany(int companyId)
        {
            this.authorizer.AssertCanDo(CompanyActions.View, companyId);

            var company = this.companiesRepository.Find(companyId);

            return new CompanyDO(company);
        }

        [HttpGet]
        [Route("new")]
        public CompanyDO NewCompany()
        {
            this.authorizer.AssertCanDo(CompanyListActions.Create);

            var defaultCountryId = this.countryNomsRepository.GetNomIdByCode("BG");

            return new CompanyDO
            {
                SeatCountryId = defaultCountryId,
                CorrCountryId = defaultCountryId,
            };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateCompany(CompanyDO company)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Create);

            var errorList = this.companiesRepository.CanCreateCompany(company.UinType.Value, company.Uin);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Companies.Create))]
        public CompanyDO CreateCompany(CompanyDO company)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Create);

            Company newCompany = new Company(
                company.Uin,
                company.UinType.Value,
                company.CompanyTypeId.Value,
                company.CompanyLegalTypeId.Value,
                company.Name,
                company.NameAlt,
                company.SeatCountryId,
                company.SeatSettlementId,
                company.SeatPostCode,
                company.SeatStreet,
                company.SeatAddress,
                company.CorrCountryId,
                company.CorrSettlementId,
                company.CorrPostCode,
                company.CorrStreet,
                company.CorrAddress,
                company.Representative,
                company.Phone1,
                company.Phone2,
                company.Fax,
                company.Email,
                company.ContactName,
                company.ContactPhone,
                company.ContactEmail,
                company.ProgrammePriorityType.Value);

            this.companiesRepository.Add(newCompany);

            this.unitOfWork.Save();

            return new CompanyDO(newCompany);
        }

        [HttpPut]
        [Route("{companyId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Companies.Edit), IdParam = "companyId")]
        public void UpdateCompany(int companyId, CompanyDO company)
        {
            this.authorizer.AssertCanDo(CompanyActions.Edit, companyId);

            Company oldCompany = this.companiesRepository.FindForUpdate(companyId, company.Version);

            oldCompany.UpdateAttributes(
                company.CompanyTypeId.Value,
                company.CompanyLegalTypeId.Value,
                company.Name,
                company.NameAlt,
                company.SeatCountryId,
                company.SeatSettlementId,
                company.SeatPostCode,
                company.SeatStreet,
                company.SeatAddress,
                company.CorrCountryId,
                company.CorrSettlementId,
                company.CorrPostCode,
                company.CorrStreet,
                company.CorrAddress,
                company.Representative,
                company.Phone1,
                company.Phone2,
                company.Fax,
                company.Email,
                company.ContactName,
                company.ContactPhone,
                company.ContactEmail,
                company.ProgrammePriorityType.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{companyId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Companies.Delete), IdParam = "companyId")]
        public void DeleteCompany(int companyId, string version)
        {
            this.authorizer.AssertCanDo(CompanyActions.Delete, companyId);

            if (this.companiesRepository.CanDeleteCompany(companyId).Count != 0)
            {
                throw new InvalidOperationException("Cannot delete company.");
            }

            byte[] vers = System.Convert.FromBase64String(version);

            Company oldCompany = this.companiesRepository.FindForUpdate(companyId, vers);

            this.companiesRepository.Remove(oldCompany);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{companyId:int}/canDelete")]
        public ErrorsDO CanDeleteCompany(int companyId)
        {
            this.authorizer.AssertCanDo(CompanyActions.Delete, companyId);

            var errorList = this.companiesRepository.CanDeleteCompany(companyId);

            return new ErrorsDO(errorList);
        }

        [HttpGet]
        [Route("isUniqueUin")]
        public bool IsUniqueUin(string uin, UinType uinType, int? companyId = null)
        {
            if (companyId.HasValue)
            {
                this.authorizer.AssertCanDo(CompanyActions.View, companyId.Value);
            }
            else
            {
                this.authorizer.AssertCanDo(CompanyListActions.Create);
            }

            var isUniqueUin = this.companiesRepository.IsUniqueUin(uin, uinType, companyId);

            return isUniqueUin;
        }

        [Route("getCompanyByUin")]
        public CompanyDO GetCompanyByUin(string uin, UinType uinType)
        {
            this.authorizer.AssertCanDo(CompanyListActions.Create);

            CompanyDO companyDO = null;

            if (!string.IsNullOrWhiteSpace(uin) && (uinType == UinType.Eik))
            {
                var company = this.regixService.GetCommercialRegisterCompany(uin);
                companyDO = new CompanyDO(company);
            }

            if (!string.IsNullOrWhiteSpace(uin) && (uinType == UinType.Bulstat))
            {
                var company = this.regixService.GetBulstatRegisterCompany(uin);
                companyDO = new CompanyDO(company);
            }

            if (!string.IsNullOrWhiteSpace(uin) && (uinType == UinType.PersonalBulstat))
            {
                var personNames = this.regixService.GetPersonNames(uin);
                companyDO = new CompanyDO();
                companyDO.Name = personNames;
                companyDO.Uin = uin;
                companyDO.UinType = UinType.PersonalBulstat;
            }

            return companyDO;
        }
    }
}
