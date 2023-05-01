using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class DriveTypeP
{
    public int DriveTypeId { get; set; }

    public string DriveTypeName { get; set; } = null!;

    public virtual ICollection<HardDrive> HardDrives { get; set; } = new List<HardDrive>();
}
