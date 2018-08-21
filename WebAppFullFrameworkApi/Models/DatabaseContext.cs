using Shareds;
using System.Data.Entity;

namespace WebAppFullFrameworkApi.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext()            
            :base("DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        public DbSet<Comment> Comment { get; set; }
    }
}