using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommand : IRequest
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
                if (entity == null) throw new NotFoundException(nameof(Card), request.Id);
                _context.Cards.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}