using Microsoft.EntityFrameworkCore;

namespace PayloadApp.Models
{
    public class PayloadDbContext : DbContext
    {
        public PayloadDbContext(DbContextOptions<PayloadDbContext> options) : base(options)
        {
        }

        public DbSet<Payload> Payloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the table name and primary key
            modelBuilder.Entity<Payload>().ToTable("Payload");
            modelBuilder.Entity<Payload>().HasKey(p => p.Id);
        }
    }
}