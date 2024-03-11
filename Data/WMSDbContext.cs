using Microsoft.EntityFrameworkCore;

namespace WorkshopManagementServiceBackend.Data
{
    public class WMSDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost; user=admin; password=!(*wpySyhkDB/zGJ; database=workshopmanagement";

            var serverVersion = new MySqlServerVersion(new Version(10,4,32));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
