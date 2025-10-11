using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace Microservice_Net9_.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
     
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        public static AppDbContext Create(IMongoDatabase database)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

            var appDbContext = new AppDbContext(dbContextOptionsBuilder.Options);

            return appDbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablo     /Satır   /Sütün
            // Collection/Document/Field

            //Bir dizin veriyoruz. O dizindeki ilgili kütüphanyi import etmiş bütün configurasyonları kullanır
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
