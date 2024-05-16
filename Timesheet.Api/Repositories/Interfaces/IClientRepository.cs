using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IClientRepository
{
    IEnumerable<ClientDto> GetClients(bool? disabled);
    IEnumerable<ClientLightDto> GetClientsForDropDown();
    ClientDto GetClientById(int clientId);
    bool CreateClient(ClientDto client);
    bool UpdateClient(ClientDto client);
    bool DeleteClient(ClientDto clientId);
}
