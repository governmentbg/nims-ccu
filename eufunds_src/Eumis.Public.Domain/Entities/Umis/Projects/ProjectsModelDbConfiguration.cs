using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public class ProjectsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectVersionXmlMap());
            modelBuilder.Configurations.Add(new ProjectTypeMap());
            modelBuilder.Configurations.Add(new ProjectFileMap());
            modelBuilder.Configurations.Add(new ProjectFileSignatureMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMessageMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationMessageFileMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationFileMap());
            modelBuilder.Configurations.Add(new ProjectCommunicationFileSignatureMap());
        }
    }
}
