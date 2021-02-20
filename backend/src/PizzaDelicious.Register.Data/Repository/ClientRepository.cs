using Microsoft.EntityFrameworkCore;
using PizzaDelicious.Core.Data;
using PizzaDelicious.Register.Domain.Models;
using PizzaDelicious.Register.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Register.Data.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly RegisterContext _context;

        public ClientRepository(RegisterContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task<IEnumerable<Client>> GetAll()
        {
            var result = await _context.Set<Client>().AsNoTracking()
                .Include(p => p.Addresses).Where(c => c.Addresses.Select(x => x.ClientId == c.Id).FirstOrDefault())
                .ToListAsync();
            return result;
        }


        public async Task<Client> GetById(Guid id)
        {
            var result = await _context.Set<Client>().AsNoTracking()
                .Where(x => x.Id == id)
                .Include(p => p.Addresses).Where(c => c.Addresses.Select(x => x.ClientId == c.Id).FirstOrDefault())
                .ToListAsync();



            return result.FirstOrDefault() ;
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }


        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }


        public void Add(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void Update(Address address)
        {
            _context.Addresses.Update(address);
        }


        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
