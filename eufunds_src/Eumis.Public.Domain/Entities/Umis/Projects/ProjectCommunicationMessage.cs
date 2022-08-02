using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public class ProjectCommunicationMessage
    {
        public string Xml { get; private set; }
        public string Hash { get; private set; }
        public DateTime? MessageDate { get; set; }
        public string Content { get; set; }
    }

    public class ProjectCommunicationMessageMap : ComplexTypeConfiguration<ProjectCommunicationMessage>
    {
        public ProjectCommunicationMessageMap()
        {
            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10);
        }
    }
}
