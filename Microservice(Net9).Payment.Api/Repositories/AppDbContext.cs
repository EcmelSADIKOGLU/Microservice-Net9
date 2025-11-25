using Microsoft.EntityFrameworkCore;

namespace Microservice_Net9_.Payment.Api.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.BuyerId).IsRequired();
                entity.Property(e => e.OrderCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.CreatedTime).IsRequired();
                entity.Property(e => e.Amount).IsRequired().HasPrecision(18,2);
                entity.Property(e => e.Status).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
            
        }
        public DbSet<Payment> Payments { get; set; }

    }
}
