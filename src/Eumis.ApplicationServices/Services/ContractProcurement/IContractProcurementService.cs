using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractProcurement
{
    public interface IContractProcurementService
    {
        IList<string> CanCreateProcurement(int contractId);

        IList<string> CanCreateProcurement(Guid contractGid);

        void ActivateProcurement(int procurementId, byte[] version);

        Domain.Contracts.ContractProcurementXml CreateProcurementFromAdministrativeAuthority(int contractId, string note);

        Domain.Contracts.ContractProcurementXml CreateProcurementFromBeneficiary(int contractId, string note);
    }
}
