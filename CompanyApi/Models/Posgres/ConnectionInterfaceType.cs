using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class ConnectionInterfaceType
{
    public int ConnectionInterfaceId { get; set; }

    public string InterfaceName { get; set; } = null!;

    public int TransferRate { get; set; }

}
