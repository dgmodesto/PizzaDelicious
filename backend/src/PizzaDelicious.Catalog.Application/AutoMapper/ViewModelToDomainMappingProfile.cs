using AutoMapper;
using PizzaDelicious.Catalog.Application.ViewModels;
using PizzaDelicious.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p =>
                    new Product(p.CategoryId, p.Name, p.Description, p.Enable,
                        p.Value, p.RegisterDate,
                        p.Image, new Dimension(p.Height, p.Width, p.Depth)));

            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));

        }
    }
}
