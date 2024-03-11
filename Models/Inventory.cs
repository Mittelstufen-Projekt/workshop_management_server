using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class Inventory
{
    public int MaterialId { get; set; }

    public int? Amount { get; set; }

    public virtual Material Material { get; set; } = null!;
}
