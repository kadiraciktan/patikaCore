using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {

        public CreateGenreModel Model { get; set; }

        private readonly BookContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);

            if (genre is not null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
