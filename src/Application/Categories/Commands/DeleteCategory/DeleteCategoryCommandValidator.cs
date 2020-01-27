using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
