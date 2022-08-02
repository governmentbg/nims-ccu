using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Xml.Serialization;
using Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public partial class SapFile : IAggregateRoot
    {
        private SapDocument sapDocument;
        private object lockObj = new object();

        public SapFile()
        {
        }

        public SapFile(int sapSchemaId, Guid fileKey, string fileName, string xml, int userId)
        {
            this.Status = SapFileStatus.New;
            this.SapSchemaId = sapSchemaId;
            this.FileKey = fileKey;
            this.FileName = fileName;
            this.Xml = xml;

            var document = this.GetDocument();
            this.SapKey = document.SapKey;
            this.SapDate = document.Date
                .AddHours(document.Time.Hour)
                .AddMinutes(document.Time.Minute)
                .AddSeconds(document.Time.Second)
                .AddMilliseconds(document.Time.Millisecond);
            this.SapUser = document.SapUser;

            this.CreatedByUserId = userId;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int SapFileId { get; set; }

        public int SapSchemaId { get; set; }

        public SapFileStatus Status { get; set; }

        public Guid FileKey { get; set; }

        public string FileName { get; set; }

        public string SapKey { get; set; }

        public DateTime SapDate { get; set; }

        public string SapUser { get; set; }

        public string Xml { get; private set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public SapDocument GetDocument()
        {
            if (sapDocument == null)
            {
                lock (lockObj)
                {
                    if (sapDocument == null)
                    {
                        using (StringReader sr = new StringReader(this.Xml))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(SapDocument));
                            sapDocument = (SapDocument)serializer.Deserialize(sr);
                        }
                    }
                }
            }

            return sapDocument;
        }
    }

    public class SapFileMap : EntityTypeConfiguration<SapFile>
    {
        public SapFileMap()
        {
            // Primary Key
            this.HasKey(t => t.SapFileId);

            // Properties
            this.Property(t => t.SapFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SapSchemaId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.FileKey)
                .IsRequired();
            this.Property(t => t.FileName)
                .HasMaxLength(500)
                .IsRequired();
            this.Property(t => t.SapKey)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.SapDate)
                .IsRequired();
            this.Property(t => t.SapUser)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.Xml)
                .IsRequired();
            this.Property(t => t.CreatedByUserId)
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
            this.ToTable("SapFiles");
            this.Property(t => t.SapFileId).HasColumnName("SapFileId");
            this.Property(t => t.SapSchemaId).HasColumnName("SapSchemaId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.FileKey).HasColumnName("FileKey");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.SapKey).HasColumnName("SapKey");
            this.Property(t => t.SapDate).HasColumnName("SapDate");
            this.Property(t => t.SapUser).HasColumnName("SapUser");
            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
