using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaDelicious.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoryId { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Enable { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int QuantityStock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Depth { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

    }
}
