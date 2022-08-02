using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Portal.Model.Entities;
using Eumis.Common.Data;

namespace Eumis.Portal.Model.Repositories
{
    public interface IAddressRepository
    {
        IEnumerable<Country> GetAllCountries();
        IEnumerable<ProtectedZone> GetAllProtectedZones();
        IEnumerable<Nuts1s> GetAllNuts1s();
        IEnumerable<Nuts2s> GetAllNuts2s();
        IEnumerable<District> GetAllDistricts();
        IEnumerable<Municipality> GetAllMunicipalities();
        IEnumerable<Settlement> GetAllSettlements();
    }

    public class AddressRepository : IAddressRepository
    {
        private IUnitOfWork _unitOfWork;

        public AddressRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _unitOfWork.DbContext.Set<Country>().AsEnumerable();
        }

        public IEnumerable<ProtectedZone> GetAllProtectedZones()
        {
            return _unitOfWork.DbContext.Set<ProtectedZone>().AsEnumerable();
        }

        public IEnumerable<Nuts1s> GetAllNuts1s()
        {
            return _unitOfWork.DbContext.Set<Nuts1s>().AsEnumerable();
        }

        public IEnumerable<Nuts2s> GetAllNuts2s()
        {
            return _unitOfWork.DbContext.Set<Nuts2s>().AsEnumerable();
        }

        public IEnumerable<District> GetAllDistricts()
        {
            return _unitOfWork.DbContext.Set<District>().AsEnumerable();
        }

        public IEnumerable<Municipality> GetAllMunicipalities()
        {
            return _unitOfWork.DbContext.Set<Municipality>().AsEnumerable();
        }

        public IEnumerable<Settlement> GetAllSettlements()
        {
            return _unitOfWork.DbContext.Set<Settlement>().AsEnumerable();
        }
    }
}
