using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public abstract partial class MapNode : IAggregateRoot
    {
        public MapNode()
        {
            this.MapNodeDocuments = new List<MapNodeDocument>();
        }

        protected MapNode(string code, string shortName, string name, string nameAlt)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.Status = MapNodeStatus.Draft;
            this.Code = code;
            this.ShortName = shortName.Substring(0, Math.Min(shortName.Length, 50));
            this.Name = name;
            this.NameAlt = nameAlt;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public abstract MapNodeType Type { get; }

        public MapNodeStatus Status { get; set; }

        public int MapNodeId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual MapNodeRelation MapNodeRelation { get; set; }

        public virtual ICollection<MapNodeDocument> MapNodeDocuments { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MapNodeMap : EntityTypeConfiguration<MapNode>
    {
        public MapNodeMap()
        {
            // Primary Key
            this.HasKey(t => t.MapNodeId);

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Ignore(t => t.Type);

            // Table & Column Mappings
            this.ToTable("MapNodes");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Derived entities
            this.Map<Programme>(t => t.Requires("Type").HasValue("Programme"));
            this.Map<ProgrammePriority>(t => t.Requires("Type").HasValue("ProgrammePriority"));
        }
    }
}
