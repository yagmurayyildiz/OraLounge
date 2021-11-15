using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.OraLounge.Models;

namespace Web.OraLounge.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Image, o => o.MapFrom<ImageSrcResolver>());

            CreateMap<Booking, BookingViewModel>().ReverseMap()
                .ForMember(d => d.BookingTime, o => o.MapFrom(s => s.BookingTime.Date + s.SelectedTime.TimeOfDay));
        }
    }
}