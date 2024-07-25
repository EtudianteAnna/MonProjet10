using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedicalNotesService.Data;
using MedicalNotesService.Models;
using MedicalNotesService.Repositories;
using MedicalNotesService.Services;
using Microsoft.Extensions.Logging;

namespace MedicalNotesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration des fichiers appsettings
            builder.Configuration
                .AddJsonFile("appsettingsMediacal.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettingsMediacal.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            // Ajouter les services au conteneur
            builder.Services.AddControllers();
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection(nameof(MongoDbSettings)));
            builder.Services.AddSingleton<MongoDbContext>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<INoteService, NoteService>();

            // Ajouter Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Ajouter la prise en charge de la journalisation
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            // Configurer le pipeline de requêtes HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical Notes API V1");
                    c.RoutePrefix = string.Empty; // Pour accéder à Swagger directement à l'URL racine
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}