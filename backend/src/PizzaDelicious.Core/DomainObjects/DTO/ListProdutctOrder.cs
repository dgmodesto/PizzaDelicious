using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.DomainObjects.DTO
{
    public class ListProdutctOrder
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
