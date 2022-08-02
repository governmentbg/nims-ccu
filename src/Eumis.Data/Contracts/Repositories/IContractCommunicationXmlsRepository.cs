using System;
using System.Collections.Generic;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractCommunicationXmlsRepository : IAggregateRepository<ContractCommunicationXml>
    {
        IList<ContractCommunicationVO> GetContractCommunications(int contractId, ContractCommunicationType type);

        IList<AdminAuthorityContractCommunicationVO> GetAllCommunications(
            int[] programmeIds,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            DateTime? fromDate,
            DateTime? toDate,
            Source? source);

        ContractCommunicationXml Find(Guid gid);

        ContractCommunicationXml FindForUpdate(Guid gid, byte[] version);

        Tuple<int, ContractCommunicationType> GetCommunicationIdAndType(Guid gid);

        int GetContractId(int communicationId);

        int GetProjectId(int communicationId);

        PagePVO<ContractCommunicationPVO> GetPortalContractCommunications(Guid contractGid, ContractCommunicationType type, int offset = 0, int? limit = null);
    }
}
