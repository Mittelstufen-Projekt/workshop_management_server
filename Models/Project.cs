using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ClientId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Startpoint { get; set; }

    public DateTime Endpoint { get; set; }

    public float EstimatedCosts { get; set; }

    public float Costs { get; set; }

    public float EstimatedHours { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<ProjectFile> ProjectFiles { get; } = new List<ProjectFile>();

    public virtual ICollection<ProjectMaterial> ProjectMaterials { get; } = new List<ProjectMaterial>();
}
