using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class ProjectFileController : ControllerBase
    {
        private readonly Repository<ProjectFile> _Repository;
        public ProjectFileController(Repository<ProjectFile> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectFile projectFile)
        {
            await _Repository.Create(projectFile);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Repository.Delete(id);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectFile>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _Repository.GetAll();
            return Ok(list);
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(List<ProjectFile>), 200)]
        public async Task<IActionResult> GetAllByProjectId(int projectId)
        {
            var result = await _Repository.GetAll();

            var filteredFiles = result.Where(pm => pm.ProjectId == projectId).ToList();
            return Ok(filteredFiles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectFile), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            return Ok(entry);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProjectFile), 200)]
        public async Task<IActionResult> Update(ProjectFile projectFile)
        {
            var entry = await _Repository.Update(projectFile);
            return Ok(entry);
        }
    }
}
