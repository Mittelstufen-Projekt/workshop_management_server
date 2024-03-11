using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<ProjectMaterial> ProjectMaterials { get; } = new List<ProjectMaterial>();
}
