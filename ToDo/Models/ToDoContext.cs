using Microsoft.EntityFrameworkCore;

namespace ToDo.Models
{
    // Using in-memory database context
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}