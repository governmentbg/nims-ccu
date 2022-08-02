using Eumis.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.AccessContexts
{
    public class UserAccessContext : IAccessContext
    {
        private int userId;
        private string username;

        public UserAccessContext(int userId, string username)
        {
            this.userId = userId;
            this.username = username;
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public bool IsUser
        {
            get { return true; }
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
            get { return this.userId; }
        }

        public string Username
        {
            get { return this.username; }
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
