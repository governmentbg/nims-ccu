using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractProcurementsRepository : IAggregateRepository<ContractProcurementXml>
    {
        IList<ContractProcurementVO> GetContractProcurements(int contractId);

        ContractProcurementPlan GetContractProcurement(int procurementId);

        IList<ContractProcurementXml> GetNonArchivedProcurements(int contractId);

        int GetProcurementId(Guid gid);

        int GetProjectId(int procurementId);

        ContractProcurementXml Find(Guid gid, Source source);

        ContractProcurementXml FindForUpdate(Guid gid, Source source, byte[] version);

        ContractProcurementXml GetActiveProcurementOrDefault(int contractId);

        Task<ContractProcurementXml> GetActiveProcurementOrDefaultAsync(int contractId, CancellationToken ct);

        ContractProcurementXml GetLastProcurementOrDefault(int contractId);

        string GetActualProcurementXml(Guid contractGid);

        int GetContractId(int procurementId);

        bool HasContractProcurementsInProgress(int contractId);

        PagePVO<ContractProcurementPVO> GetPortalContractProcurements(Guid contractGid, int offset = 0, int? limit = null);

        IList<ContractProcurementOfferVO> GetContractProcurementOffers(int contractId);

        int? GetLastActiveContractProcurementId(int contractId);
    }
}
