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

}
