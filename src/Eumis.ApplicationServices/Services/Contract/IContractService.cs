using Eumis.Domain.Contracts;
using Eumis.Domain.Projects;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.Contract
{
    public interface IContractService
    {
        IList<string> CanCreate(int projectId, int programmeId, int userId);

        Eumis.Domain.Contracts.Contract CreateContract(int projectId, int programmeId, Domain.Contracts.ContractType contractType, ContractRegistrationType registrationType, int? attachedContractId, int userId);

        Domain.Contracts.Contract CreateServiceContractAgreement(int projectId, int programmeId, ContractType contractType, ContractRegistrationType registrationType, int companyId, int userId);

        void EnterContract(int contractId, byte[] version, byte[] contractVersion);
    }
}
