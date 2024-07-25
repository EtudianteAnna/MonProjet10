using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez des fichiers de configuration suppl�mentaires.
builder.Configuration
    .AddJsonFile("appsettingsapigateway.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettingsapigateway.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ajoutez les services n�cessaires.
builder.Services.AddOcelot();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure le pipeline de requ�tes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
        c.RoutePrefix = string.Empty; // Pour acc�der � Swagger directement � la racine de l'URL
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