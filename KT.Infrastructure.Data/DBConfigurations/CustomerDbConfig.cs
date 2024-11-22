namespace KT.Infrastructure.Data.DBConfigurations
{
    public class CustomerDbConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.EmailAddress).IsRequired().HasMaxLength(75).IsUnicode(false);
            builder.Property(c => c.MobileNo).IsRequired().HasMaxLength(15).IsUnicode(false);
            builder.Property(c => c.IsActive).IsRequired();
            builder.HasIndex(c => c.MobileNo).IsUnique();
            builder.HasIndex(c => c.EmailAddress).IsUnique();
        }
    }
}

