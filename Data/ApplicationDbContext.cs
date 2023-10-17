using Microsoft.EntityFrameworkCore;
using next_generation.Models;

namespace next_generation.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        public DbSet<NexGenUsers> NextGenUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NexGenUsers>().HasData(
                new NexGenUsers()
                {
                    Id= 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    CreationDate = DateTime.Now,
                    Password = "secretpassword1"
                },
                new NexGenUsers()
                {
                    Id= 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    CreationDate = DateTime.Now,
                    Password = "secretpassword2"
                }
            );
        }
    }
}
