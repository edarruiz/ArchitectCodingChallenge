using ArchitectCodingChallenge.Application;
using ArchitectCodingChallenge.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddSwaggerGen();

// Adds the application and infrastructure services needed for the application
builder.Services
    .AddApplication() // Adds the application layer services to the pipeline
    .AddInfrastructure(); // Adds the infrastructure layer services to the pipeline


var app = builder.Build();

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
