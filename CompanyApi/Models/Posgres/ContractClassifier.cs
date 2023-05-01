using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class ContractClassifier
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
