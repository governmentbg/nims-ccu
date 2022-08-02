using System;
using System.Collections.Generic;
using Eumis.Domain.Contracts;

namespace Eumis.ApplicationServices.Services.ContractCommunication
{
    public interface IContractCommunicationService
    {
        bool CanDelete(int communicationId);

        bool CanUpdateCommunication(Guid communicationGid);

        bool CanActivateCommunication(Guid communicationGid);
    }
}
