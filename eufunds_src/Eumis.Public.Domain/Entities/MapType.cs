using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities
{
    public partial class MapType
    {
        public MapType()
        {
            this.Maps = new List<Map>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Map> Maps { get; set; }
    }

    public class MapTypeMap : EntityTypeConfiguration<MapType>
    {
        public MapTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MapTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
