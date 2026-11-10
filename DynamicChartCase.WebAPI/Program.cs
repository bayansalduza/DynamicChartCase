using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using DynamicChartCase.Application.Services;
using DynamicChartCase.Domain.Repositories;
using DynamicChartCase.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin();
        }
        else
        {
            var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? throw new InvalidOperationException("Configure Cors:AllowedOrigins in production.");
            policy.WithOrigins(origins);
        }

        policy.AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IChartRepository, ChartRepository>();

builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<IMetadataService, MetadataService>();
builder.Services.AddScoped<IChartDataService, ChartDataService>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.ForwardLimit = 1;
    if (IPAddress.TryParse(builder.Configuration["ReverseProxy:Address"], out var address))
    {
        options.KnownProxies.Add(address);
    }
});

var app = builder.Build();
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));

app.Run();
