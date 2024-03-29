﻿using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class MaterialType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
