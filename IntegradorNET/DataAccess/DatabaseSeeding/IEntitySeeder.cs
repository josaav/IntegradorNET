using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.DatabaseSeeding
{
    public interface IEntitySeeder
    {
        void SeedDatabase(ModelBuilder modelBuilder);
    }
}
