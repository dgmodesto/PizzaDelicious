using PizzaDelicious.Core.DomainObjects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaDelicious.Register.Domain.Models
{
    public class Address: Entity
    {
        public Address(Guid clientId, string street, int number, string neighborhood, string city, string state)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;

            Validate();
        }

        public Guid ClientId { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        //EF Relation
        public Client Client { get; set; }

        public void SetAddressId(Guid id)
        {
            Id = id;
        }

        public void Validate()
        {
            Validations.ValidateIfEmpty(Street, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateIfEqualEqual(Number, 0, "O campo Codigo não pode ser 0");
            Validations.ValidateIfEmpty(Neighborhood, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateIfEmpty(City, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateIfEmpty(State, "O campo Nome da categoria não pode estar vazio");

        }
    }
}