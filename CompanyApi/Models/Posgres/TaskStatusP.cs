using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class TaskStatusP
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
