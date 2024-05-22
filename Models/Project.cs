using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopManagementServiceBackend.Models;


/*
 * Dies ist die Klasse welche beim scaffolden generiert wurde für den Table Project, verknüpfte Listen zu anderen Tables wurden entfernt.
 * Es wurden Annotations für die Validation hinzugefügt, welche von asp übernommen wird. Es wird durch Regex ausdrücke gefiltert und über entsprechende 
 * Annotaions wie StringLength().
 */

public partial class Project
{
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int Id { get; set; }


    [Required(ErrorMessage = "The Name cannot be null.")]
    [StringLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")] 
    public string Name { get; set; } = null!;


    [Required (ErrorMessage = "The ClientId cannot be null.")]
    public int ClientId { get; set; }


    [Required(ErrorMessage = "The Description cannot be null.")]
    [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
    [RegularExpression("^[a-zA-Z0-9.,]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "The Endpoint cannot be null.")]
    [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format")]
    public DateTime Endpoint { get; set; }

    [Required(ErrorMessage = "The Startpoint cannot be null.")]
    [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format")]
    public DateTime Startpoint { get; set; }


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
