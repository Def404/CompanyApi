using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class ContactPerson
{
    public int PersonId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int OrganizationId { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
