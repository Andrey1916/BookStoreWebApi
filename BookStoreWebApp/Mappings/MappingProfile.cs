using AutoMapper;
using BookStoreWebApp.Models.BindingModels;
using BookStoreWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Services.Dtos.Review, ReviewViewModel>().ReverseMap();
            CreateMap<Services.Dtos.Review, AddReviewBindingModel>().ReverseMap();
            CreateMap<Services.Dtos.Book, BookViewModel>().ReverseMap();
            CreateMap<Services.Dtos.LineItem, LineItemViewModel>().ReverseMap();
        }
    }
}
