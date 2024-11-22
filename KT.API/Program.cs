using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.IsProduction() ? "" : $".{builder.Environment.EnvironmentName}";
builder.Configuration.AddJsonFile($"appsettings{environmentName}.json", false, true);

var services = builder.Services;
var configuration = builder.Configuration;

//register built-in services
services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddMvcOptions(options =>
{
    options.Filters.AddService<ExceptionHandler>();
    options.Filters.AddService<InputValidationFilter>();
});

services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

services.AddHttpContextAccessor();
services.AddAuthorization();
services.AddLocalization();
services.AddCors(options => { options.AddPolicy("KTCorsPolicy", builder => { builder.WithOrigins(configuration["Cors:Origins"].Split(";")).AllowAnyHeader().AllowAnyMethod(); }); });

//register third-party services
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c => { var filePath = Path.Combine(AppContext.BaseDirectory, "KT.API.xml"); c.IncludeXmlComments(filePath); });
services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblies(new List<Assembly> { typeof(IMediatRMarker).Assembly, typeof(IAPIMarker).Assembly });
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IMediatRMarker).Assembly, typeof(IAPIMarker).Assembly));

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());

services.AddDatabase(configuration).AddApiServices();

//app pipeline
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("en-GB").AddSupportedCultures(new string[] { "en-GB" });
app.UseRequestLocalization(localizationOptions);

app.UseCors("KTCorsPolicy");

app.UseAuthorization();
app.MapControllers();

Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Welcome to KT-API. You have successfully launched KT-API");
Console.ForegroundColor = ConsoleColor.White;

app.Run();
