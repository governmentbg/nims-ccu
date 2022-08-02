using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities
{
    public partial class MapRegion
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string Path { get; set; }
        public NutsLevel NutsLevel { get; set; }
        public int RegionId { get; set; }
        public virtual Map Map { get; set; }

        public string TransName
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

    public class MapRegionMap : EntityTypeConfiguration<MapRegion>
    {
        public MapRegionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Path)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MapRegions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MapId).HasColumnName("MapId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Path).HasColumnName("Path");
            this.Property(t => t.NutsLevel).HasColumnName("NutsLevel");
            this.Property(t => t.RegionId).HasColumnName("RegionId");

            // Relationships
            this.HasRequired(t => t.Map)
                .WithMany(t => t.MapRegions)
                .HasForeignKey(d => d.MapId);
        }
    }
}
