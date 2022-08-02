using Eumis.Common.Db;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Data.Companies.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Companies.Repositories
{
    internal class CompaniesRepository : AggregateRepository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Company, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Company, object>>[]
                {
                    c => c.CompanyType,
                    c => c.CompanyLegalType,
                    c => c.SeatCountry,
                    c => c.SeatSettlement,
                    c => c.CorrCountry,
                    c => c.CorrSettlement,
                    c => c.LocalActionGroupMunicipalities,
                };
            }
        }

        public Company FindByUinOrDefault(string uin, UinType uinType)
        {
            return this.Set()
                .Where(c => c.Uin == uin && c.UinType == uinType)
                .SingleOrDefault();
        }

        public IList<CompaniesVO> GetCompanies(
            string name = null,
            UinType? uinType = null,
            string uin = null)
        {
            var predicate = PredicateBuilder.True<Company>()
                .AndStringMatches(c => c.Name, name, false)
                .AndEquals(c => c.Uin, uin)
                .AndEquals(c => c.UinType, uinType);

            return (from c in this.unitOfWork.DbContext.Set<Company>().Where(predicate)

                    join sc in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryId equals sc.CountryId into g1
                    from sc in g1.DefaultIfEmpty()

                    join cc in this.unitOfWork.DbContext.Set<Country>() on c.CorrCountryId equals cc.CountryId into g2
                    from cc in g2.DefaultIfEmpty()

                    join ss in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementId equals ss.SettlementId into g3
                    from ss in g3.DefaultIfEmpty()

                    join cs in this.unitOfWork.DbContext.Set<Settlement>() on c.CorrSettlementId equals cs.SettlementId into g4
                    from cs in g4.DefaultIfEmpty()

                    select new
                    {
                        c.CompanyId,
                        c.Uin,
                        c.UinType,
                        c.Name,
                        SeatCountryName = sc.Name,
                        SeatCountryNutsCode = sc.NutsCode,
                        SeatSettlementName = ss.DisplayName,
                        c.SeatPostCode,
                        c.SeatStreet,
                        c.SeatAddress,
                        CorrCountryName = cc.Name,
                        CorrCountryNutsCode = cc.NutsCode,
                        CorrSettlementName = cs.DisplayName,
                        c.CorrPostCode,
                        c.CorrStreet,
                        c.CorrAddress,
                    })
                    .ToList()
                    .Select(c => new CompaniesVO
                    {
                        CompanyId = c.CompanyId,
                        Uin = c.Uin,
                        UinType = c.UinType,
                        UinTypeId = c.UinType,
                        Name = c.Name,
                        Seat = c.SeatCountryNutsCode == "BG" ? c.SeatSettlementName + " " + c.SeatPostCode + " " + c.SeatStreet : c.SeatCountryName + " " + c.SeatAddress,
                        Corr = c.CorrCountryNutsCode == "BG" ? c.CorrSettlementName + " " + c.CorrPostCode + " " + c.CorrStreet : c.CorrCountryName + " " + c.CorrAddress,
                    })
                    .ToList();
        }

        public CompanyPVO GetPortalCompany(string uin, UinType uinType)
        {
            return (from c in this.unitOfWork.DbContext.Set<Company>()

                    join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId

                    join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId

                    join sc in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryId equals sc.CountryId into g4
                    from sc in g4.DefaultIfEmpty()

                    join ss in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementId equals ss.SettlementId into g5
                    from ss in g5.DefaultIfEmpty()

                    join cc in this.unitOfWork.DbContext.Set<Country>() on c.CorrCountryId equals cc.CountryId into g6
                    from cc in g6.DefaultIfEmpty()

                    join cs in this.unitOfWork.DbContext.Set<Settlement>() on c.CorrSettlementId equals cs.SettlementId into g7
                    from cs in g7.DefaultIfEmpty()

                    where c.Uin == uin && c.UinType == uinType
                    select new { c, ct, clt, sc, ss, cc, cs })
                    .ToList()
                    .Select(c =>
                    new CompanyPVO
                    {
                        Uin = c.c.Uin,
                        UinType = new EnumPVO<UinType>()
                        {
                            Description = c.c.UinType,
                            Value = c.c.UinType,
                        },
                        CompanyType = new EntityGidNomVO
                        {
                            NomValueId = c.ct.CompanyTypeId,
                            Gid = c.ct.Gid,
                            Name = c.ct.Name,
                        },
                        CompanyLegalType = new EntityGidNomVO
                        {
                            NomValueId = c.clt.CompanyLegalTypeId,
                            Gid = c.clt.Gid,
                            Name = c.clt.Name,
                        },
                        Name = c.c.Name,
                        NameAlt = c.c.NameAlt,

                        SeatCountry = this.CreateCountryNomVO(c.sc),
                        SeatSettlement = this.CreateSettlementNomVO(c.ss),
                        SeatPostCode = c.c.SeatPostCode,
                        SeatStreet = c.c.SeatStreet,
                        SeatAddress = c.c.SeatAddress,

                        CorrCountry = this.CreateCountryNomVO(c.cc),
                        CorrSettlement = this.CreateSettlementNomVO(c.cs),
                        CorrPostCode = c.c.CorrPostCode,
                        CorrStreet = c.c.CorrStreet,
                        CorrAddress = c.c.CorrAddress,

                        Representative = c.c.Representative,
                        Phone1 = c.c.Phone1,
                        Phone2 = c.c.Phone2,
                        Fax = c.c.Fax,
                        Email = c.c.Email,

                        ContactName = c.c.ContactName,
                        ContactPhone = c.c.ContactPhone,
                        ContactEmail = c.c.ContactEmail,
                    })
                    .SingleOrDefault();
        }

        public IList<string> CanDeleteCompany(int companyId)
        {
            var errors = new List<string>();

            var hasAssociatedProjects = (from p in this.unitOfWork.DbContext.Set<Project>()
                                         where p.CompanyId == companyId
                                         select p.ProjectId).Any();
            if (hasAssociatedProjects)
            {
                errors.Add("Не може да се изтрие кандидат, към който има асоциирани проектни предложения.");
            }

            return errors;
        }

        private EntityCodeNomVO CreateCountryNomVO(Country c)
        {
            if (c == null)
            {
                return null;
            }

            return new EntityCodeNomVO
            {
                NomValueId = c.CountryId,
                Code = c.NutsCode,
                Name = c.Name,
            };
        }

        private EntityCodeNomVO CreateSettlementNomVO(Settlement s)
        {
            if (s == null)
            {
                return null;
            }

            return new EntityCodeNomVO
            {
                NomValueId = s.SettlementId,
                Code = s.LauCode,
                Name = s.DisplayName,
            };
        }

        private EntityCodeNomVO CreateKidCodeNomVO(KidCode kc)
        {
            if (kc == null)
            {
                return null;
            }

            return new EntityCodeNomVO
            {
                NomValueId = kc.KidCodeId,
                Code = kc.Code,
                Name = kc.Name,
            };
        }

        public bool IsUniqueUin(string uin, UinType uinType, int? companyId = null)
        {
            if (companyId.HasValue)
            {
                return !this.unitOfWork.DbContext.Set<Company>()
                    .Where(p => p.UinType == uinType && p.Uin == uin && p.CompanyId != companyId).Any();
            }
            else
            {
                return !this.unitOfWork.DbContext.Set<Company>()
                    .Where(p => p.UinType == uinType && p.Uin == uin).Any();
            }
        }

        public IList<string> CanCreateCompany(UinType uinType, string uin)
        {
            var errors = new List<string>();

            var isUniqueUin = this.IsUniqueUin(uin, uinType);

            if (!isUniqueUin)
            {
                errors.Add($"Вече съществува кандидат с Булстат/ЕИК/ЕГН: {uin}");
            }

            return errors;
        }

        public CompanyLegalType GetLegalTypeByCommercialRegisterCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            return this.GetLegalType(x => x.CodeCommercialRegister == code);
        }

        public CompanyLegalType GetLegalTypeByBulstatRegisterCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            return this.GetLegalType(x => x.CodeBulstatRegister == code);
        }

        private CompanyLegalType GetLegalType(Func<CompanyLegalType, bool> predicate)
        {
            return this.unitOfWork.DbContext.Set<CompanyLegalType>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public IList<LocalActionGroupMunicipalitiesVO> GetLocalActionGroupMunicipalities(int companyId)
        {
            return (from lagm in this.unitOfWork.DbContext.Set<LocalActionGroupMunicipality>().Where(x => x.CompanyId == companyId)
                    join m in this.unitOfWork.DbContext.Set<Municipality>() on lagm.MunicipalityId equals m.MunicipalityId

                    select new LocalActionGroupMunicipalitiesVO
                    {
                        LocalActionGroupMunicipalityId = lagm.LocalActionGroupMunicipalityId,
                        MunicipalityName = m.DisplayName,
                        NutsLevel = NutsLevel.Municipality,
                    })
                    .ToList();
        }

        public CompanyPVO GetPortalCompany(Company company)
        {
            List<Company> companies = new List<Company> { company };

            return (from c in companies

                    join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId into g1
                    from ct in g1.DefaultIfEmpty()

                    join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g2
                    from clt in g2.DefaultIfEmpty()

                    join sc in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryId equals sc.CountryId into g5
                    from sc in g5.DefaultIfEmpty()

                    join ss in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementId equals ss.SettlementId into g6
                    from ss in g6.DefaultIfEmpty()

                    join cc in this.unitOfWork.DbContext.Set<Country>() on c.CorrCountryId equals cc.CountryId into g7
                    from cc in g7.DefaultIfEmpty()

                    join cs in this.unitOfWork.DbContext.Set<Settlement>() on c.CorrSettlementId equals cs.SettlementId into g8
                    from cs in g8.DefaultIfEmpty()

                    select new { c, ct, clt, sc, ss, cc, cs })
                    .ToList()
                    .Select(c =>
                    new CompanyPVO
                    {
                        Uin = c.c.Uin,
                        UinType = new EnumPVO<UinType>()
                        {
                            Description = c.c.UinType,
                            Value = c.c.UinType,
                        },
                        CompanyType = new EntityGidNomVO
                        {
                            NomValueId = c.ct?.CompanyTypeId ?? 0,
                            Gid = c.ct?.Gid ?? Guid.Empty,
                            Name = c.ct?.Name ?? string.Empty,
                        },
                        CompanyLegalType = new EntityGidNomVO
                        {
                            NomValueId = c.clt?.CompanyLegalTypeId ?? 0,
                            Gid = c.clt?.Gid ?? Guid.Empty,
                            Name = c.clt?.Name ?? string.Empty,
                        },
                        Name = c.c?.Name,
                        NameAlt = c.c?.NameAlt,

                        SeatCountry = this.CreateCountryNomVO(c.sc),
                        SeatSettlement = this.CreateSettlementNomVO(c.ss),
                        SeatPostCode = c.c?.SeatPostCode,
                        SeatStreet = c.c?.SeatStreet,
                        SeatAddress = c.c?.SeatAddress,

                        CorrCountry = this.CreateCountryNomVO(c.cc),
                        CorrSettlement = this.CreateSettlementNomVO(c.cs),
                        CorrPostCode = c.c?.CorrPostCode,
                        CorrStreet = c.c?.CorrStreet,
                        CorrAddress = c.c?.CorrAddress,

                        Phone1 = c.c?.Phone1,
                        Fax = c.c?.Fax,
                        Email = c.c?.Email,
                    })
                    .SingleOrDefault();
        }
    }
}
