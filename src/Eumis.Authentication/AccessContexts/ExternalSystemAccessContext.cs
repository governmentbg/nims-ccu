using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.AccessContexts
{
    public class ExternalSystemAccessContext : IAccessContext
    {
        private string externalSystemProperty;

        public ExternalSystemAccessContext(string externalSystemProperty)
        {
            this.externalSystemProperty = externalSystemProperty;
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
            get { return false; }
        }

        public bool IsExternalSystem
        {
            get { return true; }
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
            get { throw new NotSupportedException(); }
        }

        public string ContractAccessCodeEmail
        {
            get { throw new NotSupportedException(); }
        }

        public string ExternalSystemProperty
        {
            get { return this.externalSystemProperty; }
        }
    }
}
