using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientBusiness clientBusiness, ILogger<ClientController> logger)
        {
            _clientBusiness = clientBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetClients([FromQuery]bool? disabled)
        {
            try
            {
               return Ok(_clientBusiness.GetClients(disabled));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured getting clients");
            }
        }

        [HttpGet("fordrop")]
        public IActionResult GetClientsForDropDown()
        {
            try
            {
                return Ok(_clientBusiness.GetClientsForDropDown());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured getting clients");
            }
        }

        [HttpGet("{clientId}")]
        public IActionResult GetClient([FromRoute] int clientId)
        {
            try
            {
                return Ok(_clientBusiness.GetClientById(clientId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured getting client");
            }
        }

        [HttpPost]
        public IActionResult CreateClient(ClientDto client)
        {
            ActionResult result;
            try
            {
                result = Ok(_clientBusiness.CreateClient(client));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'UC_Client_Name'"))
                {
                    result = BadRequest($"Client already exist");
                }
                else 
                {
                    result = StatusCode(500, "An error occured creating client");
                }
            }

            return result;
        }

        [HttpPut]
        public IActionResult UpdateClient(ClientDto client)
        {
            try
            {
                return Ok(_clientBusiness.UpdateClient(client));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured updating client");
            }
        }

        [HttpDelete("{clientId}")]
        public IActionResult DeleteClient(int clientId)
        {
            try
            {
                return Ok(_clientBusiness.DeleteClient(clientId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured deleting a client");
            }
        }
    }
}
