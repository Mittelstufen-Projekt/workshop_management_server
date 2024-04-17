using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class MaterialController : ControllerBase
    {
        private readonly Repository<Material> _Repository;
        public MaterialController(Repository<Material> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            await _Repository.Create(material);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Repository.Delete(id);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Material>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{typeId}")]
        [ProducesResponseType(typeof(List<Material>), 200)]
        public async Task<IActionResult> GetAllByTypeId(int typeId)
        {
            var result = await _Repository.GetAll();

            var filteredMaterials = result.Where(pm => pm.TypeId == typeId).ToList();
            return Ok(filteredMaterials);
        }

        [HttpGet("{term}")]
        [ProducesResponseType(typeof(List<Material>), 200)]
        public async Task<IActionResult> Find(string term)
        {
            var result = await _Repository.Find(term);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Material), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            return Ok(entry);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Material), 200)]
        public async Task<IActionResult> Update(Material material)
        {
            var entry = await _Repository.Update(material);
            return Ok(entry);
        }
    }
}
