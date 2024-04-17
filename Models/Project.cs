using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

public partial class Project
{
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Name { get; set; } = null!;
    [Required (ErrorMessage = "The ClientId cannot be null.")]
    public int ClientId { get; set; }
    [Required]
    [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
    [RegularExpression("^[a-zA-Z0-9.,]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Description { get; set; } = null!;
    [Required]
    [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format")]
    public DateTime Startpoint { get; set; }
    [Required]
    [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format")]
    public DateTime Endpoint { get; set; }
    [Required]
    [Range(0, 10000000)]
    public float EstimatedCosts { get; set; }
    [Required]
    [Range(0, 10000000)]
    public float Costs { get; set; }
    [Required]
    [Range(0, 10000000)]
    public float EstimatedHours { get; set; }

}
