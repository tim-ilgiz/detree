using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Cards.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<Card>
    {
        public long Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string Image { get; set; }

        public long ParentId { get; set; }
        public long Click { get; set; }

        public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Card>
        {
            private readonly IApplicationDbContext _context;

            public CreateCardCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Card> Handle(CreateCardCommand request, CancellationToken cancellationToken)
            {
                var entity = new Card
                {
                    Id = request.Id,
                    CardName = request.CardName,
                    LinkUrl = request.LinkUrl,
                    Image = request.Image,
                    ParentId = request.ParentId,
                    Click = request.Click
                };

                _context.Cards.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}