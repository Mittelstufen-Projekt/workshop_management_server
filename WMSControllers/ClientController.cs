using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Interface;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class ClientController: ControllerBase
    {
        private readonly Repository<Client> _Repository;
        public ClientController(Repository<Client> Repository) {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            await _Repository.Create(client);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Repository.Delete(id);
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Repository.GetAll();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            return Ok(entry);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(Client client)
        {
            _Repository.Update(client);
            return Ok();
        }
    }
}
