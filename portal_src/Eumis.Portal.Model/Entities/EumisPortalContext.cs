using System.Data.Entity;
using Eumis.Portal.Model.Entities.Mapping;

namespace Eumis.Portal.Model.Entities
{
    public partial class EumisPortalContext : DbContext
    {
        static EumisPortalContext()
        {
            Database.SetInitializer<EumisPortalContext>(null);
        }

        public EumisPortalContext()
            : base("Name=EumisPortalContext")
        {
        }

        public DbSet<BlobContent> BlobContents { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<GParam> GParams { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LoginCertificate> LoginCertificates { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Nuts1s> Nuts1s { get; set; }
        public DbSet<Nuts2s> Nuts2s { get; set; }
        public DbSet<ProtectedZone> ProtectedZones { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KidCode> KidCodes { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyLegalType> CompanyLegalTypees { get; set; }
        public DbSet<CompanySizeType> CompanySizeTypees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlobContentMap());
            modelBuilder.Configurations.Add(new BlobMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new GParamMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new LoginCertificateMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new Nuts1sMap());
            modelBuilder.Configurations.Add(new Nuts2sMap());
            modelBuilder.Configurations.Add(new ProtectedZoneMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new KidCodeMap());
            modelBuilder.Configurations.Add(new CompanyTypeMap());
            modelBuilder.Configurations.Add(new CompanyLegalTypeMap());
            modelBuilder.Configurations.Add(new CompanySizeTypeMap());
        }
    }
}
