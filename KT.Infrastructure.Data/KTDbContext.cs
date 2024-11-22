using KT.Infrastructure.Data.DBConfigurations;

namespace KT.Infrastructure.Data
{
    public class KTDbContext : DbContext
    {
        public const string SCHEMA = "KT";
        public KTDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);

            modelBuilder.ApplyConfiguration(new CustomerDbConfig());
        }
    }
}