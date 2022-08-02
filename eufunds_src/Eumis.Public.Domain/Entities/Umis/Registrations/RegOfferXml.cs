using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Registrations
{
    public partial class RegOfferXml : IAggregateRoot
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int RegOfferXmlId { get; set; }

        public Guid Gid { get; set; }

        public int RegistrationId { get; set; }

        public int ContractDifferentiatedPositionId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class RegOfferXmlMap : EntityTypeConfiguration<RegOfferXml>
    {
        public RegOfferXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.RegOfferXmlId);

            // Properties
            this.Property(t => t.RegOfferXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.RegistrationId)
                .IsRequired();

            this.Property(t => t.ContractDifferentiatedPositionId)
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
            this.ToTable("RegOfferXmls");
            this.Property(t => t.RegOfferXmlId).HasColumnName("RegOfferXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.RegistrationId).HasColumnName("RegistrationId");
            this.Property(t => t.ContractDifferentiatedPositionId).HasColumnName("ContractDifferentiatedPositionId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");
        }
    }
}
