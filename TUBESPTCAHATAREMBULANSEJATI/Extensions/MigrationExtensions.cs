using ExpedisiPaketAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpedisiPaketAPI.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ExpedisiDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
