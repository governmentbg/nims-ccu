using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProcedureManuals;

namespace Eumis.Domain.OperationalMap.Programmes
{
    public partial class Programme : MapNodeWithDirections, INotificationEventEmitter
    {
        public Programme()
        {
            this.ProgrammeProcedureManuals = new List<ProgrammeProcedureManual>();
            this.ProgrammeApplicationDocuments = new List<ProgrammeApplicationDocument>();

            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public Programme(
            string code,
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            int? companyId)
            : base(code, name, name, nameAlt)
        {
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;
            this.CompanyId = companyId;

            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public override MapNodeType Type
        {
            get
            {
                return MapNodeType.Programme;
            }
        }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public int? CompanyId { get; set; }

        public virtual ICollection<ProgrammeProcedureManual> ProgrammeProcedureManuals { get; set; }

        public virtual ICollection<ProgrammeApplicationDocument> ProgrammeApplicationDocuments { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeMap : EntityTypeConfiguration<Programme>
    {
        public ProgrammeMap()
        {
            // Properties
            this.Property(t => t.Code)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
        }
    }
}
