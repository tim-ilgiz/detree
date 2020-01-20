using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
