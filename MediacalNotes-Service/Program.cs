using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MedicalNotesService.Data;
using MedicalNotesService.Models;
using MedicalNotesService.Repositories;
using MedicalNotesService.Services;
using Microsoft.Extensions.Options;
namespace MediacalNotes_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection(nameof(MongoDbSettings)));
            builder.Services.AddSingleton<MongoDbContext>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<INoteService, NoteService>();

            // Add the following line to support logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
