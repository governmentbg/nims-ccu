using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunicationFile : IAggregateRoot
    {
        private ProjectCommunicationFile()
        {
            this.ProjectCommunicationFileSignatures = new List<ProjectCommunicationFileSignature>();
        }

        public ProjectCommunicationFile(
            int projectCommunicationId,
            int projectCommunicationAnswerId,
            Tuple<byte[], string> isunFileTuple,
            List<Tuple<byte[], string>> signaturesTuples)
            : this()
        {
            this.ProjectCommunicationId = projectCommunicationId;
            this.ProjectCommunicationAnswerId = projectCommunicationAnswerId;
            this.File = isunFileTuple.Item1;
            this.FileName = isunFileTuple.Item2;

            foreach (var signaturesTuple in signaturesTuples)
            {
                this.ProjectCommunicationFileSignatures.Add(new ProjectCommunicationFileSignature
                {
                    Signature = signaturesTuple.Item1,
                    FileName = signaturesTuple.Item2,
                });
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProjectCommunicationFileId { get; set; }

        public int ProjectCommunicationId { get; set; }

        public int ProjectCommunicationAnswerId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<ProjectCommunicationFileSignature> ProjectCommunicationFileSignatures { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationFileMap : EntityTypeConfiguration<ProjectCommunicationFile>
    {
        public ProjectCommunicationFileMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCommunicationFileId);

            // Properties
            this.Property(t => t.ProjectCommunicationFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectCommunicationId)
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
            this.ToTable("ProjectCommunicationFiles");
            this.Property(t => t.ProjectCommunicationFileId).HasColumnName("ProjectCommunicationFileId");
            this.Property(t => t.ProjectCommunicationId).HasColumnName("ProjectCommunicationId");
            this.Property(t => t.ProjectCommunicationAnswerId).HasColumnName("ProjectCommunicationAnswerId");
            this.Property(t => t.File).HasColumnName("File");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}