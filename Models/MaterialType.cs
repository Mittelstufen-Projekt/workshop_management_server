using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

public partial class MaterialType
{
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public string Description { get; set; } = null!;

}
