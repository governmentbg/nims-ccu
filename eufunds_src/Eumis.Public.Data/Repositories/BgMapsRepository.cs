using Eumis.Public.Data.Core;
using Eumis.Public.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Repositories
{
    public interface IBgMapsRepository : IMapsRepository
    {
        BgMapsEntryPoint GetMapEntryPoint (int mapId);
        IEnumerable<BgMapRegionsEntryPoint> GetMapRegionEntryPointsForMap (int mapId);
        BgMapsEntryPoint GetMapEntryPointForNomenData (NomenclatureType nomenType, int nomenId);
        Country GetCountry (int id);
        Nuts1s GetNuts1 (int id);
        Nuts2s GetNuts2 (int id);
        District GetDistrict (int id);
        Municipality GetMunicipality (int id);
        Settlement GetSettlement (int id);

        List<BgMapsEntryPoint> TestGetAllBgMapsEntryPoint();
    }

    internal class BgMapsRepository : MapsRepository, IBgMapsRepository
    {
        public BgMapsRepository (IUnitOfWork uow)
            : base(uow) { }

        public BgMapsEntryPoint GetMapEntryPoint (int mapId)
        {
            return this.unitOfWork.DbContext.Set<BgMapsEntryPoint>().FirstOrDefault(m => m.MapId == mapId);
        }

        public IEnumerable<BgMapRegionsEntryPoint> GetMapRegionEntryPointsForMap(int mapId)
        {
            IEnumerable<int> regions = this.unitOfWork.DbContext.Set<MapRegion>().Where(r => r.MapId == mapId).Select(r => r.Id);
            return this.unitOfWork.DbContext.Set<BgMapRegionsEntryPoint>().Where(r => regions.Contains(r.MapRegionId));
        }

        public BgMapsEntryPoint GetMapEntryPointForNomenData(NomenclatureType nomenType, int nomenId)
        {
            return this.unitOfWork.DbContext.Set<BgMapsEntryPoint>().FirstOrDefault(m => m.NomenType == (byte) nomenType && m.NomenId == nomenId);
        }

        public List<BgMapsEntryPoint> TestGetAllBgMapsEntryPoint()
        {
            return this.unitOfWork.DbContext.Set<BgMapsEntryPoint>().ToList();
        }

        public Country GetCountry(int id)
        {
            return this.unitOfWork.DbContext.Set<Country>().FirstOrDefault(c => c.CountryId == id);
        }

        public Nuts1s GetNuts1(int id)
        {
            return this.unitOfWork.DbContext.Set<Nuts1s>().FirstOrDefault(n => n.Nuts1Id == id);
        }

        public Nuts2s GetNuts2 (int id)
        {
            return this.unitOfWork.DbContext.Set<Nuts2s>().FirstOrDefault(n => n.Nuts2Id == id);
        }

        public District GetDistrict (int id)
        {
            return this.unitOfWork.DbContext.Set<District>().FirstOrDefault(n => n.DistrictId == id);
        }

        public Municipality GetMunicipality (int id)
        {
            return this.unitOfWork.DbContext.Set<Municipality>().FirstOrDefault(n => n.MunicipalityId == id);
        }

        public Settlement GetSettlement (int id)
        {
            return this.unitOfWork.DbContext.Set<Settlement>().FirstOrDefault(n => n.SettlementId == id);
        }
    }
}
