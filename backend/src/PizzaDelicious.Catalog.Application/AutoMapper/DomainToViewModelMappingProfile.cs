using AutoMapper;
using PizzaDelicious.Catalog.Application.ViewModels;
using PizzaDelicious.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaDelicious.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimension.Width))
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimension.Heigh))
                .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimension.Depth));

            CreateMap<Category, CategoryViewModel>();

          
        }
    }
}
