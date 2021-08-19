using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryValidator:AbstractValidator<GetActorDetailQuery>
    {
        public GetActorDetailQueryValidator()
        {
            RuleFor(query => query.ActorId).NotEmpty().GreaterThan(0);
        }
    }
}
