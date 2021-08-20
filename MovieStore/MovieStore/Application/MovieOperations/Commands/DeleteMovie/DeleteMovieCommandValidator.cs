
using FluentValidation;

namespace MovieStore.Application.MovieOperations.Commands.DeleteMovie;
public class DeleteMovieCommandValidator:AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(x => x.MovieId).GreaterThan(0);
    }
}
