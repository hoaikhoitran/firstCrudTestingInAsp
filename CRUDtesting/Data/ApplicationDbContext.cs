using CRUDtesting.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDtesting.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
