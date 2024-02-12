using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantAPI.Controllers
{
    [ApiController]
    [Route("[controller]/{tenantId:guid}")]
    public class ClientController : ControllerBase
    {
        private readonly IRepository<Client, Guid, Guid> _clientRepository;
        private readonly IRepository<Company, Guid, Guid> _companyRepository;
        private readonly Guid _tenantId;

        public ClientController(IRepository<Client, Guid, Guid> clientRepository,
            IRepository<Company, Guid, Guid> companyRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _tenantId = new Guid(httpContextAccessor.HttpContext.Request.RouteValues.GetValueOrDefault("tenantId")?.ToString());
        }

        [HttpGet("Init")]
        public void Get()
        {
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients([Required][FromQuery] Guid companyId)
        {
            var company = await _companyRepository.GetById(companyId, _tenantId, company => company.Clients);

            if(company is null)
                return NotFound();

            return Ok(company.Clients);
        }

        [HttpGet("client")]
        public async Task<IActionResult> GetClient([Required][FromQuery] Guid clientId)
        {
            var client = await _clientRepository.GetById(clientId, _tenantId);

            if (client is null)
                return NotFound();

            return Ok(client);
        }
    }
}