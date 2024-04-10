using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class MaterialTypeController : ControllerBase
    {
        private readonly Repository<MaterialType> _Repository;
        public MaterialTypeController(Repository<MaterialType> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialType materialType)
        {
            await _Repository.Create(materialType);
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
            return Ok(_Repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            return Ok(entry);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MaterialType materialType)
        {
            _Repository.Update(materialType);
            return Ok();
        }
    }
}
