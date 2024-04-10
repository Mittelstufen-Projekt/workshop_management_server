using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class ProjectMaterialController : ControllerBase
    {
        private readonly Repository<ProjectMaterial> _Repository;
        public ProjectMaterialController(Repository<ProjectMaterial> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectMaterial projectMaterial)
        {
            await _Repository.Create(projectMaterial);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _Repository.GetAll();
            return Ok(result);
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
        public async Task<IActionResult> Update(ProjectMaterial projectMaterial)
        {
            _Repository.Update(projectMaterial);
            return Ok();
        }
    }
}
