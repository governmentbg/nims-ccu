using System;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Services;

namespace Eumis.Data.DomainServices
{
    internal class NomenclatureDomainService : INomenclatureDomainService
    {
        private IEntityCodeNomsRepository<KidCode, EntityCodeNomVO> kidCodeNomsRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IEntityGidNomsRepository<CompanyLegalType, CompanyLegalTypeGidNomVO> companyLegalTypesRepository;
        private IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository;
        private ICompanySizeTypeNomsRepository companySizeTypesRepository;

        public NomenclatureDomainService(
            IEntityCodeNomsRepository<KidCode, EntityCodeNomVO> kidCodeNomsRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            ICompanyLegalTypeNomsRepository companyLegalTypesRepository,
            IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> companyTypesRepository,
            ICompanySizeTypeNomsRepository companySizeTypesRepository)
        {
            this.kidCodeNomsRepository = kidCodeNomsRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.companyLegalTypesRepository = companyLegalTypesRepository;
            this.companyTypesRepository = companyTypesRepository;
            this.companySizeTypesRepository = companySizeTypesRepository;
        }

        public int GetCompanyLegalTypeNomIdByGid(Guid gid)
        {
            return this.companyLegalTypesRepository.GetNomIdByGid(gid);
        }

        public int GetCompanySizeTypeNomIdByGid(Guid gid)
        {
            return this.companySizeTypesRepository.GetNomIdByGid(gid);
        }

        public int GetCompanyTypeNomIdByGid(Guid gid)
        {
            return this.companyTypesRepository.GetNomIdByGid(gid);
        }

        public int GetKidCodeNomIdByCode(string code)
        {
            return this.kidCodeNomsRepository.GetNomIdByCode(code);
        }

        public int GetCountryNomIdByCode(string code)
        {
            return this.countryNomsRepository.GetNomIdByCode(code);
        }

        public int GetSettlementNomIdByCode(string code)
        {
            return this.settlementNomsRepository.GetNomIdByCode(code);
        }
    }
}
