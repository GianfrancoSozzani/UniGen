
using AreaStudente.Data;
using Microsoft.EntityFrameworkCore;



namespace AreaStudente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UniGenConn")));

            // Configura la gestione della sessione
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(24); // Timeout di sessione
                options.Cookie.HttpOnly = true; // Impedisce l'accesso ai cookie via JavaScript
                options.Cookie.IsEssential = true; // Cookie essenziale per la sessione
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Strict Transport Security
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Mappa la route predefinita
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Studenti}/{action=Show}/{id?}");

            // Abilita la sessione
            app.UseSession();

            app.Run();
        }
    }
}
