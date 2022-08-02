using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes
{
    public partial class Programme : MapNodeWithCategories
    {
        public Programme()
        {
            this.ProgrammeInstitutions = new List<MapNodeInstitution>();
        }

        public override MapNodeType Type
        {
            get
            {
                return MapNodeType.Programme;
            }
        }

        public string RegulationNumber { get; set; }
        public DateTime? RegulationDate { get; set; }
        public string PortalName { get; set; }
        public string PortalNameAlt { get; set; }
        public string PortalShortNameAlt { get; set; }
        public int PortalOrderNum { get; set; }
        public string Description { get; set; }
        public string DescriptionAlt { get; set; }
        public int? CompanyId { get; set; }
        public int? ProgrammeGroupId { get; set; }

        public virtual ICollection<MapNodeInstitution> ProgrammeInstitutions { get; set; }
        public virtual ICollection<MapNodeFinanceSource> MapNodeFinanceSources { get; set; }

        public override string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.PortalNameAlt;
                }
                else
                {
                    return this.PortalName;
                }
            }
        }
    }

    public class ProgrammeMap : EntityTypeConfiguration<Programme>
    {
        public ProgrammeMap()
        {
            // Properties
            this.Property(t => t.Code)
                .IsRequired();
            this.Property(t => t.ShortName)
                .IsRequired();
            this.Property(t => t.PortalName)
                .IsRequired();
            this.Property(t => t.PortalNameAlt)
                .IsRequired();
            this.Property(t => t.PortalShortNameAlt)
                .IsRequired();
            this.Property(t => t.PortalOrderNum)
                .IsRequired();
            this.Property(t => t.RegulationNumber)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.Property(t => t.RegulationNumber).HasColumnName("RegulationNumber");
            this.Property(t => t.RegulationDate).HasColumnName("RegulationDate");
            this.Property(t => t.PortalName).HasColumnName("PortalName");
            this.Property(t => t.PortalNameAlt).HasColumnName("PortalNameAlt");
            this.Property(t => t.PortalShortNameAlt).HasColumnName("PortalShortNameAlt");
            this.Property(t => t.PortalOrderNum).HasColumnName("PortalOrderNum");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.ProgrammeGroupId).HasColumnName("ProgrammeGroupId");
        }
    }
}
