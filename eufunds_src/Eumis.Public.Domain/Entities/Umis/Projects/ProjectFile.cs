using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public partial class ProjectFile : IAggregateRoot
    {
        private ProjectFile()
        {
            this.ProjectFileSignatures = new List<ProjectFileSignature>();
        }

        public ProjectFile(int projectVersionXmlId, Tuple<byte[], string> isunFileTuple, List<Tuple<byte[], string>> signaturesTuples)
            : this()
        {
            this.ProjectVersionXmlId = projectVersionXmlId;
            this.File = isunFileTuple.Item1;
            this.FileName = isunFileTuple.Item2;

            foreach (var signaturesTuple in signaturesTuples)
            {
                this.ProjectFileSignatures.Add(new ProjectFileSignature
                {
                    Signature = signaturesTuple.Item1,
                    FileName = signaturesTuple.Item2
                });
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProjectFileId { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<ProjectFileSignature> ProjectFileSignatures { get; set; }
    }

    public class ProjectFileMap : EntityTypeConfiguration<ProjectFile>
    {
        public ProjectFileMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectFileId);

            // Properties
            this.Property(t => t.ProjectFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectVersionXmlId)
                .IsRequired();

            this.Property(t => t.File)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProjectFiles");
            this.Property(t => t.ProjectFileId).HasColumnName("ProjectFileId");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.File).HasColumnName("File");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
