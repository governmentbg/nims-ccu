using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.InvestmentPriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammePriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.SpecificTargets;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public abstract partial class MapNode : IAggregateRoot
    {
        public MapNode()
        {
            this.MapNodeIndicators = new List<MapNodeIndicator>();
            this.MapNodeDocuments = new List<MapNodeDocument>();
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

        public virtual ICollection<MapNodeIndicator> MapNodeIndicators { get; set; }

        public virtual ICollection<MapNodeDocument> MapNodeDocuments { get; set; }

        public virtual string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt;
                }
                else 
                {
                    return this.Name;
                }
            }
        }
    }

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
            this.Map<InvestmentPriority>(t => t.Requires("Type").HasValue("InvestmentPriority"));
            this.Map<SpecificTarget>(t => t.Requires("Type").HasValue("SpecificTarget"));
        }
    }
}
