using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text.RegularExpressions;
using Eumis.Public.Domain.Entities.Umis.Core;
using Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions;
using Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations;
using Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions;
using Newtonsoft.Json;

namespace Eumis.Public.Domain.Entities.Umis.Users
{
    public partial class User : IAggregateRoot, IEventEmitter
    {
        public static readonly int SystemUserId = 1;
        public static readonly string SystemUsername = "system";

        public static readonly IDictionary<string, Type> ProgrammePermissionTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "OperationalMapPermissions", typeof(OperationalMapPermissions) },
            { "ProcedurePermissions", typeof(ProcedurePermissions) },
            { "ProjectPermissions", typeof(ProjectPermissions) },
            { "EvalSessionPermissions", typeof(EvalSessionPermissions) },
            { "ContractPermissions", typeof(ContractPermissions) },
            { "ContractCommunicationPermissions", typeof(ContractCommunicationPermissions) },
            { "ContractReportPermissions", typeof(ContractReportPermissions) },
            { "MonitoringFinancialControlPermissions", typeof(MonitoringFinancialControlPermissions) },
            { "SpotCheckPermissions", typeof(SpotCheckPermissions) },
            { "AuditPermissions", typeof(AuditPermissions) },
            { "AuditAuthorityCommunicationPermissions", typeof(AuditAuthorityCommunicationPermissions) },
            { "IrregularitySignalPermissions", typeof(IrregularitySignalPermissions) },
            { "IrregularityPermissions", typeof(IrregularityPermissions) },
            { "CertificationPermissions", typeof(CertificationPermissions) },
            { "EuReimbursedAmountPermissions", typeof(EuReimbursedAmountPermissions) },
            { "CertAuthorityCommunicationPermissions", typeof(CertAuthorityCommunicationPermissions) },
            { "ProjectDossierPermissions", typeof(ProjectDossierPermissions) }

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
            { "CertAuthorityCheckPermissions", typeof(CertAuthorityCheckPermissions) },
            { "MonitoringPermissions", typeof(MonitoringPermissions) },
            { "SapInterfacePermissions", typeof(SapInterfacePermissions) },
            { "ActionLogPermissions", typeof(ActionLogPermissions) }
        };

        private Regex usernameRegex = new Regex(@"^[\w\.]{5,}$", RegexOptions.Singleline);

        private User()
        {
            this.UserPermissions = new List<UserPermission>();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
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

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        private PermissionAggregation permissionTemplate = null;
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

        //server only
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

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
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
