using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend.ApiModels
{
    /*
     * Dies ist der Model Converter, welcher die nötigen Methoden hat um aus Project das DTO ApiProject zu machen und andersherum 
     */
    public class ModelConverter
    {
        public ModelConverter() { }
        public Project apiProjectToProject(ApiProject apiProject) { 
            var project = new Project();
            project.Id = apiProject.Id;
            project.Name = apiProject.Name;
            project.ClientId = apiProject.ClientId;
            project.Description = apiProject.Description;
            project.Startpoint = UnixTimestampToDateTime(apiProject.StartpointUnixTimestamp);
            project.Endpoint = UnixTimestampToDateTime(apiProject.StartpointUnixTimestamp);
            project.EstimatedCosts = apiProject.EstimatedCosts;
            project.EstimatedHours = apiProject.EstimatedHours;
            project.Costs = apiProject.Costs;
            return project;
        }
        public ApiProject projectToApiProject(Project project)
        {
            var apiProject = new ApiProject();
            apiProject.Id = project.Id;
            apiProject.Name = project.Name;
            apiProject.ClientId = project.ClientId;
            apiProject.Description = project.Description;
            apiProject.StartpointUnixTimestamp = DateTimeToUnixTimestamp(project.Startpoint);
            apiProject.EndpointUnixTimestamp = DateTimeToUnixTimestamp(project.Endpoint);
            apiProject.EstimatedCosts = project.EstimatedCosts;
            apiProject.EstimatedHours = project.EstimatedHours;
            apiProject.Costs = project.Costs;
            return apiProject;
        }
        public DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            var datetime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimestamp);
            return datetime;
        }
        public long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var unixTimeStamp = (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return unixTimeStamp;
        }
    }
}
