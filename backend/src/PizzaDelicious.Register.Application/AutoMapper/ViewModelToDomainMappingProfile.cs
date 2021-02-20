using AutoMapper;
using PizzaDelicious.Register.Application.ViewModels;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Register.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {

            CreateMap<ClientViewModel, Client>()
                .ConstructUsing(p =>
                    new Client (
                        p.Name, p.Cpf, p.DateOfBirth, new List<Address>()
                       ));
        }
    }
}
