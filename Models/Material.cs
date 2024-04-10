using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int TypeId { get; set; }

    public int Amount { get; set; }

    public float Costs { get; set; }

    public int ThresholdValue { get; set; }

}
