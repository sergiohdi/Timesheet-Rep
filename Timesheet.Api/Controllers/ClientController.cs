using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

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
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [ProducesResponseType(typeof(void), 403)] 
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)] 
    public IActionResult GetClients([FromQuery]bool? disabled)
    {
        try
        {
           return Ok(_clientBusiness.GetClients(disabled));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting clients");
        }
    }

    [HttpGet("fordrop")]
    [ProducesResponseType(typeof(IEnumerable<ClientLightDto>), 200)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetClientsForDropDown()
    {
        try
        {
            return Ok(_clientBusiness.GetClientsForDropDown());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting clients");
        }
    }

    [HttpGet("{clientId}")]
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(ClientDto), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetClient([FromRoute] int clientId)
    {
        try
        {
            return Ok(_clientBusiness.GetClientById(clientId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting client");
        }
    }

    [HttpPost]
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(void), 500)]
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
                result = StatusCode(500, "An error occurred creating client");
            }
        }

        return result;
    }

    [HttpPut]
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult UpdateClient(ClientDto client)
    {
        try
        {
            return Ok(_clientBusiness.UpdateClient(client));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred updating client");
        }
    }

    [HttpDelete("{clientId}")]
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult DeleteClient(int clientId)
    {
        try
        {
            return Ok(_clientBusiness.DeleteClient(clientId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred deleting a client");
        }
    }
}
