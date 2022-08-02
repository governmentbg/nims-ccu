using Eumis.Domain.Procurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procurements.DataOjects
{
    public class ProcurementDifferentiatedPositionDO
    {
        public ProcurementDifferentiatedPositionDO()
        {
        }

        public ProcurementDifferentiatedPositionDO(int procurementId, byte[] version)
        {
            this.ProcurementId = procurementId;
            this.Version = version;
        }

        public ProcurementDifferentiatedPositionDO(ProcurementDifferentiatedPosition position, byte[] version)
        {
            this.ProcurementDifferentiatedPositionId = position.ProcurementDifferentiatedPositionId;
            this.ProcurementId = position.ProcurementId;
            this.Name = position.Name;
            this.Comment = position.Comment;
            this.CompanyId = position.CompanyId;

            this.Version = version;
        }

        public int? ProcurementDifferentiatedPositionId { get; set; }

        public int? ProcurementId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public int? CompanyId { get; set; }

        public byte[] Version { get; set; }
    }
}
