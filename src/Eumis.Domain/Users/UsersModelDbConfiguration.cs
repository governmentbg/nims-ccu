using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Domain.UserTypes;

namespace Eumis.Domain.Users
{
    public class UsersModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            modelBuilder.Configurations.Add(new UserDeclarationMap());
            modelBuilder.Configurations.Add(new PermissionTemplateMap());
            modelBuilder.Configurations.Add(new OperationalMapPermissionMap());
            modelBuilder.Configurations.Add(new ProcedurePermissionMap());
            modelBuilder.Configurations.Add(new ProjectPermissionMap());
            modelBuilder.Configurations.Add(new ContractPermissionMap());
            modelBuilder.Configurations.Add(new ContractCommunicationPermissionMap());
            modelBuilder.Configurations.Add(new ContractReportPermissionMap());
            modelBuilder.Configurations.Add(new MonitoringFinancialControlPermissionMap());
            modelBuilder.Configurations.Add(new IndicatorPermissionMap());
            modelBuilder.Configurations.Add(new CommonPermissionMap());
            modelBuilder.Configurations.Add(new ProgrammePermissionMap());
            modelBuilder.Configurations.Add(new AdminPermissionMap());
            modelBuilder.Configurations.Add(new CompanyPermissionMap());
            modelBuilder.Configurations.Add(new NewsPermissionMap());
            modelBuilder.Configurations.Add(new MonitoringPermissionMap());
            modelBuilder.Configurations.Add(new GuidancePermissionMap());
            modelBuilder.Configurations.Add(new OperationalMapAdminPermissionMap());
            modelBuilder.Configurations.Add(new RegistrationPermissionMap());
            modelBuilder.Configurations.Add(new ContractRegistrationPermissionMap());
            modelBuilder.Configurations.Add(new EvalSessionPermissionMap());
            modelBuilder.Configurations.Add(new ActionLogPermissionMap());
            modelBuilder.Configurations.Add(new SapInterfacePermissionMap());
            modelBuilder.Configurations.Add(new InterfacesPermissionMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
            modelBuilder.Configurations.Add(new UserOrganizationMap());
        }
    }
}
