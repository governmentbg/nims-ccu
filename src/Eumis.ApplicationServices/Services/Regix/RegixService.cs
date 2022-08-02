using Eumis.ApplicationServices.Communicators;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.Regix.Contracts.Bulstat;
using Eumis.Data.Regix.Helpers.Bulstat;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.Regix
{
    public class RegixService : IRegixService
    {
        private IRegixRestApiCommunicator regixRestApiCommunicator;
        private ICompaniesRepository companiesRepository;
        private IEntityCodeNomsRepository<Domain.NonAggregates.Country, EntityCodeNomVO> countryRepository;
        private ISettlementNomsRepository settlementRepository;
        private IEntityCodeNomsRepository<KidCode, EntityCodeNomVO> kidCodeRepository;

        public RegixService(
            IRegixRestApiCommunicator regixRestApiCommunicator,
            ICompaniesRepository companiesRepository,
            IEntityCodeNomsRepository<Domain.NonAggregates.Country, EntityCodeNomVO> countryRepository,
            ISettlementNomsRepository settlementRepository,
            IEntityCodeNomsRepository<KidCode, EntityCodeNomVO> kidCodeRepository)
        {
            this.regixRestApiCommunicator = regixRestApiCommunicator;
            this.companiesRepository = companiesRepository;
            this.countryRepository = countryRepository;
            this.settlementRepository = settlementRepository;
            this.kidCodeRepository = kidCodeRepository;
        }

        public Domain.Companies.Company GetCommercialRegisterCompany(string uin)
        {
            var companyExternalData = this.regixRestApiCommunicator.GetActualState(uin);
            if (string.IsNullOrEmpty(companyExternalData.UIC))
            {
                return null;
            }

            var legalType = this.companiesRepository.GetLegalTypeByCommercialRegisterCode(companyExternalData.LegalForm.LegalFormAbbr);

            int? countryId = this.GetCountryId(companyExternalData?.Seat?.Address?.Country);
            int? settlementId = this.settlementRepository.GetSettlementNom(companyExternalData?.Seat?.Address?.SettlementEKATTE)?.NomValueId;

            int? corrCountryId = this.GetCountryId(companyExternalData?.SeatForCorrespondence?.Country);
            int? corrSettlementId = this.settlementRepository.GetSettlementNom(companyExternalData?.SeatForCorrespondence?.SettlementEKATTE)?.NomValueId;

            int? kidCodeId = this.GetCommercialRegisterKidCode(companyExternalData?.SubjectOfActivityNKID?.NKIDcode, companyExternalData?.SubjectOfActivityNKID?.NKIDname);

            var company = new Domain.Companies.Company(
                uin,
                UinType.Eik,
                legalType?.CompanyTypeId ?? 0,
                legalType?.CompanyLegalTypeId ?? 0,
                companyExternalData.Company,
                companyExternalData.Transliteration,
                (int?)null,
                settlementId,
                companyExternalData?.Seat?.Address?.PostCode,
                companyExternalData?.Seat?.Address?.ToString(),
                companyExternalData?.Seat?.Address?.StreetNumber,
                corrCountryId,
                corrSettlementId,
                companyExternalData?.SeatForCorrespondence?.PostCode,
                companyExternalData?.SeatForCorrespondence?.ToString(),
                companyExternalData?.SeatForCorrespondence?.StreetNumber,
                companyExternalData?.Seat?.Contacts?.Phone,
                string.Empty,
                companyExternalData?.Seat?.Contacts?.Fax,
                companyExternalData?.Seat?.Contacts?.EMail,
                ProgrammePriorityType.FirstClass);
            return company;
        }

        public Domain.Companies.Company GetBulstatRegisterCompany(string uin)
        {
            Func<CommunicationType, List<Communication>, Communication> getCommunication = (ct, list) => list.Where(x => x.Type.Code.Equals(ct.ToBulstatCode())).FirstOrDefault();
            Func<AddressType, List<Address>, Address> getAddress = (at, list) => list.Where(x => x.AddressType.Code.Equals(at.ToBulstatCode())).FirstOrDefault();

            var companyExternalData = this.regixRestApiCommunicator.GetStateOfPlay(uin);
            if (companyExternalData.Subject == null)
            {
                return this.GetNpoNonCommercialData(uin);
            }

            var headquartersAddress = getAddress(AddressType.Headquarters, companyExternalData.Subject.AddressesCollection);
            var correspondenceAddress = getAddress(AddressType.Correspondence, companyExternalData.Subject.AddressesCollection);

            var phone = getCommunication(CommunicationType.Phone, companyExternalData.Subject.CommunicationsCollection);
            var email = getCommunication(CommunicationType.Email, companyExternalData.Subject.CommunicationsCollection);
            var fax = getCommunication(CommunicationType.Fax, companyExternalData.Subject.CommunicationsCollection);

            var legalType = this.companiesRepository.GetLegalTypeByBulstatRegisterCode(companyExternalData?.Subject?.LegalEntitySubject?.LegalForm?.Code);

            var company = new Domain.Companies.Company(
                uin,
                UinType.Bulstat,
                legalType?.CompanyTypeId ?? 0,
                legalType?.CompanyLegalTypeId ?? 0,
                companyExternalData.Subject.LegalEntitySubject.CyrillicFullName,
                companyExternalData.Subject.LegalEntitySubject.LatinFullName,
                Data.Regix.Helpers.Bulstat.CountryMap.GetCountryIdFromCode(headquartersAddress?.Country.Code),
                this.settlementRepository.GetSettlementNom(headquartersAddress?.Location?.Code)?.NomValueId,
                headquartersAddress?.PostalCode,
                headquartersAddress?.ToString(),
                headquartersAddress?.ToString(),
                Data.Regix.Helpers.Bulstat.CountryMap.GetCountryIdFromCode(correspondenceAddress?.Country.Code),
                this.settlementRepository.GetSettlementNom(correspondenceAddress?.Location?.Code)?.NomValueId,
                correspondenceAddress?.PostalCode,
                correspondenceAddress?.ToString(),
                correspondenceAddress?.ToString(),
                phone?.Value,
                string.Empty,
                fax?.Value,
                email?.Value,
                ProgrammePriorityType.FirstClass);
            return company;
        }

        public string GetPersonNames(string personalBulstat)
        {
            var externalData = this.regixRestApiCommunicator.GetValidPerson(personalBulstat);

            return $"{externalData.FirstName} {externalData.SurName} {externalData.FamilyName}";
        }

        public CompanyPVO GetCompany(string uin, UinType uinType, string code)
        {
            Domain.Companies.Company company = null;
            if (uinType == UinType.Eik)
            {
                company = this.GetCommercialRegisterCompany(uin);
            }

            if (uinType == UinType.Bulstat)
            {
                company = this.GetBulstatRegisterCompany(uin);
                if (company == null)
                {
                    company = this.GetNpoNonCommercialData(uin);
                }
            }

            if (company != null)
            {
                return this.companiesRepository.GetPortalCompany(company);
            }

            return new CompanyPVO();
        }

        private int GetCountryId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Domain.NonAggregates.Country.ID_BG;
            }

            var country = this.countryRepository.GetNoms(string.Empty)
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefault();

            if (country == null)
            {
                return Domain.NonAggregates.Country.ID_BG;
            }

            return country.NomValueId;
        }

        private int? GetCommercialRegisterKidCode(string code, string name)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(name))
            {
                return null;
            }

            var kidCode = this.kidCodeRepository.GetNoms(string.Empty)
                .Where(x => x.Code == code || x.Name.ToLower() == name?.ToLower())
                .FirstOrDefault();

            return kidCode?.NomValueId;
        }

        private int? GetBulstatRegisterKidCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            var kidCode = this.kidCodeRepository.GetNoms(string.Empty)
                .Where(x => x.Code.Replace(".", string.Empty) == code)
                .FirstOrDefault();

            return kidCode?.NomValueId;
        }

        private Domain.Companies.Company GetNpoNonCommercialData(string uin)
        {
            var companyExternalData = this.regixRestApiCommunicator.GetNPORegistrationInfo(uin);
            if (string.IsNullOrEmpty(companyExternalData.RegistrationNumber))
            {
                return null;
            }

            var company = new Domain.Companies.Company(
                uin,
                UinType.Bulstat,
                0,
                0,
                companyExternalData.Name,
                null,
                0,
                Data.Regix.Helpers.Bulstat.CountryMap.GetCountryIdFromCode(companyExternalData.NationalityCode),
                null,
                null,
                companyExternalData.Address,
                null,
                Data.Regix.Helpers.Bulstat.CountryMap.GetCountryIdFromCode(companyExternalData.NationalityCode),
                null,
                null,
                null,
                string.Empty,
                null,
                string.Empty,
                null,
                ProgrammePriorityType.FirstClass);
            return company;
        }
    }
}
