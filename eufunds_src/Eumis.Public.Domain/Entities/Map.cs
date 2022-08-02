using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities
{
    public partial class Map
    {
        public Map()
        {
            this.MapRegions = new List<MapRegion>();
        }

        public int Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public NutsLevel NutsLevel { get; set; }
        public int RegionId { get; set; }
        public virtual ICollection<MapRegion> MapRegions { get; set; }
        public virtual MapType MapType { get; set; }
    }

    public class MapMap : EntityTypeConfiguration<Map>
    {
        public MapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Maps");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.NutsLevel).HasColumnName("NutsLevel");

            // Relationships
            this.HasRequired(t => t.MapType)
                .WithMany(t => t.Maps)
                .HasForeignKey(d => d.Type);
        }
    }
}
