using KT.Infrastructure.Data.Cache;

namespace KT.Application
{
    public static class APIRegisteration
    {

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionHandler>();
            services.AddSingleton<InputValidationFilter>();
            services.AddSingleton<ICacheService, CacheService>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISMSService, SMSService>();


            return services;
        }
    }
}
