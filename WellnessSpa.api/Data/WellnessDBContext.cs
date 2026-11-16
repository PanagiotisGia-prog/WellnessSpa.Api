using  Microsoft.EntityFrameworkCore;
using WellnessSpa.Api.Model;

namespace WellnessSpa.Api.Data
{
    public class WellnessDBContext(DbContextOptions<WellnessDBContext> options) : DbContext(options: options)
    {
        public DbSet<Wellness> Wellnesses => Set<Wellness>();

        public DbSet<Treatment> Treatments => Set<Treatment>();

        public DbSet<Pressure> Pressures => Set<Pressure>();
    }
}