using PizzaDelicious.Core.DomainObjects;

namespace PizzaDelicious.Catalog.Domain.Models
{
    public class Dimension
    {
        public decimal Heigh { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimension(decimal heigh, decimal width, decimal depth)
        {
            Validations.ValidateIfLessThan(heigh, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validations.ValidateIfLessThan(width, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validations.ValidateIfLessThan(depth, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            Heigh = heigh;
            Width = width;
            Depth = depth;
        }

        public string DescriptionFormated()
        {
            return $"LxAxP: {Heigh} x {Width} x {Depth}";
        }

        public override string ToString()
        {
            return DescriptionFormated();
        }
    }
}