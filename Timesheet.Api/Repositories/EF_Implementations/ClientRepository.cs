using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Extensions;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ClientRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IEnumerable<ClientDto> GetClients(bool? disabled)
        {
            IQueryable<Client> result = _db.Client.AsNoTracking().OrderBy(x => x.Name);

            if (disabled != null)
            {
                result = result.Where(x => x.Disabled == disabled);
            }

            return _mapper.Map<IEnumerable<ClientDto>>(result.ToList());
        }

        public IEnumerable<ClientLightDto> GetClientsForDropDown()
        {
            return _db.Client.AsNoTracking()
                             .Where(x => x.Disabled == false)
                             .OrderBy(x => x.Name)
                             .Select(x => new ClientLightDto
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Code = x.Code,
                             })
                             .ToList();
        }

        public ClientDto GetClientById(int clientId)
        {
            Client client = _db.Client.AsNoTracking().FirstOrDefault(x => x.Id == clientId);
            return _mapper.Map<ClientDto>(client);
        }

        public bool CreateClient(ClientDto client)
        {
            Client clientDb = _mapper.Map<Client>(client);
            SetDefaultProperties(clientDb);

            clientDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Client.Add(clientDb);
            return _db.SaveChanges() > 0;
        }

        public bool UpdateClient(ClientDto client)
        {
            Client clientDb = _mapper.Map<Client>(client);
            SetDefaultProperties(clientDb);

            clientDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Entry(clientDb).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public bool DeleteClient(ClientDto client)
        {
            Client clientDb = _mapper.Map<Client>(client);

            _db.Remove(clientDb);
            return _db.SaveChanges() > 0;
        }

        private static void SetDefaultProperties(Client clientDb)
        {
            clientDb.BillingGlaccount = "7780HDI";
            clientDb.BillingCurrencyCode = "CAD";
            clientDb.GstratePercent = 0.05M;
            clientDb.ApvendorId = "HUNDIC";
            clientDb.ApcurrencyCode = "CAD";
        }
    }
}
