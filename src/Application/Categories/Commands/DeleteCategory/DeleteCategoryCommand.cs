using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public long Id { get; set; }

        public class DeleteCategoryCommandHadler : IRequestHandler<DeleteCategoryCommand>
        {
            public readonly IApplicationDbContext _context;
            public DeleteCategoryCommandHadler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Categories), request.Id);
                }
                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
