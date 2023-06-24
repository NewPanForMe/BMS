namespace BMS_Models.DbModels;

public record User
{
    public int Id { get; set; } = 0;
    public string Code { get; set; } = string.Empty;
    public string LoginName { get; set; } = string.Empty;
    public string LoginPassword { get; set; } = string.Empty;
    public string LoginPasswordSalt { get; set; } = string.Empty;

    public string? Name { get; set; }
    public string? Gender { get; set; } 
    public int? IdCard { get; set; }
    public string? Phone { get; set; } 
    public int JwtVersion { get; set; } = 0;
    public bool IsDelete { get; set; } = false;
    public bool IsLock { get; set; } = false;
}
