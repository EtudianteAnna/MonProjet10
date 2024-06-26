using DiabetesRiskAssessment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DiabetesRiskAssessment.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IPatientService, PatientService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:3001/"); // URL de votre service de patients
});
builder.Services.AddHttpClient<INotesService, NotesService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:6200/"); // URL de votre service de notes
});
builder.Services.AddScoped<IRiskAssessmentService, RiskAssessmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diabetes Risk Assessment API V1");
        
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

