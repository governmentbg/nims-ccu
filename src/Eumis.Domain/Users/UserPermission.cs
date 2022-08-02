using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Domain.Users
{
    public abstract class UserPermission
    {
        internal static readonly IDictionary<Type, Type> EnumTypeToEntityType = new Dictionary<Type, Type>()
        {
            { typeof(OperationalMapPermissions), typeof(OperationalMapPermission) },
            { typeof(ProcedurePermissions), typeof(ProcedurePermission) },
            { typeof(ProjectPermissions), typeof(ProjectPermission) },
            { typeof(UserAdminPermissions), typeof(UserAdminPermission) },
            { typeof(NewsPermissions), typeof(NewsPermission) },
            { typeof(MonitoringPermissions), typeof(MonitoringPermission) },
            { typeof(SapInterfacePermissions), typeof(SapInterfacePermission) },
            { typeof(InterfacesPermissions), typeof(InterfacesPermission) },
            { typeof(GuidancePermissions), typeof(GuidancePermission) },
            { typeof(CompanyPermissions), typeof(CompanyPermission) },
            { typeof(OperationalMapAdminPermissions), typeof(OperationalMapAdminPermission) },
            { typeof(RegistrationPermissions), typeof(RegistrationPermission) },
            { typeof(ContractRegistrationPermissions), typeof(ContractRegistrationPermission) },
            { typeof(EvalSessionPermissions), typeof(EvalSessionPermission) },
            { typeof(ContractPermissions), typeof(ContractPermission) },
            { typeof(ContractCommunicationPermissions), typeof(ContractCommunicationPermission) },
            { typeof(ContractReportPermissions), typeof(ContractReportPermission) },
            { typeof(MonitoringFinancialControlPermissions), typeof(MonitoringFinancialControlPermission) },
            { typeof(IndicatorPermissions), typeof(IndicatorPermission) },
            { typeof(ProjectDossierPermissions), typeof(ProjectDossierPermission) },
            { typeof(ActionLogPermissions), typeof(ActionLogPermission) },
        };

        protected UserPermission()
        {
        }

        protected UserPermission(string permission)
        {
            this.PermissionString = permission;
        }

        public int UserPermissionId { get; set; }

        public int UserId { get; set; }

        public string PermissionString { get; set; }

        public virtual User User { get; set; }

        public static Type GetPermissionEntityType(Type permissionType)
        {
            return EnumTypeToEntityType[permissionType];
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class UserPermissionMap : EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.UserPermissionId);

            // Properties
            this.Property(t => t.UserPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.PermissionString)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserPermissions");
            this.Property(t => t.UserPermissionId).HasColumnName("UserPermissionId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PermissionString).HasColumnName("Permission");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete();

            // Derived entities
            this.Map<OperationalMapPermission>(t => t.Requires("PermissionType").HasValue("OperationalMap"));
            this.Map<ProcedurePermission>(t => t.Requires("PermissionType").HasValue("Procedure"));
            this.Map<ProjectPermission>(t => t.Requires("PermissionType").HasValue("Project"));
            this.Map<ContractPermission>(t => t.Requires("PermissionType").HasValue("Contract"));
            this.Map<UserAdminPermission>(t => t.Requires("PermissionType").HasValue("UserAdmin"));
            this.Map<CompanyPermission>(t => t.Requires("PermissionType").HasValue("Company"));
            this.Map<NewsPermission>(t => t.Requires("PermissionType").HasValue("News"));
            this.Map<MonitoringPermission>(t => t.Requires("PermissionType").HasValue("Monitoring"));
            this.Map<SapInterfacePermission>(t => t.Requires("PermissionType").HasValue("SapInterface"));
            this.Map<InterfacesPermission>(t => t.Requires("PermissionType").HasValue("Interfaces"));
            this.Map<GuidancePermission>(t => t.Requires("PermissionType").HasValue("Guidance"));
            this.Map<OperationalMapAdminPermission>(t => t.Requires("PermissionType").HasValue("OperationalMapAdmin"));
            this.Map<RegistrationPermission>(t => t.Requires("PermissionType").HasValue("Registration"));
            this.Map<ContractRegistrationPermission>(t => t.Requires("PermissionType").HasValue("ContractRegistration"));
            this.Map<ContractCommunicationPermission>(t => t.Requires("PermissionType").HasValue("ContractCommunication"));
            this.Map<ContractReportPermission>(t => t.Requires("PermissionType").HasValue("ContractReport"));
            this.Map<MonitoringFinancialControlPermission>(t => t.Requires("PermissionType").HasValue("MonitoringFinancialControl"));
            this.Map<IndicatorPermission>(t => t.Requires("PermissionType").HasValue("Indicator"));
            this.Map<ProjectDossierPermission>(t => t.Requires("PermissionType").HasValue("ProjectDossier"));
            this.Map<EvalSessionPermission>(t => t.Requires("PermissionType").HasValue("EvalSession"));
            this.Map<ActionLogPermission>(t => t.Requires("PermissionType").HasValue("ActionLog"));
        }
    }
}
