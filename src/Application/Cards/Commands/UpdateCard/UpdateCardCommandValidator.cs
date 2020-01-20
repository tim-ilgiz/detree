using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
