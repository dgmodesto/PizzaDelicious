using AutoMapper;
using PizzaDelicious.Register.Application.ViewModels;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaDelicious.Register.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Client, ClientViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.AddressId, o => o.MapFrom(s => s.Addresses.FirstOrDefault().Id))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Addresses.FirstOrDefault().Street))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Addresses.FirstOrDefault().Number))
                .ForMember(d => d.Neighborhood, o => o.MapFrom(s => s.Addresses.FirstOrDefault().Neighborhood))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Addresses.FirstOrDefault().City))
                .ForMember(d => d.State, o => o.MapFrom(s => s.Addresses.FirstOrDefault().State));
        }
    }
}
