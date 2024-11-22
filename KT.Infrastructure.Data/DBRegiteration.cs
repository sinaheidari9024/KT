namespace KT.Infrastructure.Data
{
    public static class DBRegiteration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(KTRepository<>));
            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(KTRepository<>));

            services.AddDbContextPool<KTDbContext>(options =>
            {
                options.EnableThreadSafetyChecks(false);
                options.EnableSensitiveDataLogging();

                if (bool.Parse(configuration["db:useinmemory"]) == true)
                {
                    options.UseInMemoryDatabase(configuration["db:inmemorydbname"]);
                }
                else
                {
                    options.UseSqlServer(configuration["DB:SQLConnection"], opt =>
                        {
                            opt.CommandTimeout(30);
                            opt.MigrationsAssembly("KT.Infrastructure.Data");
                            opt.MigrationsHistoryTable("MigrationsHistory", KTDbContext.SCHEMA);
                            opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                }

            });

            return services;
        }
    }
}
