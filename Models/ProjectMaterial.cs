using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

public partial class ProjectMaterial
{
    [Required(ErrorMessage = "The ProjectMaterialId cannot be null.")]
    public int Id { get; set; }


    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int? ProjectId { get; set; }


    [Required(ErrorMessage = "The MaterialId cannot be null.")]
    public int MaterialId { get; set; }


    [Required(ErrorMessage = "The Amount cannot be null.")]
    [Range(0, 10000000)]
    public int Amount { get; set; }

}
