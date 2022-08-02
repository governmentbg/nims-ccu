using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;
using Eumis.Public.Domain.Entities.Umis.Core;
using System.Collections.Generic;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractRegistration : IAggregateRoot, IEventEmitter
    {
        public int ContractRegistrationId { get; set; }

        public string Email { get; set; }

        public PersonalUinType UinType { get; set; }

        public string Uin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        //server only
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public string ActivationCode { get; set; }

        public string PasswordRecoveryCode { get; set; }

        public bool IsActive
        {
            get
            {
                return string.IsNullOrEmpty(this.ActivationCode);
            }
        }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class ContractRegistrationMap : EntityTypeConfiguration<ContractRegistration>
    {
        public ContractRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractRegistrationId);

            // Properties
            this.Property(t => t.ContractRegistrationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Email)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.Uin)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(t => t.UinType)
                .IsRequired();
            this.Property(t => t.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.LastName)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.Phone)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.ActivationCode)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.PasswordRecoveryCode)
                .HasMaxLength(50)
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
            this.ToTable("ContractRegistrations");
            this.Property(t => t.ContractRegistrationId).HasColumnName("ContractRegistrationId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.ActivationCode).HasColumnName("ActivationCode");
            this.Property(t => t.PasswordRecoveryCode).HasColumnName("PasswordRecoveryCode");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.Ignore(t => t.IsActive);
        }
    }
}
