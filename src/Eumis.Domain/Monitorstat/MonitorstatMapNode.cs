using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Monitorstat
{
    public class MonitorstatMapNode : IAggregateRoot
    {
        public int MonitorstatMapNodeId { get; set; }

        public int MapNodeId { get; set; }

        public MonitorstatMapNodeType Type { get; set; }

        public Guid MonitorstatGid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MonitorstatMapNodeMap : EntityTypeConfiguration<MonitorstatMapNode>
    {
        public MonitorstatMapNodeMap()
        {
            // Primary Key
            this.HasKey(t => t.MonitorstatMapNodeId);

            // Properties
            this.Property(t => t.MonitorstatMapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.MapNodeId)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.MonitorstatGid)
                .IsRequired();

            this.ToTable("MonitorstatMapNodes");
            this.Property(t => t.MonitorstatMapNodeId).HasColumnName("MonitorstatMapNodeId");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.MonitorstatGid).HasColumnName("MonitorstatGid");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
