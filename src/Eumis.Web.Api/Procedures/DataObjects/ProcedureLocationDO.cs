using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureLocationDO
    {
        public ProcedureLocationDO()
        {
        }

        public ProcedureLocationDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public ProcedureLocationDO(ProcedureLocation procedureLocation, byte[] version)
        {
            this.ProcedureLocationId = procedureLocation.ProcedureLocationId;
            this.ProcedureId = procedureLocation.ProcedureId;
            this.NutsLevel = procedureLocation.NutsLevel;

            this.CountryId = procedureLocation.CountryId;
            this.Nuts1Id = procedureLocation.Nuts1Id;
            this.Nuts2Id = procedureLocation.Nuts2Id;
            this.DistrictId = procedureLocation.DistrictId;
            this.MunicipalityId = procedureLocation.MunicipalityId;
            this.SettlementId = procedureLocation.SettlementId;
            this.ProtectedZoneId = procedureLocation.ProtectedZoneId;

            this.Version = version;
        }

        public int ProcedureLocationId { get; set; }

        public int ProcedureId { get; set; }

        public NutsLevel NutsLevel { get; set; }

        public int? CountryId { get; set; }

        public int? Nuts1Id { get; set; }

        public int? Nuts2Id { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public int? SettlementId { get; set; }

        public int? ProtectedZoneId { get; set; }

        public byte[] Version { get; set; }
    }
}
