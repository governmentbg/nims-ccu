using System.Collections.Generic;
using System.Linq;
using Autofac;
using Eumis.Portal.Model.Entities;

namespace Eumis.Portal.Model.Repositories
{
    public class DataCache
    {
        private static List<Country> _countries;
        private static List<ProtectedZone> _protectedZones;
        private static List<Nuts1s> _nuts1s;
        private static List<Nuts2s> _nuts2s;
        private static List<District> _districts;
        private static List<Municipality> _municipalities;
        private static List<Settlement> _settlements;

        private static List<KidCode> _kidCodes;
        private static List<CompanyType> _companyTypes;
        private static List<CompanyLegalType> _companyLegalTypes;
        private static List<CompanySizeType> _companySizeTypes; 

        static DataCache()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new EumisPortalModelModule());

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                IAddressRepository addressRepository = scope.Resolve<IAddressRepository>();
                ICompanyRepository companyRepository = scope.Resolve<ICompanyRepository>();

                _countries = addressRepository.GetAllCountries().ToList();
                _protectedZones = addressRepository.GetAllProtectedZones().ToList();
                _nuts1s = addressRepository.GetAllNuts1s().ToList();
                _nuts2s = addressRepository.GetAllNuts2s().ToList();
                _districts = addressRepository.GetAllDistricts().ToList();
                _municipalities = addressRepository.GetAllMunicipalities().ToList();
                _settlements = addressRepository.GetAllSettlements().ToList();

                _kidCodes = companyRepository.GetAllKidCodes().ToList();
                _companyTypes = companyRepository.GetAllCompanyTypes().ToList();
                _companyLegalTypes = companyRepository.GetAllCompanyLegalTypes().ToList();
                _companySizeTypes = companyRepository.GetAllCompanySizeTypes().ToList();
            }
        }

        public static List<Country> GetAllCountries()
        {
            var bg = _countries.Where(e => e.Name.ToUpper() == "БЪЛГАРИЯ");
            var others = _countries.Where(e => e.Name.ToUpper() != "БЪЛГАРИЯ").OrderBy(e => e.Name);

            var allCountries = bg.Concat(others);

            return allCountries.ToList();
        }

        public static List<ProtectedZone> GetAllProtectedZones()
        {
            return _protectedZones;
        }

        public static List<Nuts1s> GetAllNuts1s()
        {
            return _nuts1s;
        }

        public static List<Nuts2s> GetAllNuts2s()
        {
            return _nuts2s;
        }

        public static List<District> GetAllDistricts()
        {
            return _districts;
        }

        public static List<Municipality> GetAllMunicipalities()
        {
            return _municipalities;
        }

        public static List<Settlement> GetAllSettlements()
        {
            return _settlements;
        }

        public static List<KidCode> GetAllKidCodes()
        {
            return _kidCodes;
        }

        public static List<CompanyType> GetAllCompanyTypes()
        {
            return _companyTypes;
        }

        public static List<CompanyLegalType> GetAllCompanyLegalTypes()
        {
            return _companyLegalTypes;
        }

        public static List<CompanySizeType> GetAllCompanySizeTypes()
        {
            return _companySizeTypes;
        }

        public static IEnumerable<Municipality> GetMunicipalitiesByDistrict(int parentId)
        {
            return DataCache.GetAllMunicipalities().Where(m => m.DistrictId == parentId);
        }

        public static IEnumerable<Settlement> GetSettlementsByMunicipality(int parentId)
        {
            return DataCache.GetAllSettlements().Where(s => s.MunicipalityId == parentId);
        }

        public static IEnumerable<Settlement> GetSettlementsByMunicipalityAndDistrictCode(int parentId)
        {
            return DataCache.GetAllSettlements().Where(s => s.MunicipalityId == parentId);
        }
    }
}