using API.OraLounge.Models;
using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.OraLounge.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description == null ? string.Empty : s.Description))
                .ForMember(d => d.Images, o => o.MapFrom<ProductImageSrcResolver>());
        }
    }
}