using AutoMapper;
using PizzaDelicious.Register.Application.ViewModels;
using PizzaDelicious.Register.Domain.Models;
using PizzaDelicious.Register.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Register.Application.Services
{
    public class ClientAppService : IClientAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientAppService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClientViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ClientViewModel>>(await _clientRepository.GetAll());

        }

        public async Task<ClientViewModel> GetById(Guid id)
        {
            return _mapper.Map<ClientViewModel>(await _clientRepository.GetById(id));
        }

        public async Task<bool> AddClient(ClientViewModel clientViewModel)
        {
            var client = _mapper.Map<Client>(clientViewModel);

            var listAddress = new List<Address>();
            listAddress.Add(new Address(
                clientViewModel.Id, clientViewModel.Street, clientViewModel.Number, clientViewModel.Neighborhood, clientViewModel.City, clientViewModel.State
            ));
            client.AddAddress(listAddress);

            _clientRepository.Add(client);

            return await _clientRepository.UnitOfWork.Commit();
        }


        public async Task<bool> UpdateClient(ClientViewModel clientViewModel)
        {
            var client = _mapper.Map<Client>(clientViewModel);
          
            var listAddress = new List<Address>();
            var address = new Address(
                clientViewModel.Id, clientViewModel.Street, clientViewModel.Number, clientViewModel.Neighborhood, clientViewModel.City, clientViewModel.State);
            address.SetAddressId(clientViewModel.AddressId);
            listAddress.Add(address);

            
            client.AddAddress(listAddress);

            _clientRepository.Update(client);

            return await _clientRepository.UnitOfWork.Commit();
        }

       
    }
}
