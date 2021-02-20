using PizzaDelicious.Core.DomainObjects;
using System.Collections.Generic;

namespace PizzaDelicious.Catalog.Domain.Models
{
    public class Category: Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Product> Products { get; set; }

        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }


        private void Validate()
        {
            Validations.ValidateIfEmpty(Name, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateIfEqualEqual(Code, 0, "O campo Codigo não pode ser 0");
        }
    }
}