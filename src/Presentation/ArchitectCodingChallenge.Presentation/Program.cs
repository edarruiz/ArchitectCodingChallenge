using ArchitectCodingChallenge.Application;
using ArchitectCodingChallenge.Infrastructure;
using ArchitectCodingChallenge.Presentation.Middlewares;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

// Serilog settings
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithCorrelationId()
    .Enrich.WithProperty("ApplicationName", $"ArchitectCodingChallenge API {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
    .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
    .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

// Services
builder.Services.AddControllers()
    .AddNewtonsoftJson(setup => {
        setup.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "ArchitectCodingChallenge.API",
        Version = "1.0.0",
        Description = "This API is an implementation of the BairesDev Architect Coding Challenge.",
        License = new OpenApiLicense {
            Name = "Copyright (c) 2021, Eric Darruiz, All rights reserved. <This API is licensed under the terms of 'MIT' license.>",
            Url = new Uri("https://opensource.org/license/mit/")
        },
        Contact = new OpenApiContact {
            Email = "ericrda@hotmail.com",
            Name = "Eric Roberto Darruiz",
            Url = new Uri("https://github.com/edarruiz")
        }
    });
});
builder.Services.AddSwaggerGen(setup => {
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ArchitectCodingChallenge.Application.xml"));
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ArchitectCodingChallenge.Domain.xml"));
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ArchitectCodingChallenge.Infrastructure.xml"));
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ArchitectCodingChallenge.Presentation.xml"));
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Adds the application and infrastructure services needed for the application
builder.Services
    .AddApplication() // Adds the application layer services to the pipeline
    .AddInfrastructure(); // Adds the infrastructure layer services to the pipeline


var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestSerilogMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArchitectCodingChallenge.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
