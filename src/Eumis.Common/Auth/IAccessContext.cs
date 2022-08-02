using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Auth
{
    public interface IAccessContext
    {
        bool IsAuthenticated { get; }

        bool IsUser { get; }

        bool IsRegistration { get; }

        bool IsContractRegistration { get; }

        bool IsContractAccessCode { get; }

        bool IsExternalSystem { get; }

        int UserId { get; }

        string Username { get; }

        int RegistrationId { get; }

        string RegistrationEmail { get; }

        int ContractRegistrationId { get; }

        string ContractRegistrationEmail { get; }

        int ContractAccessCodeId { get; }

        string ContractAccessCodeEmail { get; }

        string ExternalSystemProperty { get; }
    }
}
