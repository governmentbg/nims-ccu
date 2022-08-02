using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public partial class RegOfferXml : RioXmlDocumentWithFiles<Offer, RegOfferXmlFile>, IAggregateRoot
    {
        private RegOfferXml()
        {
        }

        public RegOfferXml(int registrationId, int contractDifferentiatedPositionId, string xml)
        {
            this.Gid = Guid.NewGuid();

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.RegistrationId = registrationId;
            this.ContractDifferentiatedPositionId = contractDifferentiatedPositionId;
            this.Status = RegOfferStatus.Draft;

            this.SetXmlInt(xml);
        }

        public int RegOfferXmlId { get; set; }

        public Guid Gid { get; set; }

        public int RegistrationId { get; set; }

        public int ContractDifferentiatedPositionId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public RegOfferStatus Status { get; set; }

        public byte[] Version { get; set; }

        public string Tenderer { get; set; }

        public string Uin { get; set; }

        public UinType? UinType { get; set; }

        public string Email { get; set; }

        public override IList<RegOfferXmlFile> XmlFiles
        {
            get
            {
                var offerDocument = this.GetDocument();

                return EnumerableExtensions.Concat(
                    offerDocument.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection)
                    .Select(ad =>
                        new RegOfferXmlFile(ad)
                        {
                            Type = RegOfferXmlFileType.AttachedDoc,
                        }),
                    offerDocument.GetFiles(
                        d => d.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc }))
                    .Select(ad =>
                        new RegOfferXmlFile(ad)
                        {
                            Type = RegOfferXmlFileType.Signature,
                        })).ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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

            this.Property(t => t.Tenderer)
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .HasMaxLength(200);

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
            this.Property(t => t.Tenderer).HasColumnName("Tenderer");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");

            this.Ignore(t => t.XmlFiles);
        }
    }
}
