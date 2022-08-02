using Eumis.Domain.Contracts;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractVersionXml
{
    public interface IContractVersionXmlService
    {
        IList<string> CanCreateVersion(int contractId, ContractVersionType type);
    }
}
