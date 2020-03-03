using System;
using System.Collections.Generic;
using System.Text;
using Application.Cards.Queries;
using Application.Categories.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Card, CardDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
