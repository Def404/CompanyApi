using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class TaskReceiptType
{
    public int ReceiptId { get; set; }

    public string ReceiptName { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
