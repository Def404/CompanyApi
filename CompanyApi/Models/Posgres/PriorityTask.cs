using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class PriorityTask
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
