using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Cards.Queries
{
    public class CardDto : IMapFrom<Card>
    {
        public long Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string ImageBase64 { get; set; }

        public long ParentId { get; set; }

        public long Click { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Card, CardDto>()
                .ForMember(dest => dest.ImageBase64, opt => opt.MapFrom(s => s.Image));
        }
    }
}