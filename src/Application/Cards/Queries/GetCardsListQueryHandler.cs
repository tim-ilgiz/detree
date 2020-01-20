using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cards.Queries
{
    public class GetCardsListQueryHandler : IRequestHandler<GetCardsListQuery, CardsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCardsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CardsListVm> Handle(GetCardsListQuery request, CancellationToken cancellationToken)
        {
            var cards = await _context.Cards
                .ProjectTo<CardDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.CardName)
                .ToListAsync(cancellationToken);
            
            var vm = new CardsListVm 
            { 
                Cards = cards
            };

            return vm;
        }
    }
}
