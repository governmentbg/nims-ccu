using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.AccessContexts
{
    internal class RegistrationAccessContext : IAccessContext
    {
        private int registrationId;
        private string registrationEmail;

        public RegistrationAccessContext(int registrationId, string registrationEmail)
        {
            this.registrationId = registrationId;
            this.registrationEmail = registrationEmail;
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
            get { return true; }
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
            get { return this.registrationId; }
        }

        public string RegistrationEmail
        {
            get { return this.registrationEmail; }
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
            get { throw new NotSupportedException(); }
        }
    }
}
