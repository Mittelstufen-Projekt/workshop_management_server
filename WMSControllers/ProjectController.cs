using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkshopManagementServiceBackend.ApiModels;
using WorkshopManagementServiceBackend.Models;
using WorkshopManagementServiceBackend.Repository;

namespace WorkshopManagementServiceBackend.WMSControllers
{
    [ApiController] // Kennzeichnet den Controller als API-Controller
    [Route("[controller]")] // Definiert die Standardroute für diesen Controller

    /*
     * Controller Klasse für Project
     * Der ModellConverter wird hier gebraucht um aus dem DTO ApiProject ein Project zu machen und anders herum
     */
    public class ProjectController : ControllerBase  
    {
        private readonly Repository<Project> _Repository;
        private readonly ModelConverter _ModelConverter;
        public ProjectController(Repository<Project> Repository)
        {
            _ModelConverter = new ModelConverter();
            _Repository = Repository;
        }

        /*
         * Definiert, dass es ein Post Endpunkt ist
         */
        [HttpPost] 
        [ProducesResponseType(200)]                         //Gibt dem Swagger Generator die Informationen welche Response zurückkommen kann
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Create(ApiProject apiProject)
        {
            try                                             //Try and Catch Block um Fehler abzufangen, welche in diesem Fall durch falsche IDs verursacht werden könnten
            {                                                               
                await _Repository.Create(_ModelConverter.apiProjectToProject(apiProject));
                return Ok();
            }
            catch (DbUpdateException ex)                    //Spezifische EF core Exception, deren Message noch durch die Konsole ausgeben wird
            {
                Console.WriteLine(ex.Message);
                return BadRequest("An error occurred while saving the entity.The Client Id or the Project Id might be wrong.");
            }
            catch (Exception ex)                            //Catch Block um noch andere unvorhergesehene Exceptions abzufangen 
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        /*
         * Delete Endpunkt
         */
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Delete(int id)     //Es wird noch geprüft ob die ankommende Id existiert
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

        /*
         * GetAll Endpunkt
         */
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiProject>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _Repository.GetAll();
            List<ApiProject> apiProjects = new List<ApiProject>();

            foreach (Project project in list)               //Alle vom Repository zurückgegebenen Projects werden durch den ModelConverter in das DTO ApiProject umgewandelt
            {
                apiProjects.Add(_ModelConverter.projectToApiProject(project));
            }

            return Ok(apiProjects);
        }

        /*
         * Get Endpunkt für alle Projects mit dem Fremdschlüssel von einem spezifischen Client
         */
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(typeof(List<ApiProject>), 200)]
        public async Task<IActionResult> GetAllByClientId(int clientId)
        {
            var result = await _Repository.GetAll();

            var filteredClients = result.Where(p => p.ClientId == clientId).ToList();   //In diesem Fall wird jedes Element p in der ICollection überprüft
                                                                                        //und nur diejenigen, deren TypeId mit der angegebenen typeId übereinstimmt, werden in eine Liste konvertiert
            List<ApiProject> apiProjects = new List<ApiProject>();

            foreach (Project project in filteredClients)
            {
                apiProjects.Add(_ModelConverter.projectToApiProject(project));          //Auch hier muss konvertiert werden 
            }

            return Ok(apiProjects);
        }

        /*
         * GetById Endpunkt
         */
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiProject), 200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _Repository.Get(id);          //Das Repository gibt null zurück, falls die Id icht existiert. So kann diese Information als 400 Response zurückgesendet werden
            if(entry == null) { return BadRequest("Entity with this Id doens't exist"); } 
            return Ok(_ModelConverter.projectToApiProject(entry));
        }

        /*
         * Put Endpunkt
         */
        [HttpPut]
        [ProducesResponseType(typeof(ApiProject), 200)]     //Gleich wie der Post Endpunkt, das überschriebene Project wird nur noch mit in der Response zurückgeben 
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public async Task<IActionResult> Update(ApiProject apiProject)
        {
            try
            {
                var result = await _Repository.Update(_ModelConverter.apiProjectToProject(apiProject));
                return Ok(_ModelConverter.projectToApiProject(result));
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
