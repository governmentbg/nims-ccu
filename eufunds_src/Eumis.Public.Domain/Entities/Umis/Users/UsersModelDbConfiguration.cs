using System.Data.Entity;
using Eumis.Public.Domain.Entities.Umis.PermissionTemplates;
using Eumis.Public.Domain.Entities.Umis.UserOrganizations;
using Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions;
using Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions;
using Eumis.Public.Domain.Entities.Umis.UserTypes;
using Eumis.Public.Common.Db;

namespace Eumis.Public.Domain.Entities.Umis.Users
{
    public class UsersDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            modelBuilder.Configurations.Add(new PermissionTemplateMap());
            modelBuilder.Configurations.Add(new OperationalMapPermissionMap());
            modelBuilder.Configurations.Add(new ProcedurePermissionMap());
            modelBuilder.Configurations.Add(new ProjectPermissionMap());
            modelBuilder.Configurations.Add(new ContractPermissionMap());
            modelBuilder.Configurations.Add(new ContractCommunicationPermissionMap());
            modelBuilder.Configurations.Add(new ContractReportPermissionMap());
            modelBuilder.Configurations.Add(new MonitoringFinancialControlPermissionMap());
            modelBuilder.Configurations.Add(new SpotCheckPermissionMap());
            modelBuilder.Configurations.Add(new AuditPermissionMap());
            modelBuilder.Configurations.Add(new CertAuthorityCommunicationPermissionMap());
            modelBuilder.Configurations.Add(new AuditAuthorityCommunicationPermissionMap());
            modelBuilder.Configurations.Add(new CommonPermissionMap());
            modelBuilder.Configurations.Add(new ProgrammePermissionMap());
            modelBuilder.Configurations.Add(new AdminPermissionMap());
            modelBuilder.Configurations.Add(new IrregularitySignalPermissionMap());
            modelBuilder.Configurations.Add(new IrregularityPermissionMap());
            modelBuilder.Configurations.Add(new CertificationPermissionMap());
            modelBuilder.Configurations.Add(new CompanyPermissionMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckPermissionMap());
            modelBuilder.Configurations.Add(new EuReimbursedAmountPermissionMap());
            modelBuilder.Configurations.Add(new NewsPermissionMap());
            modelBuilder.Configurations.Add(new MonitoringPermissionMap());
            modelBuilder.Configurations.Add(new GuidancePermissionMap());
            modelBuilder.Configurations.Add(new OperationalMapAdminPermissionMap());
            modelBuilder.Configurations.Add(new RegistrationPermissionMap());
            modelBuilder.Configurations.Add(new ContractRegistrationPermissionMap());
            modelBuilder.Configurations.Add(new EvalSessionPermissionMap());
            modelBuilder.Configurations.Add(new ActionLogPermissionMap());
            modelBuilder.Configurations.Add(new SapInterfacePermissionMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
            modelBuilder.Configurations.Add(new UserOrganizationMap());
        }
    }
}
