using Eumis.Common.Crypto;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using System;
using System.Web.Helpers;

namespace Eumis.Domain.Contracts
{
    public partial class ContractRegistration
    {
        public bool VerifyPassword(string password)
        {
            if (!string.IsNullOrEmpty(this.PasswordHash) &&
                !string.IsNullOrEmpty(this.PasswordSalt))
            {
                return Crypto.VerifyHashedPassword(this.PasswordHash, password + this.PasswordSalt);
            }
            else
            {
                return false;
            }
        }

        public void Activate(string password)
        {
            this.ModifyDate = DateTime.Now;

            this.ActivateInt();
            this.SetPassword(password);
        }

        public void UpdateInfo(string phone)
        {
            this.Phone = phone;

            this.ModifyDate = DateTime.Now;
        }

        public bool TryChangePassword(string oldPassword, string newPassword)
        {
            if (this.VerifyPassword(oldPassword))
            {
                this.SetPassword(newPassword);

                this.ModifyDate = DateTime.Now;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetPasswordRecoveryCode()
        {
            this.PasswordRecoveryCode = this.GenerateRandomCode();

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractRegistrationPasswordRecoveredEvent() { ContractRegistrationId = this.ContractRegistrationId });
        }

        public void RecoverPassword(string newPassword)
        {
            this.SetPassword(newPassword);
            this.PasswordRecoveryCode = null;

            this.ModifyDate = DateTime.Now;
        }

        private void ActivateInt()
        {
            if (this.IsActive)
            {
                throw new DomainValidationException("ContractRegistration already active");
            }

            this.ActivationCode = null;
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new DomainValidationException("password should not be null or empty");
            }

            if (password == null)
            {
                this.PasswordSalt = null;
                this.PasswordHash = null;
            }
            else
            {
                this.PasswordSalt = Crypto.GenerateSalt();
                this.PasswordHash = Crypto.HashPassword(password + this.PasswordSalt);
            }
        }

        public void UpdateContractRegistration(string firstName, string lastName, string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;

            this.ModifyDate = DateTime.Now;
        }

        private string GenerateRandomCode()
        {
            return CryptoUtils.GetRandomAlphanumericString(20);
        }
    }
}
