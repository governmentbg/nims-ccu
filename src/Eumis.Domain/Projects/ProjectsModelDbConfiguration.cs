using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Projects
{
    public class ProjectsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectVersionXmlMap());
            modelBuilder.Configurations.Add(new ProjectVersionXmlFileMap());
            modelBuilder.Configurations.Add(new ProjectTypeMap());
            modelBuilder.Configurations.Add(new ProjectFileMap());
            modelBuilder.Configurations.Add(new ProjectFileSignatureMap());
            modelBuilder.Configurations.Add(new ProjectMap());

            modelBuilder.Configurations.Add(new ProjectCommonCommunicationMap());
            modelBuilder.Configurations.Add(new ProjectManagingAuthorityCommunicationMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMessageMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMessageFileMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationFileMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationFileSignatureMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationAnswerMap());

            modelBuilder.Configurations.Add(new ProjectMassManagingAuthorityCommunicationMap());
            modelBuilder.Configurations.Add(new ProjectMassManagingAuthorityCommunicationDocumentMap());
            modelBuilder.Configurations.Add(new ProjectMassManagingAuthorityCommunicationRecipientMap());

            modelBuilder.Configurations.Add(new ProjectMonitorstatRequestMap());
            modelBuilder.Configurations.Add(new ProjectMonitorstatResponseMap());
        }
    }
}
