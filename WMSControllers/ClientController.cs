using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopManagementServiceBackend.ApiModels;
using WorkshopManagementServiceBackend.Interface;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] 
    [Route("[controller]")]
    /*
     * Controller Klasse für Client
     * Der Code sieht ähnlich aus wie beim ProjectController, die Kommentare sind übertragbar
     */
    public class ClientController: ControllerBase
    {
        private readonly Repository<Client> _Repository;
        public ClientController(Repository<Client> Repository) {
            _Repository = Repository;
        }

        [HttpPost]
        [ProducesResponseType(200)]                         
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Create(Client client)
        {
            try  
            {
                await _Repository.Create(client);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("An error occurred while saving the entity.The Client Id or the Project Id might be wrong.");
            }
            catch (Exception ex)       
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Delete(int id)  
        {
            try
            {
                await _Repository.Delete(id);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return BadRequest("Entity with given Id doesn't exist");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Client>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);          
            if (entry == null) { return BadRequest("Entity with this Id doens't exist"); }
            return Ok(entry);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Client), 200)]    
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public async Task<IActionResult> Update(Client client)
        {
            try
            {
                var result = await _Repository.Update(client);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("An error occurred while saving the entity.The Client Id or the Project Id might be wrong.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
