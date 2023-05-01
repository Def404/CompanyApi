namespace CompanyApi.Models;

public class HardDrive
{
    
    public string DriveName { get; set; } = null!;

    public int DriveSize { get; set; }

    public int DriveTypeId { get; set; }

    public int ConnectionInterfaceId { get; set; }

    public int Price { get; set; }
    
    public int Count { get; set; }
    
}