using Eumis.Common.Auth;
using System;

namespace Eumis.Authentication.AccessContexts
{
    internal class UnauthenticatedAccessContext : IAccessContext
    {
        public UnauthenticatedAccessContext()
        {
        }

        public bool IsAuthenticated
        {
            get { return false; }
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
