using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Categories.Queries
{
    public class CategoryDto : IMapFrom<Category>
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public long Parent { get; set; }

        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
}
