using PizzaDelicious.Core.DomainObjects;
using System;

namespace PizzaDelicious.Catalog.Domain.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Enable { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int QuantityStock { get; private set; }
        public Dimension Dimension { get; private set; }
        public virtual Category Category { get; private set; }


        protected Product() { }

        public Product(Guid categoryId, string name, string description, bool enable, decimal value, DateTime registerDate, string image, Dimension dimension)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Enable = enable;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
            Dimension = dimension;

            Validate();
        }


        public void ToEnable() => Enable = true;
        public void ToDisabel() => Enable = false;
        public void UpdateCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void UpdateDescription(string description)
        {
            Validations.ValidateIfEmpty(description, "O Campo Descrição do produto não pode estar vazio");
            Description = description;
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;

            if (!HasStock(quantity)) throw new DomainException("Estoque insuficiente");
            QuantityStock -= quantity;
        }

        public bool HasStock(int quantity)
        {
            return QuantityStock >= quantity;
        }

        private void Validate()
        {
            Validations.ValidateIfEmpty(Name, "O campo Nome do produto não pode estar vazio");
            Validations.ValidateIfEmpty(Description, "O campo Descrição do produto não pode estar vazio");
            Validations.ValidateIfEmpty(Image, "O campo Imagem do prodtuo não pode estar vazio");
            Validations.ValidateIfEqualEqual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validations.ValidateIfLessThan(Value, 1, "O campo valor do produto não pode se menor igual a 0");

        }

        public void ResetStock(int quantity)
        {
            QuantityStock += quantity;
        }
    }
}
