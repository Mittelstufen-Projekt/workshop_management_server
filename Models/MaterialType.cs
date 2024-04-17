using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

public partial class MaterialType
{
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int Id { get; set; }


    [Required(ErrorMessage = "The Name cannot be null.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "The Description cannot be null.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Description { get; set; } = null!;

}
