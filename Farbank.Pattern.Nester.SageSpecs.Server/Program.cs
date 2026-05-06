using Farbank.Pattern.Nester.SageSpecs.Server.Endpoints;
using Farbank.Pattern.Nester.SageSpecs.Server.Services;
using Farbank.Pattern.Nester.SageSpecs.Server.Services.D365;
using Farbank.Pattern.Nester.SageSpecs.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

//var activeEnvironment = Environment.GetEnvironmentVariable("ActiveEnvironment");
var connectionString = Environment.GetEnvironmentVariable("connection-string");

if (connectionString == null)
    connectionString = builder.Configuration.GetValue<string>("connection-string");

builder.Configuration.AddAzureAppConfiguration(connectionString);
builder.Configuration.AddUserSecrets<Program>();

//var DevEnvionmentValue = builder.Configuration["BlankScheduler:isDevEnvironment"];

//bool parsedValue = true;
//bool isDevEnv = true;
//if (Boolean.TryParse(DevEnvionmentValue, out parsedValue))
//{
//    isDevEnv = parsedValue;
//}
//else
//{
//    isDevEnv = true;
//}

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddSingleton<ID365Service, D365Service>();
builder.Services.AddDbContext<SpecsDbContext>(options => options.UseSqlServer(builder.Configuration.GetSection($"ConnectionStrings:BlankScheduler").Value, providerOptions => providerOptions.EnableRetryOnFailure(20)));
builder.Services.AddScoped<PatternNesterService>();
builder.Services.AddTransient<HttpLoggingHandler>();
builder.Services.AddTransient<OAuth2AuthenticationHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

var azureClientConfiguration = builder.Configuration.GetSection(nameof(D365OAuthConfiguration))
        .Get<D365OAuthConfiguration>();
builder.Services.Configure<D365OAuthConfiguration>(builder.Configuration.GetSection(nameof(D365OAuthConfiguration)));

builder.Services.AddRefitClient<ID365Api>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(azureClientConfiguration.BaseAddress_BlankScheduler))
        .AddHttpMessageHandler<HttpLoggingHandler>()
        .AddHttpMessageHandler<OAuth2AuthenticationHandler>();

builder.Services.AddAuthorization();

// Add CORS policy for development and production
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.WithOrigins("http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
    options.AddPolicy("AllowFrontendProd", policy =>
    {
        policy.WithOrigins("https://frontend.livelybush-941ccabd.westus2.azurecontainerapps.io")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Use CORS in development
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowFrontendDev");
    app.MapOpenApi();
}
else
{
    app.UseCors("AllowFrontendProd");
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var api = app.MapGroup("/api");

api.MapPatternNesterEndpoints();

app.MapDefaultEndpoints();

app.UseFileServer();

app.Run();
