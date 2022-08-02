using Eumis.Data.Procurements.PortalViewObjects;
using Eumis.Data.Procurements.ViewObjects;
using Eumis.Domain.Procurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procurements.Repositories
{
    public interface IProcurementsRepository : IAggregateRepository<Procurement>
    {
        IList<ProcurementVO> GetProcurements();

        ProcurementInfoVO GetProcurementInfo(int procurementId);

        IList<ProcurementDocumentVO> GetProcurementDocuments(int procurementId);

        IList<ProcurementDifferentiatedPositionVO> GetProcurementDifferentiatedPositions(int procurementId);

        IList<CentralProcurementPVO> GetProcurementPVOs();
    }
}
