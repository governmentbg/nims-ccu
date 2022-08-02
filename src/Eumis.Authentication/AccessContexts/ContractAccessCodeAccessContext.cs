using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.AccessContexts
{
    public class ContractAccessCodeAccessContext : IAccessContext
    {
        private int contractAccessCodeId;
        private int contractId;
        private string contractAccessCodeEmail;

        public ContractAccessCodeAccessContext(int contractAccessCodeId, int contractId, string contractAccessCodeEmail)
        {
            this.contractAccessCodeId = contractAccessCodeId;
            this.contractId = contractId;
            this.contractAccessCodeEmail = contractAccessCodeEmail;
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
            get { return false; }
        }

        public bool IsContractAccessCode
        {
            get { return true; }
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
            get { throw new NotSupportedException(); }
        }

        public string ContractRegistrationEmail
        {
            get { throw new NotSupportedException(); }
        }

        public int ContractAccessCodeId
        {
            get { return this.contractAccessCodeId; }
        }

        public string ContractAccessCodeEmail
        {
            get { return this.contractAccessCodeEmail; }
        }

        public int ContractId
        {
            get { return this.contractId; }
        }

        public string ExternalSystemProperty
        {
            get { throw new NotSupportedException(); }
        }
    }
}
