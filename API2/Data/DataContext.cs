using Microsoft.EntityFrameworkCore;

namespace API2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base (options) { }

        public DbSet<TodoList> todolist { get; set; }
    }
}
