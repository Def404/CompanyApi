﻿using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class EmployeesPosition
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
