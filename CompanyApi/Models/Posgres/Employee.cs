using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string EmployeeLogin { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int PositionId { get; set; }

    public virtual EmployeesPosition? Position { get; set; } = null!;
}
