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
        [ProducesResponseType(typeof(List<ProjectMaterial>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectMaterial), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            return Ok(entry);
        }
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(List<ProjectMaterial>), 200)]
        public async Task<IActionResult> GetAllByProjectId(int projectId)
        {
            var result = await _Repository.GetAll();

            var filteredMaterials = result.Where(pm => pm.ProjectId == projectId).ToList();
            return Ok(filteredMaterials);
        }
        [HttpGet("{materialId}")]
        [ProducesResponseType(typeof(List<ProjectMaterial>), 200)]
        public async Task<IActionResult> GetAllByMaterialId(int materialId)
        {
            var result = await _Repository.GetAll();

            var filteredMaterials = result.Where(pm => pm.MaterialId == materialId).ToList();
            return Ok(filteredMaterials);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProjectMaterial), 200)]
        public async Task<IActionResult> Update(ProjectMaterial projectMaterial)
        {
            var entry = await _Repository.Update(projectMaterial);
            return Ok(entry);
        }
    }
}
