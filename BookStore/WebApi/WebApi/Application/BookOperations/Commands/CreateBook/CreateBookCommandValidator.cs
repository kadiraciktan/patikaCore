using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.CreateBook
{
    //CreateBookCommand sınıfını valide etmesini bildirdir.
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            //GenreId 0 dan büyük olmalı
            RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0).WithMessage("GenreId 0 dan küçük veya 0 Olamaz");
            RuleFor(cmd=>cmd.Model.PageCount).GreaterThan(0);
            //publishdate boş olamaz ve bugünden küçük olmalı
            RuleFor(cmd=>cmd.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            //Kitabın adı boş olamaz ve minimum 4 olmalı
            RuleFor(cmd => cmd.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
