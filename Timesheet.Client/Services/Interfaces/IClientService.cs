using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IClientService
    {
        Task<ApiResponse<bool>> CreateClient(Models.Client client);
        Task<ApiResponse<bool>> DeleteClient(int clientId);
        Task<ApiResponse<Models.Client>> GetClientById(int clientId);
        Task<ApiResponse<IEnumerable<Models.Client>>> GetClients(bool? disabled);
        Task<ApiResponse<IEnumerable<Models.ClientLight>>> GetClientsForDropDown();
        Task<ApiResponse<bool>> UpdateClient(Models.Client client);
    }
}