using Microsoft.EntityFrameworkCore;
using OrientationAPI.Models;

namespace OrientationAPI.Data
{
    public class OrientationContext:DbContext
    {
        public OrientationContext(DbContextOptions<OrientationContext> options):base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Demand> demands { get; set; }
        public DbSet<Decision> decisions { get; set; }
        
    }
}
