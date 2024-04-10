using Microsoft.AspNetCore.Mvc;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller
    public class ProjectController : ControllerBase
    {
        private readonly Repository<Project> _Repository;
        public ProjectController(Repository<Project> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _Repository.Create(project);
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
        public async Task<IActionResult> Update(Project project)
        {
            var result = _Repository.Update(project);
            return Ok(result);
        }
    }
}
