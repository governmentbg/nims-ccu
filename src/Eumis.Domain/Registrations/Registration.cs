using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Eumis.Domain.Registrations
{
    public partial class Registration : IAggregateRoot, IEventEmitter
    {
        private Registration()
        {
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public Registration(string email, string firstName, string lastName, string phone)
            : this()
        {
            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.ActivationCode = this.GenerateRandomCode();

            ((IEventEmitter)this).Events.Add(new RegistrationCreatedEvent()
            {
                Email = this.Email,
                ActivationCode = this.ActivationCode,
            });
        }

        public int RegistrationId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string ActivationCode { get; set; }

        public string PasswordRecoveryCode { get; set; }

        // server only
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

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

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegistrationMap : EntityTypeConfiguration<Registration>
    {
        public RegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.RegistrationId);

            // Properties
            this.Property(t => t.RegistrationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Email)
                .HasMaxLength(200)
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

            // Table & Column Mappings
            this.ToTable("Registrations");
            this.Property(t => t.RegistrationId).HasColumnName("RegistrationId");
            this.Property(t => t.Email).HasColumnName("Email");
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
