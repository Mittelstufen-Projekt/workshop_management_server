using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class ProjectFile
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string File { get; set; } = null!;

}
