using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class Contract
{
    public int ContractId { get; set; }

    public string? Info { get; set; }

    public int TypeId { get; set; }

    public int PersonId { get; set; }

    public virtual ContactPerson Person { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();

    public virtual ContractClassifier Type { get; set; } = null!;
}
