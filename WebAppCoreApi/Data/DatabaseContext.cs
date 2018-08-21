using Microsoft.EntityFrameworkCore;
using Shareds;

namespace WebAppCoreApi.Data
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Comment> Comment { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
