using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IClientBusiness
{
    IEnumerable<ClientDto> GetClients(bool? disabled);
    IEnumerable<ClientLightDto> GetClientsForDropDown();
    ClientDto GetClientById(int clientId);
    bool CreateClient(ClientDto client);
    bool UpdateClient(ClientDto client);
    bool DeleteClient(int clientId);
}
