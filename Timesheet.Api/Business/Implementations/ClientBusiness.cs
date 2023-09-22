using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;

        public ClientBusiness(IClientRepository clientRepository, IProjectRepository projectRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
        }

        public IEnumerable<ClientDto> GetClients(bool? disabled)
        {
            return _clientRepository.GetClients(disabled);
        }

        public IEnumerable<ClientLightDto> GetClientsForDropDown()
        {
            return _clientRepository.GetClientsForDropDown();
        }

        public ClientDto GetClientById(int clientId)
        {
            return _clientRepository.GetClientById(clientId);
        }

        public bool CreateClient(ClientDto client)
        {
            client.Disabled = false;
            return _clientRepository.CreateClient(client);
        }

        public bool UpdateClient(ClientDto client)
        {
            return _clientRepository.UpdateClient(client);
        }

        public bool DeleteClient(int clientId)
        {
            // Validate if the client exists
            ClientDto client = _clientRepository.GetClientById(clientId);
            if (client is null)
            {
                return false;
            }

            // Validate if the client is used in any project
            bool isUsedClient = _projectRepository.ValidateProjectsByClientId(clientId);
            if (isUsedClient)
            {
                return false;
            }

            return _clientRepository.DeleteClient(client);
        }
    }
}