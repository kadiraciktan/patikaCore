using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieContext Context { get; set; }
        public IMapper   Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieContext>().UseInMemoryDatabase("BookStoreTestDB").Options;

            Context = new MovieContext(options);
            Context.Database.EnsureCreated();
            Context.AddDatas();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();

        }
    }
}
