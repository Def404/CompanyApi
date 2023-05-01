using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class OrganizationType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}
