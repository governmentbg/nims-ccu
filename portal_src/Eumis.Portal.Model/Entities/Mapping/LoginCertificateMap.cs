using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class LoginCertificateMap : EntityTypeConfiguration<LoginCertificate>
    {
        public LoginCertificateMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginCertificateId);

            // Properties
            this.Property(t => t.IP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CertificateThumbprint)
                .HasMaxLength(200);

            this.Property(t => t.ErrorCode)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("LoginCertificates");
            this.Property(t => t.LoginCertificateId).HasColumnName("LoginCertificateId");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.CertificateFile).HasColumnName("CertificateFile");
            this.Property(t => t.CertificateIssuer).HasColumnName("CertificateIssuer");
            this.Property(t => t.CertificatePolicies).HasColumnName("CertificatePolicies");
            this.Property(t => t.CertificateSubject).HasColumnName("CertificateSubject");
            this.Property(t => t.AlternativeSubject).HasColumnName("AlternativeSubject");
            this.Property(t => t.CertificateThumbprint).HasColumnName("CertificateThumbprint");
            this.Property(t => t.ErrorCode).HasColumnName("ErrorCode");
            this.Property(t => t.IsIisErrorOccurred).HasColumnName("IsIisErrorOccurred");
            this.Property(t => t.IsLoginSuccessful).HasColumnName("IsLoginSuccessful");
        }
    }
}
