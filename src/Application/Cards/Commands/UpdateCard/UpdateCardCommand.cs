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

namespace Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest
    {
        public long? Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string Image { get; set; }

        public long ParentId { get; set; }
        public long Click { get; set; }

        public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand>
        {
            public readonly IApplicationDbContext _context;
            public UpdateCardCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Cards.SingleOrDefaultAsync(c => c.Id == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Card), request.Id);
                }

                entity.Image = request.Image;
                entity.CardName = request.CardName;
                entity.Click = request.Click;
                entity.LinkUrl = request.LinkUrl;
                entity.ParentId = request.ParentId;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

        }
    }
}
