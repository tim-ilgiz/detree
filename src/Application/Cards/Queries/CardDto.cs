using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Cards.Queries
{
    public class CardDto
    {
        public long Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string ImageBase64 { get; set; }

        public long ParentId { get; set; }

        public long Click { get; set; }
    }
}