using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DataAccess
{
    public class DataGenerator
    {
        //INMEMORY Database 

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new Book()
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,//Personal Growth 
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 6, 12)
                    },
                    new Book()
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,//Since Fiction,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 5, 23)
                    },
                    new Book()
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,//Personal Growth 
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 12, 21)
                });

                context.SaveChanges();

            }
        }
    }
}
