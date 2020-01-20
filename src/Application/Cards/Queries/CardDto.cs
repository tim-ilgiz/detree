using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards.Queries
{
    public class CardDto : IMapFrom<Card>
    {
        public long Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string Image { get; set; }

        public long ParentId { get; set; }
        public long Click { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Card, CardDto>()
                .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ParentId));
        }
    }
}
