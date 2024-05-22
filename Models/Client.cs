using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;
/*
 * Dies ist die Klasse welche beim scaffolden generiert wurde für den Table Client,
 * es wurden ähnliche Änderungen wie bei Project vorgenommen.
 */
public partial class Client
{
    [Required(ErrorMessage = "The ClientId cannot be null.")]
    public int Id { get; set; }


    [StringLength(100, ErrorMessage = "The Firstname field cannot exceed 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
    [Required(ErrorMessage = "The Firstname cannot be null.")]
    public string Firstname { get; set; } = null!;


    [StringLength(100, ErrorMessage = "The Lastname field cannot exceed 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
    [Required(ErrorMessage = "The Lastname cannot be null.")]
    public string Lastname { get; set; } = null!;


    [Required(ErrorMessage = "The Phone cannot be null.")]
    [RegularExpression(@"(\(?([\d \-\)\–\+\/\(]+)\)?([ .\-–\/]?)([\d]+))", ErrorMessage = "Invalid phone number format")]
    public string Phone { get; set; } = null!;

}
