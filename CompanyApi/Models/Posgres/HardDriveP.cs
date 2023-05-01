using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class HardDriveP
{
    public long? SerialNumber { get; set; }

    public string DriveName { get; set; } = null!;

    public int DriveSize { get; set; }

    public int DriveTypeId { get; set; }

    public int ConnectionInterfaceId { get; set; }

    public virtual ConnectionInterfaceType? ConnectionInterface { get; set; }

    public virtual DriveType? DriveTypeP { get; set; }
    
}
