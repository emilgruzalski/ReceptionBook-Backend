// Using directives to include necessary namespaces for the application.
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using ReceptionBook;
using ReceptionBook.Extensions;
using Contracts;
using Repository;
using Microsoft.AspNetCore.Mvc;
using EmailService;
using Microsoft.AspNetCore.Identity;

// Entry point for the web application builder.
var builder = WebApplication.CreateBuilder(args);

// Configure NLog for logging with configurations loaded from a file.
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Extension method calls to configure services and middleware components.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

// Configuration for email services using settings from app configuration.
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Configuration for Data Protection Token lifespan.
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

// API behavior configuration to suppress automatic model state validation filter.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configure controllers to support XML and not acceptable responses.
builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(ReceptionBook.Presentation.AssemblyReference).Assembly);

// Adds services for API exploration and Swagger for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application.
var app = builder.Build();

// Configure global exception handler.
app.UseExceptionHandler(opt => { });

// HTTP Strict Transport Security middleware, only in production.
if (app.Environment.IsProduction())
    app.UseHsts();

// Swagger middleware, only in development for API testing and documentation.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirection to enforce HTTPS.
app.UseHttpsRedirection();

// Middleware to serve static files.
app.UseStaticFiles();

// Middleware to forward proxied headers to the current request.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

// Enable CORS with the previously configured policy.
app.UseCors("CorsPolicy");

// Authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

// Maps controllers to endpoints.
app.MapControllers();

// Extension method to migrate database on application start and then run the application.
app.MigrateDatabase().Run();
