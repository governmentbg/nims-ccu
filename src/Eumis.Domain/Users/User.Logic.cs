using Eumis.Common.Config;
using Eumis.Common.Crypto;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.Notifications;
using Eumis.Domain.Users.PermissionAggregations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace Eumis.Domain.Users
{
    public partial class User
    {
        #region Private Methods

        private string GenerateRandomCode()
        {
            return CryptoUtils.GetRandomAlphanumericString(20);
        }

        private void SetPassword(string password)
        {
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

        private void SetAttributes(
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.Uin = uin;
            this.Fullname = fullname;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Position = position;
            this.UserOrganizationId = userOrganizationId;
            this.UserTypeId = userTypeId;
        }

        #endregion //Private Methods

        public void UpdateUserRegData(
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.SetAttributes(uin, fullname, email, phone, address, position, userOrganizationId, userTypeId);

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateUser()
        {
            if (this.IsActive)
            {
                throw new DomainException("Cannot activate a user more than once");
            }
            else
            {
                this.IsActive = true;
                this.SetNewPasswordCode();
                ((IEventEmitter)this).Events.Add(new UserActivatedEvent() { UserId = this.UserId, NewPasswordCode = this.NewPasswordCode });
            }
        }

        public void SetUserPermissionTemplate(PermissionAggregation permissionTemplate)
        {
            this.SetPermissionTemplate(permissionTemplate);

            this.ModifyDate = DateTime.Now;
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

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (!this.VerifyPassword(oldPassword))
            {
                throw new DomainException("Wrong password provided");
            }

            if (!Regex.IsMatch(newPassword, ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordRegex")))
            {
                throw new DomainException("Invalid 'Password' format.");
            }

            this.SetPassword(newPassword);
            this.ModifyDate = DateTime.Now;
        }

        public void SetIsDeleted()
        {
            if (this.IsDeleted)
            {
                throw new DomainException("Cannot delete a deleted user");
            }
            else
            {
                this.IsDeleted = true;
            }
        }

        public void SetIsLocked(bool value)
        {
            if (this.IsLocked == value)
            {
                throw new DomainException("Cannot lock a locked user");
            }
            else
            {
                this.IsLocked = value;
            }

            if (!value)
            {
                this.ZeroFailedAttempts();
            }
        }

        public void SetPasswordRecoveryCode()
        {
            this.PasswordRecoveryCode = this.GenerateRandomCode();

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new UserPasswordRecoveredEvent() { UserId = this.UserId });
        }

        public void RecoverPassword(string newPassword)
        {
            this.SetPassword(newPassword);
            this.PasswordRecoveryCode = null;

            this.ModifyDate = DateTime.Now;
        }

        public void SetNewPasswordCode()
        {
            this.NewPasswordCode = this.GenerateRandomCode();

            this.ModifyDate = DateTime.Now;
        }

        public void SetNewPassword(string newPassword)
        {
            this.SetPassword(newPassword);
            this.NewPasswordCode = null;

            this.ModifyDate = DateTime.Now;
        }

        public void IncrementFailedAttempts()
        {
            this.FailedAttempts++;
            this.ModifyDate = DateTime.Now;
        }

        public void ZeroFailedAttempts()
        {
            this.FailedAttempts = 0;
            this.ModifyDate = DateTime.Now;
        }

        #region UserPermissions

        public IList<string> WouldChangePermissions(int[] programmeIds, PermissionAggregation permissionTemplate, Dictionary<int, string> programmes)
        {
            var errors = new List<string>();

            var userPermissions = this.GetUserPermissions(programmeIds);

            var listChangedCommonPermissions = (from cp in userPermissions.CommonPermissions
                                                join pt in permissionTemplate.CommonPermissions on new { cp.Permission } equals new { pt.Permission }
                                                where cp.IsSet && !pt.IsSet
                                                select cp).ToList();

            var listGroupedChangedProgrammePermissions = (from pp in userPermissions.ProgrammePermissions
                                                          join pt in permissionTemplate.ProgrammePermissions on new { pp.Permission, pp.ProgrammeId } equals new { pt.Permission, pt.ProgrammeId }
                                                          where pp.IsSet && !pt.IsSet
                                                          group pp by pp.ProgrammeId into g
                                                          select new { Programme = programmes[g.Key], Permissions = g.ToList() }).ToList();

            if (listChangedCommonPermissions.Any())
            {
                var permissions = new List<string>();

                foreach (var commonPermission in listChangedCommonPermissions)
                {
                    permissions.Add(
                        $"{DomainEnumTexts.ResourceManager.GetString(commonPermission.PermissionType.Name + "_" + commonPermission.Permission, new System.Globalization.CultureInfo(Common.Localization.SystemLocalization.Bg_BG))} " +
                        $"на {DomainEnumTexts.ResourceManager.GetString(commonPermission.PermissionType.Name, new System.Globalization.CultureInfo(Common.Localization.SystemLocalization.Bg_BG))}");
                }

                errors.Add($"\tПРАВА НА ДОСТЪП ПО МОДУЛИ\r\n\t\t{string.Join(";\r\n\t\t", permissions.ToArray())}");
            }

            foreach (var groupedProgrammePermission in listGroupedChangedProgrammePermissions)
            {
                var permissions = new List<string>();

                foreach (var item in groupedProgrammePermission.Permissions)
                {
                    permissions.Add(
                        $"{DomainEnumTexts.ResourceManager.GetString(item.PermissionType.Name + "_" + item.Permission, new System.Globalization.CultureInfo(Common.Localization.SystemLocalization.Bg_BG))} " +
                        $"на {DomainEnumTexts.ResourceManager.GetString(item.PermissionType.Name, new System.Globalization.CultureInfo(Common.Localization.SystemLocalization.Bg_BG))}");
                }

                errors.Add($"\tОперативна програма {groupedProgrammePermission.Programme.ToUpper()}\r\n\t\t{string.Join(";\r\n\t\t", permissions.ToArray())}");
            }

            return errors;
        }

        public void SetUserPermissions(PermissionAggregation permissions)
        {
            // Set CommonPermissions
            var unchangedCommonPermissions = from up in this.UserPermissions.OfType<CommonPermission>()
                                             join p in permissions.CommonPermissions on new { up.Permission } equals new { p.Permission }
                                             where p.IsSet
                                             select new { up, tp = p };

            // Delete missing permissions
            var missingCommonPermissions = this.UserPermissions
                .OfType<CommonPermission>()
                .Except(unchangedCommonPermissions.Select(p => p.up))
                .ToList();

            foreach (var missingCommonPermission in missingCommonPermissions)
            {
                this.UserPermissions.Remove(missingCommonPermission);
            }

            // Add new permissions
            var newCommonPermissions = permissions.CommonPermissions
                .Where(tp => tp.IsSet)
                .Except(unchangedCommonPermissions.Select(p => p.tp))
                .ToList();

            foreach (var np in newCommonPermissions)
            {
                this.UserPermissions.Add(CommonPermission.CreateCommonPermissionEntity(np.PermissionType, np.Permission));
            }

            // Set ProgrammePermissions
            var unchangedProgrammePermissions = from up in this.UserPermissions.OfType<ProgrammePermission>()
                                                join p in permissions.ProgrammePermissions on new { up.ProgrammeId, up.Permission } equals new { p.ProgrammeId, p.Permission }
                                                where p.IsSet
                                                select new { up, tp = p };

            // Delete missing permissions
            var missingProgrammePermissions = this.UserPermissions
                .OfType<ProgrammePermission>()
                .Except(unchangedProgrammePermissions.Select(p => p.up))
                .ToList();

            foreach (var missingProgrammePermission in missingProgrammePermissions)
            {
                this.UserPermissions.Remove(missingProgrammePermission);
            }

            // Add new permissions
            var newProgrammePermissions = permissions.ProgrammePermissions
                .Where(tp => tp.IsSet)
                .Except(unchangedProgrammePermissions.Select(p => p.tp))
                .ToList();

            foreach (var np in newProgrammePermissions)
            {
                this.UserPermissions.Add(ProgrammePermission.CreateProgrammePermissionEntity(np.ProgrammeId, np.PermissionType, np.Permission));
            }

            this.ModifyDate = DateTime.Now;
        }

        public PermissionAggregation GetUserPermissions(int[] programmeIds)
        {
            var commonPermissions = this.UserPermissions
                .OfType<CommonPermission>()
                .Select(p => new CommonPermissionAggregationItem(p.PermissionType, p.Permission, true))
                .ToList();

            var programmePermissions = this.UserPermissions
                .OfType<ProgrammePermission>()
                .Select(p => new ProgrammePermissionAggregationItem(p.ProgrammeId, p.PermissionType, p.Permission, true))
                .ToList();

            return new PermissionAggregation(programmeIds, commonPermissions, programmePermissions);
        }

        #endregion //UserPermissions

        public void AcceptGDPRDeclaration()
        {
            this.GDPRDeclarationAcceptDate = DateTime.Now;
        }

        public void AcceptDeclaration(int declarationId)
        {
            this.UserDeclarations.Add(new UserDeclaration()
            {
                UserId = this.UserId,
                DeclarationId = declarationId,
                AcceptDate = DateTime.Now,
            });
        }
    }
}
