using Microsoft.EntityFrameworkCore;

namespace D_AlturaSystemAPI.Modelos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define tus tablas usando DbSet<T>
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
