using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.IntegrationRegiX.Host.Auth
{
    public class RegixCallContext : IRegixCallContext
    {
        private string email;
        private string name;
        private string userId;
        private string position;

        public RegixCallContext()
        {
        }

        public RegixCallContext(string email, string name, string userId, string position)
            : this()
        {
            this.Email = email;
            this.Name = name;
            this.Position = position;
            this.UserId = userId;
        }

        public string Email
        {
            get
            {
                return string.IsNullOrEmpty(this.email) ? null : this.email;
            }

            set
            {
                this.email = value;
            }
        }

        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(this.name) ? null : this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public string UserId
        {
            get
            {
                return string.IsNullOrEmpty(this.userId) ? null : this.userId;
            }

            set
            {
                this.userId = value;
            }
        }

        public string Position
        {
            get
            {
                return string.IsNullOrEmpty(this.position) ? null : this.position;
            }

            set
            {
                this.position = value;
            }
        }
    }
}
