using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaDelicious.Register.Application.ViewModels
{
    public class ClientViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid AddressId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Street { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Number { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string State { get; set; }

    }
}
