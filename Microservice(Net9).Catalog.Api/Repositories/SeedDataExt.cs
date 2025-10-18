

using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Features.Courses;

namespace Microservice_Net9_.Catalog.Api.Repositories
{
    public static class SeedDataExt
    {
        public static async Task AppSeedDataExt(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

                if (!dbContext.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category { Id = NewId.NextSequentialGuid(), Name = "Programlama"  },
                        new Category { Id = NewId.NextSequentialGuid(), Name = "Sanat" },
                        new Category { Id = NewId.NextSequentialGuid(), Name = "Pazarlama"},
                        new Category { Id = NewId.NextSequentialGuid(), Name = "Tasarım"},
                        new Category { Id = NewId.NextSequentialGuid(), Name = "Dil Öğrenme"},

                    };

                    dbContext.Categories.AddRange(categories);
                    dbContext.SaveChanges();

                }

                if (!dbContext.Courses.Any())
                {
                    var category = dbContext.Categories.First();

                    var courses = new List<Course>
                    {
                        new Course
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "C# Fundamentals",
                            Description = "Learn the basics of C# programming and object-oriented principles.",
                            Price = 99.99m,
                            ImageUrl = "https://example.com/images/csharp.jpg",
                            CreateTime = DateTime.UtcNow,
                            UserId = NewId.NextSequentialGuid(),
                            CategoryId = category.Id,
                            Feature = new Feature { Duration = 12, Rate = 4.7f, EducatorFullName = "John Doe" }
                        },
                        new Course
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Advanced Web Design", 
                            Description = "Deep dive into modern web design techniques with Figma and Adobe XD.",
                            Price = 79.5m,
                            ImageUrl = "https://example.com/images/webdesign.jpg",
                            CreateTime = DateTime.UtcNow,
                            UserId = NewId.NextSequentialGuid(),
                            CategoryId = category.Id,
                            Feature = new Feature { Duration = 9, Rate = 3.9f, EducatorFullName = "Emma Smith" }
                        },
                        new Course
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Digital Marketing 101",
                            Description = "Understand SEO, SEM and content marketing strategies for beginners.",
                            Price = 59.9m,
                            ImageUrl = "https://example.com/images/marketing.jpg",
                            CreateTime = DateTime.UtcNow,
                            UserId = NewId.NextSequentialGuid(),
                            CategoryId = category.Id,
                            Category = category,
                            Feature = new Feature { Duration = 8, Rate = 4.6f, EducatorFullName = "Michael Johnson" }
                        },
                        new Course
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Unity Game Development",
                            Description = "Build your first 2D and 3D games using Unity and C#.",
                            Price = 129.0m,
                            ImageUrl = "https://example.com/images/unity.jpg",
                            CreateTime = DateTime.UtcNow,
                            UserId = NewId.NextSequentialGuid(),
                            CategoryId = category.Id,
                            Category = category,
                            Feature = new Feature { Duration = 15, Rate = 4.8f, EducatorFullName = "Sarah Lee" }
                        },
                        new Course
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "UI/UX for Beginners",
                            Description = "Learn the fundamentals of user interface and user experience design.",
                            Price = 69.99m,
                            ImageUrl = "https://example.com/images/uiux.jpg",
                            CreateTime = DateTime.UtcNow,
                            UserId = NewId.NextSequentialGuid(),
                            CategoryId = category.Id,
                            Category = category,
                            Feature = new Feature { Duration = 10, Rate = 4.5f, EducatorFullName = "Olivia Brown" }
                        }
                    };

                    dbContext.Courses.AddRange(courses);
                    dbContext.SaveChanges();
                }

            }




        }
    }
}
