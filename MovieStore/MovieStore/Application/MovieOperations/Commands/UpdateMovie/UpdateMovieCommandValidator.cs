﻿
using FluentValidation;

namespace MovieStore.Application.MovieOperations.Commands.UpdateMovie;
public class UpdateMovieCommandValidator:AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.DirectorId).NotEmpty().GreaterThan(0);

   
    }
}
