using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Categories.Commands.CreateCategory
{

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Parent).NotEmpty();
        }
    }
}
