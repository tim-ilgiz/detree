using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandValidator: AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(x => x.ParentId).NotEmpty();
        }
    }
}
