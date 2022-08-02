using System;
using Eumis.Common.Json;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Rio;

namespace Eumis.ApplicationServices.Services.Company
{
    internal class CompanyCreationService : ICompanyCreationService
    {
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IEntityGidNomsRepository<CompanyLegalType, CompanyLegalTypeGidNomVO> companyLegalTypesRepository;
        private IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository;

        public CompanyCreationService(
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            ICompanyLegalTypeNomsRepository companyLegalTypesRepository,
            IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository)
        {
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.companyLegalTypesRepository = companyLegalTypesRepository;
            this.companyTypesRepository = companyTypesRepository;
        }

        public Eumis.Domain.Companies.Company CreateFromRioCompany(Rio.Company company)
        {
            // extract general data
            var companyLegalTypeId = company.GetPrivateNomId(d => d.CompanyLegalType, this.companyLegalTypesRepository.GetNomIdByGid).Value;
            var companyTypeId = company.GetPrivateNomId(d => d.CompanyType, this.companyTypesRepository.GetNomIdByGid).Value;
            var contactEmail = company.CompanyContactPersonEmail;
            var contactName = company.CompanyContactPerson;
            var contactPhone = company.CompanyContactPersonPhone;
            var email = company.Email;
            var fax = company.Fax;
            var name = company.Name;
            var nameAlt = company.NameEN;
            var phone1 = company.Phone1;
            var phone2 = company.Phone2;
            var representative = company.CompanyRepresentativePerson;
            var uin = company.Uin;
            var uinType = company.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value;

            // extract correspondence data
            var corrCountryId = company.GetPublicNomId(d => d.Correspondence.Country, this.countryNomsRepository.GetNomIdByCode);
            var corrSettlementId = company.GetPublicNomId(d => d.Correspondence.Settlement, this.settlementNomsRepository.GetNomIdByCode);
            var corrAddress = company.Get(d => d.Correspondence.FullAddress);
            var corrPostCode = company.Get(d => d.Correspondence.PostCode);
            var corrStreet = company.Get(d => d.Correspondence.Street);

            // extract seat data
            var seatCountryId = company.GetPublicNomId(d => d.Seat.Country, this.countryNomsRepository.GetNomIdByCode);
            var seatSettlementId = company.GetPublicNomId(d => d.Seat.Settlement, this.settlementNomsRepository.GetNomIdByCode);
            var seatAddress = company.Get(d => d.Seat.FullAddress);
            var seatPostCode = company.Get(d => d.Seat.PostCode);
            var seatStreet = company.Get(d => d.Seat.Street);

            return new Eumis.Domain.Companies.Company(
                uin,
                uinType,
                companyTypeId,
                companyLegalTypeId,
                name,
                nameAlt,
                seatCountryId,
                seatSettlementId,
                seatPostCode,
                seatStreet,
                seatAddress,
                corrCountryId,
                corrSettlementId,
                corrPostCode,
                corrStreet,
                corrAddress,
                representative,
                phone1,
                phone2,
                fax,
                email,
                contactName,
                contactPhone,
                contactEmail,
                ProgrammePriorityType.FirstClass);
        }

        public Rio.Company CreateRioCompany(Eumis.Domain.Companies.Company company)
        {
            var companyType = this.companyTypesRepository.GetNom(company.CompanyTypeId);
            var companyLegalType = this.companyLegalTypesRepository.GetNom(company.CompanyLegalTypeId);

            var candidate = new Rio.Company()
            {
                id = Guid.NewGuid().ToString(),
                Name = company.Name,
                NameEN = company.NameAlt,
                Uin = company.Uin,
                UinType = new Rio.PrivateNomenclature()
                {
                    Id = company.UinType.ToString(),
                    Name = company.UinType.GetEnumDescription(),
                },
                CompanyType = new Rio.PrivateNomenclature()
                {
                    Id = companyType.Gid.ToString(),
                    Name = companyType.Name,
                },
                CompanyLegalType = new Rio.PrivateNomenclature()
                {
                    Id = companyLegalType.Gid.ToString(),
                    Name = companyLegalType.Name,
                },
                Seat = new Rio.Address()
                {
                    PostCode = company.SeatPostCode,
                    Street = company.SeatStreet,
                    FullAddress = company.SeatAddress,
                },
                Correspondence = new Rio.Address()
                {
                    PostCode = company.CorrPostCode,
                    Street = company.CorrStreet,
                    FullAddress = company.CorrAddress,
                },
                Email = company.Email,
                Phone1 = company.Phone1,
                Phone2 = company.Phone2,
                Fax = company.Fax,
                CompanyRepresentativePerson = company.Representative,
                CompanyContactPerson = company.ContactName,
                CompanyContactPersonPhone = company.ContactPhone,
                CompanyContactPersonEmail = company.ContactEmail,
            };

            if (company.SeatCountryId != null)
            {
                var seatCountry = this.countryNomsRepository.GetNom(company.SeatCountryId.Value);
                candidate.Seat.Country = new Rio.PublicNomenclature()
                {
                    Code = seatCountry.Code,
                    Name = seatCountry.Name,
                };
            }

            if (company.SeatSettlementId != null)
            {
                var seatSettlement = this.settlementNomsRepository.GetNom(company.SeatSettlementId.Value);
                candidate.Seat.Settlement = new Rio.PublicNomenclature()
                {
                    Code = seatSettlement.Code,
                    Name = seatSettlement.Name,
                };
            }

            if (company.CorrCountryId != null)
            {
                var corrCountry = this.countryNomsRepository.GetNom(company.CorrCountryId.Value);
                candidate.Correspondence.Country = new Rio.PublicNomenclature()
                {
                    Code = corrCountry.Code,
                    Name = corrCountry.Name,
                };
            }

            if (company.CorrSettlementId != null)
            {
                var corrSettlement = this.settlementNomsRepository.GetNom(company.CorrSettlementId.Value);
                candidate.Correspondence.Settlement = new Rio.PublicNomenclature()
                {
                    Code = corrSettlement.Code,
                    Name = corrSettlement.Name,
                };
            }

            return candidate;
        }
    }
}
