using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractCommunicationXml : IAggregateRoot, IEventEmitter
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int ContractCommunicationXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public ContractCommunicationType Type { get; set; }

        public ContractCommunicationStatus Status { get; set; }

        public string StatusNote { get; set; }

        public Source Source { get; set; }

        public ContractCommunicationSource DisplaySource
        {
            get
            {
                if (this.Source == Contracts.Source.Beneficiary)
                {
                    return ContractCommunicationSource.Beneficiary;
                }

                switch (this.Type)
                {
                    case ContractCommunicationType.Administrative :
                        return ContractCommunicationSource.AdministrativeAuthority;
                    case ContractCommunicationType.Audit :
                        return ContractCommunicationSource.AuditAuthority;
                    case ContractCommunicationType.Cert :
                        return ContractCommunicationSource.CertAuthority;
                    default:
                        throw new DomainValidationException("Invalid type.");
                }
            }
        }

        public string RegNumber { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? SendDate { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class ContractCommunicationXmlMap : EntityTypeConfiguration<ContractCommunicationXml>
    {
        public ContractCommunicationXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractCommunicationXmlId);

            // Properties
            this.Property(t => t.ContractCommunicationXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Source)
                .IsRequired();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();

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
            this.ToTable("ContractCommunicationXmls");
            this.Property(t => t.ContractCommunicationXmlId).HasColumnName("ContractCommunicationXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.ReadDate).HasColumnName("ReadDate");

            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Content).HasColumnName("Content");

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

            this.Ignore(t => t.DisplaySource);
        }
    }
}
