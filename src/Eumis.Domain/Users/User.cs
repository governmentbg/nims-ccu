using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text.RegularExpressions;
using Eumis.Domain.Core;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.Users.PermissionAggregations;
using Eumis.Domain.Users.ProgrammePermissions;
using Newtonsoft.Json;

namespace Eumis.Domain.Users
{
    public partial class User : IAggregateRoot, IEventEmitter
    {
        public static readonly int SystemUserId = 1;
        public static readonly string SystemUsername = "system";

        public static readonly IDictionary<string, Type> ProgrammePermissionTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "OperationalMapPermissions", typeof(OperationalMapPermissions) },
            { "IndicatorPermissions", typeof(IndicatorPermissions) },
            { "ProcedurePermissions", typeof(ProcedurePermissions) },
            { "ProjectPermissions", typeof(ProjectPermissions) },
            { "EvalSessionPermissions", typeof(EvalSessionPermissions) },
            { "ContractPermissions", typeof(ContractPermissions) },
            { "ContractCommunicationPermissions", typeof(ContractCommunicationPermissions) },
            { "ContractReportPermissions", typeof(ContractReportPermissions) },
            { "MonitoringFinancialControlPermissions", typeof(MonitoringFinancialControlPermissions) },
            { "ProjectDossierPermissions", typeof(ProjectDossierPermissions) },
        };

        public static readonly IDictionary<string, Type> CommonPermissionTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "NewsPermissions", typeof(NewsPermissions) },
            { "GuidancePermissions", typeof(GuidancePermissions) },
            { "OperationalMapAdminPermissions", typeof(OperationalMapAdminPermissions) },
            { "CompanyPermissions", typeof(CompanyPermissions) },
            { "UserAdminPermissions", typeof(UserAdminPermissions) },
            { "RegistrationPermissions", typeof(RegistrationPermissions) },
            { "ContractRegistrationPermissions", typeof(ContractRegistrationPermissions) },
            { "MonitoringPermissions", typeof(MonitoringPermissions) },
            { "SapInterfacePermissions", typeof(SapInterfacePermissions) },
            { "InterfacesPermissions", typeof(InterfacesPermissions) },
            { "ActionLogPermissions", typeof(ActionLogPermissions) },
        };

        private static readonly Regex UsernameRegex = new Regex(@"^[\w\.]{5,}$", RegexOptions.Singleline);

        private PermissionAggregation permissionTemplate = null;

        private User()
        {
            this.UserPermissions = new List<UserPermission>();
            this.UserDeclarations = new List<UserDeclaration>();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public User(
            string username,
            string uin,
            int userTypeId,
            int userOrganizationId,
            PermissionAggregation permissionTemplate,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            bool isActive,
            bool isDeleted,
            bool isLocked)
            : this()
        {
            this.Gid = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(username) || !UsernameRegex.IsMatch(username))
            {
                throw new DomainValidationException("'Username' should be at least 5 character long and contain only letters, numbers, underscores('_') and dots('.').");
            }

            this.Username = username;
            this.UserTypeId = userTypeId;
            this.UserOrganizationId = userOrganizationId;
            this.SetPermissionTemplate(permissionTemplate);
            this.IsDeleted = isDeleted;
            this.IsLocked = isLocked;
            this.IsActive = isActive;
            this.IsSystem = false;

            this.SetAttributes(uin, fullname, email, phone, address, position, userOrganizationId, userTypeId);

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int UserId { get; set; }

        public Guid Gid { get; set; }

        public string Username { get; set; }

        public string Uin { get; set; }

        public int UserTypeId { get; set; }

        public int UserOrganizationId { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }

        public bool IsSystem { get; set; }

        public string PermissionTemplateString { get; set; }

        public string PasswordRecoveryCode { get; set; }

        public string NewPasswordCode { get; set; }

        public int FailedAttempts { get; set; }

        public DateTime? GDPRDeclarationAcceptDate { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        public virtual ICollection<UserDeclaration> UserDeclarations { get; set; }

        // server only
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public PermissionAggregation GetPermissionTemplate(int[] porgrammeIds)
        {
            if (this.permissionTemplate == null)
            {
                this.permissionTemplate = new PermissionAggregation(porgrammeIds, this.PermissionTemplateString);
            }

            return this.permissionTemplate;
        }

        public void SetPermissionTemplate(PermissionAggregation permissionTemplate)
        {
            this.permissionTemplate = permissionTemplate;
            this.PermissionTemplateString = this.permissionTemplate.ToPermissionsString();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Username)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Uin)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Fullname)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.Phone)
                .HasMaxLength(100)
                .IsOptional();

            this.Property(t => t.Address)
                .HasMaxLength(300)
                .IsOptional();

            this.Property(t => t.Position)
                .HasMaxLength(300)
                .IsOptional();

            this.Property(t => t.PasswordRecoveryCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.NewPasswordCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.GDPRDeclarationAcceptDate)
                .IsOptional();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.UserOrganizationId).HasColumnName("UserOrganizationId");
            this.Property(t => t.PermissionTemplateString).HasColumnName("PermissionTemplate");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsLocked).HasColumnName("IsLocked");
            this.Property(t => t.IsSystem).HasColumnName("IsSystem");
            this.Property(t => t.PasswordRecoveryCode).HasColumnName("PasswordRecoveryCode");
            this.Property(t => t.NewPasswordCode).HasColumnName("NewPasswordCode");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.GDPRDeclarationAcceptDate).HasColumnName("GDPRDeclarationAcceptDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
