using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using WorkshopManagementServiceBackend.Extensions;
using WorkshopManagementServiceBackend.Interface;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Erstellen des ApplicationBuilders 
            var builder = WebApplication.CreateBuilder(args);

            // Konfiguration basierend auf Umgebung laden
            var env = builder.Environment;
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.WebHost.UseUrls("http://0.0.0.0:5000");

            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.Configure(context.Configuration.GetSection("Kestrel"));
            });

            // Festlegen der MySQL-Serverversion
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            // Hinzuf�gen des DbContext
            builder.Services.AddDbContext<WorkshopmanagementContext>(options =>
            {
                // Verwenden der MySQL-Datenbankverbindung, welche als String in einer Kofigurationsdatei gespeichert ist
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });
            Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

            //Hinzuf�gen der Cors Einstellungen 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddAuthentication("Bearer")
            .AddScheme<KeyCloakAuthentificationOptions, KeyCloakAuthentificationHandler>("Bearer", options =>{});
            // Hinzuf�gen des generischen Repositories
            builder.Services.AddScoped(typeof(Repository<>));

            // Hinzuf�gen der Controller
            builder.Services.AddControllers(config =>
            {
                // Globale Authorize-Filterregel hinzuf�gen
                var policy = new AuthorizationPolicyBuilder(KeyCloakAuthentificationOptions.DefaultScheme)
                        .RequireAuthenticatedUser()
                        .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            //builder.Services.AddKeycloakAuthentication(builder.Services, builder.Configuration);

            // Hinzuf�gen des API-Explorers f�r Endpunkte, ist f�r den sp�teren Einsatz nicht n�tig 
            builder.Services.AddEndpointsApiExplorer();

            // Hinzuf�gen von Swagger f�r die API-Dokumentation
            builder.Services.AddSwaggerGen();

            // Konfigurieren des Logging-Dienstes, um das Debugging von Ef Core zu vereinfachen, muss also nicht in den finalen Build

            builder.Services.AddLogging(builder =>
            {
                // Hinzuf�gen der Konsolenprotokollierung
                builder.AddConsole();
            });

            // Erstellen der Anwendung
            var app = builder.Build();

            app.UseCors("AllowAll");

            // Konfigurieren der HTTP-Anforderungspipeline
            if (app.Environment.IsDevelopment())
            {
                // Aktivieren von Swagger und SwaggerUI f�r Entwicklungsumgebung
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Aktivieren der Authentication und Authorization

            app.UseAuthentication();
            app.UseAuthorization();
            // Mappen der Controller-Endpunkte
            app.MapControllers();

            // Starten der Anwendung
            app.Run();
        }
    }
}