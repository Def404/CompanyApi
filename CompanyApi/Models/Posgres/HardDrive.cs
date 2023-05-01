using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class HardDrive
{
    public long SerialNumber { get; set; }

    public string DriveName { get; set; } = null!;

    public int DriveSize { get; set; }

    public int DriveTypeId { get; set; }

    public int ConnectionInterfaceId { get; set; }

    public virtual ConnectionInterfaceType ConnectionInterface { get; set; } = null!;

    public virtual DriveTypeP DriveTypeP { get; set; } = null!;

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
