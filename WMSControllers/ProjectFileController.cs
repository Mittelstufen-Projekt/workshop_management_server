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
     * Controller Klasse für ProjectFile
     * Der Code sieht ähnlich aus wie beim ProjectController, die Kommentare sind übertragbar
     */

    public class ProjectFileController : ControllerBase
    {
        private readonly Repository<ProjectFile> _Repository;
        public ProjectFileController(Repository<ProjectFile> Repository)
        {
            _Repository = Repository;
        }

        [HttpPost]
        [ProducesResponseType(200)]                         //Gibt dem Swagger Generator die Informationen welche Response zurückkommen kann
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Create(ProjectFile projectFile)
        {
            try
            {
                await _Repository.Create(projectFile);
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
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);
            if (entry == null) { return BadRequest("Entity with this Id doens't exist"); }
            return Ok(entry);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProjectFile), 200)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public async Task<IActionResult> Update(ProjectFile projectFile)
        {
            try
            {
                var result = await _Repository.Update(projectFile);
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
