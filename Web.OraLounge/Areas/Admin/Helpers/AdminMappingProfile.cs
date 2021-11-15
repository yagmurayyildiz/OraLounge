using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.OraLounge.Areas.Admin.Models;

namespace Web.OraLounge.Areas.Admin.Helpers
{
    public class AdminMappingProfile : Profile
    {
        public AdminMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, PostCategoryViewModel>().ReverseMap();

            CreateMap<Category, SubCategoryViewModel>();
            CreateMap<Category, PostSubCategoryViewModel>().ReverseMap();

            CreateMap<Product, MenuViewModel>();

            CreateMap<Table, TableViewModel>();
            CreateMap<Table, TableViewModel>().ReverseMap();

            CreateMap<Booking, BookingScheduleViewModel>();
            CreateMap<Booking, PendingBookingViewModel>();
            CreateMap<Booking, BookingViewModel>();
            CreateMap<Booking, BookingViewModel>().ReverseMap()
                .ForMember(d => d.BookingTime, o => o.MapFrom(s => s.BookingTime.Date + s.SelectedHour.TimeOfDay ));
        }
    }
}