using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend.ApiModels
{
    /*
     * Dies ist das DTO welches zum senden verwendet wird, es hat bis auf StartpointUnixTimestamp und 
     * EndpointUnixTimestamp die selben Properties wie die Klasse Project
     */
    public partial class ApiProject
    {
        [Required(ErrorMessage = "The ProjectId cannot be null.")]
        public int Id { get; set; }


        [Required(ErrorMessage = "The Name cannot be null.")]
        [StringLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "The ClientId cannot be null.")]
        public int ClientId { get; set; }


        [Required(ErrorMessage = "The Description cannot be null.")]
        [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        [RegularExpression("^[a-zA-Z0-9.,]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Description { get; set; } = null!;
        public long StartpointUnixTimestamp { get; set; }
        public long EndpointUnixTimestamp { get; set; }

        [Required(ErrorMessage = "The EstimatedCosts cannot be null.")]
        [Range(0, 10000000)]
        public float EstimatedCosts { get; set; }


        [Required(ErrorMessage = "The Costs cannot be null.")]
        [Range(0, 10000000)]
        public float Costs { get; set; }


        [Required(ErrorMessage = "The EstimatedHours cannot be null.")]
        [Range(0, 10000000)]
        public float EstimatedHours { get; set; }
    }
}
