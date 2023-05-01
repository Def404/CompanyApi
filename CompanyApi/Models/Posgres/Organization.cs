using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string OrganizationName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int TypeId { get; set; }

    public virtual ICollection<ContactPerson> ContactPeople { get; set; } = new List<ContactPerson>();

    public virtual OrganizationType Type { get; set; } = null!;
}
