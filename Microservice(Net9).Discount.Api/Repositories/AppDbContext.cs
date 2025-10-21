using MongoDB.Driver;
using System.Reflection;

namespace Microservice_Net9_.Discount.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {


        public DbSet<Features.Discounts.Discount> Discounts { get; set; }



        public static AppDbContext Create(IMongoDatabase database)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

            var appDbContext = new AppDbContext(dbContextOptionsBuilder.Options);

            return appDbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bir dizin veriyoruz. O dizindeki ilgili kütüphanyi import etmiş bütün configurasyonları kullanır
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
