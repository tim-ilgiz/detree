using MediatR;

namespace Application.Cards.Queries
{
    public class GetCardsListQuery : IRequest<CardsListVm>
    {
        public long ParentId { get; set; }
    }
}