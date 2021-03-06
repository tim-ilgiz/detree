﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                .Where(el => el.ParentId == request.ParentId)
                .ProjectTo<CardDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CardsListVm
            {
                Cards = cards
            };

            return vm;
        }
    }
}