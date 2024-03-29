﻿using System;
using System.Collections.Generic;

namespace WorkshopManagementServiceBackend.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Firstnamel { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
