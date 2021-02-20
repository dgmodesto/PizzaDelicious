using PizzaDelicious.Core.Data;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Register.Domain.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);

        void Add(Client product);
        void Update(Client product);

        void Add(Address category);
        void Update(Address category);
    }
}
