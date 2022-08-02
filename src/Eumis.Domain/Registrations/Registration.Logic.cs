using System;
using System.Web.Helpers;
using Eumis.Common.Crypto;
using Eumis.Domain.Core;
using Eumis.Domain.Events;

namespace Eumis.Domain.Registrations
{
    public partial class Registration
    {
        #region Registration

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

        private string GenerateRandomCode()
        {
            return CryptoUtils.GetRandomAlphanumericString(20);
        }

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

        public void Activate(string password)
        {
            this.ModifyDate = DateTime.Now;

            this.ActivateInt();
            this.SetPassword(password);
        }

        public void SetPasswordRecoveryCode()
        {
            this.PasswordRecoveryCode = this.GenerateRandomCode();

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new RegistrationPasswordRecoveredEvent() { RegistrationId = this.RegistrationId });
        }

        public void RecoverPassword(string newPassword)
        {
            this.SetPassword(newPassword);
            this.PasswordRecoveryCode = null;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateInfo(string firstName, string lastName, string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;

            this.ModifyDate = DateTime.Now;
        }

        private void ActivateInt()
        {
            if (this.IsActive)
            {
                throw new DomainValidationException("Registration already active");
            }

            this.ActivationCode = null;
        }

        #endregion //Registration
    }
}
