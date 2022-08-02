using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionDistributionUserDO
    {
        public EvalSessionDistributionUserDO()
        {
            this.IsDeleted = false;
        }

        public EvalSessionDistributionUserDO(EvalSessionDistributionUser evalSessionDistributionUser, string username, string fullname)
        {
            this.EvalSessionId = evalSessionDistributionUser.EvalSessionId;
            this.EvalSessionDistributionId = evalSessionDistributionUser.EvalSessionDistributionId;
            this.EvalSessionUserId = evalSessionDistributionUser.EvalSessionUserId;
            this.Username = username;
            this.Fullname = fullname;
            this.IsDeleted = evalSessionDistributionUser.IsDeleted;
            this.IsDeletedNote = evalSessionDistributionUser.IsDeletedNote;
        }

        public int EvalSessionId { get; set; }

        public int? EvalSessionDistributionId { get; set; }

        public int EvalSessionUserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

    }

    public class EvalSessionDistributionUser
    {
        public EvalSessionDistributionUser()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionDistributionId { get; set; }

        public int EvalSessionUserId { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public virtual EvalSessionDistribution EvalSessionDistribution { get; set; }
    }

    public class EvalSessionDistributionUserMap : EntityTypeConfiguration<EvalSessionDistributionUser>
    {
        public EvalSessionDistributionUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId, t.EvalSessionUserId });

            // Properties
            this.Property(t => t.EvalSessionUserId)
                .IsRequired();

            this.Property(t => t.IsDeleted)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionDistributionUsers");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionDistributionId).HasColumnName("EvalSessionDistributionId");
            this.Property(t => t.EvalSessionUserId).HasColumnName("EvalSessionUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");

            //Relationships
            this.HasRequired(t => t.EvalSessionDistribution)
                .WithMany(t => t.EvalSessionDistributionUsers)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId } );
        }
    }
}
