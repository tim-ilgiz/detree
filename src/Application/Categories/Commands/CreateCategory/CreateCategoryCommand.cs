using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public long Parent { get; set; }

        public string Status { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
        {
            private readonly IApplicationDbContext _context;

            public CreateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category
                {
                    Id = request.Id,
                    CategoryName = request.CategoryName,
                    Parent = request.Parent,
                    Status = request.Status
                };

                _context.Categories.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
