﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManagementServiceBackend.Models;

/*
 * Dies ist die Klasse welche beim scaffolden generiert wurde für den Table ProjectFile,
 * es wurden ähnliche Änderungen wie bei Project vorgenommen.
 */
public partial class ProjectFile
{
    [Required(ErrorMessage = "The ProjectFileId cannot be null.")]
    public int Id { get; set; }


    [Required(ErrorMessage = "The ProjectId cannot be null.")]
    public int ProjectId { get; set; }


    [Required(ErrorMessage = "The File cannot be null.")]
    [RegularExpression(@"^([a-zA-Z]:|\\)(\\[a-zA-Z_\- 0-9\.]+)+$", ErrorMessage = "Special characters are not allowed.")]
    public string File { get; set; } = null!;

}
