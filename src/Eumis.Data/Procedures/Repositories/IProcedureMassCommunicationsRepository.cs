using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using System.Collections.Generic;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureMassCommunicationsRepository : IAggregateRepository<ProcedureMassCommunication>
    {
        IList<ProcedureMassCommunicationVO> GetProcedureMassCommunications(int[] programmeIds);

        ProcedureMassCommunicationInfoVO GetInfo(int procedureMassCommunicationId);

        void DeleteProcedureMassCommunication(int procedureMassCommunicationId, byte[] vers);

        int GetCommunicationProcedureId(int procedureMassCommunicationId);

        IList<ProcedureMassCommunicationDocumentVO> GetCommunicationDocuments(int communicationId);

        IList<ProcedureMassCommunicationRecipientVO> GetAttachedContracts(int communicationId);

        IList<ProcedureMassCommunicationRecipientVO> GetUnattachedContracts(int communicationId, int procedureId);
    }
}
