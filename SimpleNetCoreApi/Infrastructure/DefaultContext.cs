using Microsoft.EntityFrameworkCore;

namespace SimpleNetCoreApi.Infrastructure
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        { }

        public DbSet<Model> Models { get; set; }
    }
}
