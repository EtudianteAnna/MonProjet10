
namespace PatientFrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurer le logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Patients}/{action=Index}/{id?}");

            app.Run();
        }
    }
}