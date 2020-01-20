using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommand: IRequest
    {
        public long Id { get; set; }

        public class DeleteCardCommandHadler : IRequestHandler<DeleteCardCommand>
        {
            public readonly IApplicationDbContext _context;
            public DeleteCardCommandHadler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Cards.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Card), request.Id);
                }
                _context.Cards.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
