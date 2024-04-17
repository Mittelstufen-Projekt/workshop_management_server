using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

public partial class Material
{
    [Required(ErrorMessage = "The MaterialId cannot be null.")]
    public int Id { get; set; }


    [Required(ErrorMessage = "The Name cannot be null.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "The Description cannot be null.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Special characters are not allowed.")]
    public string Description { get; set; } = null!;


    [Required(ErrorMessage = "The TypeId cannot be null.")]
    public int TypeId { get; set; }


    [Required(ErrorMessage = "The Amount cannot be null.")]
    [Range(0, 10000000)]
    public int Amount { get; set; }


    [Required(ErrorMessage = "The Costs cannot be null.")]
    [Range(0, 10000000)]
    public float Costs { get; set; }


    [Required(ErrorMessage = "The ThresholdValue cannot be null.")]
    [Range(0, 10000000)]
    public int ThresholdValue { get; set; }

}
