using PizzaDelicious.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Register.Domain.Models
{
    public class Client : Entity, IAggregateRoot
    {
        // EF
        protected Client() { }
        public Client(string name, string cpf, DateTime dateOfBirth, List<Address> address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cpf = cpf;
            DateOfBirth = dateOfBirth;
            Addresses = address;

            Validate();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public ICollection<Address> Addresses { get; private set; }

        public void Validate()
        {
            Validations.ValidateIfEmpty(Name, "O campo Nome do cliente não pode estar vazio");
            Validations.ValidateIfEqualEqual(Cpf, 0, "O campo Cpf não pode ser 0");
        }

        public void UpdateAddress(List<Address> address)
        {
            Addresses = address;
        }

        public void AddAddress(List<Address> address)
        {
            Addresses = address;
        }


    }
}
