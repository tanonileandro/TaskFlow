using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
namespace TaskFlow.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> GetByEmailAsync(string email)
        {
            var client = await _clientRepository.GetByEmailAsync(email);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task AddAsync(ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            // Encriptar la contraseña antes de almacenarla
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateAsync(ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            // Si estás actualizando la contraseña, encríptala
            if (!string.IsNullOrEmpty(clientDto.Password))
            {
                client.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
            }

            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }
    }
}
