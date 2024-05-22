using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopManagementServiceBackend.ApiModels;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController]
    [Route("[controller]")]
    /*
     * Controller Klasse für MaterialType
     * Der Code sieht ähnlich aus wie beim ProjectController, die Kommentare sind übertragbar
     */
    public class MaterialTypeController : ControllerBase
    {
        private readonly Repository<MaterialType> _Repository;
        public MaterialTypeController(Repository<MaterialType> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        [ProducesResponseType(200)]                         //Gibt dem Swagger Generator die Informationen welche Response zurückkommen kann
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Create(MaterialType materialType)
        {
            try
            {
                await _Repository.Create(materialType);
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
        [ProducesResponseType(typeof(List<MaterialType>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _Repository.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MaterialType), 200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            if (entry == null) { return BadRequest("Entity with this Id doens't exist"); }
            return Ok(entry);
        }

        [HttpPut]
        [ProducesResponseType(typeof(MaterialType), 200)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public async Task<IActionResult> Update(MaterialType materialType)
        {
            try
            {
                var result = await _Repository.Update(materialType);
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
