using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez des fichiers de configuration supplémentaires.
builder.Configuration
    .AddJsonFile("appsettingsapigateway.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettingsapigateway.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ajoutez les services nécessaires.
builder.Services.AddOcelot();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure le pipeline de requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
        c.RoutePrefix = string.Empty; // Pour accéder à Swagger directement à la racine de l'URL
    });
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Swagger filter for Ocelot
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), subApp =>
{
    subApp.UseOcelot().Wait();
});

app.Run();