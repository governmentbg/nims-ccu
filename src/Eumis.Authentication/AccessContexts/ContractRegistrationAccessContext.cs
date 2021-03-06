using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.AccessContexts
{
    internal class ContractRegistrationAccessContext : IAccessContext
    {
        private int contractRegistrationId;
        private string contractRegistrationEmail;

        public ContractRegistrationAccessContext(int contractRegistrationId, string contractRegistrationEmail)
        {
            this.contractRegistrationId = contractRegistrationId;
            this.contractRegistrationEmail = contractRegistrationEmail;
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public bool IsUser
        {
            get { return false; }
        }

        public bool IsRegistration
        {
            get { return false; }
        }

        public bool IsContractRegistration
        {
            get { return true; }
        }

        public bool IsContractAccessCode
        {
            get { return false; }
        }

        public bool IsExternalSystem
        {
            get { return false; }
        }

        public int UserId
        {
            get { throw new NotSupportedException(); }
        }

        public string Username
        {
            get { throw new NotSupportedException(); }
        }

        public int RegistrationId
        {
            get { throw new NotSupportedException(); }
        }

        public string RegistrationEmail
        {
            get { throw new NotSupportedException(); }
        }

        public int ContractRegistrationId
        {
            get { return this.contractRegistrationId; }
        }

        public string ContractRegistrationEmail
        {
            get { return this.contractRegistrationEmail; }
        }

        public int ContractAccessCodeId
        {
            get { throw new NotSupportedException(); }
        }

        public string ContractAccessCodeEmail
        {
            get { throw new NotSupportedException(); }
        }

        public string ExternalSystemProperty
        {
            get { throw new NotSupportedException(); }
        }
    }
}
