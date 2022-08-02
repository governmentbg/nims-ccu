using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.ActionLogs
{
    public partial class ActionLog
    {
        private ActionLog()
        {
        }

        public ActionLog(
            ActionLogType actionLogType,
            string action,
            int? aggregateRootId,
            int? childRootId,
            string username,
            string registrationEmail,
            string contractRegistrationEmail,
            string contractAccessCodeEmail,
            string postData,
            string responseData,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string rawUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            Guid requestId,
            string remoteIpAddress)
        {
            this.ActionLogType = actionLogType;
            this.Action = action;
            this.AggregateRootId = aggregateRootId;
            this.ChildRootId = childRootId;
            this.Username = string.IsNullOrEmpty(username) ? null : username;
            this.RegistrationEmail = string.IsNullOrEmpty(registrationEmail) ? null : registrationEmail;
            this.ContractRegistrationEmail = string.IsNullOrEmpty(contractRegistrationEmail) ? null : contractRegistrationEmail;
            this.ContractAccessCodeEmail = string.IsNullOrEmpty(contractAccessCodeEmail) ? null : contractAccessCodeEmail;
            this.PostData = string.IsNullOrEmpty(postData) ? null : postData;
            this.ResponseData = string.IsNullOrEmpty(responseData) ? null : responseData;
            this.RawUrl = rawUrl;
            this.RequestId = requestId;
            this.RemoteIpAddress = remoteIpAddress;
            this.LogDate = DateTime.Now;
        }

        public int ActionLogId { get; set; }

        public ActionLogType ActionLogType { get; set; }

        public string Action { get; set; }

        public int? AggregateRootId { get; set; }

        public int? ChildRootId { get; set; }

        public string Username { get; set; }

        public string RegistrationEmail { get; set; }

        public string ContractRegistrationEmail { get; set; }

        public string ContractAccessCodeEmail { get; set; }

        public string PostData { get; set; }

        public string ResponseData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1056:Uri properties should not be strings", Justification = "It's used this way in the source")]
        public string RawUrl { get; set; }

        public Guid? RequestId { get; set; }

        public string RemoteIpAddress { get; set; }

        public DateTime LogDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ActionLogMap : EntityTypeConfiguration<ActionLog>
    {
        public ActionLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionLogId);

            // Properties
            this.Property(t => t.ActionLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Action)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.Username)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.RegistrationEmail)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.ContractRegistrationEmail)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.ContractAccessCodeEmail)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.RawUrl)
                .IsRequired();
            this.Property(t => t.RemoteIpAddress)
                .HasMaxLength(50)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ActionLogs");
            this.Property(t => t.ActionLogId).HasColumnName("ActionLogId");
            this.Property(t => t.ActionLogType).HasColumnName("ActionLogType");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.AggregateRootId).HasColumnName("AggregateRootId");
            this.Property(t => t.ChildRootId).HasColumnName("ChildRootId");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.RegistrationEmail).HasColumnName("RegistrationEmail");
            this.Property(t => t.ContractRegistrationEmail).HasColumnName("ContractRegistrationEmail");
            this.Property(t => t.ContractAccessCodeEmail).HasColumnName("ContractAccessCodeEmail");
            this.Property(t => t.PostData).HasColumnName("PostData");
            this.Property(t => t.ResponseData).HasColumnName("ResponseData");
            this.Property(t => t.RawUrl).HasColumnName("RawUrl");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
            this.Property(t => t.RemoteIpAddress).HasColumnName("RemoteIpAddress");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
        }
    }
}
