using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public abstract class ContractExtension
    {
        public int ContractExtensionId { get; set; }

        public int ContractId { get; set; }

        public abstract ContractExtensionDiscriminator Discriminator { get; }

        public string Value { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractExtensionMap : EntityTypeConfiguration<ContractExtension>
    {
        public ContractExtensionMap()
        {
            this.HasKey(t => t.ContractExtensionId);

            this.Property(t => t.ContractExtensionId).HasColumnName("ContractExtensionId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(t => t.ContractId).HasColumnName("ContractId")
                .IsRequired();

            this.Property(t => t.Value).HasColumnName("Value")
                .IsRequired();

            this.Map<ContractExtensionVesselIdentifier>(t => t.Requires("Discriminator").HasValue((int)ContractExtensionDiscriminator.VesselIdentifier));
        }
    }
}
