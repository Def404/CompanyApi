using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class DriveType
{
    public int DriveTypeId { get; set; }

    public string DriveTypeName { get; set; } = null!;

}
