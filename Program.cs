
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WorkshopManagementServiceBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var serverVersion = new MySqlServerVersion(new Version(10, 4, 32));
            // Add services to the container.
            // Configures the DB Connection
            /*builder.Services.AddDbContext<WMSDbContextSelf>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });*/
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline. Probably needs to be removed later on
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}