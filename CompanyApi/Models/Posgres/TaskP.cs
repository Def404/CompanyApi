using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class TaskP
{
    public int TaskId { get; set; }

    public DateTime CreateDate { get; set; }

    public TimeSpan? ExecutionPeriod { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int PersonId { get; set; }

    public int? ContractId { get; set; }

    public long? SerialNumber { get; set; }

    public int PriorityId { get; set; }

    public int ReceiptId { get; set; }

    public int StatusId { get; set; }

    public long AuthorId { get; set; }

    public long? ExecutorId { get; set; }

    public virtual Employee Author { get; set; } = null!;

    public virtual Contract? Contract { get; set; }

    public virtual Employee? Executor { get; set; }

    public virtual ContactPerson Person { get; set; } = null!;

    public virtual PriorityTask Priority { get; set; } = null!;

    public virtual TaskReceiptType Receipt { get; set; } = null!;

    public virtual HardDrive? SerialNumberNavigation { get; set; }

    public virtual TaskStatusP StatusP { get; set; } = null!;
}
