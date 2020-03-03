using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Queries
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public long Parent { get; set; }

        public string Status { get; set; }
    }
}