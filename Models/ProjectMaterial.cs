using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class ProjectMaterial
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int MaterialId { get; set; }

    public int Amount { get; set; }

}
