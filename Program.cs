using apiWebDB.BaseDados;
using apiWebDB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApidbContext>();
builder.Services.AddScoped<clientesService>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API PARADIGMAS",
        Description = "Uma API Desenvolvida em ambiente acadêmico da Horus Faculdades",
        Contact = new OpenApiContact
        {
            Name = "Contato",
            Url = new Uri("https://github.com/4biDeN")
        },
        License = new OpenApiLicense
        {
            Name = "Linçenca By eu Mesmo",
            Url = new Uri("https://github.com/4biDeN")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Logging.AddFile("Logs/WebDB-{Date}.log");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
