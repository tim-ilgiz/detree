using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards.Queries
{
    public class GetCardsListQuery :IRequest<CardsListVm>
    {
        public long ParentId { get; set; }
    }
}
