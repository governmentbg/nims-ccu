using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractExtensionVesselIdentifier : ContractExtension
    {
        private ContractExtensionVesselIdentifier()
        {
        }

        public ContractExtensionVesselIdentifier(string vesselIdentifier)
        {
            this.Value = vesselIdentifier;
        }

        public override ContractExtensionDiscriminator Discriminator => ContractExtensionDiscriminator.VesselIdentifier;

        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractExtensionVesselIdentifierMap : EntityTypeConfiguration<ContractExtensionVesselIdentifier>
    {
        public ContractExtensionVesselIdentifierMap()
        {
            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractExtensionVesselIdentifiers)
                .WillCascadeOnDelete();
        }
    }
}
