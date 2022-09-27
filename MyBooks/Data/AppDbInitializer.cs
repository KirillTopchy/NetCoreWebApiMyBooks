using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyBooks.Data.Models;
using System;
using System.Linq;

namespace MyBooks.Data
{
    public static class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicatinBuilder)
        {
            using var serviceScope = applicatinBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (!context.Books.Any())
            {
                context.Books.AddRange(new Book()
                {
                    Title = "1st Book Title",
                    Description = "1st Book Description",
                    IsRead = true,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Detective",
                    CoverUrl = "https....",
                    DateAdded = DateTime.Now,
                },
                new Book()
                {
                    Title = "2nd Book Title",
                    Description = "2nd Book Description",
                    IsRead = false,
                    Genre = "Drama",
                    CoverUrl = "https....",
                    DateAdded = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}
