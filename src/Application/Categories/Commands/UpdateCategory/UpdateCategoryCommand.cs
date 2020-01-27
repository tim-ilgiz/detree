using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public long? Id { get; set; }

        public string CategoryName { get; set; }

        public long Parent { get; set; }

        public string Status { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
        {
            public readonly IApplicationDbContext _context;
            public UpdateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                entity.CategoryName = request.CategoryName;
                entity.Parent = request.Parent;
                entity.Status = request.Status;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

        }
    }
}
