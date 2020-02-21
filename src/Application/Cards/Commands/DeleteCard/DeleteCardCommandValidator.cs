using FluentValidation;

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